using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        //public int UserId { get; set; } // FOREIGH KEY
        //public User? User { get; set; }
    }

}
