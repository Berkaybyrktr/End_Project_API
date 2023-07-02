using Microsoft.AspNetCore.Mvc;
using MembershipAPI.Services;
using MembershipAPI.models;

namespace MembershipAPI.Controllers
{
    [ApiController]
    [Route("api/userroles")]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpPost]
        public IActionResult Create(UserRole userRole)
        {
            try
            {
                var createdUserRole = _userRoleService.Create(userRole);
                return Ok(createdUserRole);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var userRole = _userRoleService.GetById(id);
                return Ok(userRole);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var userRoles = _userRoleService.GetAll();
            return Ok(userRoles);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UserRole userRole)
        {
            if (id != userRole.Id)
            {
                return BadRequest("UserRole ID mismatch");
            }

            try
            {
                _userRoleService.Update(userRole);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _userRoleService.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
