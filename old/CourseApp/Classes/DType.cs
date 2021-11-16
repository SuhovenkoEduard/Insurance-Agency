using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Classes
{
	public class DType
	{
		public int DTypeId { get; set; }
		public string Title { get; set; }

		public DType() { }

		public DType(string title)
		{
			Title = title;
		}

		public override string ToString()
		{
			return DTypeId + " " +
				Title;
		}
	}
}
