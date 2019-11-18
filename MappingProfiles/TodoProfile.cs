namespace TodoApi.MappingProfiles
{
    using AutoMapper;
    using TodoApi.Entities;
    using TodoApi.Models.TodoModel;

    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<Todo, TodoModel>().ReverseMap();
            CreateMap<Todo, TodoCreateModel>().ReverseMap();
            CreateMap<Todo, TodoUpdateModel>().ReverseMap();
        }
    }
}
