using Microsoft.EntityFrameworkCore;
using TaskManager.Logic.Domain;
using TaskManager.Logic.Service.Authentication.Dto;

namespace TaskManager.Logic.Service.Authentication
{
    public class UserService : IUserService
    {
        private readonly CRUDService _crudService;

        public UserService(CRUDService crudService)
        {
            this._crudService = crudService;
        }

        #region Methods
        public IQueryable<User_dto> GET()
        {
            var dbos = _crudService.GET<User>().Select(a => new User_dto(a));

            return dbos;
        }

        public async Task<User_dtod> GET(int id)
        {
            var dbo = await _crudService.GET<User>(id);

            if (dbo == null) { throw new Exception("User Not Found"); }

            return new User_dtod(dbo);
        }

        public async Task<User_dtod> POST(User_dtod dto)
        {
            if (string.IsNullOrEmpty(dto.FirstName) || string.IsNullOrEmpty(dto.LastName) || string.IsNullOrEmpty(dto.UserName) ||
                string.IsNullOrEmpty(dto.Password) || string.IsNullOrEmpty(dto.Email))
            {
                throw new Exception("Input values ​​are not allowed for user entity");
            }

            var item = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                UserName = dto.UserName,
                Password = dto.Password,
                Email = dto.Email
            };

            var result = await _crudService.POST(item);

            if (result == null) { throw new Exception("bad request"); }

            return new User_dtod(result);
        }

        public async Task<User_dtod> PUT(User_dtod dto)
        {
            if (string.IsNullOrEmpty(dto.FirstName) || string.IsNullOrEmpty(dto.LastName) || string.IsNullOrEmpty(dto.UserName) ||
               string.IsNullOrEmpty(dto.Password) || string.IsNullOrEmpty(dto.Email))
            {
                throw new Exception("Input values ​​are not allowed for user entity");
            }

            var item = await _crudService.GET<User>(dto.Id);

            if (item == null) { throw new Exception("User Not Found"); }

            item.FirstName = dto.FirstName;
            item.LastName = dto.LastName;
            item.UserName = dto.UserName;
            item.Password = dto.Password;
            item.Email = dto.Email;

            var result = await _crudService.PUT(item);

            if (result == null) { throw new Exception("bad request"); }

            return new User_dtod(result);
        }

        public async Task<User_dtod> DELETE(int id)
        {
            var item = await _crudService.GET<User>(id);

            if (item == null) { throw new Exception("User Not Found"); }

            var dbo = await _crudService.DELETE<User>(id);

            if (dbo == null) { throw new Exception("bad request"); }

            return new User_dtod(dbo);
        }

        public async Task<User_dtod> Login(Login_dto dto)
        {
            if (string.IsNullOrEmpty(dto.UserName) || string.IsNullOrEmpty(dto.Password))
            {
                throw new Exception("Input values ​​are not allowed for user entity");
            }

            var user = await _crudService.GET<User>().FirstOrDefaultAsync(a => a.UserName == dto.UserName && a.Password == dto.Password);

            return new User_dtod(user!);
        }

        #endregion
    }
}
