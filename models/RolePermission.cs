namespace MembershipAPI.models{
public class RolePermission
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public int PermissionId { get; set; }

   // public Permission Permissions { get; set; }
    //public UserRole UserRoles { get; set; }
}
}