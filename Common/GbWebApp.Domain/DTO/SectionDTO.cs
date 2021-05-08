namespace GbWebApp.Domain.DTO
{
    /// <summary> section model </summary>
    public class SectionDTO
    {
        /// <summary> section id </summary>
        public int Id { get; set; }
        /// <summary> section name </summary>
        public string Name { get; set; }
        /// <summary> section order </summary>
        public int Order { get; set; }
        /// <summary> parent section id, if exists </summary>
        public int? ParentId { get; set; }
        /// <summary> count of the products in this section </summary>
        public int ProductCnt { get; set; }
    }
}