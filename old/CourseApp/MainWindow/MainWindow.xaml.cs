using CourseApp.AuthorizeWindows.SignIn;
using CourseApp.Classes;
using CourseApp.DataLayer;
using CourseApp.DataLayer.Adapters;
using System.Configuration;
using System.Windows;

namespace CourseApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected IAdapter adapter;
        protected SQLLayer dataLayer;

        public MainWindow()
        {
            InitializeComponent();

            bool isMongo = true;
            if (isMongo)
            {
                DataLayer.MongoDB mongo = new DataLayer.MongoDB(ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString);
                IAdapter adapter = new MongoAdapter(mongo);
                dataLayer = new NoSQLLayer(adapter);
            } else
            {
                AccessDB access = new AccessDB(ConfigurationManager.ConnectionStrings["Access"].ConnectionString);
                adapter = new AccessAdapter(access);
                dataLayer = new SQLLayer(adapter);
            }
            
            var signInWindow = new SignInWindow(dataLayer);
            signInWindow.Show();

            User userClient = new User("web", "qwerty");
            signInWindow.SignIn(userClient);

            User userAgent = new User("local", "princess");
            signInWindow.SignIn(userAgent);

            User userManager = new User("admin", "admin");
            signInWindow.SignIn(userManager);

            signInWindow.Close();
            this.Close();
        }

        private void Authorize(object sender, RoutedEventArgs e)
        {
            var authorizeWindow = new AuthorizeWindow(dataLayer);
            authorizeWindow.Show();
            this.Close();
        }
    }
}
