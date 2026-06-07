namespace DoAnCNPM_Snake.Services
{
    public class ContraindicationService
    {
        public bool Check(string drug, string disease)
        {
            if (drug == "Aspirin" &&
                disease == "Loét dạ dày")
            {
                return true;
            }

            return false;
        }
    }
}