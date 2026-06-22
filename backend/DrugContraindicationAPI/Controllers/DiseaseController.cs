using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DrugContraindicationAPI.Data;
using DrugContraindicationAPI.Models;
using DrugContraindicationAPI.DTOs;

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
        public async Task<IActionResult> GetAll()
        {
            var diseases = await _context.Diseases.ToListAsync();
            return Ok(diseases);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var disease = await _context.Diseases.FindAsync(id);

            if (disease == null)
                return NotFound(new { message = "Không tìm thấy bệnh nền." });

            return Ok(disease);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return BadRequest(new { message = "Vui lòng nhập từ khóa tìm kiếm." });

            keyword = keyword.Trim();

            var diseases = await _context.Diseases
                .Where(d =>
                    EF.Functions.Like(d.DiseaseName, $"%{keyword}%") ||
                    EF.Functions.Like(d.Description, $"%{keyword}%"))
                .ToListAsync();

            return Ok(diseases);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DiseaseDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var disease = new Disease
            {
                DiseaseName = dto.DiseaseName.Trim(),
                Description = dto.Description.Trim()
            };

            _context.Diseases.Add(disease);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = disease.Id }, disease);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DiseaseDTO dto)
        {
            var disease = await _context.Diseases.FindAsync(id);

            if (disease == null)
                return NotFound(new { message = "Không tìm thấy bệnh nền cần cập nhật." });

            disease.DiseaseName = dto.DiseaseName.Trim();
            disease.Description = dto.Description.Trim();

            await _context.SaveChangesAsync();

            return Ok(disease);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var disease = await _context.Diseases.FindAsync(id);

            if (disease == null)
                return NotFound(new { message = "Không tìm thấy bệnh nền cần xóa." });

            _context.Diseases.Remove(disease);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Xóa bệnh nền thành công." });
        }
    }
}