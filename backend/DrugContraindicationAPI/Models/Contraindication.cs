using System.ComponentModel.DataAnnotations;

namespace DrugContraindicationAPI.Models
{
    public class Contraindication
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string DrugName { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string DiseaseName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Level { get; set; } = "High";

        [Required]
        [MaxLength(1000)]
        public string Warning { get; set; } = string.Empty;
    }
}