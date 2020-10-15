using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace WPF_Login_Screen_SQL
{
    /// <summary>
    /// Interaction logic for CreateUser.xaml
    /// </summary>
    public partial class CreateUser : Window
    {
        public CreateUser()
        {
            InitializeComponent();
            dbUsername.Focus();
        }



        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-JQA441M; Initial Catalog=SDE; Integrated Security= True;");

        private void dbSubmit_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                String Query = "INSERT INTO Students_Users (name, password) Values (@Username, @Password)";
                SqlCommand sqlCmd = new SqlCommand(Query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Username", dbUsername.Text);
                sqlCmd.Parameters.AddWithValue("@Password", dbPassword.Text);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if (count == 0)
                {
                    LoginScreen dashboard = new LoginScreen();
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Something went wrong!!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }
    }
}
