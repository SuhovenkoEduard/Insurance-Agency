using CourseApp.Accounts.AgentWindow;
using CourseApp.Accounts.ClientWindow;
using CourseApp.Accounts.ManagerWindow;
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

namespace CourseApp.AuthorizeWindows.SignIn
{
    /// <summary>
    /// Логика взаимодействия для SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        protected DataLayer.DataLayer dataLayer;
        public SignInWindow(DataLayer.DataLayer dataLayer)
        {
            InitializeComponent();
            this.dataLayer = dataLayer;
        }

        public void SignIn(User user)
        {
            user.UserId = dataLayer.Users.GetId(user);

            Role role = dataLayer.Users.GetRole(user.UserId);
            switch (role)
            {
                case Role.Client:
                    {
                        var clientWindow = new ClientWindow(dataLayer.Clients.GetClientByUserId(user.UserId), dataLayer);
                        clientWindow.Show();
                        break;
                    }
                case Role.Agent:
                    {
                        var agentWindow = new AgentWindow(dataLayer.Agents.GetAgentByUserId(user.UserId), dataLayer);
                        agentWindow.Show();
                        break;
                    }
                case Role.Manager:
                    {
                        var managerWindow = new ManagerWindow(dataLayer.Managers.GetManagerByUserId(user.UserId), dataLayer);
                        managerWindow.Show();
                        break;
                    }
            }
        }

        private void CangeVisibleOfPassword(object sender, RoutedEventArgs e)
        {
            if (TToggle.IsChecked == true)
            {
                TPassword.Visibility = Visibility.Hidden;
                TTextPassword.Visibility = Visibility.Visible;
                TTextPassword.Text = TPassword.Password;
            }
            else
            {
                TTextPassword.Visibility = Visibility.Hidden;
                TPassword.Visibility = Visibility.Visible;
                TPassword.Password = TTextPassword.Text;
            }
        }
        private void SignIn(object sender, RoutedEventArgs e)
        {
            string login = TLogin.Text;
            string password;

            if (TToggle.IsChecked == true)
                password = TTextPassword.Text;
            else
                password = TPassword.Password;

            User user = new User(login, password);
            
            if (dataLayer.Users.Contains(user))
            {

                SignIn(user);
                this.Close();
            }
            else
            {
                TLogin.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                TPassword.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                TTextPassword.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            }
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            var authorize = new AuthorizeWindow(dataLayer);
            authorize.Show();
            this.Close();
        }
    }
}
