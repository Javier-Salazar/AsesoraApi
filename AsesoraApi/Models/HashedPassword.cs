using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsesoraApi.Models
{
    public class HashedPassword
    {
        public String password { get; set; }
        public String Salt { get; set; }
    }
}
