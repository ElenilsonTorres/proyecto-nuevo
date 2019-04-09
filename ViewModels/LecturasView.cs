using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemWalter.ViewModels
{
    public class LecturasView
    {
        public int Id { get; set; }
        public Nullable<int> Lectura1 { get; set; }
        public string Estado_Lectura { get; set; }
        public Nullable<int> Estado { get; set; }
        public Nullable<System.DateTime> Fecha_Registro { get; set; }
        public string Mes { get; set; }
        public Nullable<int> ClientesId { get; set; }
        public string NombreCliente { get; set; }
    }
}