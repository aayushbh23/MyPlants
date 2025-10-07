namespace PlantsCatalog.Models
{
    public class CatalogViewModel
    {
        public IEnumerable<Plant> Items { get; set; } = Enumerable.Empty<Plant>();
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 6;
        public int TotalCount { get; set; }
        public string? Q { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}