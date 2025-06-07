using System;

namespace McpServer.Dtos;

public class TodoItemDto
{
    public long Id { get; set; }

    public string Description { get; set; } = "";

    public bool Finished { get; set; } = false;

    public long TodoListId { get; set; }

    public string TodoListName { get; set; } = "";
}
