using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Classes
{
	public class Departament
	{
		public int DepartamentId { get; set; }
		public int DTypeID { get; set; }
		public int FilialId { get; set; }

		public Departament() { }

		public Departament(int dTypeID, int filialId)
		{
			DTypeID = dTypeID;
			FilialId = filialId;
		}

		public override string ToString()
		{
			return DepartamentId + " " +
				DTypeID + " " +
				FilialId;
		}
	}
}
