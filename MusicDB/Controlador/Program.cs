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

        while (opcion != "6")
        {
            Console.WriteLine("Bienvenido al programa de gestión de música");
            Console.WriteLine("Por favor, selecciona una opción:");
            Console.WriteLine("1. Minar Directorio");
            Console.WriteLine("2. Mostrar Canciones");
            Console.WriteLine("3. Buscar Canciones");
            Console.WriteLine("4. Agregar personas a Grupos");
            Console.WriteLine("5. Definir a un interprete como Persona o Grupo");
            Console.WriteLine("6. Salir");

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

}
