namespace MembershipAPI.models
{
    public class Application
    {
        public int Id { get; set; }
        public string? ApplicationName { get; set; }
        public Guid AppKey { get; set; }

        public int CompanyId { get; set; }
        //public Company? Company { get; set; }

        //public ICollection<UserApplication>? UserApplications { get; set; }
        //public ICollection<UserRole>? UserRoles { get; set; }
    }
   
}
