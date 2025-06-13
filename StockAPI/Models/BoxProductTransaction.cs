namespace StockAPI.Models
{
    public class BoxProductTransaction
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int BoxId { get; set; }
        public Box Box { get; set; } = null!;

        public DateTime Date { get; set; }
        public string OperationType { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
