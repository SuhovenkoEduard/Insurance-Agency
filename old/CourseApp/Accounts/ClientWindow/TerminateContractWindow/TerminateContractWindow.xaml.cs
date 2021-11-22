using CourseApp.Classes;
using System;
using System.Collections.Generic;
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

namespace CourseApp.Accounts.ClientWindow.TerminateContractWindow
{
    /// <summary>
    /// Логика взаимодействия для TerminateContractWindow.xaml
    /// </summary>
    public partial class TerminateContractWindow : Window
    {
        protected DataLayer.SQLLayer dataLayer;
        protected Client client;
        protected Contract contract;
        
        public TerminateContractWindow(Client client, DataLayer.SQLLayer dataLayer)
        {
            InitializeComponent();
            this.client = client;

            this.dataLayer = dataLayer;
            TerminateContractsDataGrid.ItemsSource = dataLayer.Contracts.GetFullInfoByClientId(client.ClientId);
        }
        
        private void EnterContract(object sender, EventArgs e)
        {
            ContractIdComboBox.ItemsSource = dataLayer.Contracts.GetContractIdByClientId(client.ClientId);
        }
        private void SelectContract(object sender, EventArgs e)
        {
            if (ContractIdComboBox.SelectedItem != null)
            {
                int contractId = (int) ContractIdComboBox.SelectedItem;
                contract = dataLayer.Contracts.GetContractById(contractId);
            }
        }

        private void TerminateContract(object sender, RoutedEventArgs e)
        {
            if (contract != null)
            {
                MessageBox.Show("Договор расторгнут.");
                dataLayer.Contracts.Remove(contract);
                this.Close();
            } 
            else
            {
                MessageBox.Show("Выберите номер договора.");
            }
        }

        private void Back(object sender, RoutedEventArgs e) => this.Close();
    }
}
