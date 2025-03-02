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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppProductsTask
{
    
    public partial class MainWindow : Window
    {
        private Ananyin223ProductsDbEntities db;
        public MainWindow()
        {
            InitializeComponent();

            db = new Ananyin223ProductsDbEntities();

            dataGridProducts.ItemsSource = db.Products.ToList();
        }

        private void ButtonDeleteCurrentProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product selectedProduct = (Product)dataGridProducts.SelectedItem;

                db.Products.Remove(selectedProduct);

                db.SaveChanges();

                dataGridProducts.ItemsSource = null;
                dataGridProducts.ItemsSource = db.Products.ToList();

                textBoxId.Clear();
                textBoxName.Clear();
                textBoxPrice.Clear();
                textBoxStock.Clear();

                MessageBox.Show("Успех. Продукт удален.");
            }
            catch 
            {
                MessageBox.Show("Ошибка. Проблема соединения с БД. Повторите попытку позже.");
            }
        }
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxName.Text.Length == 0)
            {
                MessageBox.Show("Ошибка. Название не заполнено.");
                return;
            }

            if (textBoxPrice.Text.Length == 0)
            {
                MessageBox.Show("Ошибка. Цена не заполнена.");
                return;
            }

            if (textBoxStock.Text.Length == 0)
            {
                MessageBox.Show("Ошибка. Количество не заполнена.");
                return;
            }

            Product newProduct = new Product
            {
                Name = textBoxName.Text,
                Price = decimal.Parse(textBoxPrice.Text),
                Stock = int.Parse(textBoxStock.Text)
            };

            db.Products.Add(newProduct);

            db.SaveChanges();

            dataGridProducts.ItemsSource = null;
            dataGridProducts.ItemsSource = db.Products.ToList();

            textBoxId.Clear();
            textBoxName.Clear();
            textBoxPrice.Clear();
            textBoxStock.Clear();

            MessageBox.Show("Успех. Сотрудник успешно добавлен.");
        }
        private void buttonClearFields_Click(object sender, RoutedEventArgs e)
        {
            textBoxId.Clear();
            textBoxName.Clear();
            textBoxPrice.Clear();
            textBoxStock.Clear();
        }

        private void dataGridProducts_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Product selectedProduct = (Product)dataGridProducts.SelectedItem;
            if (selectedProduct != null)
            {
                textBoxId.Text = selectedProduct.Id.ToString();
                textBoxName.Text = selectedProduct.Name;
                textBoxPrice.Text = selectedProduct.Price.ToString();
                textBoxStock.Text = selectedProduct.Stock.ToString();
            }
        }
        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxId.Text.Length == 0)
            {
                MessageBox.Show("Ошибка. ИД не заполнен.");
                return;
            }

            if (textBoxName.Text.Length == 0)
            {
                MessageBox.Show("Ошибка. Название не заполнено.");
                return;
            }

            if (textBoxPrice.Text.Length == 0)
            {
                MessageBox.Show("Ошибка. Цена не заполнена.");
                return;
            }

            if (textBoxStock.Text.Length == 0)
            {
                MessageBox.Show("Ошибка. Количество не заполнена.");
                return;
            }


            int updatedId = int.Parse(textBoxId.Text);

            Product selectedProduct = db.Products.FirstOrDefault(item => item.Id == updatedId);

            selectedProduct.Name = textBoxName.Text;
            selectedProduct.Price = decimal.Parse(textBoxPrice.Text);
            selectedProduct.Stock = int.Parse(textBoxStock.Text);

            db.SaveChanges();

            dataGridProducts.ItemsSource = null;
            dataGridProducts.ItemsSource = db.Products.ToList();

            textBoxId.Clear();
            textBoxName.Clear();
            textBoxPrice.Clear();
            textBoxStock.Clear();

            MessageBox.Show("Успех. Сотрудник успешно обновлён.");

        }

        private void SortByStockAsc_Click(object sender, RoutedEventArgs e)
        {
            dataGridProducts.ItemsSource = db.Products.OrderBy(s => s.Stock).ToList();
        }

        private void SortByStockDesc_Click(object sender, RoutedEventArgs e)
        {
            dataGridProducts.ItemsSource = db.Products.OrderByDescending(s => s.Stock).ToList();
        }
    }
}
