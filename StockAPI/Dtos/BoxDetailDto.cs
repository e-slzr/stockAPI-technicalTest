namespace StockAPI.Dtos
{
    public class BoxDetailDto
    {
        public int BoxId { get; set; }
        public string BoxCode { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public DateTime? LastTransactionDate { get; set; }
    }
}
