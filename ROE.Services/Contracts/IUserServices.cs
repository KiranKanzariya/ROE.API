using ROE.DataAccess.Entities;
using ROE.DataAccess.DTO;

namespace ROE.Services.Contracts
{
    public interface IUserServices
    {
        User AuthenticateUser(string userName, string password, out string returnCode);

        List<User> FetchAllUsers(int customerId);

        UserDTOModel GetUserByUserName(string userName);
    }
}
