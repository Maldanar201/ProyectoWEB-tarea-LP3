namespace Modelos
{
    public class DetalleFactura
    {
        public int Id { get; set; }
        public int IdFactura { get; set; }
        public string CodigoProducto { get; set; }
        public string Descripcion { get; set; }
        public Decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public Decimal Total { get; set; }
    }
}
