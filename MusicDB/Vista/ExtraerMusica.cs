using System;
using Gtk;

public class ExtraerMusica : Window
{
    private Entry directoryPath;
    private Button continueButton;

    public ExtraerMusica() : base("Pantalla 2 - Extraer Directorio")
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

        Label directoryPathLabel = new Label("Ingresa la ruta del directorio:");
        directoryPath = new Entry();
        directoryPath.MarginLeft = 10;
        directoryPath.MarginRight = 10;
        continueButton = new Button("Extraer Directorio");
        continueButton.Clicked += OnContinueButtonClicked;
        continueButton.MarginLeft = 10;
        continueButton.MarginRight = 10;

        vbox.PackStart(directoryPathLabel, false, false, 0);
        vbox.PackStart(directoryPath, false, false, 0);
        vbox.PackStart(continueButton, false, false, 0);

        Add(vbox);
        ShowAll();
    }

    private void OnContinueButtonClicked(object sender, EventArgs e)
    {
        // Obtener el texto ingresado en el Entry
        string path = directoryPath.Text;
        int minadoExitoso; // Variable que muestra si se mino o no el directorio

        // Lógica condicional para el botón
        if (!string.IsNullOrEmpty(path))
        {
            // Agregar logica para extraer con modelo y controlador
            minadoExitoso = Program.minarDirectorio(path);
            // podemos entrar a esta nueva ventana con un booleano y si es cierto o falso mostrar diferentes mensajes
            MensajeExito mensajeExitoWindow = new MensajeExito(minadoExitoso);
            mensajeExitoWindow.Show();
            this.Hide(); // Ocultar la ventana de Extraer Música
        }
        else
        {
            // Mostrar un mensaje de error si el campo está vacío
            MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, "Por favor, ingresa una ruta de directorio válida.");
            md.Run();
            md.Destroy();
        }
    }

    public class MensajeExito : Window
    {
        public MensajeExito(int tipoMensaje) : base("DirectorioMinado")
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
            if (tipoMensaje==1){
                Label successLabel = new Label("¡Directorio extraído con éxito!");
                successLabel.Halign = Align.Center;
                vbox.PackStart(successLabel, false, false, 0);
            } else {
                Label successLabel = new Label("Minado no realizado, directorio no encontrado.");
                successLabel.Halign = Align.Center;
                vbox.PackStart(successLabel, false, false, 0);
            }

            // Imagen de palomita
            if (tipoMensaje == 1){
                Image successImage = new Image("Vista/images/check.png"); // Cambia la ruta a la imagen
                vbox.PackStart(successImage, false, false, 0);
            } else {
                Image successImage = new Image("Vista/images/error.png"); // Cambia la ruta a la imagen
                vbox.PackStart(successImage, false, false, 0);
            }

            // Botón para volver a extraer directorio
            Button volverExtraerButton = new Button("Volver a extraer directorio");
            volverExtraerButton.Clicked += (sender, e) =>
            {
                ExtraerMusica extraerMusicaWindow = new ExtraerMusica();
                extraerMusicaWindow.Show();
                this.Hide(); // Ocultar la ventana de Mensaje de Éxito
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
