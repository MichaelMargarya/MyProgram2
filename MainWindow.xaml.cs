using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using Newtonsoft.Json.Linq;
namespace MyProgram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection sqlConnection;
        string jsontext = File.ReadAllText("config.json");
       
       
        public MainWindow()
        {
            InitializeComponent();
            Connection();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = Login.Text.Trim();
            string pass = Password.Password.Trim();
            string pass_2 = RePassword.Password.Trim();
            string email = Email.Text.Trim();

            if (login.Length < 5)
            {
                SetError(Login, "This login is incorrect. Enter at least 5 characters.");
            }
            else if (pass.Length < 5)
            {
                SetError(Password, "Incorrect Password! Password should be at least 5 characters long.");
            }
            else if (pass != pass_2)
            {
                SetError(RePassword, "Passwords do not match!");
            }
            else if (!IsValidEmail(email))
            {
                SetError(Email, "Invalid email address!");
            }
            else
            {
                ClearErrors();
                InsertData(login, pass, email);
                MessageBox.Show("We're all done!");
            }
        }

        private void SetError(Control control, string message)
        {
            control.ToolTip = message;
            control.Background = Brushes.Red;
        }

        private void ClearErrors()
        {
            Login.ToolTip = "";
            Login.Background = Brushes.Transparent;
            Password.ToolTip = "";
            Password.Background = Brushes.Transparent;
            RePassword.ToolTip = "";
            RePassword.Background = Brushes.Transparent;
            Email.ToolTip = "";
            Email.Background = Brushes.Transparent;
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                return Regex.IsMatch(email, pattern);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        public void Connection()
        {
            try
            {
                string jsontext = File.ReadAllText("config.json");
                JObject jsonobj = JObject.Parse(jsontext);
                string connectionString = jsonobj["ConnectionString"].ToString();
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to the database: {ex.Message}");
            }
        }


        private void InsertData(string login, string pass, string email)
        {
            try
            {
                JObject jsonobj = JObject.Parse(jsontext);
                string connectionString = jsonobj["ConnectionString"].ToString();
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationsContext>();
                optionsBuilder.UseSqlServer(connectionString);

                using (var dbContext = new ApplicationsContext(optionsBuilder.Options))
                {
                    var user = new User
                    {
                        Login = login,
                        Password = pass,
                        Email = email
                    };
                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();
                    MessageBox.Show("Data inserted successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting data into the database: {ex.Message}");
            }
        }
    }
}
