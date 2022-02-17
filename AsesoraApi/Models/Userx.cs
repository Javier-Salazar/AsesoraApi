using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AsesoraApi.Models
{
    public class UserxBase
    {
        [Key]
        public String userx_code { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        [StringLength(30, ErrorMessage = "El nombre no puede tener más de 30 caracteres")]
        public String userx_name { get; set; }

        [Required]
        [Display(Name = "Apellido")]
        [StringLength(30, ErrorMessage = "El apellido no puede tener más de 30 caracteres")]
        public String userx_lastname { get; set; }

        [Display(Name = "Apellido")]
        [StringLength(30, ErrorMessage = "El apellido no puede tener más de 30 caracteres")]
        public String userx_mother_lastname { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo electrónico")]
        public String userx_email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public String userx_password { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Teléfono")]
        public String userx_phone { get; set; }

        [Required]
        [RegularExpression(@"^(A|N|S){1}$", ErrorMessage = "Tipo de usuario inválido")]
        public Char userx_type { get; set; }

        [Required]
        [RegularExpression(@"^(N|S){1}$", ErrorMessage = "Estatus inválido")]
        public Char userx_istmp_password { get; set; }

        [DataType(DataType.Date)]
        public DateTime userx_date { get; set; }

        [Required]
        [RegularExpression(@"^(N|S){1}$", ErrorMessage = "Estatus inválido")]
        public Char userx_islockedout { get; set; }

        [DataType(DataType.Date)]
        public DateTime userx_islockedout_date { get; set; }

        [DataType(DataType.Date)]
        public DateTime userx_islockedout_enable_date { get; set; }

        [DataType(DataType.Date)]
        public DateTime userx_last_login_date { get; set; }

        [DataType(DataType.Date)]
        public DateTime userx_lastfailed_login_date { get; set; }

        [Required]
        [RegularExpression(@"^(A|I){1}$", ErrorMessage = "Estatus inválido")]
        public Char userx_status { get; set; }
    }

    public class UserxImage : UserxBase
    {
        public Byte[] userx_image { get; set; }
    }

    public class UserxImageDto
    {
        public String userx_image { get; set; }
    }

    public class Userx
    {
        public String userx_code { get; set; }
        public String userx_name { get; set; }
        public String userx_lastname { get; set; }
        public String userx_mother_lastname { get; set; }
        public String userx_email { get; set; }
        public String userx_password { get; set; }
        public String userx_phone { get; set; }
        public Char userx_type { get; set; }
        public Char userx_istmp_password { get; set; }
        public DateTime userx_date { get; set; }
        public Char userx_islockedout { get; set; }
        public DateTime userx_islockedout_date { get; set; }
        public DateTime userx_islockedout_enable_date { get; set; }
        public DateTime userx_last_login_date { get; set; }
        public DateTime userx_lastfailed_login_date { get; set; }
        public Char userx_status { get; set; }
        public String userx_image { get; set; }
    }
}
