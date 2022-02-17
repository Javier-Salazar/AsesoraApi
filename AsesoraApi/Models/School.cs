using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AsesoraApi.Models
{
    public class School
    {
        [Key]
        public String school_code { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public String school_name { get; set; }

        [Display(Name = "Calle")]
        [StringLength(30, ErrorMessage = "La calle no puede tener más de 30 caracteres")]
        public String school_street { get; set; }

        [Display(Name = "Número interior")]
        public String school_num_int { get; set; }

        [Display(Name = "Número exterior")]
        public String school_num_ext { get; set; }

        [Display(Name = "Colonia")]
        [StringLength(30, ErrorMessage = "La colonia no puede tener más de 30 caracteres")]
        public String school_suburb { get; set; }

        [Display(Name = "Ciudad")]
        [StringLength(30, ErrorMessage = "La ciudad no puede tener más de 30 caracteres")]
        public String school_county { get; set; }

        [Display(Name = "Estado")]
        [StringLength(30, ErrorMessage = "El estado no puede tener más de 30 caracteres")]
        public String school_state { get; set; }

        [Display(Name = "País")]
        [StringLength(30, ErrorMessage = "El país no puede tener más de 30 caracteres")]
        public String school_country { get; set; }

        [Display(Name = "Código Postal")]
        [StringLength(10, ErrorMessage = "El código postal no puede tener más de 10 caracteres")]
        public String school_cp { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Teléfono")]
        public String school_phone { get; set; }

        [Required]
        [Display(Name = "RFC")]
        [StringLength(13, ErrorMessage = "El nombre no puede tener más de 13 caracteres")]
        public String school_rfc { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo electrónico")]
        public String school_email { get; set; }

        [Required]
        [RegularExpression(@"^(A|I){1}$", ErrorMessage = "Estatus inválido")]
        public Char school_status { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime school_date { get; set; }
    }
}
