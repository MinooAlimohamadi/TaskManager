using Task = TaskManager.Logic.Domain.Task;

namespace TaskManager.Logic.Service.Tasks.Dto
{
    public class Task_dtod : Task_dto
    {
        public Task_dtod() { }

        public Task_dtod(Task dbo) : base(dbo)
        {
            if (dbo != null)
            {
                this.Description = dbo.Description;
                this.DueDate = dbo.DueDate;
                this.CreateDate = dbo.CreateDate;
            }
        }
        public string Description { get; set; } = null!;
        public DateTime DueDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
