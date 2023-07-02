using Microsoft.AspNetCore.Mvc;
using MembershipAPI.models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MembershipAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolePermissionsController : ControllerBase
    {
        private readonly IRolePermissionService _rolePermissionService;

        public RolePermissionsController(IRolePermissionService rolePermissionService)
        {
            _rolePermissionService = rolePermissionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<RolePermission>>> GetAllRolePermissions()
        {
            var rolePermissions = await _rolePermissionService.GetAllRolePermissions();
            return Ok(rolePermissions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RolePermission>> GetRolePermissionById(int id)
        {
            var rolePermission = await _rolePermissionService.GetRolePermissionById(id);
            if (rolePermission == null)
            {
                return NotFound();
            }
            return Ok(rolePermission);
        }

        [HttpPost]
        public async Task<ActionResult<RolePermission>> CreateRolePermission(RolePermission rolePermission)
        {
            try
            {
                var createdRolePermission = await _rolePermissionService.CreateRolePermission(rolePermission);
                return CreatedAtAction(nameof(GetRolePermissionById), new { id = createdRolePermission.Id }, createdRolePermission);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RolePermission>> UpdateRolePermission(int id, RolePermission rolePermission)
        {
            try
            {
                var updatedRolePermission = await _rolePermissionService.UpdateRolePermission(id, rolePermission);
                return Ok(updatedRolePermission);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRolePermission(int id)
        {
            var deleted = await _rolePermissionService.DeleteRolePermission(id);
            if (deleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
