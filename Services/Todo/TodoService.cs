namespace TodoApi.Services.Todo
{
    using AutoMapper;
    using MongoDB.Driver;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TodoApi.Entities;
    using TodoApi.Models;
    using TodoApi.Models.TodoModel;

    public class TodoService : ITodoService
    {
        private readonly IMongoCollection<Todo> _todo;
        private readonly IMapper _mapper;

        public TodoService(
            ITodoDatabaseSettings settings,
            IMapper mapper
        )
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _mapper = mapper;
            _todo = database.GetCollection<Todo>(settings.TodosCollectionName);
        }

        /// <summary>
        /// get list of TODOs
        /// </summary>
        /// <returns>a list of TODOs</returns>
        public async Task<List<TodoModel>> Get()
        {

            // select list of TODOs
            var todos = await _todo.AsQueryable().ToListAsync();

            // mapping TODO list to TODO model list
            return _mapper.Map<List<Todo>, List<TodoModel>>(todos);
        }

        public async Task<TodoModel> Get(string id)
        {
            var todos = await _todo.Find(todo => todo.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<Todo, TodoModel>(todos);
        }

        /// <summary>
        /// create TODO
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
        public async Task<TodoModel> Create(TodoCreateModel todoModel)
        {
            var todo = _mapper.Map<Todo>(todoModel);
            await _todo.InsertOneAsync(todo);
            return _mapper.Map<Todo, TodoModel>(todo);
        }

        /// <summary>
        /// update TODO
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todoIn"></param>
        public void Update(string id, TodoUpdateModel todoIn)
        {
            var todo = _mapper.Map<Todo>(todoIn);
            _todo.ReplaceOne(t => t.Id == id, todo);
        }

        /// <summary>
        /// remove TODO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Remove(string id) => await _todo.DeleteOneAsync(todo => todo.Id == id);

        /// <summary>
        /// check is title unique
        /// </summary>
        /// <param name="title">the title we want to check</param>
        /// <returns>a boolean</returns>
        public async Task<bool> IsUniqueTitle(string title)
        {
            var countNumberTodoWithSameTitle = await _todo.CountDocumentsAsync(todo => todo.Title.Equals(title));
            return countNumberTodoWithSameTitle > 0;
        }
    }
}
