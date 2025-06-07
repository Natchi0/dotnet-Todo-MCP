using System;

namespace TodoApi.Dtos;

public class CreateTodoItem
{
    public required string Description { get; set; }

    public required long TodoListId { get; set; }
}
