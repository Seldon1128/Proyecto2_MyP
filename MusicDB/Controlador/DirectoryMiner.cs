using System;
using System.IO;

public class DirectoryMiner
{
    private MP3TagExtractor tagExtractor;
    private MusicDAO musicDAO;

    public DirectoryMiner(MP3TagExtractor tagExtractor, MusicDAO musicDAO)
    {
        this.tagExtractor = tagExtractor;
        this.musicDAO = musicDAO;
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

        // Determinar el id_type (0: Persona, 1: Grupo, 2: Desconocido)
        int idType = DeterminePerformerType(performer); // Método para determinar si es persona, grupo o desconocido

        // Insertar o recuperar el id del intérprete
        int performerId = musicDAO.InsertOrGetPerformer(performer, idType);

         // Si el tipo es persona, podrías intentar agregar más información sobre la persona (opcional)
        if (idType == 0) // Si es persona
        {
            // Insertar o actualizar información en la tabla persons
            // realName, birthDate, deathDate serán valores opcionales con default
            musicDAO.InsertOrGetPerson(performer, "Real Name", "0000-00-00", "0000-00-00");
        } else if (idType == 1) // Si es grupo
        {
            // Puedes implementar lógica para manejar los grupos en la tabla correspondiente
            musicDAO.InsertOrGetGroup(performer, "0000-00-00", "0000-00-00");
        }

        // Inserta o recupera el id del álbum, pasando también la ruta
        int albumId = musicDAO.InsertOrGetAlbum(album, filePath, (int)year);

        // Insertar la canción en la tabla rolas
        musicDAO.InsertRola(title, filePath, performerId, albumId, (int)track, (int)year, genre);

        Console.WriteLine($"Canción '{title}' procesada y almacenada en la base de datos.");
    }

    private int DeterminePerformerType(string performer)
    {
        if (performer.Contains("Band") || performer.Contains("band") || performer.Contains("banda") || performer.Contains("Group") || performer.Contains("Los") || performer.Contains("los") || performer.Contains("grupo") || performer.Contains("Grupo") || performer.Contains("Las") || performer.Contains("las")) // Aquí podrías hacer un análisis más detallado
        {
            return 1; // Grupo
        }
        else if (!string.IsNullOrEmpty(performer))
        {
            return 0; // Persona
        }
        return 2; // Desconocido
    }
}