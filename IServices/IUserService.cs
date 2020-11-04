using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserApi.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<IdentityUser>> GetUsers();
        IdentityUser GetUserById(string id);
        Task<string> AddUser(IdentityUser user, string password);
        IdentityUser UpdateUser(IdentityUser user);
        Task<string> DeleteUser(string id);
    }
}
