namespace TaskManager.Logic.Infrastructure
{
    public abstract class BaseDomain
    {
        public DateTime CreateDate { get; set; }

        public bool IsDelete { get; set; }
    }
}
