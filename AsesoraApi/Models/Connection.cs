using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AsesoraApi.Models
{
    public class Connection
    {
        [Key]
        public String con_DB { get; set; }

        [Required]
        public String con_svr { get; set; }

        [Required]
        public String con_url { get; set; }

        [Required]
        [RegularExpression(@"^(A|I){1}$", ErrorMessage = "Estatus inválido")]
        public Char con_status { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Los comentarios no pueden tener más de 200 caracteres")]
        public String con_comments { get; set; }
    }
}
