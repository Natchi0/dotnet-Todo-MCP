using System;
using System.ComponentModel;
using System.Net.Http.Json;
using McpServer.Dtos;
using ModelContextProtocol.Server;

namespace McpServer;

[McpServerToolType]
public class TodoTools
{
    private readonly HttpClient _httpClient;

    public TodoTools(IHttpClientFactory httpClientFactory)
    {
        //cliente preconfigurado en el program
        _httpClient = httpClientFactory.CreateClient("TodoApi");
    }

    [McpServerTool, Description("Get all todo items")]
    public async Task<IList<TodoItemDto>> GetTodoItems()
    {
        var response = await _httpClient.GetAsync("api/todoItems");
        response.EnsureSuccessStatusCode();

        var items = await response.Content.ReadFromJsonAsync<List<TodoItemDto>>();

        return items ?? new List<TodoItemDto>();
    }

    [McpServerTool, Description("Get a todo item by id")]
    public async Task<TodoItemDto?> GetTodoItemById([Description("Id of the item to get")] long id)
    {
        var response = await _httpClient.GetAsync($"api/todoItems/{id}");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<TodoItemDto>();
        }
        else return null;
    }

    [McpServerTool, Description("Set a todo item as finished")]
    public async Task<bool> FinishTodoItem([Description("Id of the item to set as finished")] long id)
    {
        var response = await _httpClient.PatchAsync($"api/todoItems/{id}/finish", null);

        return response.IsSuccessStatusCode;
    }

    [McpServerTool, Description("Create a new todo item")]
    public async Task<TodoItemDto?> CreateTodoItem(
        [Description("description of the todo item")] string description,
        [Description("Id of the list asigned to the todo item")] long todoListId)
    {
        var newTodo = new CreateTodoItem
        {
            Description = description,
            TodoListId = todoListId
        };

        var response = await _httpClient.PostAsJsonAsync("api/todoItems", newTodo);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<TodoItemDto>();
        }
        else return null;
    }

    [McpServerTool, Description("Delete a todo item")]
    public async Task<bool> DeleteTodoItem([Description("Id of the item to delete")] long id)
    {
        var response = await _httpClient.DeleteAsync($"api/todoItems/{id}");
        return response.IsSuccessStatusCode;
    }

    [McpServerTool, Description("Fully update a todo item")]
    public async Task<TodoItemDto?> UpdateTodoItem(
        [Description("Id of the item to update")] long id,
        [Description("New description of the todo item")] string description,
        [Description("New finished state of the todo item")] bool finished,
        [Description("New id of the list asigned to the todo item")] long todoListId)
    {
        var updateTodo = new UpdateTodoItem
        {
            Description = description,
            Finished = finished,
            TodoListId = todoListId
        };

        var response = await _httpClient.PutAsJsonAsync($"api/todoItems/{id}", updateTodo);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<TodoItemDto>();
        }
        else return null;
    }

    [McpServerTool, Description("Partialy update a todo item")]
    public async Task<TodoItemDto?> PatchTodoItem(
        [Description("Id of the item to update")] long id,
        [Description("New description of the todo item")] string? description = null,
        [Description("New finished state of the todo item")] bool? finished = null,
        [Description("New id of the list asigned to the todo item")] long? todoListId = null)
    {
        var patchTodo = new PatchTodoItem
        {
            Description = description,
            Finished = finished,
            TodoListId = todoListId
        };

        var response = await _httpClient.PatchAsJsonAsync($"api/todoItems/{id}", patchTodo);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<TodoItemDto>();
        }
        else return null;
    }
}
