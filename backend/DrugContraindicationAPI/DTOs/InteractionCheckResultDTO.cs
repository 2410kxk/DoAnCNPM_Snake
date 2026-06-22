namespace DrugContraindicationAPI.DTOs
{
    public class InteractionWarningDTO
    {
        public string DrugA { get; set; } = string.Empty;

        public string DrugB { get; set; } = string.Empty;

        public string Level { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }

    public class InteractionCheckResultDTO
    {
        public bool HasInteraction { get; set; }

        public string Summary { get; set; } = string.Empty;

        public List<InteractionWarningDTO> Interactions { get; set; } = new();
    }
}