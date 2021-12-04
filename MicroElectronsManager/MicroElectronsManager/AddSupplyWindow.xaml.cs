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
    /// Логика взаимодействия для AddSupplyWindow.xaml
    /// </summary>
    public partial class AddSupplyWindow : Window
    {
        private RestClient apiClient = ApiBuilder.GetInstance();
        private List<ProductData> products = new List<ProductData>();

        public AddSupplyWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                WriteListCounterparty();

                WriteListProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void WriteListCounterparty()
        {
            var response = apiClient.Get<List<string>>(new RestRequest("counterparty/AllNameList"));

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(ResponseMessageHandler.GetMessage(response.Content));
            }

            CbCounterparty.ItemsSource = response.Data;
        }

        public void WriteListProducts()
        {
            var response = apiClient.Get<List<string>>(new RestRequest("product/AllNameList"));

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(ResponseMessageHandler.GetMessage(response.Content));
            }

            CbProducts.ItemsSource = response.Data;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Owner.IsEnabled = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnAddProductInSupply_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidProductForm();
                ProductData productData = new ProductData()
                {
                    Name = CbProducts.SelectedItem.ToString(),
                    Quantity = Convert.ToInt32(TbQuantity.Text),
                    Price = Convert.ToInt32(TbPrice.Text)
                };
                products.Add(productData);

                DataGridProductsWrite();
                ClearProductForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DataGridProductsWrite()
        {
            DataGridProducts.ClearValue(ItemsControl.ItemsSourceProperty);
            DataGridProducts.ItemsSource = products;
        }

        private void ValidProductForm()
        {
            if (CbProducts.SelectedItem == null) { throw new Exception("Выберите товар"); }
            if (TbQuantity.Text.Trim() == "") { throw new Exception("Введите количество товара"); }
            if (TbPrice.Text.Trim() == "") { throw new Exception("Введите сумму товара"); }
        }

        private void ValidForm()
        {
            if (CbCounterparty.SelectedItem == null) { throw new Exception("Выберите контрагента"); }
            if (products.Count == 0) { throw new Exception("В корзине поставки ничего нету"); }
        }

        private void ClearProductForm()
        {
            CbProducts.SelectedItem = null;
            TbQuantity.Text = "";
            TbPrice.Text = "";
        }

        private void BtnAddCounterparty_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            new AddCounterpartyWindow() { Owner = this }.Show();
        }

        private void BtnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            new AddProductWindow() { Owner = this }.Show();
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidForm();

                var response = apiClient.Post(new RestRequest("supply/add")
                    .AddJsonBody(new
                    {
                        IsSell = RbIsSell.IsChecked,
                        CounterpartyName = CbCounterparty.SelectedItem.ToString(),
                        Products = products
                    }));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(ResponseMessageHandler.GetMessage(response.Content));
                }

                (this.Owner as SupplyWindow).DataGridSupplyWrite();
                MessageBox.Show("Поставка зарегистрирована", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
