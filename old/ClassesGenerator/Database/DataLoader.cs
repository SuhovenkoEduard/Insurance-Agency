using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Text;

namespace ClassesGenerator.Database
{
    static class DataLoader
    {
        public static List<TableInfo> Load() => GetTables(GetTableNames());
        public static List<string> GetTableNames()
        {
            List<string> list = new List<string>();

            var connection = DB.GetConnection();
            connection.Open();

            DataTable dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            foreach (DataRow item in dt.Rows)
                list.Add(item["TABLE_NAME"].ToString());
            connection.Close();

            return list;
        }
        static List<TableInfo> GetTables(List<string> names)
        {
            List<TableInfo> tables = new List<TableInfo>();
            foreach (var name in names)
                tables.Add(GetTable(name));
            return tables;
        }
        public static TableInfo GetTable(string name)
        {
            var connection = DB.GetConnection();
            connection.Open();

            string sql = "SELECT * FROM [" + name + "];";
            var command = new OleDbCommand(sql, connection);
            var reader = command.ExecuteReader();
            var schema = reader.GetSchemaTable();

            var table = new TableInfo(name);
            foreach (DataRow row in schema.Rows)
            {
                MyType myType = (row.Field<Type>("DataType").Name == "Int32" ? MyType.Int : MyType.String);

                var column = new ColumnInfo(
                    row.Field<string>("ColumnName"),
                    myType,
                    row.Field<int>("ColumnSize")
                );

                table.Add(column);
            }

            connection.Close();

            return table;
        }
    }
}
