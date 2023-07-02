using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MembershipAPI.models;
using Microsoft.EntityFrameworkCore;

namespace MembershipAPI.Services
{
    public interface IUserApplicationService
    {
        List<UserApplication> GetAll();
        UserApplication GetById(int id);
        bool IsDuplicateUserApplication(UserApplication userApplication);
        int GetUserApplicationId(int userId, int applicationId);
        string GetPassword(int userApplicationId);
        void Add(UserApplication userApplication);
        void Update(UserApplication userApplication);
        void Delete(int id);
    }

    public class UserApplicationService : IUserApplicationService
    {
        private readonly GlobalMembershipDbContext _context;

        public UserApplicationService(GlobalMembershipDbContext context)
        {
            _context = context;
        }
        public bool IsDuplicateUserApplication(UserApplication userApplication)
        {
            return _context.UserApplications.Any(u => u.UserId == userApplication .UserId && u.ApplicationId == userApplication.ApplicationId);
        }

        public List<UserApplication> GetAll()
        {
            return _context.UserApplications.ToList();
        }

        public UserApplication GetById(int id)
        {
            return _context.UserApplications.FirstOrDefault(ua => ua.Id == id);
        }
        public int GetUserApplicationId(int userId, int applicationId)
        {
            return _context.UserApplications
                .FirstOrDefault(ua => ua.UserId == userId && ua.ApplicationId == applicationId)?.Id ?? 0;
        }

        public void Add(UserApplication userApplication)
        {
            _context.UserApplications.Add(userApplication);
            _context.SaveChanges();
        }

        public void Update(UserApplication userApplication)
        {
            _context.Entry(userApplication).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public string GetPassword(int userApplicationId)
        {
            
            var userApplication = GetById(userApplicationId);
            if (userApplication != null)
            {
                return userApplication.Password;
            }
            return null;
        }

        public void Delete(int id)
        {
            var userApplication = GetById(id);
            if (userApplication != null)
            {
                _context.UserApplications.Remove(userApplication);
                _context.SaveChanges();
            }
        }
    }
}
