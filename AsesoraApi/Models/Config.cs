using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AsesoraApi.Models
{
    public class Config
    {
        [Key]
        public String cfg_clave { get; set; }

        [Required]
        [RegularExpression(@"^(S|U){1}$", ErrorMessage = "Grupo inválido")]
        public Char cfg_grupo { get; set; }

        [Required]
        public String cfg_tabla { get; set; }

        [Required]
        public String cfg_value { get; set; }

        [Required]
        [StringLength(80, ErrorMessage = "La descrpción no puede tener más de 80 caracteres")]
        public String cfg_desc { get; set; }
    }
}
