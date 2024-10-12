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

        // Iniciar la aplicación de GTK
        Application.Init();
        MainWindow mainWindow = new MainWindow();  // Pasar MusicDAO a la ventana principal
        mainWindow.Show();
        Application.Run();

        // Cerrar la conexión a la base de datos
        dbManager.CloseConnection();
    }

    // A continuación hay metodos que llama la GUI para mostrar información de la base de datos musical

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

    public static int DefinirInterprete(string performerName, string defineOption){
        int resultChange = musicDAO.DefinePerformer(performerName, defineOption);
        return resultChange;
    }

    public static int AgregarPersonaAGrupo(string personName, string groupName){
        int resultAgregarPersonaAGrupo = musicDAO.AddPersonToGroup(personName, groupName);
        return resultAgregarPersonaAGrupo;
    }

    public static List<Album> AlbumsList(){
        List<Album> albums = musicDAO.GetAlbums();
        return albums;
    }

    public static int EditarAlbum(int selectedAlbumId, string newText, string fieldToUpdate){
        int resultAlbumModEdit = musicDAO.EditAlbum(selectedAlbumId, newText, fieldToUpdate);
        return resultAlbumModEdit;
    }

    public static int EditarRola(int selectedRolaId, string nuevoTexto, string intentoCambio){
        int resultRolaMod = musicDAO.EditSong(selectedRolaId, nuevoTexto, intentoCambio);
        return resultRolaMod;
    }

}
