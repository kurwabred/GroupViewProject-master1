using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace MyBookshelf
{
    class DataClass
    {
        MySqlConnectionStringBuilder connectionStr;
        MySqlConnection connection;
        string errors = "dump" ;

        // Метод создания Строки подключения, и Коннекшина
       public void CreateStrConnection()
        {
            connectionStr =  new MySqlConnectionStringBuilder();
            connectionStr.Server = "localhost";
            connectionStr.UserID = "root";
            connectionStr.Password = "root";
            connectionStr.Database = "database_book";

            connection = new MySqlConnection(connectionStr.ToString());

        }

        /// <summary>
        ///  Метод добавления книги в БД
        /// </summary>
        /// <param name="Title">Название книги</param>
        /// <param name="Author">Автор</param>
        /// <param name="Genre">Жанр</param>
        /// <param name="Date">Год издания</param>
        public void AddBook(string Title,string Author, string Genre, int Date, string Description)
        {
            // Формируем команду для добавления данных
            string CommandText = $"INSERT INTO books (Title,Genre,Author,DateCreate,Description) " +
                $"VALUES ('{Title}','{Genre}','{Author}',{Date},'{Description}');";
      
            // оборнем код в обработку исключений
            try
            {
                // Открываем подключение
                connection.Open();
                // Создаем объект класса и передаем в конструктор нашу команду.И  Коннект
                MySqlCommand command = new MySqlCommand(CommandText, connection);

                // Выполняем команду
                command.ExecuteNonQuery();

            }
            catch(Exception error)
            {
                System.Windows.MessageBox.Show(error.Message);
            }

            //Закрываем подключение
            connection.Close();
            
        } 
    
    
        public void UpdBook(int Id,string newTitle, string newAuthor, string newGenre, int newDate, string Description)
        {
            // Формируем команду для добавления данных
            string CommandText = $"UPDATE books SET Title = '{newTitle}', " +
                $"Genre = '{newGenre}', " +
                $"Author = '{newAuthor}', " +
                $"DateCreate = {newDate}," +
                $"Description = '{Description}' WHERE idbooks = {Id};";

            // оборнем код в обработку исключений
            try
            {
                // Открываем подключение
                connection.Open();
                // Создаем объект класса и передаем в конструктор нашу команду.И  Коннект
                MySqlCommand command = new MySqlCommand(CommandText, connection);

                // Выполняем команду
                command.ExecuteNonQuery();

            }
            catch (Exception error)
            {
                System.Windows.MessageBox.Show(error.Message);
            }

            //Закрываем подключение
            connection.Close();
        }

        public void DelBook(int Id)
        {
            // Формируем команду для добавления данных
            string CommandText = $"DELETE FROM books  WHERE idbooks = {Id};";

            // оборнем код в обработку исключений
            try
            {
                // Открываем подключение
                connection.Open();
                // Создаем объект класса и передаем в конструктор нашу команду.И  Коннект
                MySqlCommand command = new MySqlCommand(CommandText, connection);

                // Выполняем команду
                command.ExecuteNonQuery();

            }
            catch (Exception error)
            {
                System.Windows.MessageBox.Show(error.Message);
            }

            //Закрываем подключение
            connection.Close();
        }
        public List<Book> ReadBook()
        {
            // Создаем список который будет возвращать данный метод
            List<Book> books = new List<Book>();

            // Формируем команду для выборки данных
            string CommandText = $"SELECT * FROM books;";

            // оборнем код в обработку исключений
            try
            {
                // Открываем подключение
                connection.Open();
                // Создаем объект класса и передаем в конструктор нашу команду.И  Коннект
                MySqlCommand command = new MySqlCommand(CommandText, connection);

                // Создаем reader 
                // Сохраняем в него результат выполнения команды ExecuteReader
                MySqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    // Построчно считываем данные
                    while(reader.Read())
                    {
                        books.Add(new Book()
                        {
                            idbooks = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Genre = reader.GetString(2),
                            Author = reader.GetString(3),
                            DateCreate = reader.GetInt32(4),
                            Description = (reader.GetValue(5) == null) ? "Отсутствует " :  reader.GetValue(5).ToString()
                        });
                    }
                }

                
            }
            catch (Exception error)
            {
                System.Windows.MessageBox.Show(error.Message);
              
            }

            //Закрываем подключение
            connection.Close();
            // Возвращаем наш список
            return books;


        }


        // Метод авторизации, в случае совпадений вернет число отличное от 0 и -1
        public int Authorize(string login, string password)
        {
            // Формируем команду для добавления данных
            string CommandText = $"SELECT * FROM user WHERE login='{login}' AND password='{password}'";
            int codeAuth = -1;
            // оборнем код в обработку исключений
            try
            {
                // Открываем подключение
                connection.Open();
                // Создаем объект класса и передаем в конструктор нашу команду.И  Коннект
                MySqlCommand command = new MySqlCommand(CommandText, connection);

                // Выполняем команду
                codeAuth = Convert.ToInt32(command.ExecuteScalar());

            }
            catch (Exception error)
            {
                System.Windows.MessageBox.Show(error.Message);
            }

            //Закрываем подключение
            connection.Close();
            return codeAuth;

        }

    }
}
