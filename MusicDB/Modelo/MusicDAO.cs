using System;
using Microsoft.Data.Sqlite;
using MusicDB.Modelo;

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
        using (var connection = this.connection)  // Usar el using para cerrar la conexión al finalizar
        {
            connection.Open();

            // Verificar si el álbum ya existe con nombre, ruta y año
            string selectQuery = "SELECT id_album FROM albums WHERE name = @name AND path = @path AND year = @year";
            using (var selectCmd = new SqliteCommand(selectQuery, connection))
            {
                selectCmd.Parameters.AddWithValue("@name", albumName);
                selectCmd.Parameters.AddWithValue("@path", albumPath);  // Verificación del path
                selectCmd.Parameters.AddWithValue("@year", year);  // Verificación del año
                var result = selectCmd.ExecuteScalar();
        
                if (result != null)
                {
                    return Convert.ToInt32(result); // Si existe, retorna el id_album
                }
            }

            // Si no existe, insertar el álbum
            string insertQuery = "INSERT INTO albums (name, path, year) VALUES (@name, @path, @year); SELECT last_insert_rowid();";
            using (var insertCmd = new SqliteCommand(insertQuery, connection))
            {
                insertCmd.Parameters.AddWithValue("@name", albumName);
                insertCmd.Parameters.AddWithValue("@path", albumPath); 
                insertCmd.Parameters.AddWithValue("@year", year);  
                return Convert.ToInt32(insertCmd.ExecuteScalar()); // Retorna el nuevo id_album
            }
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

    public List<Song> GetAllSongs()
    {
        List<Song> canciones = new List<Song>();
        connection.Open();

        string selectQuery = @"SELECT r.title, p.name as performer, r.year, r.id_rola, r.genre, a.name as album 
                            FROM rolas r
                            JOIN performers p ON r.id_performer = p.id_performer
                            LEFT JOIN albums a ON r.id_album = a.id_album";


        using (var selectCmd = new SqliteCommand(selectQuery, connection))
        {
            using (var reader = selectCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Song song = new Song
                    {
                        Title = reader["title"].ToString(),
                        PerformerName = reader["performer"].ToString(),
                        Year = Convert.ToInt32(reader["year"]),
                        IdRola = Convert.ToInt32(reader["id_rola"]),
                        Genre = reader["genre"].ToString(), 
                        AlbumName = reader["album"]?.ToString()
                    };
                    canciones.Add(song);
                }
            }
        }

        return canciones;
    }

    public List<Song> SearchSongs(string searchText, string searchBy)
    {
        List<Song> canciones = new List<Song>();
        connection.Open();

        // Definir la consulta SQL dependiendo de lo que se quiera buscar
        string selectQuery = "";

        if (searchBy == "title")
        {
            selectQuery = @"SELECT r.title, p.name as performer, r.year, r.id_rola, r.genre, a.name as album
                            FROM rolas r
                            JOIN performers p ON r.id_performer = p.id_performer
                            LEFT JOIN albums a ON r.id_album = a.id_album
                            WHERE r.title LIKE @searchText";
        }
        else if (searchBy == "performer")
        {
            selectQuery = @"SELECT r.title, p.name as performer, r.year, r.id_rola, r.genre, a.name as album 
                            FROM rolas r
                            JOIN performers p ON r.id_performer = p.id_performer
                            LEFT JOIN albums a ON r.id_album = a.id_album
                            WHERE p.name LIKE @searchText";
        }
        else if (searchBy == "album")
        {
            selectQuery = @"SELECT r.title, p.name as performer, r.year, r.id_rola, r.genre, a.name as album
                            FROM rolas r
                            JOIN performers p ON r.id_performer = p.id_performer
                            JOIN albums a ON r.id_album = a.id_album
                            WHERE a.name LIKE @searchText";
        }

        using (var selectCmd = new SqliteCommand(selectQuery, connection))
        {
            selectCmd.Parameters.AddWithValue("@searchText", "%" + searchText + "%"); // Agregar comodines para buscar coincidencias parciales

            using (var reader = selectCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Song song = new Song
                    {
                        Title = reader["title"].ToString(),
                        PerformerName = reader["performer"].ToString(),
                        Year = Convert.ToInt32(reader["year"]),
                        IdRola = Convert.ToInt32(reader["id_rola"]),
                        Genre = reader["genre"].ToString(), 
                        AlbumName = reader["album"]?.ToString()
                    };
                    canciones.Add(song);
                }
            }
        }
    
        return canciones;
    }

    public int AddPersonToGroup(string personName, string groupName)
    {
        // Abrir la conexión dentro de un bloque using
        using (var connection = this.connection)
        {
            connection.Open(); // Asegurarte de abrir la conexión antes de las operaciones

            try
            {
                // Verificar si el grupo existe
                string checkGroupQuery = @"SELECT COUNT(*) FROM groups WHERE name = @groupName";
                using (var checkGroupCmd = new SqliteCommand(checkGroupQuery, connection))
                {
                    checkGroupCmd.Parameters.AddWithValue("@groupName", groupName);
                    int groupExists = Convert.ToInt32(checkGroupCmd.ExecuteScalar());

                    if (groupExists == 0)
                    {
                        return 1; // El grupo no existe
                    }
                }

                // Verificar si la persona ya está en el grupo
                string checkPersonInGroupQuery = @"
                    SELECT COUNT(*) FROM in_group ig
                    JOIN persons p ON ig.id_person = p.id_person
                    JOIN groups g ON ig.id_group = g.id_group
                    WHERE p.stage_name = @personName AND g.name = @groupName";
            
                using (var checkPersonInGroupCmd = new SqliteCommand(checkPersonInGroupQuery, connection))
                {
                    checkPersonInGroupCmd.Parameters.AddWithValue("@personName", personName);
                    checkPersonInGroupCmd.Parameters.AddWithValue("@groupName", groupName);
                    int personInGroupExists = Convert.ToInt32(checkPersonInGroupCmd.ExecuteScalar());

                    if (personInGroupExists > 0)
                    {
                        return 4; // La persona ya está en el grupo
                    }
                }

                // Verificar si la persona existe
                string checkPersonQuery = @"SELECT id_person FROM persons WHERE stage_name = @personName";
                int personId = -1;
                using (var checkPersonCmd = new SqliteCommand(checkPersonQuery, connection))
                {
                    checkPersonCmd.Parameters.AddWithValue("@personName", personName);
                    var result = checkPersonCmd.ExecuteScalar();
                    if (result != null)
                    {
                        personId = Convert.ToInt32(result);
                    }
                }

                // Si la persona no existe, insertarla
                if (personId == -1)
                {
                    string insertPersonQuery = @"INSERT INTO persons (stage_name) VALUES (@personName); SELECT last_insert_rowid();";
                    using (var insertPersonCmd = new SqliteCommand(insertPersonQuery, connection))
                    {
                        insertPersonCmd.Parameters.AddWithValue("@personName", personName);
                        personId = Convert.ToInt32(insertPersonCmd.ExecuteScalar());
                    }
                }

                // Agregar la persona al grupo
                string insertPersonInGroupQuery = @"
                    INSERT INTO in_group (id_person, id_group)
                    SELECT @personId, id_group FROM groups WHERE name = @groupName";
                using (var insertPersonInGroupCmd = new SqliteCommand(insertPersonInGroupQuery, connection))
                {
                    insertPersonInGroupCmd.Parameters.AddWithValue("@personId", personId);
                    insertPersonInGroupCmd.Parameters.AddWithValue("@groupName", groupName);
                    insertPersonInGroupCmd.ExecuteNonQuery();
                }

                return 2; // Persona agregada al grupo con éxito
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return -1; // Error general
            }
        } // La conexión se cierra automáticamente aquí
    }

    public int DefinePerformer(string performerName, string defineOption)
    {
        // Abrir conexión a la base de datos
        using (var connection = this.connection)
        {
            connection.Open();
        
            // Verificar si el intérprete existe en la tabla performers
            string checkPerformerQuery = @"SELECT id_performer FROM performers WHERE name = @performerName";
            int performerId = -1;

            using (var checkPerformerCmd = new SqliteCommand(checkPerformerQuery, connection))
            {
                checkPerformerCmd.Parameters.AddWithValue("@performerName", performerName);
                var result = checkPerformerCmd.ExecuteScalar();

                if (result != null)
                {
                    performerId = Convert.ToInt32(result); // Almacena el id_performer
                }
                else
                {
                    return 3; // Error, intérprete no encontrado
                }
            }

            // Determinar el tipo de definición
            if (defineOption == "1") // Definir como persona
            {
                // Verificar si ya está asociado como persona
                string checkPersonQuery = @"SELECT COUNT(*) FROM persons WHERE stage_name = @performerName";
                using (var checkPersonCmd = new SqliteCommand(checkPersonQuery, connection))
                {
                    checkPersonCmd.Parameters.AddWithValue("@performerName", performerName);
                    int personExists = Convert.ToInt32(checkPersonCmd.ExecuteScalar());

                    if (personExists > 0)
                    {
                        return 1; // Error, ya está definido como persona
                    }
                }

                // Insertar en la tabla persons
                string insertPersonQuery = @"INSERT INTO persons (stage_name) VALUES (@performerName)";
                using (var insertPersonCmd = new SqliteCommand(insertPersonQuery, connection))
                {
                    insertPersonCmd.Parameters.AddWithValue("@performerName", performerName);
                    insertPersonCmd.ExecuteNonQuery();
                }

                return 2; // Éxito, se definió como persona
            }
            else if (defineOption == "2") // Definir como grupo
            {
                // Verificar si ya está asociado como grupo
                string checkGroupQuery = @"SELECT COUNT(*) FROM groups WHERE name = @performerName";
                using (var checkGroupCmd = new SqliteCommand(checkGroupQuery, connection))
                {
                    checkGroupCmd.Parameters.AddWithValue("@performerName", performerName);
                    int groupExists = Convert.ToInt32(checkGroupCmd.ExecuteScalar());

                    if (groupExists > 0)
                    {
                        return 1; // Error, ya está definido como grupo
                    }
                }

                // Insertar en la tabla groups
                string insertGroupQuery = @"INSERT INTO groups (name) VALUES (@performerName)";
                using (var insertGroupCmd = new SqliteCommand(insertGroupQuery, connection))
                {
                    insertGroupCmd.Parameters.AddWithValue("@performerName", performerName);
                    insertGroupCmd.ExecuteNonQuery();
                }

                return 2; // Éxito, se definió como grupo
            }

            return 3; // Error, opción no válida
        }
    }

    public List<Album> GetAlbums()
    {
        List<Album> albums = new List<Album>();

        using (var connection = this.connection)
        {
            connection.Open();

            string selectQuery = "SELECT id_album, name, year FROM albums";

            using (var selectCmd = new SqliteCommand(selectQuery, connection))
            {
                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Album album = new Album
                        {
                            Id = Convert.ToInt32(reader["id_album"]),
                            Name = reader["name"].ToString(),
                            Year = Convert.ToInt32(reader["year"])
                        };
                        albums.Add(album);
                    }
                }
            }
        }
    
        return albums;
    }

    /* Este método actualiza el nombre o el año de un álbum. Si el campo no es válido, devuelve un código de error. 
       Se ejecuta una consulta SQL para actualizar el álbum, y si no se encuentra, retorna otro código de error. */

    public int EditAlbum(int albumId, string newText, string fieldToUpdate)
    {
        if (fieldToUpdate != "name" && fieldToUpdate != "year")
        {
            return 3; // Error, campo no válido para actualizar
        }

        using (var connection = this.connection)
        {
            connection.Open();

            try
            {
                string updateQuery = "";

                // Verifica qué campo se actualizará
                if (fieldToUpdate == "name")
                {
                    updateQuery = @"UPDATE albums SET name = @newText WHERE id_album = @albumId";
                }
                else if (fieldToUpdate == "year")
                {
                    updateQuery = @"UPDATE albums SET year = @newText WHERE id_album = @albumId";
                }

                // Crear y ejecutar el comando
                using (var updateCmd = new SqliteCommand(updateQuery, connection))
                {
                    updateCmd.Parameters.AddWithValue("@newText", newText);
                    updateCmd.Parameters.AddWithValue("@albumId", albumId);

                    int rowsAffected = updateCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return 1; // Éxito, álbum actualizado
                    }
                    else
                    {
                        return 2; // Error, álbum no encontrado
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return -1; // Error general
            }
        }
    }

    /* Este método actualiza el campo especificado de una canción. Si se intenta cambiar el intérprete o álbum, se verifica su existencia 
    en las tablas `performers` o `albums` respectivamente. Si no existen, no se realiza ningún cambio y se retorna un código de error. */

    public int EditSong(int songId, string newValue, string fieldToUpdate)
    {
        using (var connection = this.connection)
        {
            connection.Open();

            string updateQuery = "";
            int performerId = -1;
            int albumId = -1;

            if (fieldToUpdate == "title")
            {
                updateQuery = "UPDATE rolas SET title = @newValue WHERE id_rola = @songId";
            }
            else if (fieldToUpdate == "year")
            {
                updateQuery = "UPDATE rolas SET year = @newValue WHERE id_rola = @songId";
            }
            else if (fieldToUpdate == "genre")
            {
                updateQuery = "UPDATE rolas SET genre = @newValue WHERE id_rola = @songId";
            }
            else if (fieldToUpdate == "performer")
            {
                // Obtener id_performer del nuevo intérprete
                string getPerformerIdQuery = "SELECT id_performer FROM performers WHERE name = @newValue";
                using (var getPerformerCmd = new SqliteCommand(getPerformerIdQuery, connection))
                {
                    getPerformerCmd.Parameters.AddWithValue("@newValue", newValue);
                    var result = getPerformerCmd.ExecuteScalar();
                    if (result != null)
                    {
                        performerId = Convert.ToInt32(result);
                    }
                    else
                    {
                        return 1; // El intérprete nuevo no existe
                    }
                }

                // Actualizar id_performer
                updateQuery = "UPDATE rolas SET id_performer = @performerId WHERE id_rola = @songId";
            }
            else if (fieldToUpdate == "album")
            {
                // Obtener id_album del nuevo álbum
                string getAlbumIdQuery = "SELECT id_album FROM albums WHERE name = @newValue";
                using (var getAlbumCmd = new SqliteCommand(getAlbumIdQuery, connection))
                {
                    getAlbumCmd.Parameters.AddWithValue("@newValue", newValue);
                    var result = getAlbumCmd.ExecuteScalar();
                    if (result != null)
                    {
                        albumId = Convert.ToInt32(result);
                    }
                    else
                    {
                        return 2; // El álbum no existe
                    }
                }

                // Actualizar id_album
                updateQuery = "UPDATE rolas SET id_album = @albumId WHERE id_rola = @songId";
            }

            if (!string.IsNullOrEmpty(updateQuery))
            {
                using (var updateCmd = new SqliteCommand(updateQuery, connection))
                {
                    updateCmd.Parameters.AddWithValue("@songId", songId);

                    if (fieldToUpdate == "performer")
                    {
                        updateCmd.Parameters.AddWithValue("@performerId", performerId);
                    }
                    else if (fieldToUpdate == "album")
                    {
                        updateCmd.Parameters.AddWithValue("@albumId", albumId);
                    }
                    else
                    {
                        updateCmd.Parameters.AddWithValue("@newValue", newValue);
                    }

                    int rowsAffected = updateCmd.ExecuteNonQuery();
                    return rowsAffected > 0 ? 0 : -1; // 0 si se actualizó, -1 si no
                }
            }

            return -1; // Error en la actualización
        }
    }

    public List<Performer> GetPerformers()
    {
        List<Performer> performers = new List<Performer>();

        using (var connection = this.connection)
        {
            connection.Open();

            string selectQuery = "SELECT id_performer, id_type, name FROM performers";

            using (var selectCmd = new SqliteCommand(selectQuery, connection))
            {
                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Performer performer = new Performer
                        {
                            Id = Convert.ToInt32(reader["id_performer"]),
                            TypeId = Convert.ToInt32(reader["id_type"]),
                            Name = reader["name"].ToString()
                        };
                        performers.Add(performer);
                    }
                }
            }
        }

        return performers;
    }








}
