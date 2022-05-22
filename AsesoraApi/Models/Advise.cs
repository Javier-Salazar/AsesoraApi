using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AsesoraApi.Models
{
    public class Advise
    {
        [Key]
        public int advise_code { get; set; }

        [Required]
        public String advise_student { get; set; }

        [Required]
        [Display(Name = "Tema")]
        [StringLength(30, ErrorMessage = "El nombre del tema no puede tener más de 30 caracteres")]
        public String advise_topic { get; set; }

        [Required]
        public int advise_subject { get; set; }

        [Required]
        public String advise_advisor { get; set; }

        [Required]
        public String advise_school { get; set; }

        [Required]
        public String advise_building { get; set; }

        [Required]
        public String advise_classroom { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime advise_date_request { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime advise_date_start { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime advise_date_ends { get; set; }

        [Required]
        [RegularExpression(@"^(P|V){1}$", ErrorMessage = "Modalidad inválido")]
        public Char advise_modality { get; set; }

        [Required]
        public String advise_url { get; set; }

        [DisplayFormat(NullDisplayText = "-")]
        public String advise_comments { get; set; }
        
        [Range(0, 5)]
        public int advise_rating { get; set; }

        [Required]
        [RegularExpression(@"^(A|C|S){1}$", ErrorMessage = "Estatus inválido")]
        public Char advise_status { get; set; }
    }
}
