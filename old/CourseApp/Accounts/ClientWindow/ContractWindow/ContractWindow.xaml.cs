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

namespace CourseApp.Accounts.ClientWindow.ContractWindow
{
    /// <summary>
    /// Логика взаимодействия для ContractWindow.xaml
    /// </summary>
    public partial class ContractWindow : Window
    {
        protected DataLayer.DataLayer dataLayer;
        protected Client client;

        protected Filial filial;
        protected Departament departament;
        protected DType dType;
        protected Agent agent;
        protected Service service;
        protected int insuransePeriod;
        protected int cost;
        protected string comment;

        public ContractWindow(Client client, DataLayer.DataLayer dataLayer)
        {
            InitializeComponent();
            this.client = client;

            filial = new Filial();
            departament = new Departament();
            dType = new DType();
            agent = new Agent();
            service = new Service();

            comment = "-";
            this.dataLayer = dataLayer;
        }

        private void Exit(object sender, RoutedEventArgs e) => this.Close();

        
        // filial
        private void EnterFilial(object sender, EventArgs e)
        {
            FilialComboBox.ItemsSource = dataLayer.Filials.GetCities();
        }
        private void SelectFilial(object sender, EventArgs e)
        {
            if (FilialComboBox.SelectedItem != null)
            {
                filial = dataLayer.Filials.GetFilialByCity((string) FilialComboBox.SelectedItem);

                ClearDepartament();
                ClearAgent();
                ClearService();
            }
        }


        // departament
        private void EnterDepartament(object sender, EventArgs e)
        {
            DepartamentComboBox.ItemsSource = dataLayer.Departaments.GetDepartamentTitlesByFilial(filial);
        }
        private void SelectDepartament(object sender, EventArgs e)
        {
            if (DepartamentComboBox.SelectedItem != null)
            {
                departament = dataLayer.Departaments.GetDepartamentByTitleAndFilialId
                (
                    (string) DepartamentComboBox.SelectedItem,
                    filial.FilialId
                );

                dType = dataLayer.DTypes.GetDtypeById(departament.DTypeID);

                ClearAgent();
                ClearService();

                UpdateCost();
            }
        }

        // agent
        private void EnterAgent(object sender, EventArgs e)
        {
            AgentComboBox.ItemsSource = dataLayer.Agents.GetFullNamesByDepartamentId(departament.DepartamentId);
        }
        private void SelectAgent(object sender, EventArgs e)
        {
            if (AgentComboBox.SelectedItem != null)
            {
                agent = dataLayer.Agents.GetAgentByFullnameAndDepartamentId
                (
                    (string)AgentComboBox.SelectedItem,
                    departament.DepartamentId
                );
            }
        }


        // service
        private void EnterService(object sender, EventArgs e)
        {
            ServiceComboBox.ItemsSource = dataLayer.Services.GetNamesByDtypeId(dType.DTypeId);
        }
        private void SelectService(object sender, EventArgs e)
        {
            if (ServiceComboBox.SelectedItem != null)
            {
                service = dataLayer.Services.GetServiceByTitleAndDTypeId
                (
                    (string) ServiceComboBox.SelectedItem,
                    dType.DTypeId
                );

                UpdateCost();
            }
        }


        // period
        private void SelectPeriod(object sender, EventArgs e)
        {
            if (PeriodComboBox.SelectedItem != null)
            {
                insuransePeriod = int.Parse(((TextBlock)PeriodComboBox.SelectedItem).Text);
                UpdateCost();
            }
        }
        

        // comment
        private void CommentChanged(object sender, EventArgs e)
        {
            comment = CommentTextBox.Text;
        }


        // registrate button
        private void RegistrateContract(object sender, RoutedEventArgs e)
        {

            if (service.ServiceId == 0 ||
                client.ClientId == 0 || 
                agent.AgentId == 0 || 
                cost == 0 || 
                insuransePeriod == 0)
            {
                MessageBox.Show("Заполните все поля.");
            } 
            else
            {
                dataLayer.Contracts.Add
                (
                    new Contract
                    (
                        GetDate(),
                        service.ServiceId,
                        client.ClientId,
                        agent.AgentId,
                        cost,
                        insuransePeriod,
                        comment,
                        2
                    )
                );

                MessageBox.Show("Договор отправлен на рассмотрение, ожидайте подтверждения.");
                this.Close();
            }
        }

        private void ClearDepartament()
        {
            DepartamentComboBox.Text = "";
            DepartamentComboBox.ItemsSource = null;
            departament = new Departament();
        }
        private void ClearAgent()
        {
            AgentComboBox.Text = "";
            AgentComboBox.ItemsSource = null;
            agent = new Agent();
        }
        private void ClearService()
        {
            ServiceComboBox.Text = "";
            ServiceComboBox.ItemsSource = null;
            service = new Service();

            UpdateCost();
        }
        private void UpdateCost()
        {
            cost = insuransePeriod * service.Cost;
            ServiceCostTextBlock.Text = service.Cost.ToString();
            ContractCost.Text = cost.ToString();
        }
        private string GetDate()
        {
            Random random = new Random();
            string day = (random.Next() % 31 + 1).ToString();
            string month = (random.Next() % 12 + 1).ToString();
            string year = (random.Next() % 22 + 2000).ToString();

            if (day.Length < 2)
                day = "0" + day;

            if (month.Length < 2)
                month = "0" + month;

            while (year.Length < 4)
                year = "0" + year;

            return day + ":" + month + ":" + year;
        }
    }
}
