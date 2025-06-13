namespace StockAPI.Dtos
{
    public class ProductStockDto
    {
        public int ProductId { get; set; }                      // ID del producto
        public string Code { get; set; } = string.Empty;        // Código del producto
        public string Name { get; set; } = string.Empty;        // Nombre
        public string Description { get; set; } = string.Empty; // Descripción
        public string Status { get; set; } = string.Empty;      // Estado
        public int AvailableStock { get; set; }                 // Stock disponible (IN - OUT)

        public DateTime? LastTransactionDate { get; set; }      // Fecha de la última transacción
        public string? LastOperationType { get; set; }          // Tipo de operación (IN/OUT)
        public int? LastTransactionQuantity { get; set; }       // Cantidad de la última transacción
    }
}
