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
    /// Логика взаимодействия для AddCounterpartyWindow.xaml
    /// </summary>
    public partial class AddCounterpartyWindow : Window
    {
        private RestClient apiClient = ApiBuilder.GetInstance();

        public AddCounterpartyWindow()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Owner.IsEnabled = true;
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidForm();

                var response = apiClient.Post(new RestRequest("counterparty/Add")
                    .AddJsonBody(new
                    {
                        Name = TbName.Text.ToString(),
                        Tin = TbTin.Text.ToString(),
                        Address = TbAddress.Text.ToString(),
                        Bic = TbBic.Text.ToString()
                    }));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(ResponseMessageHandler.GetMessage(response.Content));
                }

                (this.Owner as AddSupplyWindow).WriteListCounterparty();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ValidForm()
        {
            if (TbAddress.Text.Trim() == "") { throw new Exception("Введите адрес"); }
            if (TbName.Text.Trim() == "") { throw new Exception("Введите наименование"); }
            if (TbBic.Text.Trim() == "") { throw new Exception("Введите бик"); }
            if (TbTin.Text.Trim() == "") { throw new Exception("Введите инн"); }
        }
    }
}
