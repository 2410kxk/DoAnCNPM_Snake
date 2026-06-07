namespace DrugContraindicationAPI.Models
{
    public class Interaction
    {
        public int Id { get; set; }

        public string DrugA { get; set; }

        public string DrugB { get; set; }

        public string Level { get; set; }

        public string Description { get; set; }
    }
}