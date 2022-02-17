using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AsesoraApi.Models
{
    public class Student
    {
        [Key]
        public String student_code { get; set; }

        [Required]
        public String student_school { get; set; }

        [Required]
        public String student_career { get; set; }

        [Required]
        public String student_major { get; set; }

        [Required]
        [Range (1, 12)]
        public int student_semester { get; set; }

        [Required]
        [RegularExpression(@"^(A|I){1}$", ErrorMessage = "Estatus inválido")]
        public Char student_status { get; set; }
    }
}
