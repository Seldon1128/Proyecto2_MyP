using Gtk;
using System;

public class MainWindow : Window
{
    public MainWindow() : base("Base de Datos Musical")
    {
        // Configurar el tamaño de la ventana
        SetDefaultSize(400, 300);
        SetPosition(WindowPosition.Center);

        // Crear un contenedor vertical
        VBox vbox = new VBox(false, 2);

        // Crear el título
        Label titleLabel = new Label();
        titleLabel.Text = "<span size='18000' weight='bold'>Base de Datos Musical</span>";
        titleLabel.UseMarkup = true;  // Usar etiquetas de marcado Pango para darle estilo
        titleLabel.Halign = Align.Center;
        titleLabel.Valign = Align.Center;

        // Agregar el título al contenedor
        vbox.PackStart(titleLabel, true, true, 0);

        // Crear un botón de inicio
        Button startButton = new Button("Iniciar");
        startButton.SetSizeRequest(80, 30);
        startButton.Clicked += OnStartButtonClicked;

        // Agregar el botón al contenedor
        vbox.PackStart(startButton, false, false, 0);

        // Agregar el contenedor a la ventana
        Add(vbox);

        ShowAll();
    }

    // Acción del botón de inicio
    void OnStartButtonClicked(object? sender, EventArgs args)
    {
        Console.WriteLine("Botón de Inicio presionado");
    }
}
