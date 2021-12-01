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
    /// Логика взаимодействия для AddVisitor.xaml
    /// </summary>
    public partial class AddVisitor : Window
    {
        private RestClient apiClient = ApiBuilder.GetInstance();
        public UserData user;

        public AddVisitor()
        {
            InitializeComponent();
        }

        private void ValidForm()
        {
            if (TbLastName.Text.Trim() == "") { throw new Exception("Введите фамилию"); }
            if (TbFirstName.Text.Trim() == "") { throw new Exception("Введите имя"); }
            if (TbPatronymic.Text.Trim() == "") { throw new Exception("Введите отчество"); }
            if (TbPassport.Text.Trim() == "") { throw new Exception("Введите серию и номер паспорта (10 чисел)"); }
            if (TbPassport.Text.Trim().Length != 10) { throw new Exception("Некорректный ввод серии и номера паспорта (должно быть 10 чисел)"); }
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidForm();

                var response = apiClient.Post(new RestRequest("visitor/writeentry")
                    .AddJsonBody(new
                    {
                        EmployeeEntryId = user.EmployeeId,
                        VisitorLastName = TbLastName.Text.ToString(),
                        VisitorFirstName = TbFirstName.Text.ToString(),
                        VisitorPatronymic = TbPatronymic.Text.ToString(),
                        Passport = TbPassport.Text.ToString()
                    }));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(ResponseMessageHandler.GetMessage(response.Content));
                }

                (this.Owner as VisitorWindow).DataGridVisitorWrite();
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

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Owner.IsEnabled = true;
        }
    }
}
