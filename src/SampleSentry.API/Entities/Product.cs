namespace SampleSentry.API.Entities
{
    public class Product
    {
        public Guid ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Guid CategoryID { get; set; }
        public Category Category { get; set; } = new Category();
        public bool IsActive { get; set; }
    }
}
