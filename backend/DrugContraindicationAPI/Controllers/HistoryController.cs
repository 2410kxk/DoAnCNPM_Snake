using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DrugContraindicationAPI.Data;
using DrugContraindicationAPI.Models;
using DrugContraindicationAPI.DTOs;

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
        public async Task<IActionResult> GetAll()
        {
            var histories = await _context.Histories
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return Ok(histories);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var histories = await _context.Histories
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return Ok(histories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var history = await _context.Histories.FindAsync(id);

            if (history == null)
                return NotFound(new { message = "Không tìm thấy lịch sử tra cứu." });

            return Ok(history);
        }

        [HttpPost]
        public async Task<IActionResult> Save(HistoryDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var history = new History
            {
                UserId = dto.UserId,
                DrugList = dto.DrugList.Trim(),
                Result = dto.Result.Trim(),
                CreatedAt = DateTime.Now
            };

            _context.Histories.Add(history);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Lưu lịch sử tra cứu thành công.",
                history
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var history = await _context.Histories.FindAsync(id);

            if (history == null)
                return NotFound(new { message = "Không tìm thấy lịch sử cần xóa." });

            _context.Histories.Remove(history);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Xóa lịch sử thành công." });
        }
    }
}