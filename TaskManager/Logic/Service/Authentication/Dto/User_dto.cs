using TaskManager.Logic.Domain;

namespace TaskManager.Logic.Service.Authentication.Dto
{
    public class User_dto
    {
        public User_dto() { }
        public User_dto(User dbo)
        {
            if (dbo != null)
            {
                this.Id = dbo.Id;
                this.FirstName = dbo.FirstName;
                this.LastName = dbo.LastName;
                this.UserName = dbo.UserName;
            }
        }
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
    }
}
