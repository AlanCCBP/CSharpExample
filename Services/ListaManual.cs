using System;
using CSharpExample.Models;

namespace CSharpExample.Services
{
    public class ListaManual<Persona>
    {
        public Nodo<Persona>? Primero { get; private set; }
        public Nodo<Persona>? Ultimo { get; private set; }

        public ListaManual()
        {
            Primero = null;
            Ultimo = null;
        }

        public void Agregar(Persona persona)
        {
            var nuevoNodo = new Nodo<Persona>(persona);

            if (Primero == null)
            {
                Primero = nuevoNodo;
                Ultimo = nuevoNodo;
            }
            else
            {
                Ultimo!.Siguiente = nuevoNodo;
                nuevoNodo.Anterior = Ultimo;
                Ultimo = nuevoNodo;
            }
        }

        public void ImprimirLista()
        {
            var actual = Primero;
            while (actual != null)
            {
                Console.WriteLine(actual.Dato);
                actual = actual.Siguiente;
            }
        }
    }
}
