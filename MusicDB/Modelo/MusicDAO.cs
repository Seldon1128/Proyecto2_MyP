using System;
using Microsoft.Data.Sqlite;

public class MusicDAO
{
    private SqliteConnection connection;


    public MusicDAO(SqliteConnection connection)
    {
        this.connection = connection;
    }

    // Método para insertar un intérprete
    public int InsertOrGetPerformer(string name, int typeId)
    {
        connection.Open();
        // Verificar si el intérprete ya existe
        string selectQuery = "SELECT id_performer FROM performers WHERE name = @name";
        using (var selectCmd = new SqliteCommand(selectQuery, connection))
        {
            selectCmd.Parameters.AddWithValue("@name", name);
            var result = selectCmd.ExecuteScalar();
            
            if (result != null)
            {
                return Convert.ToInt32(result); // Si existe, retorna el id_performer
            }
        }

        // Si no existe, insertarlo
        string insertQuery = "INSERT INTO performers (name, id_type) VALUES (@name, @typeId); SELECT last_insert_rowid();";
        using (var insertCmd = new SqliteCommand(insertQuery, connection))
        {
            insertCmd.Parameters.AddWithValue("@name", name);
            insertCmd.Parameters.AddWithValue("@typeId", typeId);
            return Convert.ToInt32(insertCmd.ExecuteScalar()); // Retorna el nuevo id_performer
        }
        
    }

    public int InsertOrGetAlbum(string albumName, string albumPath, int year)
    {
        connection.Open();

        // Verificar si el álbum ya existe
        string selectQuery = "SELECT id_album FROM albums WHERE name = @name AND path = @path";
        using (var selectCmd = new SqliteCommand(selectQuery, connection))
        {
            selectCmd.Parameters.AddWithValue("@name", albumName);
            selectCmd.Parameters.AddWithValue("@path", albumPath); // Asegúrate de pasar la ruta
            var result = selectCmd.ExecuteScalar();
        
            if (result != null)
            {
                return Convert.ToInt32(result); // Retorna el id_album si existe
            }
        }

        // Si no existe, insertar el álbum
        string insertQuery = "INSERT INTO albums (name, path, year) VALUES (@name, @path, @year); SELECT last_insert_rowid();";
        using (var insertCmd = new SqliteCommand(insertQuery, connection))
        {
            insertCmd.Parameters.AddWithValue("@name", albumName);
            insertCmd.Parameters.AddWithValue("@path", albumPath); // Asegúrate de pasar la ruta aquí también
            insertCmd.Parameters.AddWithValue("@year", year);
            return Convert.ToInt32(insertCmd.ExecuteScalar()); // Retorna el nuevo id_album
        }
    }

    public void InsertRola(string title, string filePath, int performerId, int albumId, int trackNumber, int year, string genre)
    {
        connection.Open();

        string insertQuery = @"INSERT INTO rolas (title, path, id_performer, id_album, track, year, genre) VALUES (@title, @path, @performerId, @albumId, @trackNumber, @year, @genre)";
        
        using (var insertCmd = new SqliteCommand(insertQuery, connection))
        {
            insertCmd.Parameters.AddWithValue("@title", title);
            insertCmd.Parameters.AddWithValue("@path", filePath);
            insertCmd.Parameters.AddWithValue("@performerId", performerId);
            insertCmd.Parameters.AddWithValue("@albumId", albumId);
            insertCmd.Parameters.AddWithValue("@trackNumber", trackNumber);
            insertCmd.Parameters.AddWithValue("@year", year);
            insertCmd.Parameters.AddWithValue("@genre", genre);
            insertCmd.ExecuteNonQuery();
        }
    }

    public int InsertOrGetPerson(string stageName, string realName, string birthDate, string deathDate)
    {
        connection.Open();

        // Verificar si la persona ya existe
        string selectQuery = "SELECT id_person FROM persons WHERE stage_name = @stageName";
        using (var selectCmd = new SqliteCommand(selectQuery, connection))
        {
            selectCmd.Parameters.AddWithValue("@stageName", stageName);
            var result = selectCmd.ExecuteScalar();
            
            if (result != null)
            {
                return Convert.ToInt32(result); // Retorna id_person si ya existe
            }
        }

        // Insertar nueva persona
        string insertQuery = @"INSERT INTO persons (stage_name, real_name, birth_date, death_date) VALUES (@stageName, @realName, @birthDate, @deathDate); SELECT last_insert_rowid();";
        using (var insertCmd = new SqliteCommand(insertQuery, connection))
        {
            insertCmd.Parameters.AddWithValue("@stageName", stageName);
            insertCmd.Parameters.AddWithValue("@realName", realName);
            insertCmd.Parameters.AddWithValue("@birthDate", birthDate);
            insertCmd.Parameters.AddWithValue("@deathDate", deathDate);
            return Convert.ToInt32(insertCmd.ExecuteScalar()); // Retorna id_person
        }
        
    }

    public int InsertOrGetGroup(string groupName, string startDate, string endDate)
    {
        connection.Open();

        // Verificar si el grupo ya existe
        string selectQuery = "SELECT id_group FROM groups WHERE name = @groupName";
        using (var selectCmd = new SqliteCommand(selectQuery, connection))
        {
            selectCmd.Parameters.AddWithValue("@groupName", groupName);
            var result = selectCmd.ExecuteScalar();
            
            if (result != null)
            {
                return Convert.ToInt32(result); // Retorna id_group si ya existe
            }
        }

        // Insertar nuevo grupo
        string insertQuery = @"INSERT INTO groups (name, start_date, end_date) VALUES (@groupName, @startDate, @endDate); SELECT last_insert_rowid();";
        using (var insertCmd = new SqliteCommand(insertQuery, connection))
        {
            insertCmd.Parameters.AddWithValue("@groupName", groupName);
            insertCmd.Parameters.AddWithValue("@startDate", startDate);
            insertCmd.Parameters.AddWithValue("@endDate", endDate);
            return Convert.ToInt32(insertCmd.ExecuteScalar()); // Retorna id_group
        }
    }



}
