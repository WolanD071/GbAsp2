namespace GbWebApp.Domain.DTO
{
    /// <summary> brand model </summary>
    public class BrandDTO
    {
        /// <summary> brand id </summary>
        public int Id { get; set; }
        /// <summary> brand name </summary>
        public string Name { get; set; }
        /// <summary> brand order </summary>
        public int Order { get; set; }
        /// <summary> count of the products in this brand </summary>
        public int ProductCnt { get; set; }
    }
}
