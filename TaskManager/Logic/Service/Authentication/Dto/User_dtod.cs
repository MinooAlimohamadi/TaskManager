using TaskManager.Logic.Domain;

namespace TaskManager.Logic.Service.Authentication.Dto
{
    public class User_dtod : User_dto
    {
        public User_dtod() { }
        public User_dtod(User dbo) : base(dbo)
        {
            if (dbo != null)
            {
                this.Email = dbo.Email;
                this.Password = dbo.Password;
            }
        }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
