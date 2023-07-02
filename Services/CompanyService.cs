using MembershipAPI;
using System.Collections.Generic;
using System.Linq;
using MembershipAPI.models;
using Microsoft.EntityFrameworkCore;

public interface ICompanyService
{
    IEnumerable<Company> GetAllCompanies();
    Company GetCompanyById(int id);
    void AddCompany(Company company);
    void UpdateCompany(Company company);
    void DeleteCompany(int id);
}
public class CompanyService : ICompanyService
{
    private readonly GlobalMembershipDbContext _dbContext;

    public CompanyService(GlobalMembershipDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Company> GetAllCompanies()
    {
        return _dbContext.Companies.ToList();
    }

    public Company GetCompanyById(int id)
    {
        return _dbContext.Companies.FirstOrDefault(c => c.Id == id);
    }

    public void AddCompany(Company company)
    {
        _dbContext.Companies.Add(company);
        _dbContext.SaveChanges();
    }

    public void UpdateCompany(Company company)
    {
        var existingCompany = _dbContext.Companies.FirstOrDefault(c => c.Id == company.Id);

        if (existingCompany != null)
        {
            existingCompany.CompanyName = company.CompanyName;
            _dbContext.SaveChanges();
        }
    }

    public void DeleteCompany(int id)
    {
        var existingCompany = _dbContext.Companies.FirstOrDefault(c => c.Id == id);

        if (existingCompany != null)
        {
            _dbContext.Companies.Remove(existingCompany);
            _dbContext.SaveChanges();
        }
    }
}

