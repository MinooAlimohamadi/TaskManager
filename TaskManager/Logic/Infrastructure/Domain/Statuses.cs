namespace TaskManager.Logic.Infrastructure.Domain
{
    public class Statuses
    {
        public enum Status : short
        {
            completed = 1,
            incomplete = 2,
            overdue = 3
        }

        public static string Title(Status value)
        {
            try
            {
                return Enum.GetName(typeof(Status), value)!;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
