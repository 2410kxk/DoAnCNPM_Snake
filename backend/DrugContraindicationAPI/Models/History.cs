namespace DrugContraindicationAPI.Models
{
    public class History
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string DrugList { get; set; }

        public string Result { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}