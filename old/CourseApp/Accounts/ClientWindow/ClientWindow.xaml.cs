using CourseApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CourseApp.Accounts.ClientWindow.TerminateContractWindow;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CourseApp.Accounts.ClientWindow
{
    /// <summary>
    /// Логика взаимодействия для ClientWin.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        protected DataLayer.SQLLayer dataLayer;
        protected Client client;

        public ClientWindow(Client client, DataLayer.SQLLayer dataLayer)
        {
            InitializeComponent();
            this.client = client;
            this.dataLayer = dataLayer;
            ClientInfoString.Text = dataLayer.Clients.GetFullInfoById(client.ClientId);
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            var authorize = new AuthorizeWindow(dataLayer);
            authorize.Show();
            this.Close();
        }

        private void ShowTypesOfInsurance(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource = dataLayer.Services.GetServices();
        }

        private void ConcludeContract(object sender, RoutedEventArgs e)
        {
            var contractWindow = new ContractWindow.ContractWindow(client, dataLayer);
            contractWindow.ShowDialog();
        }

        private void CurrentContracts(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource = dataLayer.Contracts.GetFullInfoByClientId(client.ClientId);
        }

        private void TerminateContract(object sender, RoutedEventArgs e)
        {
            if (dataLayer.Contracts.GetContractIdByClientId(client.ClientId).Any())
            {
                var terminateContractWindow = new TerminateContractWindow.TerminateContractWindow(client, dataLayer);
                terminateContractWindow.ShowDialog();
            } 
            else
            {
                MessageBox.Show("На текущего клиента не оформлено ни одного договора.");
            }
        }
    }
}
