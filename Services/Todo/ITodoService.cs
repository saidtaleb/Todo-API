namespace TodoApi.Services.Todo
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TodoApi.Models.TodoModel;

    /// <summary>
    /// 
    /// </summary>
    public interface ITodoService
    {
        Task<List<TodoModel>> Get();
        Task<TodoModel> Get(string id);
        Task<TodoModel> Create(TodoCreateModel todo);
        void Update(string id, TodoUpdateModel todoIn);
        Task Remove(string id);
        Task<bool> IsUniqueTitle(string title);
    }
}
