namespace StockAPI.Dtos
{
    public class ProductDetailDto
    {
        public int ProductId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public List<BoxDetailDto> Boxes { get; set; } = new();
    }
}
