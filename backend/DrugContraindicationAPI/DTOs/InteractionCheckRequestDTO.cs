
namespace DrugContraindicationAPI.DTOs
{
    public class InteractionCheckRequestDTO
    {
        public List<string> DrugNames { get; set; } = new();
    }
}