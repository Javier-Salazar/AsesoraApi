using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AsesoraApi.Models
{
    public class Props
    {
        [Key]
        public String props_program { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "La propiedad no puede tener más de 250 caracteres")]
        public String props_text { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "El apellido no puede tener más de 250 caracteres")]
        public String props_caption { get; set; }

        [StringLength(250, ErrorMessage = "El caption no puede tener más de 250 caracteres")]
        public String props_desc { get; set; }

        [Required]
        public String props_default_value { get; set; }

        [Required]
        public int props_display_order { get; set; }

        [Required]
        [RegularExpression(@"^(N|S){1}$", ErrorMessage = "Valor inválido")]
        public Char props_visible { get; set; }

        [Required]
        public String props_user_code { get; set; }

        [Required]
        public String props_data_type { get; set; }

        [Required]
        public Char props_mask { get; set; }

        [Required]
        public int props_length { get; set; }
    }
}
