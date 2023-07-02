using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MembershipAPI.Services.Interfaces;
using MembershipAPI.models;
using Microsoft.AspNetCore.Mvc;

namespace MembershipAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Application>> GetById(int id)
        {
            var application = await _applicationService.GetByIdAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            return Ok(application);
        }
        [HttpGet("applications/{id}/appkey")]
        public async Task<IActionResult> GetAppKeyById(int id)
        {
            try
            {
                var appKey = await _applicationService .GetAppKeyByIdAsync(id);

                if (appKey != Guid.Empty)
                {
                    return Ok(appKey);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions or log the error
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the appKey.");
            }
        }


        [HttpGet("appkey/{appKey}")]
        public async Task<ActionResult<int>> GetIdByAppKeyAsync(Guid appKey)
        {
            var applicationId = await _applicationService.GetIdByAppKeyAsync(appKey);
            if (applicationId == 0)
            {
                return NotFound(); // Return 404 Not Found if the application is not found
            }

            return Ok(applicationId);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Application>>> GetAll()
        {
            var applications = await _applicationService.GetAllAsync();
            return Ok(applications);
        }

        [HttpPost]
        public async Task<ActionResult<Application>> Create(Application application)
        {
            try
            {
                // Generate a new random AppKey
                application.AppKey = Guid.NewGuid();

                var createdApplication = await _applicationService.CreateAsync(application);
                return CreatedAtAction(nameof(GetById), new { id = createdApplication.Id }, createdApplication);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Application>> Update(int id, Application application)
        {
            if (id != application.Id)
            {
                return BadRequest("Invalid ID");
            }

            try
            {
                var updatedApplication = await _applicationService.UpdateAsync(application);
                return Ok(updatedApplication);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var application = await _applicationService.GetByIdAsync(id);
            if (application == null)
            {
                return NotFound();
            }

            await _applicationService.DeleteAsync(application);
            return NoContent();
        }
    }
}
