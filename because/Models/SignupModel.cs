using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace because.Models
{
    public class SignupModel
    {
        public string HouseHoldName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
		
		public string ParentName { get; set; }
		public IBrowserFile ProfileImage { get; set; }
	}
}

