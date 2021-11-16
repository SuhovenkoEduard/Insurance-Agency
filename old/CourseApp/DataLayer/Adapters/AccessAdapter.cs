using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseApp.DataLayer.Adapters
{
    public class AccessAdapter : IAdapter
    {
        protected AccessDB access;

        public AccessAdapter(AccessDB access)
        {
            this.access = access;
        }

        public void Add<T>(T item)
            where T: class, new()
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            try
            {
                using (OleDbConnection connection = access.GetConnection())
                {
                    connection.Open();
                    string sql = "INSERT INTO [" + type.Name + "] (";

                    for (int i = 1; i < properties.Length; ++i)
                        sql += "[" + properties[i].Name + "]" + (i != properties.Length - 1 ? ", " : ")");

                    sql += " VALUES (";

                    for (int i = 1; i < properties.Length; ++i)
                        sql += "?" + (i != properties.Length - 1 ? ", " : ")");

                    OleDbCommand command = new OleDbCommand(sql, connection);

                    for (int i = 1; i < properties.Length; ++i)
                        command.Parameters.AddWithValue(properties[i].Name, properties[i].GetValue(item));

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Запрос \"Add()\" в таблице " + type.Name + " не удался.\n" +
                    exception.Message);
            }
        }
        public void Remove<T>(T item)
            where T : class, new()
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            try
            {
                using (OleDbConnection connection = access.GetConnection())
                {
                    connection.Open();
                    string sql = "DELETE FROM [" + type.Name + "] WHERE (" + properties[0].Name + "=?)";
                    OleDbCommand command = new OleDbCommand(sql, connection);
                    command.Parameters.AddWithValue(properties[0].Name, properties[0].GetValue(item));
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Запрос \"Remove()\" в таблице " + type.Name + " не удался.\n" +
                    exception.Message);
            }
        }

        public void Update<T>(T item)
            where T : class, new()
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            try
            {
                using (OleDbConnection connection = access.GetConnection())
                {
                    connection.Open();
                    string sql = "UPDATE [" + type.Name + "] SET ";
                    for (int i = 1; i < properties.Length; ++i)
                        sql += "[" + properties[i].Name + "]=?" + (i != properties.Length - 1 ? ", " : "");
                    sql += " WHERE ([" + properties[0].Name + "]=?)";

                    OleDbCommand command = new OleDbCommand(sql, connection);
                    for (int i = 1; i < properties.Length; ++i)
                        command.Parameters.AddWithValue(properties[i].Name, properties[i].GetValue(item));
                    command.Parameters.AddWithValue(properties[0].Name, properties[0].GetValue(item));
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Запрос \"Update()\" в таблице " + type.Name + " не удался.\n" +
                    exception.Message);
            }
        }

        public List<T> GetAll<T>()
            where T : class, new()
        {
            List<T> result = new List<T>();
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            try
            {
                using (OleDbConnection connection = access.GetConnection())
                {
                    connection.Open();
                    string sql = "SELECT * FROM [" + type.Name + "]";

                    OleDbCommand command = new OleDbCommand(sql, connection);
                    OleDbDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        T value = new T();
                        for (int i = 0; i < properties.Length; ++i)
                            properties[i].SetValue(value, reader.GetValue(i));
                        result.Add(value);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Запрос \"GetAll()\" в таблице " + type.Name + " не удался.\n" +
                    exception.Message);
            }

            return result;
        }
    }
}
