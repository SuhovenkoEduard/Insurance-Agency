using CourseApp.AuthorizeWindows.Register;
using CourseApp.AuthorizeWindows.SignIn;
using System.Windows;

namespace CourseApp
{
    /// <summary>
    /// Логика взаимодействия для AuthorizeWindow.xaml
    /// </summary>
    public partial class AuthorizeWindow : Window
    {
        protected DataLayer.SQLLayer dataLayer;
        public AuthorizeWindow(DataLayer.SQLLayer dataLayer)
        {
            InitializeComponent();
            this.dataLayer = dataLayer;
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            var register = new RegisterWindow(dataLayer);
            register.Show();
            this.Close();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            var signIn = new SignInWindow(dataLayer);
            signIn.Show();
            this.Close();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
