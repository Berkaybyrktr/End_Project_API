using MembershipAPI;
using MembershipAPI.models;
using MembershipAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MembershipAPI.Services
{
    public interface IPermissionService
    {
        IEnumerable<Permission> GetAllPermissions();

        Permission GetPermissionById(int permissionId);

        void AddPermission(Permission permission);

        void UpdatePermission(int permissionId, Permission permission);

        void DeletePermission(int permissionId);
    }
}
    public class PermissionService : IPermissionService
{
            private readonly GlobalMembershipDbContext _context;

            public PermissionService(GlobalMembershipDbContext context)
            {
                _context = context;
            }

            public IEnumerable<Permission> GetAllPermissions()
            {
                return _context.Permissions.ToList();
            }

            public Permission GetPermissionById(int permissionId)
            {
                return _context.Permissions.Find(permissionId);
            }

            public void AddPermission(Permission permission)
            {
                if (_context.Permissions.Any(p => p.Id == permission.Id))
                {
                    throw new ArgumentException("A permission with the same ID already exists.");
                }

                _context.Permissions.Add(permission);
                _context.SaveChanges();
            }

            public void UpdatePermission(int permissionId, Permission permission)
            {
                if (permissionId != permission.Id)
                {
                    throw new ArgumentException("The permission ID in the request body does not match the permission ID in the URL.");
                }

                _context.Entry(permission).State = EntityState.Modified;
                _context.SaveChanges();
            }

            public void DeletePermission(int permissionId)
            {
                var permission = _context.Permissions.Find(permissionId);
                if (permission == null)
                {
                    throw new ArgumentException("Permission not found.");
                }

                _context.Permissions.Remove(permission);
                _context.SaveChanges();
            }
        }
    
