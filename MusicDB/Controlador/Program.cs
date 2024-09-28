using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, 1 try");

        // Directorio donde se encuentran los MP3
        //string musicDirectory = "/Macintosh HD/Usuarios/seldonsauttoramirez/Documentos/PyM/Canciones mama";
        string musicDirectory = "/Users/seldonsauttoramirez/Documents/PyM/Canciones mama"; // Verifica esta línea


        Console.WriteLine($"Attempting to access: {musicDirectory}");

        if (!Directory.Exists(musicDirectory)){
            Console.WriteLine($"El directorio no existe: {musicDirectory}");
        }


        MP3TagExtractor tagExtractor = new MP3TagExtractor();
        DirectoryMiner miner = new DirectoryMiner(tagExtractor);

        // Recorrer el directorio y procesar los archivos MP3
        miner.TraverseDirectory(musicDirectory);

        Console.WriteLine("Proceso completado.");

    }
}
