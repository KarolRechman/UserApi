using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Data;
using UserApi.IServices;

namespace UserApi.Sevices
{
    public class UserService : IUserService
    {
        ApplicationDbContext dbContext;
        private UserManager<IdentityUser> userManager { get; }

        public UserService(ApplicationDbContext _db, UserManager<IdentityUser> _userManager)
        {
            dbContext = _db;
            userManager = _userManager;
        }

        public async Task<IEnumerable<IdentityUser>> GetUsers()
        {
            return await dbContext.Users.ToListAsync();
        }

        public async Task<string> AddUser(IdentityUser user, string password)
        {
            return await Register(user, password);

            if (user != null)
            {
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
                return await Task.FromResult("");
            }
            return null;
        }

        public IdentityUser UpdateUser(IdentityUser user)
        {
            dbContext.Entry(user).State = EntityState.Modified;
            dbContext.SaveChanges();
            return user;
        }

        public async Task<string> DeleteUser(string id)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Id == id);
            dbContext.Entry(user).State = EntityState.Deleted;
            dbContext.SaveChanges();
            return await Task.FromResult("");
        }

        public IdentityUser GetUserById(string id)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Id == id);
            return user;
        }


        private async Task<string> Register(IdentityUser user, string password)
        {
            string massage = "";
            try
            {
                var checkUserExistance = await userManager.FindByNameAsync(user.UserName);
                if (checkUserExistance == null)
                {
                    //user = new IdentityUser();
                    //user.UserName = "TestUser";
                    //user.Email = "TestUser@test.com";

                    var result = await userManager.CreateAsync(user, password);

                    if(result.Succeeded)
                        massage = "OK";
                }
                else
                {
                    massage = "User not available";
                }

            }
            catch (Exception ex)
            {
                massage = ex.Message;
            }

            return await Task.FromResult(massage);
        }
    }
}



