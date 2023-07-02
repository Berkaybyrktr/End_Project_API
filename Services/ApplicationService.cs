using MembershipAPI.models;
using MembershipAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MembershipAPI.Services.Interfaces
{
    public interface IApplicationService
    {
        Task<Application> GetByIdAsync(int id);
        Task<Application> GetByAppKeyAsync(Guid appKey);
        Task<int> GetIdByAppKeyAsync(Guid appKey);
        Task<IEnumerable<Application>> GetAllAsync();
        Task<Guid> GetAppKeyByIdAsync(int id);
        Task<Application> CreateAsync(Application application);
        Task<Application> UpdateAsync(Application application);
        Task DeleteAsync(Application application);
    }
    


    public class ApplicationService : IApplicationService
    {
       
        private readonly GlobalMembershipDbContext _dbContext;


        public ApplicationService(GlobalMembershipDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Application> GetByIdAsync(int id)
        {
            return await _dbContext.Applications
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Application>> GetAllAsync()
        {
            return await _dbContext.Applications
                .ToListAsync();
        }
        public async Task<Application> GetByAppKeyAsync(Guid appKey)
        {
            return await _dbContext.Applications.FirstOrDefaultAsync(a => a.AppKey == appKey);
        }

        public async Task<int> GetIdByAppKeyAsync(Guid appKey)
        {
            var application = await _dbContext.Applications.FirstOrDefaultAsync(a => a.AppKey == appKey);
            return application?.Id ?? 0;
        }
        public async Task<Guid> GetAppKeyByIdAsync(int id)
        {
            var application = await _dbContext.Applications.FirstOrDefaultAsync(a => a.Id == id);
            return application?.AppKey ?? Guid.Empty;
        }
        public async Task<Application> CreateAsync(Application application)
        {
            _dbContext.Applications.Add(application);
            await _dbContext.SaveChangesAsync();
            return application;
        }

        public async Task<Application> UpdateAsync(Application application)
        {
            _dbContext.Entry(application).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return application;
        }

        public async Task DeleteAsync(Application application)
        {
            _dbContext.Applications.Remove(application);
            await _dbContext.SaveChangesAsync();
        }
    }

}


