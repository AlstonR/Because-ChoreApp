using Firebase.Auth;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace because.Data
{
    public class CurrentUser
    {
        public string Email { get; set; }
        public string HouseHoldName { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public int Coins { get; set; }
	}
}
