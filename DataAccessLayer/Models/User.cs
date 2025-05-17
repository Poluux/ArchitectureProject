using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public int IdCard { get; set; }
        public double Balance { get; set; }
        public int Quota { get; set; }
    }
}
