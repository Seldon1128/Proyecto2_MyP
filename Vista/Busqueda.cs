using System;
using Gtk;

public class Busqueda : Window
{
    private Entry search;
    private Button continueSearchTitleButton;
    private Button continueSearchInterpreteButton;
    private Button continueSearchAlbumButton;

    public Busqueda() : base("Pantalla 3 - Búsqueda")
    {
        SetDefaultSize(600, 600);
        SetPosition(WindowPosition.Center);

        VBox vbox = new VBox(false, 10);

        // Crear una barra de título horizontal
        HBox titleBar = new HBox(false, 5);
        titleBar.ModifyBg(StateType.Normal, new Gdk.Color(200, 200, 200)); // Color de fondo gris

        // Crear el botón "Inicio" en la esquina superior izquierda
        Button startButton = new Button("Inicio");
        titleBar.PackStart(startButton, false, false, 5);

        startButton.Clicked += (sender, e) =>
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide(); // Ocultar la ventana de Busqueda
        };

        // Crear el título centrado
        Label titleLabel = new Label("<span size='14000' weight='bold'>BaseMusical</span>");
        titleLabel.UseMarkup = true;
        titleLabel.Halign = Align.Center;
        titleBar.PackStart(titleLabel, true, true, 0);

        vbox.PackStart(titleBar, false, false, 5);

        Label searchLabel = new Label("Ingresa palabra clave y selecciona qué buscar:");
        search = new Entry();
        search.MarginLeft = 10;
        search.MarginRight = 10;
        
        // Crear los tres botones en la misma línea
        HBox buttonBox = new HBox(false, 10);

        continueSearchTitleButton = new Button("Título");
        continueSearchTitleButton.Clicked += OnContinueSearchTitleButtonClicked;
        continueSearchTitleButton.MarginLeft = 10;

        continueSearchInterpreteButton = new Button("Intérprete");
        continueSearchInterpreteButton.Clicked += OnContinueSearchInterpreteButtonClicked;

        continueSearchAlbumButton = new Button("Álbum");
        continueSearchAlbumButton.Clicked += OnContinueSearchAlbumButtonClicked;
        continueSearchAlbumButton.MarginRight = 10;

        buttonBox.PackStart(continueSearchTitleButton, true, true, 0);
        buttonBox.PackStart(continueSearchInterpreteButton, true, true, 0);
        buttonBox.PackStart(continueSearchAlbumButton, true, true, 0);

        vbox.PackStart(searchLabel, false, false, 0);
        vbox.PackStart(search, false, false, 0);
        vbox.PackStart(buttonBox, false, false, 0);

        Add(vbox);
        ShowAll();
    }

    // Métodos vacíos para cada botón
    private void OnContinueSearchTitleButtonClicked(object sender, EventArgs e)
    {
        // Método inicializado pero sin lógica
    }

    private void OnContinueSearchInterpreteButtonClicked(object sender, EventArgs e)
    {
        // Método inicializado pero sin lógica
    }

    private void OnContinueSearchAlbumButtonClicked(object sender, EventArgs e)
    {
        // Método inicializado pero sin lógica
    }
}

