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
    /// Логика взаимодействия для AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        private RestClient apiClient = ApiBuilder.GetInstance();

        public AddProductWindow()
        {
            InitializeComponent();
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidForm();

                var response = apiClient.Post(new RestRequest("product/add")
                    .AddJsonBody(new
                    {
                        Name = TbName.Text.ToString(),
                        CategoryName = CbCategory.SelectedItem.ToString()
                    }));

                (this.Owner as AddSupplyWindow).WriteListProducts();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var response = apiClient.Get<List<string>>(new RestRequest("product/allcategorylist"));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(ResponseMessageHandler.GetMessage(response.Content));
                }

                CbCategory.ItemsSource = response.Data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Owner.IsEnabled = true;
        }

        private void ValidForm()
        {
            if (TbName.Text.Trim() == "") { throw new Exception("Введите наименвоание"); }
            if (CbCategory.SelectedItem == null) { throw new Exception("Выберите категорию"); }
        }
    }
}
