using System;
using Microsoft.Data.Sqlite;

public class DatabaseManager
{
    private string connectionString;
    private SqliteConnection connection;

    //Metodo para crear las tablas de la base de datos y agregar las tablas del esquema
    public DatabaseManager(string dbFilePath)
    {
        connectionString = $"Data Source={dbFilePath};";
        connection = new SqliteConnection(connectionString);
        connection.Open();

        // Crear las tablas si no existen
        CreateTables();
    }

    private void CreateTables()
    {
        string createTablesQuery = @"
            CREATE TABLE IF NOT EXISTS types (
                id_type INTEGER PRIMARY KEY,
                description TEXT
            );

            CREATE TABLE IF NOT EXISTS performers (
                id_performer INTEGER PRIMARY KEY AUTOINCREMENT,
                id_type INTEGER,
                name TEXT,
                FOREIGN KEY (id_type) REFERENCES types(id_type)
            );

            CREATE TABLE IF NOT EXISTS persons (
                id_person INTEGER PRIMARY KEY AUTOINCREMENT,
                stage_name TEXT,
                real_name TEXT,
                birth_date TEXT,
                death_date TEXT
            );

            CREATE TABLE IF NOT EXISTS groups (
                id_group INTEGER PRIMARY KEY AUTOINCREMENT,
                name TEXT,
                start_date TEXT,
                end_date TEXT
            );

            CREATE TABLE IF NOT EXISTS in_group (
                id_person INTEGER,
                id_group INTEGER,
                PRIMARY KEY (id_person, id_group),
                FOREIGN KEY (id_person) REFERENCES persons(id_person),
                FOREIGN KEY (id_group) REFERENCES groups(id_group)
            );

            CREATE TABLE IF NOT EXISTS albums (
                id_album INTEGER PRIMARY KEY AUTOINCREMENT,
                path TEXT,
                name TEXT,
                year INTEGER
            );

            CREATE TABLE IF NOT EXISTS rolas (
                id_rola INTEGER PRIMARY KEY AUTOINCREMENT,
                id_performer INTEGER,
                id_album INTEGER,
                path TEXT,
                title TEXT,
                track INTEGER,
                year INTEGER,
                genre TEXT,
                FOREIGN KEY (id_performer) REFERENCES performers(id_performer),
                FOREIGN KEY (id_album) REFERENCES albums(id_album)
            );
        ";

        using (var command = new SqliteCommand(createTablesQuery, connection))
        {
            command.ExecuteNonQuery();
        }

        // Insertar los tipos si no existen
        string insertTypesQuery = @"
            INSERT OR IGNORE INTO types (id_type, description) VALUES
            (0, 'Person'),
            (1, 'Group'),
            (2, 'Unknown');
        ";

        using (var command = new SqliteCommand(insertTypesQuery, connection))
        {
            command.ExecuteNonQuery();
        }
    }

    public SqliteConnection GetConnection()
    {
        return connection;
    }

    public void CloseConnection()
    {
        connection.Close();
    }
}
