using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace bookrecord
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonInsert_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection con = new MySqlConnection("host=localhost; user=maker; password=123456; database=econlib");
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM booktable WHERE bookID = '" + textBoxBookID.Text + "'", con);
            Int32 checkDup = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();

            if (checkDup == 1)
            {
                MessageBox.Show("รหัสซ้ำ");
            }
            else
            {
                string sql = "INSERT INTO booktable (bookID,bookName,bookAuther,Status) " +
                    "VALUES('" + textBoxBookID.Text + "','" + textBoxBookName.Text + "','" + textBoxBookAuther.Text + "','" + textBoxStatus.Text + "')";
                //MySqlConnection con = new MySqlConnection("host=localhost; user=maker; password=123456; database=econlib");
                MySqlCommand cmd2 = new MySqlCommand(sql, con);
                con.Open();

                cmd2.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("เพิ่มหนังสือ \"" + textBoxBookName.Text + "\" แล้ว");
                textBoxBookID.Text = String.Empty;
                textBoxBookName.Text = String.Empty;
                textBoxBookAuther.Text = String.Empty;
                textBoxStatus.Text = "1";
            }
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            string sql = "DELETE FROM booktable WHERE bookID='" + textBoxBookID.Text + "'";
            MySqlConnection con = new MySqlConnection("host=localhost; user=maker; password=123456; database=econlib");
            MySqlCommand cmd = new MySqlCommand(sql, con);
            con.Open();

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("ลบหนังสือรหัส " + textBoxBookID.Text + " แล้ว");
            textBoxBookID.Text = String.Empty;
            textBoxBookName.Text = String.Empty;
            textBoxBookAuther.Text = String.Empty;
            textBoxStatus.Text = "1";
        }

        private void buttonListAll_Click(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM booktable";
            MySqlConnection con = new MySqlConnection("host=localhost; user=maker; password=123456; database=econlib");
            MySqlCommand cmd = new MySqlCommand(sql, con);
            con.Open();

            DataSet ds = new DataSet(); // พักข้อมูล
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            dataGrid1.ItemsSource = ds.Tables[0].DefaultView;
            con.Close();
        }

        private void buttonEdit(object sender, RoutedEventArgs e)
        {
            MySqlConnection con = new MySqlConnection("host=localhost; user=maker; password=123456; database=econlib");
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM booktable WHERE bookID = '" + textBoxBookID.Text + "'", con);
            Int32 checkDup = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();

            if (checkDup == 0)
            {
                MessageBox.Show("ไม่มีหนังสือรหัสนี้");
            }
            else
            {
                string sql = "UPDATE booktable " +
                    "SET bookName = '" + textBoxBookName.Text + "'," +
                    " bookAuther = '" + textBoxBookAuther.Text + "'," +
                    " Status = '" + textBoxStatus.Text + "'" +
                    " WHERE bookID = '" + textBoxBookID.Text + "'";

                MySqlCommand cmd2 = new MySqlCommand(sql, con);
                con.Open();

                cmd2.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("แก้ไขหนังสือรหัส " + textBoxBookID.Text + " แล้ว");
                textBoxBookID.Text = String.Empty;
                textBoxBookName.Text = String.Empty;
                textBoxBookAuther.Text = String.Empty;
                textBoxStatus.Text = "1";
            }
        }
    }
}
