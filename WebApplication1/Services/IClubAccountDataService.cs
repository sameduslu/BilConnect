using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IClubAccountDataService
    {
        List<ClubAccount> GetAllClubAccounts();

        ClubAccount GetClubAccountById(int userId);

        int Insert(ClubAccount clubAccount);
        int Update(ClubAccount clubAccount);
        int Delete(ClubAccount clubAccount);
    }
}
