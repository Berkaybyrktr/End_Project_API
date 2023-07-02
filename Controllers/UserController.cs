using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MembershipAPI.Services;
using MembershipAPI.models;
using Microsoft.EntityFrameworkCore;

namespace MembershipAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _userService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("{id}/username")]
        public ActionResult<string> GetUsername(int id)
        {
            var username = _userService.GetUsername(id);
            if (username == null)
            {
                return NotFound();
            }
            return Ok(username);
        }
        [HttpGet("username/{username}")]
        public ActionResult<int> GetIdByUsername(string username)
        {
            var userId = _userService.GetIdByUsername(username);
            if (userId == 0)
            {
                return NotFound(); // Return 404 Not Found if the user is not found
            }

            return Ok(userId);
        }

        [HttpPost]
        public ActionResult<User> AddUser(User user)
        {
            if (_userService.IsDuplicateUser (user))
            {
                return BadRequest("A user with the same username or email already exists.");
            }

            _userService.Add(user);


            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }
        
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _userService.Update(user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            _userService.Delete(id);

            return NoContent();
        }
    }
}
