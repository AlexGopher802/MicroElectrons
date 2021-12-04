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
    /// Логика взаимодействия для AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        private RestClient apiClient = ApiBuilder.GetInstance();

        public AddTaskWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WriteListProducts();
            DpStart.DisplayDateStart = DateTime.Now;
            DpDeadline.DisplayDateStart = DateTime.Now;
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

                var product = apiClient.Post<ProductData>(new RestRequest("product/FindByName")
                    .AddJsonBody(new
                    {
                        Name = CbProducts.SelectedItem.ToString()
                    })).Data;

                var response = apiClient.Post(new RestRequest("manuf/add")
                    .AddJsonBody(new
                    {
                        DateStart = DpStart.SelectedDate.Value.ToString("dd.MM.yyyy"),
                        DateDeadline = DpDeadline.SelectedDate.Value.ToString("dd.MM.yyyy"),
                        Quantity = TbQuantity.Text.ToString(),
                        ProductId = product.ProductId,
                        EmployeeId = (this.Owner as ManufactureWindow).user.EmployeeId
                    }));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(ResponseMessageHandler.GetMessage(response.Content));
                }

                (this.Owner as ManufactureWindow).DataGridManufsWrite();
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
            if (CbProducts.SelectedItem == null) { throw new Exception("Выберите товар"); }
            if (TbQuantity.Text.Trim() == "") { throw new Exception("Введите количество"); }
            if (DpStart.SelectedDate == null) { throw new Exception("Выберите дату начала производства"); }
            if (DpDeadline.SelectedDate == null) { throw new Exception("Выберите дату дедлайна"); }
            if (DpStart.SelectedDate.Value > DpDeadline.SelectedDate.Value) { throw new Exception("Некорректные даты"); }
        }

        private void WriteListProducts()
        {
            try
            {
                var response = apiClient.Get<List<string>>(new RestRequest("product/AllNameList"));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(ResponseMessageHandler.GetMessage(response.Content));
                }

                CbProducts.ItemsSource = response.Data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
