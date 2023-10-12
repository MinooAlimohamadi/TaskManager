using Task = TaskManager.Logic.Domain.Task;
using TaskManager.Logic.Service.Tasks.Dto;

namespace TaskManager.Logic.Service.Tasks
{
    public class TaskService : ITaskService
    {
        private readonly CRUDService _crudService;

        public TaskService(CRUDService crudService)
        {
            this._crudService = crudService;
        }

        public IQueryable<Task_dto> GET()
        {
            var dbos = _crudService.GET<Task>().Select(a => new Task_dto(a));

            return dbos;
        }

        public async Task<Task_dtod> GET(int id)
        {
            var dbo = await _crudService.GET<Task>(id);

            if (dbo == null) { throw new Exception("Task Not Found"); }

            return new Task_dtod(dbo);
        }

        public async Task<Task_dtod> POST(Task_dtod dto)
        {
            if (string.IsNullOrEmpty(dto.Title) || string.IsNullOrEmpty(dto.Description))
            {
                throw new Exception("Input values ​​are not allowed for Task entity");
            }

            var item = new Task
            {
                Title = dto.Title,
                Description = dto.Description,
                Priority = dto.Priority,
                Status = dto.Status,
                DueDate = dto.DueDate
            };

            var result = await _crudService.POST(item);

            if (result == null) { throw new Exception("bad request"); }

            return new Task_dtod(result);
        }

        public async Task<Task_dtod> PUT(Task_dtod dto)
        {
            if (string.IsNullOrEmpty(dto.Title) || string.IsNullOrEmpty(dto.Description))
            {
                throw new Exception("Input values ​​are not allowed for Task entity");
            }

            var item = await _crudService.GET<Task>(dto.Id);

            if (item == null) { throw new Exception("Task Not Found"); }

            item.Title = dto.Title;
            item.Description = dto.Description;
            item.Priority = dto.Priority;
            item.Status = dto.Status;
            item.DueDate = dto.DueDate;

            var result = await _crudService.PUT(item);

            if (result == null) { throw new Exception("bad request"); }

            return new Task_dtod(result);
        }

        public async Task<Task_dtod> DELETE(int id)
        {
            var item = await _crudService.GET<Task>(id);

            if (item == null) { throw new Exception("Task Not Found"); }

            var dbo = await _crudService.DELETE<Task>(id);

            if (dbo == null) { throw new Exception("bad request"); }

            return new Task_dtod(dbo); ;
        }
    }
}
