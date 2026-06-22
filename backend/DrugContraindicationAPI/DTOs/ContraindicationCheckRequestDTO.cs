namespace DrugContraindicationAPI.DTOs
{
    public class ContraindicationCheckRequestDTO
    {
        public List<string> DrugNames { get; set; } = new();

        public List<string> DiseaseNames { get; set; } = new();
    }
}