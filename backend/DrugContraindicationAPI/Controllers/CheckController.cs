using Microsoft.AspNetCore.Mvc;
using DrugContraindicationAPI.Data;

namespace DrugContraindicationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CheckController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Check(string drug, string disease)
        {
            var result = _context.Contraindications
                .FirstOrDefault(x =>
                    x.DrugName == drug &&
                    x.DiseaseName == disease);

            if (result == null)
                return Ok("Không có chống chỉ định");

            return Ok(result);
        }
    }
}