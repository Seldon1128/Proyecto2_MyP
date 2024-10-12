using System;
using Gtk;
using MusicDB.Modelo;


public class EditarInformacion : Window
{
    public EditarInformacion() : base("BaseMusical")
    {
        // Tamaño de la ventana
        SetDefaultSize(600, 600);
        SetPosition(WindowPosition.Center);

        // Crear un contenedor vertical principal
        VBox mainVBox = new VBox(false, 10);

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
        mainVBox.PackStart(titleBar, false, false, 5);

        // Crear un contenedor para las cuatro secciones con márgenes iguales
        VBox sectionsBox = new VBox(false, 10);
        sectionsBox.BorderWidth = 10; // Márgenes alrededor de los botones

        // Crear y agregar las secciones con sus imágenes y títulos
        sectionsBox.PackStart(CreateSection("Definir Interprete", "Vista/images/defInterprete.png"), true, true, 0);
        sectionsBox.PackStart(CreateSection("Agregar Persona a Grupo", "Vista/images/nuevaPersona.png"), true, true, 0);
        sectionsBox.PackStart(CreateSection("Editar Informacion Rolas", "Vista/images/editar.png"), true, true, 0);
        sectionsBox.PackStart(CreateSection("Editar Información Album", "Vista/images/editar.png"), true, true, 0);
        

         // Agregar el contenedor para el desplazamiento
        ScrolledWindow scrolledWindow = new ScrolledWindow();
        scrolledWindow.Add(sectionsBox); // Agregar el contenedor de secciones al ScrolledWindow

        // Agregar el ScrolledWindow al contenedor principal
        mainVBox.PackStart(scrolledWindow, true, true, 0);

        // Agregar el contenedor principal a la ventana
        Add(mainVBox);

        ShowAll();
    }

    // Función para crear una sección con imagen arriba y título abajo
    private VBox CreateSection(string title, string imagePath)
    {
        // Crear un contenedor vertical para la sección
        VBox section = new VBox(false, 5);

        // Crear una imagen
        Image icon = new Image(imagePath);
        icon.SetSizeRequest(50, 50);

        // Crear el título de la sección
        Label sectionLabel = new Label(title);
        sectionLabel.Halign = Align.Center;

        // Crear un botón que contiene la imagen y el título
        Button sectionButton = new Button();
        VBox buttonContent = new VBox(false, 5);
        buttonContent.PackStart(icon, true, true, 0); // Imagen arriba
        buttonContent.PackStart(sectionLabel, false, false, 10); // Texto abajo
        sectionButton.Add(buttonContent);

        // Agregar el evento de clic para el botón
        sectionButton.Clicked += (sender, e) =>
        {
            if (title == "Definir Interprete"){
                // abrir ventana
                DefinirInterprete definirInterprete = new DefinirInterprete();
                definirInterprete.ShowAll();
                this.Hide();
            } else if (title == "Agregar Persona a Grupo"){
                // abrir ventana
                AgregarPersonaAGrupo agregarPersonaWindow = new AgregarPersonaAGrupo();
                agregarPersonaWindow.ShowAll();
                this.Hide();
            } else if (title == "Editar Informacion Rolas"){
                // abrir ventana
                EditarRola editarRolaWindow = new EditarRola();
                editarRolaWindow.ShowAll();
                this.Hide();
            } else if (title == "Editar Información Album"){
                // abrir ventana
                List<Album> albumes = Program.AlbumsList();
                EditAlbumScreen editAlbumWindow = new EditAlbumScreen(albumes);
                editAlbumWindow.ShowAll();
                this.Hide();
            } 
        };

        // Agregar el botón al contenedor de la sección
        section.PackStart(sectionButton, true, true, 0);

        return section;
    }
    
}