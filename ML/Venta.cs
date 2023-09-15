using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public string? IdUser { get; set; }//pendiente

        public decimal Total { get; set; }

        public int? IdMetodoPago { get; set; }

        public DateTime? Fecha { get; set; }
        public  DL.AspNetUser? User { get; set; }//pendiente
    }
}
