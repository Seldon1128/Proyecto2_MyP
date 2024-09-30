using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Iniciando el programa");

        // Ruta a la base de datos
        string databasePath = "musica.db";

        // Crear el DatabaseManager y las tablas
        DatabaseManager dbManager = new DatabaseManager(databasePath);

        // Crear el DAO y pasarle la conexión
        MusicDAO musicDAO = new MusicDAO(dbManager.GetConnection()); 

        // Directorio donde se encuentran los MP3
        string musicDirectory = "/Users/seldonsauttoramirez/Documents/PyM/Canciones mama"; // Verifica esta línea


        Console.WriteLine($"Attempting to access: {musicDirectory}");

        if (!Directory.Exists(musicDirectory)){
            Console.WriteLine($"El directorio no existe: {musicDirectory}");
        }


        MP3TagExtractor tagExtractor = new MP3TagExtractor();
        DirectoryMiner miner = new DirectoryMiner(tagExtractor, musicDAO);

        // Recorrer el directorio y procesar los archivos MP3
        miner.TraverseDirectory(musicDirectory);

        // Cerrar la conexión a la base de datos
        dbManager.CloseConnection();

        Console.WriteLine("Proceso completado.");

    }
}
