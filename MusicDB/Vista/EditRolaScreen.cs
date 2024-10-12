using System;
using Gtk;
using System.Collections.Generic;
using MusicDB.Modelo;

public class EditRolaScreen : Window
{
    private TreeView rolaTable;
    private Entry idEntry;
    private Entry fieldEntry;
    private Entry newValueEntry;
    private Button updateButton;

    public EditRolaScreen(List<Song> rolas) : base("Editar Rola")
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
            this.Hide(); // Ocultar la ventana de edición
        };

        // Crear el título centrado
        Label titleLabel = new Label("<span size='14000' weight='bold'>BaseMusical</span>");
        titleLabel.UseMarkup = true;
        titleLabel.Halign = Align.Center;
        titleLabel.MarginLeft = 200;  // Margen izquierdo
        titleLabel.MarginRight = 200; // Margen derecho
        titleBar.PackStart(titleLabel, true, true, 0);

        // Agregar la barra de título al contenedor principal
        vboxTable.PackStart(titleBar, false, false, 5);

        // Crear la tabla de rolas
        rolaTable = new TreeView();
        CreateRolaTable();
        PopulateRolaTable(rolas);

        // Agregar la tabla al contenedor con scroll
        ScrolledWindow scrolledWindow = new ScrolledWindow();
        scrolledWindow.Add(rolaTable);
        vboxTable.PackStart(scrolledWindow, true, true, 5);

        // Agregar la sección de tabla a la interfaz principal
        hboxMain.PackStart(vboxTable, true, true, 10);

        // Crear la sección de edición de rola
        VBox vboxForm = new VBox(false, 10);

        // Texto: Ingrese el ID de la rola que desea editar
        Label labelId = new Label("Ingrese el ID de la rola que desea editar:");
        vboxForm.PackStart(labelId, false, false, 5);

        // Entrada para ID de la rola
        idEntry = new Entry();
        vboxForm.PackStart(idEntry, false, false, 5);

        // Texto: ¿Qué desea cambiar? (nombre, duración, etc.)
        Label labelField = new Label("¿Qué desea cambiar? (title/year/genre/performer/album):");
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

    // Método para crear las columnas de la tabla de rolas
    private void CreateRolaTable()
    {
        // Crear columna para ID de la rola
        TreeViewColumn idColumn = new TreeViewColumn { Title = "ID" };
        CellRendererText idCell = new CellRendererText();
        idColumn.PackStart(idCell, true);
        idColumn.AddAttribute(idCell, "text", 0);  // Columna 0
        rolaTable.AppendColumn(idColumn);

        // Crear columna para el Nombre de la rola
        TreeViewColumn nameColumn = new TreeViewColumn { Title = "Titulo" };
        CellRendererText nameCell = new CellRendererText();
        nameColumn.PackStart(nameCell, true);
        nameColumn.AddAttribute(nameCell, "text", 1);  // Columna 1
        rolaTable.AppendColumn(nameColumn);

        TreeViewColumn performerColumn = new TreeViewColumn { Title = "Performer" };
        CellRendererText performerCell = new CellRendererText();
        performerColumn.PackStart(performerCell, true);
        performerColumn.AddAttribute(performerCell, "text", 2);  // Columna 1
        rolaTable.AppendColumn(performerColumn);

        // Crear columna para la Duración
        TreeViewColumn albumColumn = new TreeViewColumn { Title = "Album" };
        CellRendererText albumCell = new CellRendererText();
        albumColumn.PackStart(albumCell, true);
        albumColumn.AddAttribute(albumCell, "text", 3);  // Columna 2
        rolaTable.AppendColumn(albumColumn);
    }

    // Método para poblar la tabla con rolas
    private void PopulateRolaTable(List<Song> rolas)
    {
        ListStore store = new ListStore(typeof(int), typeof(string), typeof(string), typeof(string));

        foreach (var rola in rolas)
        {
            store.AppendValues(rola.IdRola, rola.Title, rola.PerformerName, rola.AlbumName);
        }

        rolaTable.Model = store;
    }

    // Método para manejar el clic del botón "Hacer cambios"
    private void OnUpdateButtonClicked(object sender, EventArgs e)
    {
        string rolaIdText = idEntry.Text;
        string fieldToUpdate = fieldEntry.Text;
        string newValue = newValueEntry.Text;

        // Validar los campos
        if (string.IsNullOrEmpty(rolaIdText) || string.IsNullOrEmpty(fieldToUpdate) || string.IsNullOrEmpty(newValue))
        {
            ShowMessage("Por favor, complete todos los campos.");
            return;
        }

        int rolaId;
        if (!int.TryParse(rolaIdText, out rolaId))
        {
            ShowMessage("El ID de la rola debe ser un número entero.");
            return;
        }

        // Llamar al método de edición de rolas
        int resultadoEdicion = Program.EditarRola(rolaId, newValue, fieldToUpdate);

        if (resultadoEdicion == 0)
        {
            ShowMessage("Rola actualizada con éxito.");
        }
        else
        {
            ShowMessage("Error al actualizar la rola.");
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
