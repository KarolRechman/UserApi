using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using UserApi.IServices;
using UserApi.Sevices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<IdentityUser> userManager { get; }
        private SignInManager<IdentityUser> signInManager { get; }
        private IUserService userService { get; }

        public UserController(UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager, IUserService _userService)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            userService = _userService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<dynamic> Get()
        {
            using (var connection = new MySqlConnection("server=remotemysql.com;port=3306;database=vNB3Rw2pVo;user=vNB3Rw2pVo;password=TEXnbd23EV;"))
            {
                return connection.Query<dynamic>("SELECT * FROM new_table;").ToArray();
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<string> Post([FromBody] IdentityUser user, string password)
        {
           return await userService.AddUser(user, password);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
