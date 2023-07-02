using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MembershipAPI.models;
using MembershipAPI;

public interface IRolePermissionService
{
    Task<List<RolePermission>> GetAllRolePermissions();
    Task<RolePermission> GetRolePermissionById(int id);
    Task<RolePermission> CreateRolePermission(RolePermission rolePermission);
    Task<RolePermission> UpdateRolePermission(int id, RolePermission rolePermission);
    Task<bool> DeleteRolePermission(int id);
}

public class RolePermissionService : IRolePermissionService
{
    private readonly GlobalMembershipDbContext _context;

    public RolePermissionService(GlobalMembershipDbContext context)
    {
        _context = context;
    }

    public async Task<List<RolePermission>> GetAllRolePermissions()
    {
        return await _context.RolePermissions.ToListAsync();
    }

    public async Task<RolePermission> GetRolePermissionById(int id)
    {
        return await _context.RolePermissions.FindAsync(id);
    }

    public async Task<RolePermission> CreateRolePermission(RolePermission rolePermission)
    {
        // Check if the role and permission exist
        var role = await _context.UserRoles.FindAsync(rolePermission.RoleId);
        if (role == null)
        {
            throw new Exception("Role not found");
        }

        var permission = await _context.Permissions.FindAsync(rolePermission.PermissionId);
        if (permission == null)
        {
            throw new Exception("Permission not found");
        }

        // Add the new role permission
        _context.RolePermissions.Add(rolePermission);
        await _context.SaveChangesAsync();

        return rolePermission;
    }

    public async Task<RolePermission> UpdateRolePermission(int id, RolePermission rolePermission)
    {
        // Check if the role permission exists
        var existingRolePermission = await _context.RolePermissions.FindAsync(id);
        if (existingRolePermission == null)
        {
            throw new Exception("Role permission not found");
        }

        // Check if the role and permission exist
        var role = await _context.UserRoles.FindAsync(rolePermission.RoleId);
        if (role == null)
        {
            throw new Exception("Role not found");
        }

        var permission = await _context.Permissions.FindAsync(rolePermission.PermissionId);
        if (permission == null)
        {
            throw new Exception("Permission not found");
        }

        // Update the existing role permission
        existingRolePermission.RoleId = rolePermission.RoleId;
        existingRolePermission.PermissionId = rolePermission.PermissionId;

        await _context.SaveChangesAsync();

        return existingRolePermission;
    }

    public async Task<bool> DeleteRolePermission(int id)
    {
        // Check if the role permission exists
        var existingRolePermission = await _context.RolePermissions.FindAsync(id);
        if (existingRolePermission == null)
        {
            return false;
        }

        // Delete the role permission
        _context.RolePermissions.Remove(existingRolePermission);
        await _context.SaveChangesAsync();

        return true;
    }
}
