using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IUserDataService
    {
        List<User> GetAllUsers();

        User GetUserById(int userId);

        int Insert(User user);
        int Update(User user);
        int Delete(User user);
    }
}
