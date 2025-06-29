using ROE.DataAccess.Entities;
using ROE.DataAccess.DTO;
using ROE.DataAccess.Repository;
using ROE.Services.Contracts;

namespace ROE.Services.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserRepository _userRepository;
        public UserServices(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User AuthenticateUser(string userName, string password, out string returnCode)
        {
            returnCode = string.Empty;
            User user = _userRepository.AuthenticateUser(userName, password, out returnCode);
            return user;
        }

        public List<User> FetchAllUsers(int customerId)
        {
            List<User> users = _userRepository.FetchAllUsers(customerId);
            return users;
        }

        public UserDTOModel GetUserByUserName(string userName)
        {
            UserDTOModel user = _userRepository.GetUserByUserName(userName);
            return user;
        }
    }
}
