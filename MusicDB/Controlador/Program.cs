using System;
using System.IO;
using Gtk;
using MusicDB.Modelo;

class Program
{
    private static MusicDAO musicDAO;

    static void Main()
    {
        // Ruta a la base de datos
        string databasePath = "musica.db";

        // Crear el DatabaseManager y las tablas
        DatabaseManager dbManager = new DatabaseManager(databasePath);

        // Inicializar el DAO y pasarle la conexión
        musicDAO = new MusicDAO(dbManager.GetConnection());

        // Directorio donde se encuentran los MP3
        string musicDirectory = "/Users/seldonsauttoramirez/Documents/PyM/Canciones mama"; // Verifica esta línea

        string opcion = "";

        Console.WriteLine("Bienvenido al programa de gestión de música");

        while (opcion != "8")
        {
        
            Console.WriteLine("Por favor, selecciona una opción:");
            Console.WriteLine("1. Minar Directorio");
            Console.WriteLine("2. Mostrar Canciones");
            Console.WriteLine("3. Buscar Canciones");
            Console.WriteLine("4. Agregar personas a Grupos");
            Console.WriteLine("5. Definir a un interprete como Persona o Grupo");
            Console.WriteLine("6. Editar la informacion de albumes");
            Console.WriteLine("7. Editar la informacion de rolas");
            Console.WriteLine("8. Salir");

            opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.WriteLine("Iniciando el proceso de minado del directorio...");

                    if (!Directory.Exists(musicDirectory))
                    {
                        Console.WriteLine($"El directorio no existe: {musicDirectory}");
                    }
                    else
                    {
                        MP3TagExtractor tagExtractor = new MP3TagExtractor();
                        DirectoryMiner miner = new DirectoryMiner(tagExtractor, musicDAO);

                        // Recorrer el directorio y procesar los archivos MP3
                        miner.TraverseDirectory(musicDirectory);
                    }

                    Console.WriteLine("Minado completado.");
                    break;

                case "2":
                    Console.WriteLine("Mostrando las canciones almacenadas en la base de datos...");

                    // Obtener las canciones desde la base de datos y mostrarlas
                    var canciones = musicDAO.GetAllSongs();

                    foreach (var cancion in canciones)
                    {
                        Console.WriteLine($"Título: {cancion.Title}, Intérprete: {cancion.PerformerName}, Año: {cancion.Year}");
                    }
                    break;

                case "3":
                    // Buscar canciones
                    Console.WriteLine("¿Por qué criterio deseas buscar?");
                    Console.WriteLine("1. Título");
                    Console.WriteLine("2. Intérprete");
                    Console.WriteLine("3. Álbum");
                    string searchOption = Console.ReadLine();

                    string searchBy = "";
                    if (searchOption == "1") searchBy = "title";
                    else if (searchOption == "2") searchBy = "performer";
                    else if (searchOption == "3") searchBy = "album";

                    Console.Write("Introduce el texto de búsqueda: ");
                    string searchText = Console.ReadLine();

                    var searchResults = musicDAO.SearchSongs(searchText, searchBy);
                    if (searchResults.Count > 0)
                    {
                        Console.WriteLine("Resultados de la búsqueda:");
                        foreach (var song in searchResults)
                        {
                            // Solo imprimir título, intérprete y año
                            Console.WriteLine($"Título: {song.Title}, Intérprete: {song.PerformerName}, Año: {song.Year}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No se encontraron canciones.");
                    }
                    break;
                case "4":
                    // Agregar persona a grupo
                    Console.WriteLine("Introduce el nombre del grupo:");
                    string groupName = Console.ReadLine();

                    Console.WriteLine("Introduce el nombre de la persona a agregar:");
                    string personName = Console.ReadLine();

                    int result = musicDAO.AddPersonToGroup(personName, groupName);

                    if (result == 1){
                        Console.WriteLine("Error, Grupo: " + groupName + " no encontrado.");
                    }
                    else if (result == 2){
                        Console.WriteLine("Persona: " + personName + " agregada correctamente al grupo " + groupName + ".");
                    } else if (result == 3){
                        Console.WriteLine("Persona: " + personName + " no encontrada, se agregó a tabla persons y al grupo " + groupName + ".");
                    } else if (result == 4){
                        Console.WriteLine("Persona: " + personName + " se encontraba en el grupo: " + groupName + ".");
                    } else {
                        Console.WriteLine("Ocurrió un error");
                    }
                    break;

                case "5":
                    //  Definir a un interprete como persona o grupo
                    // Mostrar una pagina con los interpretes, trabajar como en un get interpretes
                    // Agregar persona a grupo
                    Console.WriteLine("Introduce el nombre del interprete:");
                    string performerName = Console.ReadLine();

                    Console.WriteLine("Introduce el numero de la opción a definir");
                    Console.WriteLine("1. Persona");
                    Console.WriteLine("2. Grupo");
                    string defineOption = Console.ReadLine();

                    string defineBy = "";
                    if (defineOption == "1") defineBy = "Person";
                    else if (defineOption == "2") defineBy = "Group";

                    int resultChange = musicDAO.DefinePerformer(performerName, defineOption);

                    if (resultChange == 1){
                        Console.WriteLine("Error, el nombre del interprete: " + performerName + " ya estaba definido como: " + defineBy + ".");
                    }
                    else if (resultChange == 2){
                        Console.WriteLine("Exito, cambio realizado, interprete: " + performerName + ", definido como: " + defineBy + ".");
                    } else if (resultChange == 3){
                        Console.WriteLine("Error, interprete: " + performerName + " no encontrado.");
                    }
                    break;
                case "6":
                    List<Album> albums = musicDAO.GetAlbums();

                    // Mostrar la lista de álbumes
                    Console.WriteLine("Lista de álbumes disponibles:");
                    foreach (var album in albums)
                    {
                        Console.WriteLine(album); // Llama al método ToString() de la clase Album
                    }

                    // Solicitar que el usuario elija un álbum por ID
                    Console.WriteLine("Ingrese el ID del álbum que desea editar:");
                    int selectedAlbumId = Convert.ToInt32(Console.ReadLine());

                    // Solicitar qué desea editar
                    Console.WriteLine("¿Qué desea cambiar? (name/year):");
                    string fieldToUpdate = Console.ReadLine();

                    // Solicitar el nuevo valor
                    Console.WriteLine($"Ingrese el nuevo valor para {fieldToUpdate}:");
                    string newText = Console.ReadLine();

                    // Llamar al método EditAlbum para realizar la actualización
                    int resultAlbumMod = musicDAO.EditAlbum(selectedAlbumId, newText, fieldToUpdate);

                    if (resultAlbumMod == 1)
                    {
                        Console.WriteLine("Álbum actualizado con éxito.");
                    }
                    else if (resultAlbumMod == 2)
                    {
                        Console.WriteLine("Álbum no encontrado.");
                    }
                    else if (resultAlbumMod == 3)
                    {
                        Console.WriteLine("Campo no válido para actualizar.");
                    }
                    else
                    {
                        Console.WriteLine("Error al actualizar el álbum.");
                    }
                    break;
                case "7":
                    // Obtener la lista de rolas disponibles
                    List<Song> rolas = musicDAO.GetAllSongs();

                    // Mostrar la lista de rolas
                    Console.WriteLine("Lista de rolas disponibles:");
                    foreach (var rola in rolas)
                    {
                        Console.WriteLine(rola); // Llama al método ToString() de la clase Song
                    }

                    // Solicitar que el usuario elija una rola por ID
                    Console.WriteLine("Ingrese el ID de la rola que desea editar:");
                    int selectedRolaId = Convert.ToInt32(Console.ReadLine());

                    // Solicitar qué desea editar
                    Console.WriteLine("¿Qué desea cambiar? (title/year/genre/performer/album):");
                    string intentoCambio = Console.ReadLine();

                    // Solicitar el nuevo valor
                    Console.WriteLine($"Ingrese el nuevo valor para {intentoCambio}:");
                    string nuevoTexto = Console.ReadLine();

                    // Llamar al método EditSong para realizar la actualización
                    int resultRolaMod = musicDAO.EditSong(selectedRolaId, nuevoTexto, intentoCambio);

                    // Verificar el resultado de la actualización
                    if (resultRolaMod == 0)
                    {
                        Console.WriteLine("Rola actualizada con éxito.");
                    }
                    else if (resultRolaMod == 1)
                    {
                        Console.WriteLine("El intérprete no existe.");
                    }
                    else if (resultRolaMod == 2)
                    {
                        Console.WriteLine("El álbum no existe.");
                    }
                    else if (resultRolaMod == -1)
                    {
                        Console.WriteLine("Error al actualizar la rola.");
                    }
                    break;
                case "8":
                    Console.WriteLine("Saliendo del programa...");
                    break;

                default:
                    Console.WriteLine("Opción no válida. Por favor, intenta nuevamente.");
                    break;
            }
        }

        // Iniciar la aplicación de GTK
        Application.Init();
        MainWindow mainWindow = new MainWindow();  // Pasar MusicDAO a la ventana principal
        mainWindow.Show();
        Application.Run();

        // Cerrar la conexión a la base de datos
        dbManager.CloseConnection();
    }

    public static List<Song> Directorio(){
        List<Song> canciones = musicDAO.GetAllSongs();  
        return canciones;
    }

    public static int minarDirectorio(string musicDirectory){
        string databasePath = "musica.db";
        DatabaseManager dbManager = new DatabaseManager(databasePath); // por si la base de datos fue eliminada
        if (!Directory.Exists(musicDirectory)) {
            return 0; // El directorio no existe
        } else {
            MP3TagExtractor tagExtractor = new MP3TagExtractor();
            DirectoryMiner miner = new DirectoryMiner(tagExtractor, musicDAO);

            // Recorrer el directorio y procesar los archivos MP3
            miner.TraverseDirectory(musicDirectory);
            return 1;
        }
    }

    public static List<Song> BuscarCanciones(string searchText, string searchBy){
        List<Song> canciones = musicDAO.SearchSongs(searchText, searchBy); 
        return canciones;
    }

    public static void EliminarBaseDatos(){
        if (File.Exists("musica.db")){
            File.Delete("musica.db");
        }
    }

    public static int DefinirInterprete(string performerName, string defineOption){
        int resultChange = musicDAO.DefinePerformer(performerName, defineOption);
        return resultChange;
    }

}
