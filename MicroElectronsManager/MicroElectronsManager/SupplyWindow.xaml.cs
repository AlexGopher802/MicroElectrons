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
using RestSharp;
using MicroElectronsManager.Logics;
using MicroElectronsManager.Models;

namespace MicroElectronsManager
{
    /// <summary>
    /// Логика взаимодействия для SupplyWindow.xaml
    /// </summary>
    public partial class SupplyWindow : Window
    {
        public UserData user;
        private RestClient apiClient = ApiBuilder.GetInstance();

        public SupplyWindow()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Owner.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TbWelcome.Text = $"Здравствуйте, {user.FirstName} {user.Patronymic}";
            DataGridSupplyWrite();
        }

        public void DataGridSupplyWrite()
        {
            try
            {
                DataGridSupply.ClearValue(ItemsControl.ItemsSourceProperty);
                var response = apiClient.Get<List<SupplyData>>(new RestRequest("supply/alllist"));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(ResponseMessageHandler.GetMessage(response.Content));
                }

                DataGridSupply.ItemsSource = response.Data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CmAdd_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            new AddSupplyWindow() { Owner = this }.Show();
        }

        private void CmCompos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItem = DataGridSupply.SelectedItem as SupplyData;
                var response = apiClient.Post<SupplyData>(new RestRequest("supply/moreinfobyid")
                    .AddJsonBody(new
                    {
                        SupplyId = selectedItem.SupplyId
                    }));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(ResponseMessageHandler.GetMessage(response.Content));
                }

                this.IsEnabled = false;
                new SupplyMoreInfoWindow() { Owner = this, supplyData = response.Data }.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DataGridSupply_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CmCompos.IsEnabled = DataGridSupply.SelectedItem != null;
        }
    }
}
