namespace SeleniumFramework.Models
{
    public class OwnerModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Telephone { get; set; }

        public IList<PetModel> Pets { get; set; } = new List<PetModel>();
    }
}
