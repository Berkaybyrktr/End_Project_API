namespace MembershipAPI.models
{
    public class UserApplication
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ApplicationId { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }

       // public Application Applications { get; set; }
       // public UserRole UserRoles { get; set; }
       // public User Users { get; set; }

    }
}