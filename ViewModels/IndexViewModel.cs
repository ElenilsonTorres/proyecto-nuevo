using Paginacion.Models;
using SistemWalter.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemWalter.ViewModel
{
    public class IndexViewModel : BaseModelo
    {
        public List<Cliente> clientes { get; set; }
        public List<Lectura> lecturas { get; set; }
        public List<Pago> pagos { get; set; }
        public List<MoraCliente> moraClientes { get; set; }
        public List<Empleado> empleados { get; set; }
    }
}