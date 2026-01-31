namespace SIGID.Core.Application.DTO.Dashboard
{
    /// <summary>
    /// DTO para productos con stock bajo
    /// </summary>
    public class ProductStockBajoDTO
    {
        /// <summary>
        /// Identificador del producto
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nombre del producto
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Stock actual disponible
        /// </summary>
        public int CurrStock { get; set; }

        /// <summary>
        /// Stock mínimo permitido
        /// </summary>
        public int StockMin { get; set; }

        /// <summary>
        /// Diferencia entre stock actual y mínimo (negativo = crítico)
        /// </summary>
        public int DiferenciaStock { get; set; }

        /// <summary>
        /// Estado del producto
        /// </summary>
        public string Status { get; set; }
    }
}
