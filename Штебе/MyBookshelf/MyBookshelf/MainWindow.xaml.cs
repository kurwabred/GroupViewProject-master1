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
using MySql.Data.MySqlClient;

namespace MyBookshelf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataClass db = new DataClass();
        int idBook;

        public MainWindow()
        {
            InitializeComponent();
            db.CreateStrConnection();
            dtBooks.ItemsSource = db.ReadBook();
           
        }

        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            TextRange DescText = new TextRange(tbDescription.Document.ContentStart, tbDescription.Document.ContentEnd);
            db.AddBook(tbTitle.Text, tbAuthor.Text, tbGenre.Text, Convert.ToInt32(tbDate.Text), DescText.Text);
            // Обновим данные в таблице
            dtBooks.ItemsSource =  db.ReadBook();
        }

        private void dtBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            Book book = new Book();
            // Сохраняем строку из DataGrid в переменную book
            book = dtBooks.SelectedItem as Book;
            if(book != null)
            {
                // Выводим данные в texbox-сы
                tbTitle.Text = book.Title;
                tbAuthor.Text = book.Author;
                tbGenre.Text = book.Genre;
                tbDate.Text = book.DateCreate.ToString();
                idBook = book.idbooks;
                tbDescription.AppendText(book.Description);
            }
         

        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            TextRange DescText = new TextRange(tbDescription.Document.ContentStart, tbDescription.Document.ContentEnd);
            // Вызываем метод UpdBook из класса DataClass, передаем в него измененные данные
            db.UpdBook(idBook, tbTitle.Text, tbAuthor.Text, tbGenre.Text, Convert.ToInt32(tbDate.Text), DescText.Text);
            // Обновим данные в таблице
            dtBooks.ItemsSource =  db.ReadBook();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Удаляем данные о выбранно книге
            db.DelBook(idBook);
            // Обновлям данные в DataGrid
            dtBooks.ItemsSource = db.ReadBook();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InWin window = new InWin();
            window.Show();
            this.Hide();

        }
    }
}
