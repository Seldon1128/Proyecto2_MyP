namespace MusicDB.Modelo
{

    public class Song
    {
        public string Title { get; set; }           // Título de la canción
        public string PerformerName { get; set; }   // Nombre del intérprete
        public int Year { get; set; }               // Año de lanzamiento

        // Otras propiedades adicionales que puedes necesitar
        public int IdRola { get; set; }             // ID único de la canción
        public string Genre { get; set; }           // Género de la canción
        public string AlbumName { get; set; }  

        // Constructor opcional para inicializar la canción con valores
        public Song(string title, string performerName, int year, int idRola, string genre, string albumName)
        {
            Title = title;
            PerformerName = performerName;
            Year = year;
            IdRola = idRola;
            Genre = genre;
            AlbumName = albumName;
        }

        // Constructor vacío
        public Song() { }

        public override string ToString()
        {
            return $"ID: {IdRola}, Title: {Title}, Performer: {PerformerName}, Year: {Year}, Genre: {Genre}, Album: {AlbumName}";
        }

    }
}

