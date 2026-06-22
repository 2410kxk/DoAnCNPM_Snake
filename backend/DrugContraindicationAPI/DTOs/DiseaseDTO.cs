using System.ComponentModel.DataAnnotations;

namespace DrugContraindicationAPI.DTOs
{
    public class DiseaseDTO
    {
        [Required]
        public string DiseaseName { get; set; } = string.Empty;
    
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;
    }
}