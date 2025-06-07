using System;

namespace TodoApi.Dtos;

public class UpdateTodoItem
{
    public required string Description { get; set; }

    public required long TodoListId { get; set; }

    public required bool Finished { get; set; }
}
