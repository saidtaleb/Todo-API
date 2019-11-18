namespace TodoApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TodoApi.Models.TodoModel;
    using TodoApi.Services.Todo;

    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly TodoService _service;

        public TodosController(TodoService service)
        {
            _service = service;
        }

        /// <summary>
        /// get list of TODO
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<TodoModel>>> Get() => await _service.Get();

        /// <summary>
        /// get todo by id
        /// </summary>
        /// <param name="id">the id TODO</param>
        /// <returns>a instance TODO</returns>
        [HttpGet("{id:length(24)}", Name = "GetTodo")]
        public async Task<ActionResult<TodoModel>> Get(string id)
        {
            var todo = await _service.Get(id);

            if (todo == null)
                return NotFound();

            return todo;
        }

        /// <summary>
        /// create TODO
        /// </summary>
        /// <param name="todo">TODO create model</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<TodoModel>> Create(TodoCreateModel todo)
        {
            try
            {
                var todoResult = await _service.Create(todo);
                return CreatedAtRoute("GetTodo", new { id = todoResult.Id.ToString() }, todo);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// update TODO
        /// </summary>
        /// <param name="id">the id of TODO</param>
        /// <param name="todoIn">the TODO update model</param>
        /// <returns>a instance TODO</returns>
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, TodoUpdateModel todoIn)
        {
            var todo = await _service.Get(id);

            if (todo == null)
                return NotFound();

            _service.Update(id, todoIn);

            return Ok(todoIn);
        }

        /// <summary>
        /// delete todo by id
        /// </summary>
        /// <param name="id">the id TODO</param>
        /// <returns>a instance TODO</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _service.Get(id);

            if (book == null)
                return NotFound();

            await _service.Remove(book.Id);

            return Ok();
        }
    }
}
