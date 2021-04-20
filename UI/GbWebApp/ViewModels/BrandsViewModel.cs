namespace GbWebApp.ViewModels
{
    public record BrandsViewModel
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int Count { get; set; }
    }
}
