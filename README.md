# dotnet-Todo-MCP

API base extraida desde el proyecto [dotnet-interview](https://github.com/crunchloop/dotnet-interview) de Crunchloop.

API TodoList y servidor MCP para comunicar la api con clientes MCP (Testeado con Claude Desktop).

## Base de Datos

El proyecto viene con una base de datos SQL Server mediante devcontainters, pero lo mejor es usar una propia ya que abrir el proyecto con devcontainer complica la comunicacion con Claude Desktop. En el proyecto TodoApi se encuentra el archivo `appsettings.json` donde se puede configurar la cadena de conexión a la base de datos.

## Instalación

 - Clonar el respositorio
 - Migrar la base de datos con `dotnet ef database update`
    - Es posible que requiera restaurar las tools con `dotnet tool restore` para poder usar `dotnet ef`
    - Es importante que la consola de comandos se encuentre en la carpeta `TodoApi` para hacer la migración.

## Iniciar Proyecto

Para iniciar el proyecto, asegurarse de tener instalado [.NET SDK](https://dotnet.microsoft.com/download) y luego desde la raiz del proyecto ejecutar:

`dotnet run --project TodoApi` y 
`dotnet run --project McpServer` en consolas separadas.

## Conectar con Claude Desktop

El proyecto deberia funcionar para cualquier cliente MCP pero ha sido testeado con Claude Desktop.
 
 - Buscar el archivo `claude_desktop_config`
    - Desde la app, ir a `Configuración` > `Desarrollador` > `Editar configuración`
 - Agregar el servidor, se debería ver así:
    ```
    {
        "mcpServers":{
            "TodoApi":{
                "type": "stdio",
                "command": "dotnet",
                "args":[
                    "run",
                    "--project",
                    "C:\\Ruta\\Absoluta\\Del\\Proyecto\\McpServer.csproj",
                    "--no-build"
                ]
            }
        }
    }
    ```
 - Guardar el archivo y reiniciar Claude Desktop (asegurarse de tener el proyecto corriendo antes de reiniciar).