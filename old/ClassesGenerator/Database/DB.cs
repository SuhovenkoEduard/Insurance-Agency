using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;

namespace ClassesGenerator.Database
{
    static class DB
    {
        static private readonly string connectStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Projects\\Sem_4\\OOP Kurs\\CourseApp\\db\\CourseDB.mdb";
        static public OleDbConnection GetConnection() => new OleDbConnection(connectStr);
    }
}
