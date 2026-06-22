using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DrugContraindicationAPI.Data;
using DrugContraindicationAPI.Models;
using DrugContraindicationAPI.DTOs;

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
        public async Task<IActionResult> GetAll()
        {
            var drugs = await _context.Drugs.ToListAsync();
            return Ok(drugs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var drug = await _context.Drugs.FindAsync(id);

            if (drug == null)
                return NotFound(new { message = "Không tìm thấy thuốc." });

            return Ok(drug);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return BadRequest(new { message = "Vui lòng nhập từ khóa tìm kiếm." });

            keyword = keyword.Trim();

            var drugs = await _context.Drugs
                .Where(d =>
                    EF.Functions.Like(d.DrugName, $"%{keyword}%") ||
                    EF.Functions.Like(d.ActiveIngredient, $"%{keyword}%"))
                .ToListAsync();

            return Ok(drugs);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DrugDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var drug = new Drug
            {
                DrugName = dto.DrugName.Trim(),
                Description = dto.Description.Trim(),
                Manufacturer = dto.Manufacturer.Trim(),
                ActiveIngredient = dto.ActiveIngredient.Trim()
            };

            _context.Drugs.Add(drug);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = drug.DrugId }, drug);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DrugDTO dto)
        {
            var drug = await _context.Drugs.FindAsync(id);

            if (drug == null)
                return NotFound(new { message = "Không tìm thấy thuốc cần cập nhật." });

            drug.DrugName = dto.DrugName.Trim();
            drug.Description = dto.Description.Trim();
            drug.Manufacturer = dto.Manufacturer.Trim();
            drug.ActiveIngredient = dto.ActiveIngredient.Trim();

            await _context.SaveChangesAsync();

            return Ok(drug);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var drug = await _context.Drugs.FindAsync(id);

            if (drug == null)
                return NotFound(new { message = "Không tìm thấy thuốc cần xóa." });

            _context.Drugs.Remove(drug);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Xóa thuốc thành công." });
        }
    }
}