using System;
using Gtk;
using System.Collections.Generic;
using MusicDB.Modelo;

public class EditAlbumScreen : Window
{
    private TreeView albumTable;
    private Entry idEntry;
    private Entry fieldEntry;
    private Entry newValueEntry;
    private Button updateButton;

    public EditAlbumScreen(List<Album> albums) : base("Editar Álbumes")
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

        // Crear la tabla de álbumes
        albumTable = new TreeView();
        CreateAlbumTable();
        PopulateAlbumTable(albums);

        // Agregar la tabla al contenedor con scroll
        ScrolledWindow scrolledWindow = new ScrolledWindow();
        scrolledWindow.Add(albumTable);
        vboxTable.PackStart(scrolledWindow, true, true, 5);

        // Agregar la sección de tabla a la interfaz principal
        hboxMain.PackStart(vboxTable, true, true, 10);

        // Crear la sección de edición de álbum
        VBox vboxForm = new VBox(false, 10);

        // Texto: Ingrese el ID del álbum que desea editar
        Label labelId = new Label("Ingrese el ID del álbum que desea editar:");
        vboxForm.PackStart(labelId, false, false, 5);

        // Entrada para ID del álbum
        idEntry = new Entry();
        vboxForm.PackStart(idEntry, false, false, 5);

        // Texto: ¿Qué desea cambiar? (name o year)
        Label labelField = new Label("¿Qué desea cambiar? (name o year):");
        vboxForm.PackStart(labelField, false, false, 5);

        // Entrada para el campo a cambiar
        fieldEntry = new Entry();
        vboxForm.PackStart(fieldEntry, false, false, 5);

        // Texto: Ingrese el nuevo valor
        Label labelNewValue = new Label("Ingrese el nuevo valor:");
        vboxForm.PackStart(labelNewValue, false, false, 5);

        // Entrada para el nuevo valor
        newValueEntry = new Entry();
        vboxForm.PackStart(newValueEntry, false, false, 5);

        // Botón para hacer cambios
        updateButton = new Button("Hacer cambios");
        vboxForm.PackStart(updateButton, false, false, 5);

        // Manejar el evento de clic
        updateButton.Clicked += OnUpdateButtonClicked;

        // Agregar la sección de formulario a la interfaz principal
        hboxMain.PackStart(vboxForm, false, false, 10);

        // Agregar la caja principal a la ventana
        Add(hboxMain);
        ShowAll();
    }

    // Método para crear las columnas de la tabla de álbumes
    private void CreateAlbumTable()
    {
        // Crear columna para ID del álbum
        TreeViewColumn idColumn = new TreeViewColumn { Title = "ID" };
        CellRendererText idCell = new CellRendererText();
        idColumn.PackStart(idCell, true);
        idColumn.AddAttribute(idCell, "text", 0);  // Columna 0
        albumTable.AppendColumn(idColumn);

        // Crear columna para el Nombre del álbum
        TreeViewColumn nameColumn = new TreeViewColumn { Title = "Nombre" };
        CellRendererText nameCell = new CellRendererText();
        nameColumn.PackStart(nameCell, true);
        nameColumn.AddAttribute(nameCell, "text", 1);  // Columna 1
        albumTable.AppendColumn(nameColumn);

        // Crear columna para el Año
        TreeViewColumn yearColumn = new TreeViewColumn { Title = "Año" };
        CellRendererText yearCell = new CellRendererText();
        yearColumn.PackStart(yearCell, true);
        yearColumn.AddAttribute(yearCell, "text", 2);  // Columna 2
        albumTable.AppendColumn(yearColumn);
    }

    // Método para poblar la tabla con álbumes
    private void PopulateAlbumTable(List<Album> albums)
    {
        ListStore store = new ListStore(typeof(int), typeof(string), typeof(int));

        foreach (var album in albums)
        {
            store.AppendValues(album.Id, album.Name, album.Year);
        }

        albumTable.Model = store;
    }

    // Método para manejar el clic del botón "Hacer cambios"
    private void OnUpdateButtonClicked(object sender, EventArgs e)
    {
        string albumIdText = idEntry.Text;
        string fieldToUpdate = fieldEntry.Text;
        string newValue = newValueEntry.Text;

        // Validar los campos
        if (string.IsNullOrEmpty(albumIdText) || string.IsNullOrEmpty(fieldToUpdate) || string.IsNullOrEmpty(newValue))
        {
            ShowMessage("Por favor, complete todos los campos.");
            return;
        }

        int albumId;
        if (!int.TryParse(albumIdText, out albumId))
        {
            ShowMessage("El ID del álbum debe ser un número entero.");
            return;
        }

        // llamar al metodo y ver que mensaje imprimir
        int resultadoEditicion = Program.EditarAlbum(albumId, newValue, fieldToUpdate);


        if (resultadoEditicion == 1){
            ShowMessage("Álbum actualizado con éxito.");
        } else {
            ShowMessage("Error al actualizar Album.");
        }
    }

    // Método para mostrar mensajes emergentes
    private void ShowMessage(string message)
    {
        MessageDialog dialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, message);
        dialog.Run();
        dialog.Destroy();
        // Volver a la página de busqueda después del mensaje de error, eliminando la entrada
        EditarInformacion editarInfoWindow = new EditarInformacion();
        editarInfoWindow.ShowAll();
        this.Hide();
    }
}
