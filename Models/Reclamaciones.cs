using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_santa_ursula_II.Models
{
     [Table("L_Reclamaciones")]    
    public class Reclamaciones
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }

        [Display(Name = "DNI: ")]
        public int Dni { get; set; }

        [Display(Name = "Nombres: ")]
        public string Nombres { get; set; }

        [Display(Name = "Apellidos: ")]
        public string Apellido { get; set; }

        [Display(Name = "Celular: ")]
        public int N_Celular { get; set; }

        [Display(Name = "Correo: ")]
        public string Correo { get; set; }

        [Display(Name = "Sugerencia o Reclamo: ")]
        public string Reclamo { get; set; }

    }
}