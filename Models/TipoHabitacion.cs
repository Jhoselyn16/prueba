using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_santa_ursula_II.Models
{
     [Table("T_tipo_habitacion")]    
    public class TipoHabitacion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }

        [Required(ErrorMessage = "Por favor ingrese un nombre para el tipo de habitación")]   
        [Display(Name="Nombre del tipo de habitación: ")]
        [Column("nomtiphabitacion")]
        public string nomtiphabitacion { get; set; }
        
        [Required(ErrorMessage = "Por favor ingrese una descripción")]   
        [Display(Name="Descripción: ")]
        [Column("desctiphab")]
        public string desctiphab { get; set; }

      
    }
}