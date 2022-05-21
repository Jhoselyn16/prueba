using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace hotel_santa_ursula_II.Models
{
    //CARRITO
    [Table("t_proforma")]
    public class Carrito
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        public String UserID {get; set;}

        [Display(Name = "Numero de Habitación: ")]
        public Habitaciones habitacion {get; set;}
        [Display(Name = "Cantidad: ")]
        public int Quantity{get; set;}
        [Display(Name = "Precio por Noche: ")]
        public int Precio { get; set; }
        [Display(Name = "Noches: ")]
        public int C_noches {get; set;} =1;
        [Display(Name = "Estado: ")]
        public String Status { get; set; } = "PENDIENTE";
        [Display(Name = "Fecha de Reserva: ")]
        public DateTime fechar{get; set;}
    }
}