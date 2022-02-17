using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AsesoraApi.Models
{
    public class Advisor
    {
        [Key]
        public String advisor_code { get; set; }

        [Required]
        [Range(1, 5)]
        public int advisor_rating { get; set; }

        [DisplayFormat(NullDisplayText = "-")]
        public String advisor_comments { get; set; }

        [Required]
        [RegularExpression(@"^(A|I){1}$", ErrorMessage = "Estatus inválido")]
        public Char advisor_status { get; set; }
    }
}
