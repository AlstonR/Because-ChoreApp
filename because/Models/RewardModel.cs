using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace because.Models
{
	public class RewardModel
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string AssigneeName { get; set; }
		public int Coins { get; set; }
		public IBrowserFile Image { get; set; }
		public string ImageString { get; set; }
		public string Status { get; set; }
	}
}
