using Parky.DTO;
using Parky.Entity;

namespace Parky.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        User Authenticate(string username, string password);
        User Register(UserLoginDTO model);
    }
}