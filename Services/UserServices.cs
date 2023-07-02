using System;
using System.Collections.Generic;
using System.Linq;
using MembershipAPI.models;
using Microsoft.EntityFrameworkCore;

namespace MembershipAPI.Services
{
    public interface IUserService
    {
        User GetById(int id);
        string GetUsername(int id);
        bool IsDuplicateUser(User user);
        int GetIdByUsername(string username);
        IEnumerable<User> GetAll();
        void Add(User user);

        void Update(User user);
        void Delete(int id);
    }

    public class UserService : IUserService
    {
        private readonly GlobalMembershipDbContext _dbContext;

        public UserService(GlobalMembershipDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool IsDuplicateUser(User user)
        {
            return _dbContext.Users.Any(u => u.Username == user.Username || u.Email == user.Email);
        }

        public string GetUsername(int id)
        {
            var user = GetById(id);
            return user?.Username;
        }
        public int GetIdByUsername(string username)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Username == username);
            return user?.Id ?? 0;
        }


        public User GetById(int id)
        {
            return _dbContext.Users.SingleOrDefault(u => u.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            return _dbContext.Users.ToList();
        }

        public void Add(User user)
        {
            user.UpdatedAt = DateTime.Now;
            user.CreatedAt = DateTime.Now;
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

        }

        public void Update(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _dbContext.Users.Find(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
        }
    }
}
