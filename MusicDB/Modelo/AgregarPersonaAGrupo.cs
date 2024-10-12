using System;
using Gtk;

public class AgregarPersonaAGrupo : Window
{
    private Entry nombrePersonaEntry;
    private Entry nombreGrupoEntry;
    private Button agregarButton;

    public AgregarPersonaAGrupo() : base("Agregar Persona a Grupo")
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
            this.Hide(); // Ocultar la ventana actual
        };

        // Crear el título centrado
        Label titleLabel = new Label("<span size='14000' weight='bold'>BaseMusical</span>");
        titleLabel.UseMarkup = true;
        titleLabel.Halign = Align.Center;
        titleBar.PackStart(titleLabel, true, true, 0);

        vbox.PackStart(titleBar, false, false, 5);

        // Etiqueta para el campo de nombre de la persona
        Label nombrePersonaLabel = new Label("Ingresa el nombre de la persona:");
        nombrePersonaEntry = new Entry();
        nombrePersonaEntry.MarginLeft = 10;
        nombrePersonaEntry.MarginRight = 10;

        // Etiqueta para el campo de nombre del grupo
        Label nombreGrupoLabel = new Label("Ingresa el nombre del grupo:");
        nombreGrupoEntry = new Entry();
        nombreGrupoEntry.MarginLeft = 10;
        nombreGrupoEntry.MarginRight = 10;

        // Botón para agregar la persona al grupo
        agregarButton = new Button("Agregar");
        agregarButton.Clicked += OnAgregarButtonClicked;
        agregarButton.MarginLeft = 10;
        agregarButton.MarginRight = 10;
        

        vbox.PackStart(nombrePersonaLabel, false, false, 0);
        vbox.PackStart(nombrePersonaEntry, false, false, 0);
        vbox.PackStart(nombreGrupoLabel, false, false, 0);
        vbox.PackStart(nombreGrupoEntry, false, false, 0);
        vbox.PackStart(agregarButton, false, false, 10);

        Add(vbox);
        ShowAll();
    }

    // Método para manejar el evento de clic del botón "Agregar"
    private void OnAgregarButtonClicked(object sender, EventArgs e)
    {
        string nombrePersona = nombrePersonaEntry.Text;
        string nombreGrupo = nombreGrupoEntry.Text;

        if (!string.IsNullOrEmpty(nombrePersona) && !string.IsNullOrEmpty(nombreGrupo))
        {
            int result = Program.AgregarPersonaAGrupo(nombrePersona, nombreGrupo);
            if (result == 1)
            {
                // Mostrar mensaje de éxito
                MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Error, grupo no encontrado.");
                md.Run();
                md.Destroy();
                // Volver a la página de editar informacion después del mensaje de error
                EditarInformacion editarInfoWindow = new EditarInformacion();
                editarInfoWindow.ShowAll();
                this.Hide();
            }
            else
            {
                // Mostrar mensaje de error
                MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, "Operación Exitosa.");
                md.Run();
                md.Destroy();
                // Volver a la página de editar informacion después del mensaje de exito
                EditarInformacion editarInfoWindow = new EditarInformacion();
                editarInfoWindow.ShowAll();
                this.Hide();
            }
        }
        else
        {
            // Mostrar un mensaje de error si los campos están vacíos
            MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, "Por favor, ingresa un nombre de persona y un nombre de grupo válidos.");
            md.Run();
            md.Destroy();
        }
    }
}
