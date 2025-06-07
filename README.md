# dotnet-Todo-MCP

API base extraida desde el proyecto [dotnet-interview](https://github.com/crunchloop/dotnet-interview) de Crunchloop.

API TodoList y servidor MCP para comunicar la api con clientes MCP (Testeado con Claude Desktop).

## Base de Datos

El proyecto viene con una base de datos SQL Server mediante devcontainters, pero lo mejor es usar una propia ya que abrir el proyecto con devcontainer complica la comunicacion con Claude Desktop. En el proyecto TodoApi se encuentra el archivo `appsettings.json` donde se puede configurar la cadena de conexión a la base de datos.

## Instalación

 - Clonar el respositorio
 - Migrar la base de datos con `dotnet ef database update`
    - Es posible que requiera restaurar las tools con `dotnet tool restore` para poder usar `dotnet ef`

## Iniciar Proyecto

Para iniciar el proyecto, asegúrate de tener instalado [.NET SDK](https://dotnet.microsoft.com/download) y luego desde la raiz del proyecto ejecuta:

`dotnet run --project TodoApi`
`dotnet run --project McpServer`

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
 - Guardar el archivo y reiniciar la Claude Desktop (asegurarse de tener el proyecto corriendo antes de reiniciarla).