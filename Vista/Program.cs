using System;
using Gtk;

class Program
{

    static void Main()
    {
        Application.Init(); // Inicializa GTK

        // Crea una nueva ventana
        var ventana = new Window("Hola Mundo");
        ventana.SetDefaultSize(300, 200);
        ventana.DeleteEvent += (o, e) => { Application.Quit(); };

        // Crea una etiqueta con "Hola Mundo"
        var etiqueta = new Label("Hola Mundo");
        ventana.Add(etiqueta);

        ventana.ShowAll(); // Muestra todos los elementos de la ventana
        Application.Run(); // Inicia el loop de eventos
    }

}
