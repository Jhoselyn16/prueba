using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_santa_ursula_II.Models
{
    //CARRITO
 
    public class Impresion
    {
        
        public int id { get; set; }
       
        
        public String UserID{ get; set; }
        public DateTime FechaPago { get; set; }
        public int MontoTotal{ get; set; }
      
        public Habitaciones hab { get; set; }
        public Detallepedido det { get; set; }
    }
}