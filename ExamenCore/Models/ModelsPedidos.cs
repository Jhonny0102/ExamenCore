using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenCore.Models
{
    public class ModelsPedidos
    {
        public string codigopedido {  get; set; }
        public string fechaentrega { get; set; }
        public string formaenvio { get; set; }
        public int importe {  get; set; }
    }
}
