using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AsesoraApi.Models
{
    public class Log
    {
        [Key]
        public int log_id { get; set; }

        [Required]
        [Timestamp]
        public DateTime log_timestamp { get; set; }

        [Required]
        public String log_user { get; set; }

        [Required]
        public String log_prog { get; set; }

        [Required]
        [RegularExpression(@"^(A|D|I|M|P|S){1}$", ErrorMessage = "Acción inválida")]
        public Char log_action { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "La información no puede tener más de 250 caracteres")]
        public String log_info { get; set; }

        [Required]
        public String log_key { get; set; }
    }
}
