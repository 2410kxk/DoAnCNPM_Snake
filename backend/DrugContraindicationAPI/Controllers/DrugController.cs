using Microsoft.AspNetCore.Mvc;
using DrugContraindicationAPI.Data;

namespace DrugContraindicationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DrugController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DrugController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Drugs.ToList());
        }
    }
}