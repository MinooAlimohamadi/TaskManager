using TaskManager.Logic.Infrastructure.Domain;
using Task = TaskManager.Logic.Domain.Task;

namespace TaskManager.Logic.Service.Tasks.Dto
{
    public class Task_dto
    {
        public Task_dto() { }
        public Task_dto(Task dbo)
        {
            if (dbo != null)
            {
                this.Id = dbo.Id;
                this.Title = dbo.Title;
                this.Priority = dbo.Priority;
                this.Status = dbo.Status;
            }
        }
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public Priorities.Priority Priority { get; set; }
        public Statuses.Status Status { get; set; }
    }
}
