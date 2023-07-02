namespace MembershipAPI.models
{
    public class UserRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public int ApplicationId { get; set; }

       // public Application Applications { get; set; }
        //public ICollection<RolePermission> RolePermissions { get; set; }
     //   public ICollection<UserApplication> UserApplications { get; set; }
    }

}