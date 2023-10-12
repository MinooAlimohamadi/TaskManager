using TaskManager.Logic.Service.Tasks.Dto;

namespace TaskManager.Logic.Service.Tasks
{
    public interface ITaskService
    {
        public IQueryable<Task_dto> GET();
        public Task<Task_dtod> GET(int id);
        public Task<Task_dtod> POST(Task_dtod dto);
        public Task<Task_dtod> PUT(Task_dtod dto);
        public Task<Task_dtod> DELETE(int id);
    }
}
