using System;

namespace TodoApi.Models;

public class TodoItem
{
    public long Id { get; set; }

    public required string Description { get; set; }

    public bool Finished { get; set; } = false;

    public long TodoListId { get; set; }

    public required TodoList TodoList { get; set; }
}
