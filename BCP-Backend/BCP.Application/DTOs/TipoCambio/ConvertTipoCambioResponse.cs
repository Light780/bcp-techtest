namespace BCP.Application.DTOs.TipoCambio
{
    public class ConvertTipoCambioResponse
    {
        public decimal Monto { get; set; }
        public decimal MontoConvertido { get; set; }
        public string MonedaOrigen { get; set; }
        public string MonedaDestino { get; set; }
        public decimal TipoCambio { get; set; }
    }
}