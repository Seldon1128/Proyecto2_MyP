public class Song
{
    public string Title { get; set; }           // Título de la canción
    public string PerformerName { get; set; }   // Nombre del intérprete
    public int Year { get; set; }               // Año de lanzamiento

    // Otras propiedades adicionales que puedes necesitar
    public int IdRola { get; set; }             // ID único de la canción
    public string Genre { get; set; }           // Género de la canción

    // Constructor opcional para inicializar la canción con valores
    public Song(string title, string performerName, int year, int idRola, string genre)
    {
        Title = title;
        PerformerName = performerName;
        Year = year;
        IdRola = idRola;
        Genre = genre;
    }

    // Constructor vacío
    public Song() { }

}

