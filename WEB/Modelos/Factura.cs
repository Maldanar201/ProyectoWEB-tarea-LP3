namespace Modelos
{
    public class Factura
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string IdentidadCliente { get; set; }
        public string CodigoUsuario { get; set; }
        public Decimal Isv { get; set; }
        public Decimal Descto { get; set; }
        public Decimal Subtotal { get; set; }
        public Decimal Total { get; set; }
    }
}
