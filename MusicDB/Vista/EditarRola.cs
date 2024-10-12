using System;
using Gtk;
using MusicDB.Modelo;

public class EditarRola : Window
{
    private Entry search;
    private Button continueSearchTitleButton;
    private Button continueSearchInterpreteButton;
    private Button continueSearchAlbumButton;

    public EditarRola() : base("Editar Rola")
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

        Label searchLabel = new Label("Ingresa palabra clave para buscar cancion a editar y selecciona qué buscar:");
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

    // Métodos para cada botón
    private void OnContinueSearchTitleButtonClicked(object sender, EventArgs e)
    {
        string input = search.Text;
        
        if (!string.IsNullOrEmpty(input))
        {
            List<Song> canciones = Program.BuscarCanciones(input,"title");
            if(canciones.Count() == 0){
                // mensaje de no encontramos lo que buscas
                MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, "Lo siento, no encontramos algo relacionado a tu busqueda.");
                md.Run();
                md.Destroy();
                // Volver a la página de busqueda después del mensaje de error, eliminando la entrada
                EditarInformacion editarInfoWindow = new EditarInformacion();
                editarInfoWindow.ShowAll();
                this.Hide();
            } else {
                EditRolaScreen editRolaWindow = new EditRolaScreen(canciones);
                editRolaWindow.ShowAll();
                this.Hide();
            }
        }
        else
        {
            // Mostrar un mensaje de error si el campo está vacío
            MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, "Por favor, ingresa un título válido.");
            md.Run();
            md.Destroy();
        }
    }

    private void OnContinueSearchInterpreteButtonClicked(object sender, EventArgs e)
    {
        string input = search.Text;
        
        if (!string.IsNullOrEmpty(input))
        {
            List<Song> canciones = Program.BuscarCanciones(input,"performer");
            if(canciones.Count() == 0){
                // mensaje de no encontramos lo que buscas
                MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, "Lo siento, no encontramos algo relacionado a tu busqueda.");
                md.Run();
                md.Destroy();
                // Volver a la página de busqueda después del mensaje de error, eliminando la entrada
                EditarInformacion editarInfoWindow = new EditarInformacion();
                editarInfoWindow.ShowAll();
                this.Hide();
            } else {
                EditRolaScreen editRolaWindow = new EditRolaScreen(canciones);
                editRolaWindow.ShowAll();
                this.Hide();
            }
        }
        else
        {
            // Mostrar un mensaje de error si el campo está vacío
            MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, "Por favor, ingresa un interprete válido.");
            md.Run();
            md.Destroy();
        }
    }

    private void OnContinueSearchAlbumButtonClicked(object sender, EventArgs e)
    {
        string input = search.Text;
        
        if (!string.IsNullOrEmpty(input))
        {
            List<Song> canciones = Program.BuscarCanciones(input,"album");
            if(canciones.Count() == 0){
                // mensaje de no encontramos lo que buscas
                MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, "Lo siento, no encontramos algo relacionado a tu busqueda.");
                md.Run();
                md.Destroy();
                // Volver a la página de busqueda después del mensaje de error, eliminando la entrada
                EditarInformacion editarInfoWindow = new EditarInformacion();
                editarInfoWindow.ShowAll();
                this.Hide();
            } else {
                EditRolaScreen editRolaWindow = new EditRolaScreen(canciones);
                editRolaWindow.ShowAll();
                this.Hide();
            }
        }
        else
        {
            // Mostrar un mensaje de error si el campo está vacío
            MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, "Por favor, ingresa un nombre de album válido.");
            md.Run();
            md.Destroy();
        }
    }
}
