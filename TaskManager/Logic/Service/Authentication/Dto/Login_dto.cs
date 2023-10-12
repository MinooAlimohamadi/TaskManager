using TaskManager.Logic.Domain;

namespace TaskManager.Logic.Service.Authentication.Dto
{
    public class Login_dto
    {
        public Login_dto() { }
        public Login_dto(User dbo)
        {
            if (dbo != null)
            {
                this.UserName = dbo.UserName;
                this.Password = dbo.Password;
            }
        }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
