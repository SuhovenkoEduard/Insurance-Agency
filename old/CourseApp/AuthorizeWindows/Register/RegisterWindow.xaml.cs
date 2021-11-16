using CourseApp.Accounts.ClientWindow;
using CourseApp.Classes;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CourseApp.AuthorizeWindows.Register
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public int CodeOfUser { get; set; }
        protected DataLayer.DataLayer dataLayer;
        private RegisterInfoOfClient registerInfo;
        
        public RegisterWindow(DataLayer.DataLayer dataLayer)
        {
            InitializeComponent();
            registerInfo = new RegisterInfoOfClient(dataLayer);
            this.DataContext = registerInfo;
            this.dataLayer = dataLayer;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            var authorize = new AuthorizeWindow(dataLayer);
            authorize.Show();
            this.Close();
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            if (registerInfo.IsValid())
            {
                RegPopUp.Visibility = Visibility.Hidden;

                User user = new User(registerInfo.Login, registerInfo.Password1);
                dataLayer.Users.Add(user);

                CodeOfUser = dataLayer.Users.GetId(user);
                
                Client client = new Client(registerInfo.Number, registerInfo.ClientName, registerInfo.Adress, CodeOfUser);
                dataLayer.Clients.Add(client);

                var clientWindow = new ClientWindow(dataLayer.Clients.GetClientByUserId(user.UserId), dataLayer);
                clientWindow.Show();
                this.Close();
            }
            else
            {
                RegPopUpText.Visibility = Visibility.Visible;
                RegPopUp.IsOpen = true;
            }
        }
        
        private void PopUpLeave(object sender, MouseEventArgs e)
        {
            if (RegPopUpText.Visibility == Visibility.Visible)
                RegPopUp.IsOpen = false;
        }
    }
}
