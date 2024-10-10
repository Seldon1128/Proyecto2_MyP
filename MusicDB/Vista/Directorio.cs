using System;
using Gtk;
using System.Collections.Generic;
using MusicDB.Modelo;


public class Directorio : Window
{
    private TreeView songTable;
    private Label songDetails;
    private VBox detailsBox;

    public Directorio(List<Song> canciones) : base("Pantalla 4 - Mostrar Directorio Base de Datos")
    {
        SetDefaultSize(800, 600);
        SetPosition(WindowPosition.Center);

        // Crear layout principal con HBox
        HBox hboxMain = new HBox(false, 10);

        // Crear la sección de la tabla
        VBox vboxTable = new VBox(false, 10);
        
        // Crear la barra de título horizontal
        HBox titleBar = new HBox(false, 5);
        titleBar.ModifyBg(StateType.Normal, new Gdk.Color(200, 200, 200)); // Color de fondo gris

        // Crear el botón "Inicio" en la esquina superior izquierda
        Button startButton = new Button("Inicio");
        titleBar.PackStart(startButton, false, false, 5);

        startButton.Clicked += (sender, e) =>
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide(); // Ocultar la ventana de Directorio
        };

        // Crear el título centrado
        Label titleLabel = new Label("<span size='14000' weight='bold'>BaseMusical</span>");
        titleLabel.UseMarkup = true;  // Habilitar el formato
        titleLabel.Halign = Align.Center;
        titleLabel.MarginLeft = 200;  // Margen izquierdo
        titleLabel.MarginRight = 200; // Margen derecho
        titleBar.PackStart(titleLabel, true, true, 0);

        // Agregar la barra de título al contenedor principal
        vboxTable.PackStart(titleBar, false, false, 5);

        // Crear la tabla de canciones
        songTable = new TreeView();
        CreateSongTable();
        PopulateSongTable(canciones);

        // Agregar la tabla al contenedor con scroll
        ScrolledWindow scrolledWindow = new ScrolledWindow();
        scrolledWindow.Add(songTable);
        vboxTable.PackStart(scrolledWindow, true, true, 5);

        // Agregar la sección de tabla a la interfaz principal
        hboxMain.PackStart(vboxTable, true, true, 10);

        // Crear la caja para mostrar los detalles
        detailsBox = new VBox(false, 10);
        Label detailsLabel = new Label("<span size='12000' weight='bold'>Detalles de la Canción</span>");
        detailsLabel.UseMarkup = true;
        detailsBox.PackStart(detailsLabel, false, false, 5);

        // Crear el área de detalles
        songDetails = new Label();
        detailsBox.PackStart(songDetails, false, false, 5);

        hboxMain.PackStart(detailsBox, false, false, 10);

        // Agregar la caja principal a la ventana
        Add(hboxMain);
        ShowAll();
    }

    // Método para crear las columnas de la tabla
    private void CreateSongTable()
    {
        // Crear columna para el Título
        TreeViewColumn titleColumn = new TreeViewColumn { Title = "Título" };
        CellRendererText titleCell = new CellRendererText();
        titleColumn.PackStart(titleCell, true);
        titleColumn.AddAttribute(titleCell, "text", 0);  // Columna 0
        songTable.AppendColumn(titleColumn);

        // Crear columna para el Intérprete
        TreeViewColumn performerColumn = new TreeViewColumn { Title = "Intérprete" };
        CellRendererText performerCell = new CellRendererText();
        performerColumn.PackStart(performerCell, true);
        performerColumn.AddAttribute(performerCell, "text", 1);  // Columna 1
        songTable.AppendColumn(performerColumn);

        // Crear columna para el Año
        TreeViewColumn yearColumn = new TreeViewColumn { Title = "Año" };
        CellRendererText yearCell = new CellRendererText();
        yearColumn.PackStart(yearCell, true);
        yearColumn.AddAttribute(yearCell, "text", 2);  // Columna 2
        songTable.AppendColumn(yearColumn);

        // Columna para el Género
        TreeViewColumn genreColumn = new TreeViewColumn { Title = "Género" };
        CellRendererText genreCell = new CellRendererText();
        genreColumn.PackStart(genreCell, true);
        genreColumn.AddAttribute(genreCell, "text", 3);  // Columna 3
        songTable.AppendColumn(genreColumn);

        // Columna para el Álbum
        TreeViewColumn albumColumn = new TreeViewColumn { Title = "Álbum" };
        CellRendererText albumCell = new CellRendererText();
        albumColumn.PackStart(albumCell, true);
        albumColumn.AddAttribute(albumCell, "text", 4);  // Columna 4
        songTable.AppendColumn(albumColumn);
    }

    // Método para poblar la tabla con canciones
    private void PopulateSongTable(List<Song> canciones)
    {
        ListStore store = new ListStore(typeof(string), typeof(string), typeof(int), typeof(string), typeof(string));

        foreach (var cancion in canciones)
        {
            store.AppendValues(cancion.Title, cancion.PerformerName, cancion.Year, cancion.Genre, cancion.AlbumName);
        }

        songTable.Model = store;

        // Manejar la selección de una canción
        songTable.Selection.Changed += OnSongSelected;
    }

    // Método para manejar la selección de una canción y mostrar detalles
    private void OnSongSelected(object sender, EventArgs e)
    {
        TreeIter iter;
        if (songTable.Selection.GetSelected(out iter))
        {
            // Obtener los valores de la canción seleccionada
            string titulo = (string)songTable.Model.GetValue(iter, 0);
            string performer = (string)songTable.Model.GetValue(iter, 1);
            int year = (int)songTable.Model.GetValue(iter, 2);
            string genero = (string)songTable.Model.GetValue(iter, 3);
            string album = (string)songTable.Model.GetValue(iter, 4);


            // Actualizar el label de detalles
            songDetails.Text = $"Título: {titulo}\nIntérprete: {performer}\nAño: {year}\nGénero: {genero}\nÁlbum: {album}";
        }
    }
}


