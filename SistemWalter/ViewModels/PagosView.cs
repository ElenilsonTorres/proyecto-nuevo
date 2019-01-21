using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemWalter.ViewModels
{
    public class PagosView
    {

        public int Id { get; set; }
        public Nullable<int> Lectura_Id { get; set; }
        public Nullable<int> ClienteId { get; set; }
        public int Numero_Factura { get; set; }
        public Nullable<int> Lectura_Anterior { get; set; }
        public Nullable<int> Lectura_Actual { get; set; }
        public Nullable<int> Consumo { get; set; }
        public Nullable<decimal> Cuota_Fija { get; set; }
        public Nullable<decimal> Mes_Atrasado { get; set; }
        public Nullable<decimal> Mora { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<System.DateTime> Fecha_Lectura { get; set; }

        public Nullable<int> Estado { get; set; }
        public Nullable<System.DateTime> Fecha_Registro { get; set; }

        public DateTime Fecha_Pago { get; set; }
        public DateTime Fecha_Vencimiento { get; set; }

        public string NombreCliente { get; set; }
    }
}