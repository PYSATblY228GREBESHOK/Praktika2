using Practika2;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Windows;
using System.Windows.Controls;

namespace Praktika2
{
    public partial class BuyersEFPage : Page
    {
        private BuyersDbContext _dbContext;

        public BuyersEFPage()
        {
            InitializeComponent();
            _dbContext = new BuyersDbContext();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                buyersEF.ItemsSource = _dbContext.Buyers.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(NameTbx.Text))
                {
                    var newBuyer = new Buyer { FullName = NameTbx.Text };
                    _dbContext.Buyers.Add(newBuyer);
                    _dbContext.SaveChanges();
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Имя покупателя не может быть пустым.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении: {ex.Message}");
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedBuyer = buyersEF.SelectedItem as Buyer;
                if (selectedBuyer != null)
                {
                    if (!string.IsNullOrWhiteSpace(NameTbx.Text))
                    {
                        selectedBuyer.FullName = NameTbx.Text;
                        _dbContext.SaveChanges();
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Имя покупателя не может быть пустым.");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите покупателя для изменения.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при изменении: {ex.Message}");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedBuyer = buyersEF.SelectedItem as Buyer;
                if (selectedBuyer != null)
                {
                    _dbContext.Buyers.Remove(selectedBuyer);
                    _dbContext.SaveChanges();
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Выберите покупателя для удаления.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}");
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }

    }

    public class BuyersDbContext : DbContext
    {
        public DbSet<Buyer> Buyers { get; set; }
    }

    public class Buyer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }
}
