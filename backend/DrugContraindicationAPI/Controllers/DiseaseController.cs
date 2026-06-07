using Microsoft.AspNetCore.Mvc;
using DrugContraindicationAPI.Data;

namespace DrugContraindicationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiseaseController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DiseaseController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Diseases.ToList());
        }
    }
}