using CSharpExample.Models;
using CSharpExample.Services;
using System.IO;
using System.Text.Json;

namespace CSharpExample;

public partial class MainPage : ContentPage
{
    private ListaManual<Persona> listaPersonas = new ListaManual<Persona>();
    private const string archivoPersonas = "personas.csv"; // Nombre del archivo

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnAgregarPersonaClicked(object sender, EventArgs e)
    {
        string nombre = NombreInput.Text ?? string.Empty;
        string apellido = ApellidoInput.Text ?? string.Empty;
        DateTime fechaNacimiento = FechaNacimientoInput.Date;

        if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido))
        {
            DisplayAlert("Error", "Debe completar todos los campos", "OK");
            return;
        }

        // Crear la nueva persona usando el constructor con parámetros
        Persona nuevaPersona = new Persona(nombre, apellido, fechaNacimiento);

        listaPersonas.Agregar(nuevaPersona);

        MostrarPersonas();  // Actualiza la lista mostrada en la UI
    }

    private void MostrarPersonas()
    {
        Nodo<Persona>? nodoActual = listaPersonas.Primero;
        string contenido = string.Empty;

        while (nodoActual != null)
        {
            Persona persona = nodoActual.Dato;
            contenido += $"Nombre: {persona.Nombre}, Apellido: {persona.Apellido}, Edad: {persona.GetEdad()} años\n";
            nodoActual = nodoActual.Siguiente;
        }

        PersonasEditor.Text = contenido;  // Muestra las personas en el TextArea
    }

    private void OnCargarPersonasClicked(object sender, EventArgs e)
    {
        CargarPersonasDesdeArchivo();
    }

    private void OnGuardarPersonasClicked(object sender, EventArgs e)
    {
        GuardarPersonasEnArchivo();
    }

    private void CargarPersonasDesdeArchivo()
    {
        string rutaArchivo = ObtenerRutaArchivo();

        // Si el archivo no existe, crearlo con una lista vacía
        if (!File.Exists(rutaArchivo))
        {
            // Crear el archivo y escribir un archivo vacío
            FileStream fs = new FileStream(rutaArchivo, FileMode.Create, FileAccess.Write);
            fs.Close();
        }
        else
        {
            // Leer todas las líneas del archivo CSV
            var lineas = File.ReadAllLines(rutaArchivo);

            // Asegurarnos de que no estamos tratando con un archivo vacío
            if (lineas.Length > 1)
            {
                // Empezamos desde la segunda línea, ya que la primera es el encabezado
                for (int i = 1; i < lineas.Length; i++)
                {
                    var campos = lineas[i].Split(',');

                    if (campos.Length == 3)
                    {
                        string nombre = campos[0];
                        string apellido = campos[1];
                        DateTime fechaNacimiento;

                        if (DateTime.TryParse(campos[2], out fechaNacimiento))
                        {
                            Persona persona = new Persona(nombre, apellido, fechaNacimiento);
                            listaPersonas.Agregar(persona);
                        }
                    }
                }
            }

            MostrarPersonas();  // Actualiza la lista mostrada en la UI
        }
    }

    private string ObtenerRutaArchivo()
    {
        string rutaCarpeta = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        return Path.Combine(rutaCarpeta, archivoPersonas);
    }


    private void GuardarPersonasEnArchivo()
    {
        string rutaArchivo = ObtenerRutaArchivo();

        // Preparamos el archivo CSV
        var lista = new List<string>();  // Lista de líneas para escribir en el CSV

        // Encabezado del CSV
        lista.Add("Nombre,Apellido,FechaNacimiento");

        // Recorrer la lista de personas y agregar cada una al CSV
        Nodo<Persona>? nodoActual = listaPersonas.Primero;
        while (nodoActual != null)
        {
            Persona persona = nodoActual.Dato;
            // Convertimos los datos a formato CSV (valores separados por comas)
            string linea = $"{persona.Nombre},{persona.Apellido},{persona.FechaNacimiento:yyyy-MM-dd}";
            lista.Add(linea);
            nodoActual = nodoActual.Siguiente;
        }

        // Guardamos el CSV en el archivo
        File.WriteAllLines(rutaArchivo, lista);

        DisplayAlert("Éxito", "Las personas se guardaron correctamente en el archivo", "OK");
    }

}
