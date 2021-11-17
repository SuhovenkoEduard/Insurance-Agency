using CourseApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CourseApp.Accounts.AgentWindow
{
    /// <summary>
    /// Логика взаимодействия для AgentWin.xaml
    /// </summary>
    public partial class AgentWindow : Window
    {
        protected DataLayer.DataLayer dataLayer;
        protected object cellValue;
        protected Agent agent;

        public AgentWindow(Agent agent, DataLayer.DataLayer dataLayer)
        {
            InitializeComponent();

            this.agent = agent;
            this.dataLayer = dataLayer;
            AgentInfoString.Text = dataLayer.Agents.GetFullInfoById(agent.AgentId);
        }

        // 1
        private void ShowTypesOfInsurance(object sender, RoutedEventArgs e)
        {
            HideSaveButton();
            DataGrid.ItemsSource = dataLayer.Services.GetServicesByAgentId(agent.AgentId);
        }
        // 2
        private void ContractsInProcessing(object sender, RoutedEventArgs e)
        {
            ShowSaveButton();
            DataGrid.ItemsSource = dataLayer.Contracts.GetContractsInProcessingByAgentId(agent.AgentId);
            for (int i = 0; i < DataGrid.Columns.Count - 1; ++i)
                DataGrid.Columns[i].IsReadOnly = true;
        }
        // 3
        private void ConfirmedContracts(object sender, RoutedEventArgs e)
        {
            HideSaveButton();
            DataGrid.ItemsSource = dataLayer.Contracts.GetConfirmedContractsByAgentId(agent.AgentId);
        }
        // 4
        private void SalaryForTheLastYear(object sender, RoutedEventArgs e)
        {
            HideSaveButton();
            string salaryStr = "Зарплата за последний год: " + 
                dataLayer.Agents.GetSalary(agent.AgentId);
            MessageBox.Show(salaryStr);
        }


        // buttons 
        private void Save(object sender, RoutedEventArgs e)
        {
            if (DataGrid.ItemsSource is IEnumerable<Contract> contracts)
                foreach (Contract contract in contracts)
                    dataLayer.Contracts.Update(contract);

            MessageBox.Show("Сохранение выполнено успешно.");
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            var authorize = new AuthorizeWindow(dataLayer);
            authorize.Show();
            this.Close();
        }


        // menu
        private void ShowSaveButton() 
        {
            DataGrid.IsReadOnly = false;
            SaveButton.Visibility = Visibility.Visible;
        }
        private void HideSaveButton()
        {
            DataGrid.IsReadOnly = true;
            SaveButton.Visibility = Visibility.Hidden;
        }
    }
}
