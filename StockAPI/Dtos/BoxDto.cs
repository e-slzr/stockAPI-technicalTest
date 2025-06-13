namespace StockAPI.Dtos
{
    public class BoxDTO
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string Location { get; set; } = string.Empty;

        public List<TransactionDTO> Transactions { get; set; } = new();
    }

    public class TransactionDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
    }
}
