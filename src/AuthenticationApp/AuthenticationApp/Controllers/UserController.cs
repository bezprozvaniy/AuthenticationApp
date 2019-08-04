using System.Collections.Generic;
using AuthenticationApp.Core;
using AuthenticationApp.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationApp.Controllers
{
    [Route("api/User")]
    [Authorize]
    public class UserController : Controller
    {
        // GET api/User
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<User> GetUsers()
        {
            return UserManager.GetUsers();
        }

        // GET api/User/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public User GetUser(long id)
        {
            return UserManager.GetUser(id);
        }

        // POST api/User
        [HttpPost]
        public void AddUser([FromBody]User user)
        {
            UserManager.AddUser(user);
        }

        // PUT api/User/5
        [HttpPut("{id}")]
        public void EditUser(long id, [FromBody]User user)
        {
            UserManager.EditUser(id, user);
        }

        // DELETE api/User/5
        [HttpDelete("{id}")]
        public void DeleteUser(long id)
        {
            UserManager.DeleteUser(id);
        }
    }
}