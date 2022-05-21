using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_santa_ursula_II.Models
{
    //CARRITO
    [Table("T_reserva")]
    public class Reservas
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        public String UserID {get; set;}
        public Habitaciones Habitaciones {get; set;}
        public int CantHuespedes{get; set;}
        public DateTime DiaEntrada{get; set;}
        public DateTime DiaSalida{get; set;}
        public Double Precio { get; set; }
        public String Estado { get; set; } = "PENDIENTE";
    }
}