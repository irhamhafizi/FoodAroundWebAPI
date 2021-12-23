using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Register
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Pin { get; set; }
        public string RepeatPin { get; set; }
    }
}
