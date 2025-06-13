namespace StockAPI.Models
{
    public class Product
    {
        public int Id { get; set; }

        // Nombre del producto
        public string Name { get; set; } = string.Empty;

        // Descripción del producto
        public string Description { get; set; } = string.Empty;

        // Estado del producto (ej. activo/inactivo)
        public string Status { get; set; } = string.Empty;

        // Código único del producto (antes SKU)
        public string Code { get; set; } = string.Empty;

        // Transacciones asociadas al producto
        public ICollection<BoxProductTransaction> Transactions { get; set; } = new List<BoxProductTransaction>();
    }
}
