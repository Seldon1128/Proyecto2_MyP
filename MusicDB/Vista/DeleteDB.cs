using System;
using Gtk;

public class DeleteDB : Window
{
    private Button yesButton;
    private Button noButton;

    public DeleteDB() : base("Pantalla n - Eliminar Base de Datos")
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

        Label messageLabel = new Label("¿Deseas eliminar la base de datos?");
        
        yesButton = new Button("Eliminar Base de Datos");
        yesButton.Clicked += yesButtonClicked;
        yesButton.MarginLeft = 10;
        yesButton.MarginRight = 10;

        noButton = new Button("Cancelar");
        noButton.MarginLeft = 10;
        noButton.MarginRight = 10;

        noButton.Clicked += (sender, e) =>
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide(); // Ocultar la ventana de Extraer Música
        };

        vbox.PackStart(messageLabel, false, false, 0);
        vbox.PackStart(yesButton, false, false, 0);
        vbox.PackStart(noButton, false, false, 0);

        Add(vbox);
        ShowAll();
    }

    private void yesButtonClicked(object sender, EventArgs e)
    {
        Program.EliminarBaseDatos();
        MensajeEliminarDB mensajeEliminarDB = new MensajeEliminarDB();
        mensajeEliminarDB.Show();
        this.Hide();
    }

    public class MensajeEliminarDB : Window
    {
        public MensajeEliminarDB() : base("DB eliminada")
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
            Label successLabel = new Label("¡Base de datos eliminada!");
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