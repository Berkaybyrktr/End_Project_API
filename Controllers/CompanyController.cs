using MembershipAPI.models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MembershipAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public IActionResult GetAllCompanies()
        {
            IEnumerable<Company> companies = _companyService.GetAllCompanies();
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public IActionResult GetCompanyById(int id)
        {
            Company company = _companyService.GetCompanyById(id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        [HttpPost]
        public IActionResult AddCompany(Company company)
        {
            _companyService.AddCompany(company);
            return CreatedAtAction(nameof(GetCompanyById), new { id = company.Id }, company);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCompany(int id, Company company)
        {
            if (id != company.Id)
            {
                return BadRequest();
            }

            _companyService.UpdateCompany(company);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCompany(int id)
        {
            _companyService.DeleteCompany(id);
            return NoContent();
        }
    }
}
