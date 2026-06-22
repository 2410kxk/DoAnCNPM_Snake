using System.ComponentModel.DataAnnotations;

namespace DrugContraindicationAPI.DTOs
{
    public class HistoryDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string DrugList { get; set; } = string.Empty;

        [Required]
        public string Result { get; set; } = string.Empty;
    }
}