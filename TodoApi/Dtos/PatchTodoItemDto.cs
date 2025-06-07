using System;

namespace TodoApi.Dtos;

public class PatchTodoItemDto
{
    public string? Description { get; set; }
    public bool? Finished { get; set; }
    public long? TodoListId { get; set; }
}
