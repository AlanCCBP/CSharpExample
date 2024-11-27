namespace CSharpExample.Models
{
    public class Nodo<T>
    {
        public T Dato { get; set; }
        public Nodo<T>? Anterior { get; set; }
        public Nodo<T>? Siguiente { get; set; }

        public Nodo(T dato)
        {
            Dato = dato;
            Anterior = null;
            Siguiente = null;
        }
    }
}
