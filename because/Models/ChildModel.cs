using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace because.Models
{
	public class ChildModel
	{
		public string Name { get; set; }
		public IBrowserFile ProfileImage { get; set; }
	}
}
