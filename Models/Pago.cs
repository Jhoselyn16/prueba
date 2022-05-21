using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_santa_ursula_II.Models
{
    //CARRITO
    [Table("T_pago")]
    public class Pago
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        public DateTime FechaPago { get; set; }
        public String NombreTarjeta { get; set; }
        
        public String NumeroTarjeta { get; set; }
        
        [NotMapped]
        public String DueDateYYMM { get; set; }
        [NotMapped]
        public String Cvv { get; set; }
        public int MontoTotal{ get; set; }
        public String UserID{ get; set; }
  
    }
}