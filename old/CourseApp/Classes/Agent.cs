using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Classes
{
	public class Agent
	{
		public int AgentId { get; set; }
		public int WorkerId { get; set; }
		public string PersonalData { get; set; }

		public Agent() { }

		public Agent(int workerId, string personalData)
		{
			WorkerId = workerId;
			PersonalData = personalData;
		}

		public override string ToString()
		{
			return AgentId + " " +
				WorkerId + " " +
				PersonalData;
		}
	}
}
