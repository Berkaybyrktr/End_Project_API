using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MembershipAPI.Services;
using MembershipAPI.models;
using Microsoft.EntityFrameworkCore;

namespace MembershipAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserApplicationController : ControllerBase
    {
        private readonly IUserApplicationService _userApplicationService;

        public UserApplicationController(IUserApplicationService userApplicationService)
        {
            _userApplicationService = userApplicationService;
        }

        [HttpGet]
        public ActionResult<List<UserApplication>> GetAll()
        {
            var userApplications = _userApplicationService.GetAll();
            if (userApplications.Count == 0)
            {
                return NoContent();
            }
            return Ok(userApplications);
        }

        [HttpGet("{id}")]
        public ActionResult<UserApplication> GetById(int id)
        {
            var userApplication = _userApplicationService.GetById(id);
            if (userApplication == null)
            {
                return NotFound();
            }
            return Ok(userApplication);
        }
        [HttpGet("{userId}/{applicationId}")]
        public ActionResult<int> GetUserApplicationId(int userId, int applicationId)
        {
            int userApplicationId = _userApplicationService.GetUserApplicationId(userId, applicationId);

            if (userApplicationId != 0)
            {
                return Ok(userApplicationId);
            }

            return NotFound();
        }

        [HttpGet("{id}/password")]
        public IActionResult GetPassword(int id)
        {
            var userApplication = _userApplicationService.GetById(id);
            if (userApplication == null)
            {
                return NotFound();
            }

            string password = _userApplicationService.GetPassword(id);
            if (password == null)
            {
                return NotFound();
            }

            return Ok(password);
        }
        [HttpPost]
        public ActionResult<UserApplication> Create(UserApplication userApplication)
        {
            if (_userApplicationService.IsDuplicateUserApplication(userApplication))
            {
                return BadRequest("Already exist.");
            }
            _userApplicationService.Add(userApplication);
            return CreatedAtAction(nameof(GetById), new { id = userApplication.Id }, userApplication);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UserApplication userApplication)
        {
            if (id != userApplication.Id)
            {
                return BadRequest();
            }
            try
            {
                _userApplicationService.Update(userApplication);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_userApplicationService.GetById(id) == null)
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userApplication = _userApplicationService.GetById(id);
            if (userApplication == null)
            {
                return NotFound();
            }
            _userApplicationService.Delete(id);
            return NoContent();
        }
    }
}
