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

namespace CourseApp.Accounts.ManagerWindow
{
    /// <summary>
    /// Логика взаимодействия для ManagerWin.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        protected DataLayer.DataLayer dataLayer;
        protected Manager manager;

        public ManagerWindow(Manager manager, DataLayer.DataLayer dataLayer)
        {
            InitializeComponent();
            this.manager = manager;

            this.dataLayer = dataLayer;
            ManagerInfoString.Text = dataLayer.Managers.GetFullInfoById(manager.ManagerId);
        }

        private void ShowTypesOfInsurance(object sender, RoutedEventArgs e)
        {
            ShowSaveButton();
            DataGrid.ItemsSource = dataLayer.Services.GetServices();
            for (int i = 0; i < DataGrid.Columns.Count - 1; ++i)
                DataGrid.Columns[i].IsReadOnly = true;
        }

        private void ReportOnContracts(object sender, RoutedEventArgs e)
        {
            HideSaveButton();
            DataGrid.ItemsSource = dataLayer.Contracts.GetReportByManagerId(manager.ManagerId);
        }

        private void ListOfWorkers(object sender, RoutedEventArgs e)
        {
            ShowSaveButton();
            DataGrid.ItemsSource = dataLayer.Workers.GetWorkersByDepartamentId(
                dataLayer.Managers.GetDepartamentIdByManagerId(manager.ManagerId));
            DataGrid.Columns[0].IsReadOnly = true;
            DataGrid.Columns[1].IsReadOnly = true;
            DataGrid.Columns[3].IsReadOnly = true;
            DataGrid.Columns[4].IsReadOnly = true;
        }

        private void SalaryForTheLastYear(object sender, RoutedEventArgs e)
        {
            HideSaveButton();
            MessageBox.Show("Зарплата за последний год: " +
                dataLayer.Managers.GetSalary(manager.ManagerId));
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if (DataGrid.ItemsSource is IEnumerable<Service> services)
                foreach (Service service in services)
                    dataLayer.Services.Update(service);

            if (DataGrid.ItemsSource is IEnumerable<Worker> workers)
                foreach (Worker worker in workers)
                    dataLayer.Workers.Update(worker);

            MessageBox.Show("Сохранение данных выполнено успешно.");
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
