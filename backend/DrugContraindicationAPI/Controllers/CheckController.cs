using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DrugContraindicationAPI.Data;
using DrugContraindicationAPI.DTOs;

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

        [HttpPost("contraindication")]
        public async Task<IActionResult> CheckContraindication(ContraindicationCheckRequestDTO request)
        {
            if (request.DrugNames == null || request.DrugNames.Count == 0)
                return BadRequest(new { message = "Vui lòng nhập ít nhất một thuốc." });

            if (request.DiseaseNames == null || request.DiseaseNames.Count == 0)
                return BadRequest(new { message = "Vui lòng nhập ít nhất một bệnh nền." });

            var drugNames = request.DrugNames
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            var diseaseNames = request.DiseaseNames
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (drugNames.Count == 0 || diseaseNames.Count == 0)
                return BadRequest(new { message = "Dữ liệu thuốc hoặc bệnh nền không hợp lệ." });

            var rules = await _context.Contraindications.ToListAsync();

            var warnings = new List<ContraindicationWarningDTO>();

            foreach (var drug in drugNames)
            {
                foreach (var disease in diseaseNames)
                {
                    var matchedRules = rules.Where(r =>
                        string.Equals(r.DrugName, drug, StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(r.DiseaseName, disease, StringComparison.OrdinalIgnoreCase)
                    );

                    foreach (var rule in matchedRules)
                    {
                        warnings.Add(new ContraindicationWarningDTO
                        {
                            DrugName = rule.DrugName,
                            DiseaseName = rule.DiseaseName,
                            Level = rule.Level,
                            Warning = rule.Warning
                        });
                    }
                }
            }

            var result = new ContraindicationCheckResultDTO
            {
                HasWarning = warnings.Any(),
                Summary = warnings.Any()
                    ? "Phát hiện chống chỉ định thuốc với bệnh nền."
                    : "Không phát hiện chống chỉ định trong dữ liệu hiện tại.",
                Warnings = warnings
            };

            return Ok(result);
        }

        [HttpPost("interaction")]
        public async Task<IActionResult> CheckInteraction(InteractionCheckRequestDTO request)
        {
            if (request.DrugNames == null || request.DrugNames.Count < 2)
                return BadRequest(new { message = "Cần nhập ít nhất 2 thuốc để kiểm tra tương tác." });

            var drugNames = request.DrugNames
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (drugNames.Count < 2)
                return BadRequest(new { message = "Danh sách thuốc không hợp lệ." });

            var interactions = await _context.Interactions.ToListAsync();

            var warnings = new List<InteractionWarningDTO>();

            for (int i = 0; i < drugNames.Count; i++)
            {
                for (int j = i + 1; j < drugNames.Count; j++)
                {
                    var drugA = drugNames[i];
                    var drugB = drugNames[j];

                    var matchedInteractions = interactions.Where(x =>
                        (
                            string.Equals(x.DrugA, drugA, StringComparison.OrdinalIgnoreCase) &&
                            string.Equals(x.DrugB, drugB, StringComparison.OrdinalIgnoreCase)
                        )
                        ||
                        (
                            string.Equals(x.DrugA, drugB, StringComparison.OrdinalIgnoreCase) &&
                            string.Equals(x.DrugB, drugA, StringComparison.OrdinalIgnoreCase)
                        )
                    );

                    foreach (var interaction in matchedInteractions)
                    {
                        warnings.Add(new InteractionWarningDTO
                        {
                            DrugA = interaction.DrugA,
                            DrugB = interaction.DrugB,
                            Level = interaction.Level,
                            Description = interaction.Description
                        });
                    }
                }
            }

            var result = new InteractionCheckResultDTO
            {
                HasInteraction = warnings.Any(),
                Summary = warnings.Any()
                    ? "Phát hiện tương tác giữa các thuốc."
                    : "Không phát hiện tương tác thuốc trong dữ liệu hiện tại.",
                Interactions = warnings
            };

            return Ok(result);
        }
    }
}