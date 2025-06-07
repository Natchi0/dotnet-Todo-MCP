using System;

namespace McpServer.Dtos;

public class PatchTodoItem
{
    public string? Description { get; set; }
    public bool? Finished { get; set; }
    public long? TodoListId { get; set; }
}
