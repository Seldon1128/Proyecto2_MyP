using System;
using Gtk;
using MusicDB.Modelo;

public class DefinirInterprete : Window
{
    private Entry search;
    private Button continueDefinePersonButton;
    private Button continueDefineGroupButton;

    public DefinirInterprete() : base("Pantalla 3 - Búsqueda")
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

        Label searchLabel = new Label("Ingresa el nombre del interprete y selecciona como definirlo:");
        search = new Entry();
        search.MarginLeft = 10;
        search.MarginRight = 10;
        
        // Crear los tres botones en la misma línea
        HBox buttonBox = new HBox(false, 10);

        continueDefinePersonButton = new Button("Persona");
        continueDefinePersonButton.Clicked += OncontinueDefinePersonButtonClicked;
        continueDefinePersonButton.MarginLeft = 10;

        continueDefineGroupButton = new Button("Grupo");
        continueDefineGroupButton.Clicked += OncontinueDefineGroupButtonClicked;

        buttonBox.PackStart(continueDefinePersonButton, true, true, 0);
        buttonBox.PackStart(continueDefineGroupButton, true, true, 0);
        

        vbox.PackStart(searchLabel, false, false, 0);
        vbox.PackStart(search, false, false, 0);
        vbox.PackStart(buttonBox, false, false, 0);

        Add(vbox);
        ShowAll();
    }

    // Métodos vacíos para cada botón
    private void OncontinueDefinePersonButtonClicked(object sender, EventArgs e)
    {
        string input = search.Text;
        
        if (!string.IsNullOrEmpty(input))
        {
            int result = Program.DefinirInterprete(input,"1");
            if(result == 3){
                // Mensaje de interprete no encontrado
                MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, "Error, interprete no encontrado.");
                md.Run();
                md.Destroy();
                // Volver a la página de editar informacion después del mensaje de error
                EditarInformacion editarInfoWindow = new EditarInformacion();
                editarInfoWindow.ShowAll();
                this.Hide();
            } else {
                // pantalla con mensaje de exito
                MensajeDefinirInterprete messageWindow = new MensajeDefinirInterprete();
                messageWindow.ShowAll();
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

    private void OncontinueDefineGroupButtonClicked(object sender, EventArgs e)
    {
        string input = search.Text;
        
        if (!string.IsNullOrEmpty(input))
        {
            int result = Program.DefinirInterprete(input,"2");
            if(result == 3){
                // Mensaje de interprete no encontrado
                MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, "Error, interprete no encontrado.");
                md.Run();
                md.Destroy();
                // Volver a la página de editar informacion después del mensaje de error
                EditarInformacion editarInfoWindow = new EditarInformacion();
                editarInfoWindow.ShowAll();
                this.Hide();
            } else {
                // pantalla con mensaje de exito
                MensajeDefinirInterprete messageWindow = new MensajeDefinirInterprete();
                messageWindow.ShowAll();
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

    public class MensajeDefinirInterprete : Window
    {
        public MensajeDefinirInterprete() : base("Exito al definir interprete")
        {
            SetDefaultSize(600, 600);
            SetPosition(WindowPosition.Center);

            VBox vbox = new VBox(false, 10);

            // Crear una barra de título horizontal
            HBox titleBar = new HBox(false, 5);
            // Configurar márgenes para la barra de título
            titleBar.ModifyBg(StateType.Normal, new Gdk.Color(200, 200, 200)); // Color de fondo gris

            // Crear el botón "Inicio" en la esquina superior izquierda
            Button startButton = new Button("Inicio");
            titleBar.PackStart(startButton, false, false, 5);

            startButton.Clicked += (sender, e) =>
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Hide(); // Ocultar la ventana de Extraer Música
            };

            // Crear el título centrado
            Label titleLabel = new Label("<span size='14000' weight='bold'>BaseMusical</span>");
            titleLabel.UseMarkup = true;  // Habilitar el formato
            titleLabel.Halign = Align.Center;
            titleLabel.MarginLeft = 200;  // Deja un margen izquierdo
            titleLabel.MarginRight = 200;  // Deja un margen derecho 
            titleBar.PackStart(titleLabel, true, true, 0);

            // Agregar la barra de título al contenedor principal
            vbox.PackStart(titleBar, false, false, 5);

            // Mensaje de éxito
            Label successLabel = new Label("!Operacion Realizada!");
            successLabel.Halign = Align.Center;
            vbox.PackStart(successLabel, false, false, 0);

            Image successImage = new Image("Vista/images/check.png"); // Cambia la ruta a la imagen
            vbox.PackStart(successImage, false, false, 0);

            // Botón para volver a extraer directorio
            Button volverExtraerButton = new Button("Editar Información");
            volverExtraerButton.Clicked += (sender, e) =>
            {
                EditarInformacion editarInfoWindow = new EditarInformacion();
                editarInfoWindow.ShowAll();
                this.Hide();
            };
        
            // Botón para volver a la página de inicio
            Button inicioButton = new Button("Inicio");
            inicioButton.Clicked += (sender, e) =>
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Hide(); // Ocultar la ventana de Mensaje de Éxito
            };

            // Añadir los botones al VBox
            HBox buttonBox = new HBox(true, 10);
            buttonBox.PackStart(volverExtraerButton, false, false, 0);
            buttonBox.PackStart(inicioButton, false, false, 0);
            vbox.PackStart(buttonBox, false, false, 0);

            Add(vbox);
            ShowAll();
        }
    }

    
}