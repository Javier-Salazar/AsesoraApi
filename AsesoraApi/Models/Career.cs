using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AsesoraApi.Models
{
    public class Career
    {
        [Key]
        public String career_code { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public String career_name { get; set; }

        [Required]
        public String career_school { get; set; }

        [Required]
        [RegularExpression(@"^(A|I){1}$", ErrorMessage = "Estatus inválido")]
        public Char career_status { get; set; }
    }
}
