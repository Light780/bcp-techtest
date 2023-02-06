using System;

namespace BCP.Application.DTOs.TipoCambio
{
    public class GetTipoCambioResponse
    {
        public string Id { get; set; }
        public string Moneda { get; set; }
        public string Fecha { get; set; }
        public decimal Compra { get; set; }
        public decimal Venta { get; set; }
    }
}