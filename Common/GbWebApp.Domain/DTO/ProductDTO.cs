namespace GbWebApp.Domain.DTO
{
    /// <summary> product model </summary>
    public class ProductDTO
    {
        /// <summary> product id </summary>
        public int Id { get; set; }
        /// <summary> product name </summary>
        public string Name { get; set; }
        /// <summary> product order </summary>
        public int Order { get; set; }
        /// <summary> product price </summary>
        public decimal Price { get; set; }
        /// <summary> product's image </summary>
        public string ImageUrl { get; set; }
        /// <summary> product's brand </summary>
        public BrandDTO Brand { get; set; }
        /// <summary> product's section </summary>
        public SectionDTO Section { get; set; }
    }
}