using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class SecurityController
    {
        SecurityDAO securityDAO = new SecurityDAO();

        public SecurityController()
        {

        }

        public bool isValid(User user)
        {
            return securityDAO.findUser(user);
        }

    }
}
