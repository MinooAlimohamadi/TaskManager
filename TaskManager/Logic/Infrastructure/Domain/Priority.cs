namespace TaskManager.Logic.Infrastructure.Domain
{
    public class Priorities
    {
        public enum Priority : short
        {
            low = 1,
            Normal = 2,
            High = 3
        }

        public static string Title(Priority value)
        {
            try
            {
                return Enum.GetName(typeof(Priority), value)!; 
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
