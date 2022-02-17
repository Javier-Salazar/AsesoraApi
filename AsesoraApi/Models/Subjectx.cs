using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AsesoraApi.Models
{
    public class Subjectx
    {
        [Key]
        public int subjectx_id { get; set; }

        [Required]
        public String subjectx_code { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public String subjectx_name { get; set; }

        [DisplayFormat(NullDisplayText = "-")]
        public int subjectx_credits { get; set; }

        [Required]
        public String subjectx_career { get; set; }

        [Required]
        public String subjectx_major { get; set; }

        [DisplayFormat(NullDisplayText = "-")]
        public String subjectx_classroom { get; set; }

        [Required]
        [RegularExpression(@"^(A|I){1}$", ErrorMessage = "Estatus inválido")]
        public Char subjectx_status { get; set; }
    }
}
