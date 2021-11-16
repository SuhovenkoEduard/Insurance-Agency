using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Classes
{
	public class Status
	{
		public int StatusId { get; set; }
		public string Title { get; set; }

		public Status() { }

		public Status(string title)
		{
			Title = title;
		}

		public override string ToString()
		{
			return StatusId + " " +
				Title;
		}
	}
}
