using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Babylon_V._2._0
{
    /// <summary>
    /// Логика взаимодействия для Reg_Window.xaml
    /// </summary>
    public partial class Reg_Window : Window
    {
        SqlConnection BabylonDB = new SqlConnection(ConfigurationManager.ConnectionStrings["BabylonDB"].ConnectionString);

        private double value1 = 0;
        private double valueNC = 0;

        private DateTime time1;

        public DateTime Date
        {
            get
            {
                return time1;
            }

            set
            {
                time1 = value;

            }
        }

        public Reg_Window()
        {
            InitializeComponent(); 

            Nigger_Time.IsEnabled = false;
            Nigger_Time.Text = "0";

            Thread time = new Thread(() =>
            {
                while (true)
                {
                    DateTime date1 = DateTime.Now;

                    Dispatcher.Invoke(() =>
                    {
                        
                        Time_Now.Text = date1.ToShortTimeString();

                    });

                    Thread.Sleep(2001);    

                }

                

            });
            time.Start();
            
        }



        public double Value
        {
            get
            {
                return value1;
            }
            set
            {
                if (value > 10)
                    value = 10;
                if (value < 0)
                    value = 0;
                value1 = value;
            }
        }

        public double ValueNC
        {
            get
            {
                return valueNC;
            }
            set
            {
                if (value > 10)
                    value = 10;
                if (value < 0)
                    value = 0;
                valueNC = value;
            }
        }

        private void Nigger_Time_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NumberOfComputer_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void Button_Down_TimeOfComputer_Click(object sender, RoutedEventArgs e)
        {
            if (value1 <= 0)
            {
                MessageBox.Show("Данный ввод запрещен! Ограничение от 1 до 10 часов на клиента!");

                value1 = 0;

                Button_Down_TimeOfComputer.IsEnabled = false;
            }

            if (double.TryParse(Nigger_Time.Text, out value1))
            {
                --value1;

                Nigger_Time.Text = value1.ToString();
            }

            
        }

        private void Button_Up_TimeOfComputer_Click(object sender, RoutedEventArgs e)
        {

            if (value1 >= 10)
            {
                MessageBox.Show("Данный ввод запрещен! Ограничение от 1 до 10 часов на клиента!");

                value1 = 10;

                Button_Up_TimeOfComputer.IsEnabled = false;
            }

            if (double.TryParse(Nigger_Time.Text, out value1))
            {
                ++value1;
                
                Nigger_Time.Text = value1.ToString();

            }

            
        }

        private void Button_Up_NumberOfComputer_Click(object sender, RoutedEventArgs e)
        {
            
            if (valueNC >= 10)
            {
                MessageBox.Show("Данный ввод запрещен! В каждом зале по 10 компьютеров!");

                valueNC = 10;
            }

            if (double.TryParse(NumberOfComputer.Text, out valueNC))
            {
                ++valueNC;

                NumberOfComputer.Text = valueNC.ToString();
            }

            
        }

        private void Button_Down_NumberOfComputer_Click(object sender, RoutedEventArgs e)
        {
            if (valueNC <= 0)
            {
                MessageBox.Show("Данный ввод запрещен! В каждом зале по 10 компьютеров!");

                valueNC = 10;
            }


            if (double.TryParse(NumberOfComputer.Text, out valueNC))
            {
                --valueNC;

                NumberOfComputer.Text = valueNC.ToString(); 
            }

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Time_of_game = Time_Now.Text;
            string Time_for_games = Nigger_Time.Text;
            string Number_of_computer = NumberOfComputer.Text;
            string Name = NameClient.Text;
            string SubName = SubnameClient.Text;
            string Otchestvo = OtchestvoClient.Text;
            string EMail = EMailClient.Text;
            string Number = NumberOfPhone.Text;

            string query = $"INSERT INTO ClientDB (Familiya, Imya, Otchestvo, Number, EMail, Time_of_game, Time_for_game, Number_of_computer) VALUES ('{SubName}','{Name}','{Otchestvo}','{Number}','{EMail}','{Time_of_game}','{Time_for_games}','{Number_of_computer}')";
            SqlCommand com = new SqlCommand(query, BabylonDB);

            BabylonDB.Open();

            using (SqlCommand command = new SqlCommand(query, BabylonDB))
            {
                int affectedRows = command.ExecuteNonQuery();

                if (affectedRows > 0)
                {
                    MessageBox.Show("Пользователь успешно зарегистрирован!");
                }
                else
                {
                    MessageBox.Show("Не удалось зарегистрировать пользователя!");
                }
            }
        }
    }
}
