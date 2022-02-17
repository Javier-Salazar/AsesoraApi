using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AsesoraApi.Models
{
    public class Classroom
    {
        [Key]
        public String classroom_code { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        [StringLength(30, ErrorMessage = "El nombre no puede tener más de 30 caracteres")]
        public String classroom_name { get; set; }

        [Required]
        public String classroom_building { get; set; }
    }
}
