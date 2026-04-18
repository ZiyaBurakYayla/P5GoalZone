using GoalZone.API.DTOs;
using GoalZone.API.Services;
using GoalZone.API.Services.RefereeServices;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace GoalZone.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RefereesController : ControllerBase
    {
        private readonly IRefereeService _service;
        public RefereesController(IRefereeService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
    }
}