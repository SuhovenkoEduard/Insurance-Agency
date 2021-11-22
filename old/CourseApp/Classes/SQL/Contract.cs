using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Classes
{
	public class Contract
	{
		public int ContractId { get; set; }
		public string DateOfConclusion { get; set; }
		public int ServiceId { get; set; }
		public int ClientId { get; set; }
		public int AgentId { get; set; }
		public int Cost { get; set; }
		public int InsurancePeriod { get; set; }
		public string Comment { get; set; }
		public int StatusId { get; set; }

		public Contract() { }

		public Contract(string dateOfConclusion, int serviceId, int clientId, int agentId, int cost, int insurancePeriod, string comment, int statusId)
		{
			DateOfConclusion = dateOfConclusion;
			ServiceId = serviceId;
			ClientId = clientId;
			AgentId = agentId;
			Cost = cost;
			InsurancePeriod = insurancePeriod;
			Comment = comment;
			StatusId = statusId;
		}

		public override string ToString()
		{
			return ContractId + " " +
				DateOfConclusion + " " +
				ServiceId + " " +
				ClientId + " " +
				AgentId + " " +
				Cost + " " +
				InsurancePeriod + " " +
				Comment + " " +
				StatusId;
		}
	}
}
