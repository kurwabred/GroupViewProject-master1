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

namespace MyBookshelf
{
    /// <summary>
    /// Логика взаимодействия для InWin.xaml
    /// </summary>
    public partial class InWin : Window
    {

        DataClass db = new DataClass();
        public InWin()
        {
            InitializeComponent();
            db.CreateStrConnection();
        }

        private void btnEntry_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Сохраняем результат выполнения в переменную 
                int answer = db.Authorize(tbLogin.Text, tbPassword.Text);
               
                // Проверяем если ответ больше 0, значит совпадение было найден
                if (answer > 0)
                {
                    MessageBox.Show("Авторизация прошла успешно");
                    // Создаем переход между формами
                    MainWindow main = new MainWindow();
                    main.Show();
                    // Скрываем текущию форму
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Авторизация не удалась");
                }
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
           
        }

        private void btnGuest_Click(object sender, RoutedEventArgs e)
        {
            WindowGuest window = new WindowGuest();
            window.Show();
            this.Hide();
        }
    }
}
