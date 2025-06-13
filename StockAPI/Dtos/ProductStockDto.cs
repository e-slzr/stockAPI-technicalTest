namespace StockAPI.Dtos
{
    public class ProductStockDto
    {
        public string Code { get; set; } = string.Empty;        // Código del producto
        public string Name { get; set; } = string.Empty;        // Nombre
        public string Description { get; set; } = string.Empty; // Descripción
        public string Status { get; set; } = string.Empty;      // Estado
        public int AvailableStock { get; set; }                 // Stock disponible (IN - OUT)
    }

}
