using Practika2;
using Praktika2.KFCDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Praktika2
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        buyersTableAdapter buyers = new buyersTableAdapter();
        ordersTableAdapter orders = new ordersTableAdapter();
        private List<BuyerInfo> buyerList = new List<BuyerInfo>();
        private int selectedId = -1;
        private object c = null;

        public MainPage()
        {
            InitializeComponent();


            buyersDataGrid.ItemsSource = buyers.GetData();
            productComboBox.ItemsSource = orders.GetData();

            productComboBox.SelectedValuePath = "ID_orders";
            productComboBox.DisplayMemberPath = "product";
        }

        private void Button_ClickAdd(object sender, RoutedEventArgs e)
        {
            saveNewBuyer();
        }

        //Добавление нового покупателя бд
        private void saveNewBuyer()
        {

            string firstName = firstname.Text;
            string middleName = middlename.Text;
            string surName = surname.Text;

            if (firstName == "" || middleName == "" || surName == "" || productComboBox.SelectedValue == null)
            {
                MessageBox.Show("Пожалуйста, задайте все данные покупателя", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int orderId = (int)productComboBox.SelectedValue;

            //Console.WriteLine($"Выбранное значение: {orderId}"); // Для строки

            buyers.InsertQuery(firstName, middleName, surName, orderId);
            buyersDataGrid.ItemsSource = buyers.GetData();
            SystemSounds.Hand.Play();

            //buyerList.Add(new BuyerInfo { FirstName = firstName, MiddleName = middleName, Surname = surName });
            //buyersDataGrid.ItemsSource = null;
            //buyersDataGrid.ItemsSource = buyerList;

            firstname.Text = "";
            middlename.Text = "";
            surname.Text = "";
            productComboBox.SelectedValue = null;
        }

        private void Button_ClickEdit(object sender, RoutedEventArgs e)
        {
            buyers.UpdateQuery(firstname.Text, middlename.Text, surname.Text, selectedId);
            buyersDataGrid.ItemsSource = buyers.GetData();

            if (buyersDataGrid.SelectedItem != null)
            {
                //BuyerInfo selectedBuyer = (BuyerInfo)buyersDataGrid.SelectedItem;
                //selectedBuyer.FirstName = firstname.Text;
                //selectedBuyer.MiddleName = middlename.Text;
                //selectedBuyer.Surname = surname.Text;

                //buyersDataGrid.ItemsSource = null;
                //buyersDataGrid.ItemsSource = buyerList;
            }
        }

        private void Button_ClickDelete(object sender, RoutedEventArgs e)
        {
            buyers.DeleteQuery(selectedId);
            buyersDataGrid.ItemsSource = buyers.GetData();

            firstname.Text = "";
            middlename.Text = "";
            surname.Text = "";
            productComboBox.SelectedValue = null;

            if (buyersDataGrid.SelectedItem != null)
            {
                //BuyerInfo selectedBuyer = (BuyerInfo)buyersDataGrid.SelectedItem;
                //buyerList.Remove(selectedBuyer);

                //buyersDataGrid.ItemsSource = null;
                //buyersDataGrid.ItemsSource = buyerList;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //BuyersEFPage KFCEFPage = new BuyersEFPage();
            //this.Content = KFCEFPage;
            //MainFrame.Navigate(new MainPage());
            NavigationService.Navigate(new BuyersEFPage());
        }

        //Выбираем строку из Ddata Grid,чтобы получить ID покупателя для манипуляций
        private void buyersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView selectedRow = (DataRowView)buyersDataGrid.SelectedItem;

                //Console.WriteLine($"Выбранное значение: {selectedRow}");
                if (selectedRow != null)
                {
                    selectedId = (int)selectedRow["ID_buyers"];
                    firstname.Text = (string)selectedRow["firstname"];
                    middlename.Text = (string)selectedRow["middlename"];
                    surname.Text = (string)selectedRow["surname"];
                    productComboBox.SelectedValue = selectedRow["ID_orders"];

                    //Console.WriteLine($"Выбранное значение: {selectedId}");
                    //var selectedId = selectedRow.Id; // Получите ID
                    //MessageBox.Show($"Выбранный ID: {selectedId}");
                }
            }
            catch (System.Exception ex)
            {

            }
        }

        private void middlename_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                saveNewBuyer();
            }
        }

        private void firstname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                saveNewBuyer();
            }
        }

        private void surname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                saveNewBuyer();
            }
        }
    }

    public class BuyerInfo
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
    }
}
