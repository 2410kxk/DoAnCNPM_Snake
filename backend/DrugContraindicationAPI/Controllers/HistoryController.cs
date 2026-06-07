using Microsoft.AspNetCore.Mvc;
using DrugContraindicationAPI.Data;
using DrugContraindicationAPI.Models;

namespace DrugContraindicationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HistoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Histories.ToList());
        }

        [HttpPost]
        public IActionResult Save(History history)
        {
            _context.Histories.Add(history);
            _context.SaveChanges();

            return Ok(history);
        }
    }
}