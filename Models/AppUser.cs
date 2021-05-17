using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunMath.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int Age { get; set; }
        public bool IsAdmin { get; set; }
    }
}
