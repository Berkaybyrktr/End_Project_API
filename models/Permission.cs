﻿
namespace MembershipAPI.models
{
    public class Permission
    {
        public int Id { get; set; }
        public string PermissionName { get; set; }
        public string? Description { get; set; }

        //public ICollection<RolePermission> RolePermissions { get; set; }
    }

}
