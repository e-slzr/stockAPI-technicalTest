namespace StockAPI.Models
{
    public class BoxProductTransaction
    {
        /// Id único de la transacción.
        public int Id { get; set; }

        /// Id del producto relacionado.
        public int ProductId { get; set; }

        /// Referencia al producto asociado.
        public Product Product { get; set; } = new Product();

        /// Id de la caja relacionada.
        public int BoxId { get; set; }

        /// Referencia a la caja asociada.
        public Box Box { get; set; } = new Box();

        /// Fecha en que se realizó la transacción.
        public DateTime Date { get; set; }

        /// Tipo de operación: "IN" para entrada, "OUT" para salida.
        public string OperationType { get; set; } = string.Empty;

        /// Cantidad involucrada en la transacción.
        public int Quantity { get; set; } = 0;
    }
}
