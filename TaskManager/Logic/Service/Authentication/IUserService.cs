using TaskManager.Logic.Service.Authentication.Dto;

namespace TaskManager.Logic.Service.Authentication
{
    public interface IUserService
    {
        public IQueryable<User_dto> GET();
        public Task<User_dtod> GET(int id);
        public Task<User_dtod> POST(User_dtod dto);
        public Task<User_dtod> PUT(User_dtod dto);
        public Task<User_dtod> DELETE(int id);

        public Task<User_dtod> Login(Login_dto dto);
    }
}
