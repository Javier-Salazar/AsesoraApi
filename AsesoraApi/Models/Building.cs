using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AsesoraApi.Models
{
    public class Building
    {
        [Key]
        public String building_code { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public String building_name { get; set; }

        [Required]
        public String building_school { get; set; }

        [Required]
        [RegularExpression(@"^(A|I){1}$", ErrorMessage = "Estatus inválido")]
        public Char building_status { get; set; }
    }
}
