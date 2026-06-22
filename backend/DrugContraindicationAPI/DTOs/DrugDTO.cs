using System.ComponentModel.DataAnnotations;

namespace DrugContraindicationAPI.DTOs
{
    public class DrugDTO
    {
        [Required]
        public string DrugName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Manufacturer { get; set; } = string.Empty;

        public string ActiveIngredient { get; set; } = string.Empty;
    }
}