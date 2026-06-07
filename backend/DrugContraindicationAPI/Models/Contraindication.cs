namespace DrugContraindicationAPI.Models
{
    public class Contraindication
    {
        public int Id { get; set; }

        public string DrugName { get; set; }

        public string DiseaseName { get; set; }

        public string Warning { get; set; }
    }
}