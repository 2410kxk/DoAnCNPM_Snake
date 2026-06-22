namespace DrugContraindicationAPI.DTOs
{
    public class ContraindicationWarningDTO
    {
        public string DrugName { get; set; } = string.Empty;

        public string DiseaseName { get; set; } = string.Empty;

        public string Level { get; set; } = string.Empty;

        public string Warning { get; set; } = string.Empty;
    }

    public class ContraindicationCheckResultDTO
    {
        public bool HasWarning { get; set; }

        public string Summary { get; set; } = string.Empty;

        public List<ContraindicationWarningDTO> Warnings { get; set; } = new();
    }
}