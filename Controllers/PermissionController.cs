using MembershipAPI.models;
using MembershipAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MembershipAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionsController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        public IActionResult GetAllPermissions()
        {
            var permissions = _permissionService.GetAllPermissions();
            return Ok(permissions);
        }

        [HttpGet("{id}")]
        public IActionResult GetPermissionById(int id)
        {
            var permission = _permissionService.GetPermissionById(id);
            if (permission == null)
            {
                return NotFound();
            }

            return Ok(permission);
        }

        [HttpPost]
        public IActionResult AddPermission(Permission permission)
        {
            try
            {
                _permissionService.AddPermission(permission);
                return CreatedAtAction(nameof(GetPermissionById), new { id = permission.Id }, permission);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePermission(int id, Permission permission)
        {
            try
            {
                _permissionService.UpdatePermission(id, permission);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePermission(int id)
        {
            try
            {
                _permissionService.DeletePermission(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
