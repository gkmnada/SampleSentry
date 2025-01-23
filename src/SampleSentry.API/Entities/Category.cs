namespace SampleSentry.API.Entities
{
    public class Category
    {
        public Guid CategoryID { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
