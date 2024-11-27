using System;

namespace CSharpExample.Models
{
    public class Persona
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }

        // Constructor
        public Persona(string nombre, string apellido, DateTime fechaNacimiento)
        {
            Nombre = nombre;
            Apellido = apellido;
            FechaNacimiento = fechaNacimiento;
        }

        // Método para calcular la edad
        public int GetEdad()
        {
            var edad = DateTime.Now.Year - FechaNacimiento.Year;
            if (FechaNacimiento.Date > DateTime.Now.AddYears(-edad)) edad--;
            return edad;
        }
    }

}
