using System.Collections.Generic;
using System.Linq;
using MembershipAPI.models;
using Microsoft.EntityFrameworkCore;

namespace MembershipAPI.Services
{
    public interface IUserRoleService
    {
        UserRole Create(UserRole userRole);
        UserRole GetById(int id);
        IEnumerable<UserRole> GetAll();
        void Update(UserRole userRole);
        void Delete(int id);
    }

    public class UserRoleService : IUserRoleService
    {
        private readonly GlobalMembershipDbContext _dbContext;

        public UserRoleService(GlobalMembershipDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserRole Create(UserRole userRole)
        {
            
            if (!_dbContext.Applications.Any(a => a.Id == userRole.ApplicationId))
            {
                throw new KeyNotFoundException($"Application with ID '{userRole.ApplicationId}' not found");
            }

            var result = _dbContext.UserRoles.Add(userRole);
            _dbContext.SaveChanges();
            return result.Entity;
        }

        public UserRole GetById(int id)
        {
            var userRole = _dbContext.UserRoles.SingleOrDefault(ur => ur.Id == id);
            if (userRole == null)
            {
                throw new KeyNotFoundException($"UserRole with ID '{id}' not found");
            }

            return userRole;
        }

        public IEnumerable<UserRole> GetAll()
        {
            return _dbContext.UserRoles.ToList();
        }

        public void Update(UserRole userRole)
        {
            
            if (!_dbContext.Applications.Any(a => a.Id == userRole.ApplicationId))
            {
                throw new KeyNotFoundException($"Application with ID '{userRole.ApplicationId}' not found");
            }

            _dbContext.Entry(userRole).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var userRole = _dbContext.UserRoles.SingleOrDefault(ur => ur.Id == id);
            if (userRole == null)
            {
                throw new KeyNotFoundException($"UserRole with ID '{id}' not found");
            }

            _dbContext.UserRoles.Remove(userRole);
            _dbContext.SaveChanges();
        }
    }
}
