using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_santa_ursula_II.Models
{
    [Table("t_clientes")]
    public class Usuario
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }

        [Display(Name = "Nombres: ")]
        [Column("nombres")]
        public string nombres { get; set; }

        [Display(Name = "Apellidos: ")]
        [Column("apellidos")]
        public string apellidos { get; set; }

        [Required(ErrorMessage = "Por favor ingrese un DNI")]
        [Display(Name = "DNI: ")]
        [Column("dni")]
        public int dni { get; set; }

        [Display(Name = "Dirección: ")]
        [Column("direccion")]
        public string direccion { get; set; }

        [Display(Name = "Teléfono: ")]
        [Column("telefono")]
        public int telefono { get; set; }

        [Display(Name = "Correo: ")]
        [Column("correo")]
        [EmailAddress]
        public string correo { get; set; }
        
    }
}