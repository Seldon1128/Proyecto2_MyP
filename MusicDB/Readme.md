# MusicDB

## Descripción

**MusicDB** es una aplicación diseñada para gestionar una base de datos musical, permitiendo a los usuarios extraer etiquetas de música de un directorio, consultar información sobre canciones, y editar detalles de álbumes, canciones y performers. Este proyecto está implementado en C# utilizando Gtk para la interfaz gráfica de usuario (GUI).

## Funcionalidades

La aplicación ofrece las siguientes opciones en la pantalla de inicio:

- **Extraer Música**: Permite al usuario explorar y extraer archivos de música desde un directorio especificado.
- **Directorio**: Muestra una lista de las canciones disponibles en la base de datos.
- **Búsqueda**: Permite realizar consultas sobre títulos de canciones, intérpretes o álbumes.
- **Editar Información**: Facilita la edición de la información de las canciones y álbumes. Esto incluye:
  - Definir a un intérprete como persona o grupo.
  - Agregar personas a grupos.

## Estructura del Proyecto

El proyecto consta de dos commits principales:

1. **Último Commit**: Contiene únicamente la GUI de la aplicación.
2. **Penúltimo Commit**: Incluye la lógica del programa sin la opción de GUI, permitiendo probar los métodos de la clase `MusicDao`.

## Clase MusicDao

La clase `MusicDao` es el núcleo de la aplicación, donde se implementan todos los métodos necesarios para interactuar con la base de datos. Los métodos están diseñados para:

- Extraer información de archivos de música.
- Agregar, editar y eliminar álbumes y canciones.
- Consultar detalles sobre artistas y grupos.
- Definir la relación entre artistas y su música.

### Métodos Destacados

Algunos de los métodos más relevantes incluyen:

- **Extraer Música**: Método para escanear un directorio y almacenar archivos de música en la base de datos.
- **Consultar Música**: Permite buscar por título, intérprete o álbum.
- **Actualizar Información**: Facilita la edición de detalles de canciones y álbumes.
- **Definir Intérpretes**: Método que permite clasificar a los intérpretes como individuos o grupos.

## Como correr

- Dotnet build
- Dotnet Run
