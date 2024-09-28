using System;
using System.IO;

public class DirectoryMiner
{
    private MP3TagExtractor tagExtractor;

    public DirectoryMiner(MP3TagExtractor tagExtractor)
    {
        this.tagExtractor = tagExtractor;
    }

    public void TraverseDirectory(string rootPath)
    {
        foreach (string file in Directory.GetFiles(rootPath, "*.mp3", SearchOption.AllDirectories))
        {
            Console.WriteLine($"Procesando: {file}");
            ProcessMP3File(file);
        }
    }

    private void ProcessMP3File(string filePath)
    {
        // Extraer etiquetas del MP3
        var (performer, title, album, track, year, genre) = tagExtractor.ExtractTags(filePath);

        // Aquí asumes que el intérprete es una persona (tipo 0), pero podrías agregar lógica para manejar grupos
        int idType = performer.Contains("Band") ? 1 : 0;

        // Imprimir los detalles del MP3 en la terminal
        Console.WriteLine("File Path: " + filePath);
        Console.WriteLine("Performer: " + performer);
        Console.WriteLine("Type ID: " + idType);
        Console.WriteLine("Album: " + album);
        Console.WriteLine("Title: " + title);
        Console.WriteLine("Track: " + track);
        Console.WriteLine("Year: " + year);
        Console.WriteLine("Genre: " + genre);
        Console.WriteLine("------------------------------");
    }
}
