using System.ComponentModel.DataAnnotations;

namespace DrugContraindicationAPI.Models
{
    public class Interaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string DrugA { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string DrugB { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Level { get; set; } = "Medium";

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;
    }
}