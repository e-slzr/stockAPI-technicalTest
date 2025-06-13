namespace StockAPI.Models
{
    public class Box
    {
        public int Id { get; set; }

        // Código único de la caja
        public string Code { get; set; } = string.Empty;

        // Cantidad de productos en la caja
        public int Quantity { get; set; }

        // Ubicación física de la caja
        public string Location { get; set; } = string.Empty;

        // Transacciones asociadas a la caja
        public ICollection<BoxProductTransaction> Transactions { get; set; } = new List<BoxProductTransaction>();
    }
}
