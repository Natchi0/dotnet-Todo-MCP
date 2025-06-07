using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TodoApi.Dtos;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        [HttpPatch("{id}/finish")]
        public async Task<ActionResult> FinishTodoItem(long id)
        {
            var todoItem = await _context.TodoItems
            .Include(t => t.TodoList)
            .FirstOrDefaultAsync(t => t.Id == id);

            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Finished = true;
            await _context.SaveChangesAsync();

            var todoDto = new TodoItemDto
            {
                Id = todoItem.Id,
                Description = todoItem.Description,
                Finished = todoItem.Finished,
                TodoListId = todoItem.TodoListId,
                TodoListName = todoItem.TodoList.Name
            };

            return Ok(todoDto);
        }

        [HttpGet]
        public async Task<ActionResult<IList<TodoItemDto>>> GetTodoItems()
        {
            var items = await _context.TodoItems
            .Include(item => item.TodoList)
            .Select(item => new TodoItemDto
            {
                Id = item.Id,
                Description = item.Description,
                Finished = item.Finished,
                TodoListId = item.TodoListId,
                TodoListName = item.TodoList.Name
            }).ToListAsync();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems
                .Include(t => t.TodoList)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (todoItem == null)
            {
                return NotFound();
            }

            var todoDto = new TodoItemDto
            {
                Id = todoItem.Id,
                Description = todoItem.Description,
                Finished = todoItem.Finished,
                TodoListId = todoItem.TodoListId,
                TodoListName = todoItem.TodoList.Name
            };

            return todoDto;
        }


        [HttpPost]
        public async Task<ActionResult<TodoItemDto>> PostTodoItem(CreateTodoItem request)
        {
            //validar existencia de la lista
            var todoList = await _context.TodoList.FindAsync(request.TodoListId);
            if (todoList == null)
            {
                return BadRequest("TodoList no existe");
            }

            var todoItem = new TodoItem
            {
                Description = request.Description,
                TodoList = todoList
            };

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            var todoDto = new TodoItemDto
            {
                Id = todoItem.Id,
                Description = todoItem.Description,
                Finished = todoItem.Finished,
                TodoListId = todoList.Id,
                TodoListName = todoList.Name
            };

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoDto.Id }, todoDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutTodoItem(long id, UpdateTodoItem request)
        {
            var todo = await _context.TodoItems.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            var todoList = await _context.TodoList.FindAsync(request.TodoListId);
            if (todoList == null)
            {
                return BadRequest("TodoList no existe");
            }

            todo.Description = request.Description;
            todo.Finished = request.Finished;
            todo.TodoListId = request.TodoListId;

            await _context.SaveChangesAsync();

            var todoDto = new TodoItemDto
            {
                Id = todo.Id,
                Description = todo.Description,
                Finished = todo.Finished,
                TodoListId = todo.TodoListId,
                TodoListName = todo.TodoList.Name
            };

            return Ok(todoDto);
        }

        //patch para actualizaciones parciales (actualizar la descripcion por ejemplo)
        [HttpPatch("{id}")]
        public async Task<ActionResult<TodoItemDto>> PatchTodoItem(long id, [FromBody] PatchTodoItemDto patchDto)
        {
            var todoItem = await _context.TodoItems
            .Include(item => item.TodoList)
            .FirstOrDefaultAsync(item => item.Id == id);

            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Description = patchDto.Description ?? todoItem.Description;
            todoItem.Finished = patchDto.Finished ?? todoItem.Finished;

            if (patchDto.TodoListId.HasValue)
            {
                var todoList = await _context.TodoList.FindAsync(patchDto.TodoListId.Value);
                if (todoList == null)
                {
                    return BadRequest("TodoList no existe");
                }

                todoItem.TodoList = todoList;
                todoItem.TodoListId = todoList.Id;
            }

            await _context.SaveChangesAsync();

            var todoDto = new TodoItemDto
            {
                Id = todoItem.Id,
                Description = todoItem.Description,
                Finished = todoItem.Finished,
                TodoListId = todoItem.TodoListId,
                TodoListName = todoItem.TodoList.Name
            };

            return Ok(todoDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
