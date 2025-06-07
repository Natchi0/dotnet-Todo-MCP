using System;

namespace McpServer.Dtos;

public class TodoListDto
{
    public long Id { get; set; }
    public required string Name { get; set; }
}
