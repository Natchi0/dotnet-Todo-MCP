using System;
using System.ComponentModel;
using System.Net.Http.Json;
using McpServer.Dtos;
using ModelContextProtocol.Server;

namespace McpServer;

[McpServerToolType]
public class TodoListTools
{
    /*
    Funciones para interactuar con los TodoLists de la api
    necesario para interacutar correctamente con los todoItems y facilitar el testeo
    */

    private readonly HttpClient _httpClient;

    public TodoListTools(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("TodoApi");
    }

    [McpServerTool, Description("Get all todo lists")]
    public async Task<IList<TodoListDto>> GetTodoLists()
    {
        var response = await _httpClient.GetAsync("api/todolists");
        response.EnsureSuccessStatusCode();

        var lists = await response.Content.ReadFromJsonAsync<List<TodoListDto>>();

        return lists ?? new List<TodoListDto>();
    }

    [McpServerTool, Description("Create a new todo list")]
    public async Task<TodoListDto?> CreateTodoList([Description("Name of the todo list")] string name)
    {
        var newTodoList = new CreateTodoList
        {
            Name = name
        };

        var response = await _httpClient.PostAsJsonAsync("api/todolists", newTodoList);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<TodoListDto>();
        }
        else return null;
    }

    [McpServerTool, Description("Delete a todo list by id")]
    public async Task<bool> DeleteTodoList([Description("Id of the todo list to delete")] long id)
    {
        var response = await _httpClient.DeleteAsync($"api/todolists/{id}");

        return response.IsSuccessStatusCode;
    }

}
