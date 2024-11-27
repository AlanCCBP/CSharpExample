using ObjCRuntime;
using UIKit;

namespace CSharpExample;

public class Program
{
    // Este es el punto de entrada de la aplicación
    static void Main(string[] args)
    {
        // Configura la aplicación para usar AppDelegate como delegado
        UIApplication.Main(args, null, typeof(AppDelegate));
    }
}
