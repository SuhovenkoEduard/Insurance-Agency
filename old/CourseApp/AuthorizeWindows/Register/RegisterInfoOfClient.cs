using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CourseApp.Classes
{
    class RegisterInfoOfClient : IDataErrorInfo
    {
        protected DataLayer.SQLLayer dataLayer;

        public string Number { get; set; }
        public string ClientName { get; set; }
        public string Adress { get; set; }
        public string Login { get; set; }
        public string Password1 { get; set; }
        public string Password2 { get; set; }

        public RegisterInfoOfClient(DataLayer.SQLLayer dataLayer)
        {
            Number = string.Empty;
            ClientName = string.Empty;
            Adress = string.Empty;
            Login = string.Empty;
            Password1 = string.Empty;
            Password2 = string.Empty;

            this.dataLayer = dataLayer;
        }

        public bool IsValid()
        {
            bool result = true;
            Type type = typeof(RegisterInfoOfClient);
            var properties = type.GetProperties();
            foreach (var property in properties)
                result &= (Validate(property.Name) == string.Empty);
            return result;
        }

        public string this[string columnName] => Validate(columnName);

        public string Validate(string name)
        {
            string error = string.Empty;
            switch (name)
            {
                case "Number":
                    {
                        string pattern = @"\+[0-9]{12}";
                        if (Number == string.Empty)
                            error = " ";
                        else if (!Regex.IsMatch(Number, pattern))
                            error = "Некорректный номер";
                        break;
                    }
                case "ClientName":
                    {

                        break;
                    }
                case "Adress":
                    {
                        break;
                    }
                case "Login":
                    {
                        if (Login == string.Empty)
                            error = " ";
                        else if (dataLayer.Users.ContainsLogin(Login))
                            error = "Логин уже занят";
                        break;
                    }
                case "Password1":
                    {
                        if (Password1 == string.Empty)
                            error = " ";
                        else if (Password1 != Password2 && Password2 != string.Empty)
                            error = "Пароли не совпадают";
                        break;
                    }

                case "Password2":
                    {
                        if (Password2 == string.Empty)
                            error = " ";
                        else if (Password1 != Password2)
                            error = "Пароли не совпадают";
                        break;
                    }
            }

            return error;
        }

        public string Error => throw new NotImplementedException();
    }
}
