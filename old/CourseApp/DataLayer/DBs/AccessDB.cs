using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.DataLayer
{
    public class AccessDB
    {
        protected string connectStr;

        public AccessDB(string connectStr)
        {
            this.connectStr = connectStr;
        }

        public OleDbConnection GetConnection() => new OleDbConnection(connectStr);
    }
}
