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
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Configuration;
namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow2 : Window
    {
        OracleConnection con = null;
        public MainWindow2()
        {
            this.setConnection();
            InitializeComponent();
        }
        private void updateDataGrid()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM Gost";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            myDataGrid.ItemsSource = dt.DefaultView;
            dr.Close();
        }
        private void setConnection()
        {
            String connectionString = "Data Source=127.0.0.1;User Id=system;Password=kristina;";
            con = new OracleConnection(connectionString);
            try {
                con.Open();
            }
            catch(Exception exp) { }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.updateDataGrid();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            con.Close();
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {

            employee_id_txtbx.IsReadOnly = false;

            String sql = "INSERT INTO GOST(GOSTID, IMEPREZIME, NAZIVGOSTA, KONTAKT, ZAPOSLENIID) " +
               "VALUES(:GOSTID, :IMEPREZIME, :NAZIVGOSTA, :KONTAKT, :ZAPOSLENIID)";

            this.AUD(sql, 0);
            add_btn.IsEnabled = false;
            update_btn.IsEnabled = true;
            delete_btn.IsEnabled = true;
        }

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {

            /*  String sql = "UPDATE GOST SET IMEPREZIME = :IMEPREZIME," +
                "NAZIVGOSTA=:NAZIVGOSTA, KONTAKT=:KONTAKT, ZAPOSLENIID=:ZAPOSLENIID" +
                "WHERE GOSTID = :GOSTID";
              this.AUD(sql, 1);
              */

          
            int gostId = Convert.ToInt32(employee_id_txtbx.Text);
            String imePrez = nacinTxt.Text;
            String naz = napomenaTxt.Text;
            String kont = kontaktTxt.Text;
            int zap = Convert.ToInt32(novoo.Text);

          
           
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "update  GOST set gostID='" + gostId + "' , imePrezime='" + imePrez + "', nazivGosta='" + naz + "', kontakt= '" + kont + "', zaposleniID='" + zap + "' where gostId='" + gostId + "'";

            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;


            try
            {
                int suc = cmd.ExecuteNonQuery();
                if (suc != null)
                {
                    MessageBox.Show("Successfully updated!");

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }


            updateDataGrid();





        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            String sql = "DELETE FROM Gost " +
                "WHERE GOSTID = :GOSTID";
            this.AUD(sql, 2);
            this.resetAll();
        }
        private void resetAll()
        {
            employee_id_txtbx.Text = "";
         
            nacinTxt.Text = "";
            napomenaTxt.Text = "";
            kontaktTxt.Text = "";
            novoo.Text = "";
           // employee_id_txtbx.IsReadOnly = true;

            add_btn.IsEnabled = true;
            update_btn.IsEnabled = false;
            delete_btn.IsEnabled = false;
        }
        private void reset_btn_Click(object sender, RoutedEventArgs e)
        {
            this.resetAll();
        }
        private void AUD(String sql_stmt, int state)
        {
            String msg = "";
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = sql_stmt;
            cmd.CommandType = CommandType.Text;


            switch (state)
            {
                case 0:



                    msg = "Row Inserted Successfully!";
                    cmd.Parameters.Add("GOSTID", OracleDbType.Int32, 6).Value = Int32.Parse(employee_id_txtbx.Text);
                    

                    cmd.Parameters.Add("IMEPREZIME", OracleDbType.Varchar2, 25).Value = nacinTxt.Text;
                    cmd.Parameters.Add("NAZIVGOSTA", OracleDbType.Varchar2, 25).Value = napomenaTxt.Text;
                    cmd.Parameters.Add("KONTAKT", OracleDbType.Varchar2, 25).Value = kontaktTxt.Text;
                    cmd.Parameters.Add("ZAPOSLENIID", OracleDbType.Varchar2, 25).Value = Int32.Parse(novoo.Text);



                    break;
                case 1:


                    msg = "Row Updated Successfully!";

                    cmd.Parameters.Add("IMEPREZIME", OracleDbType.Varchar2, 25).Value = nacinTxt.Text;
                    cmd.Parameters.Add("NAZIVGOSTA", OracleDbType.Varchar2, 25).Value = napomenaTxt.Text;
                    cmd.Parameters.Add("KONTAKT", OracleDbType.Varchar2, 25).Value = kontaktTxt.Text;
                    cmd.Parameters.Add("ZAPOSLENIID", OracleDbType.Varchar2, 25).Value = Int32.Parse(novoo.Text);



                    cmd.Parameters.Add("GOSTID", OracleDbType.Int32, 6).Value = Int32.Parse(employee_id_txtbx.Text);



                    break;
                case 2:
                    msg = "Row Deleted Successfully!";

                    cmd.Parameters.Add("GOSTID", OracleDbType.Int32, 6).Value = Int32.Parse(employee_id_txtbx.Text);

                    break;


             
            }
            try
            {
                

                int n = cmd.ExecuteNonQuery();
                if (n > 0)
                {
                    MessageBox.Show(msg);
                    this.updateDataGrid();
                }
            }
            catch(Exception expe) {

                MessageBox.Show(expe.ToString());
            }
        }

        private void myDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                


                employee_id_txtbx.Text = dr["GOSTID"].ToString();
                

                nacinTxt.Text = dr["IMEPREZIME"].ToString();
                napomenaTxt.Text = dr["NAZIVGOSTA"].ToString();
                kontaktTxt.Text = dr["KONTAKT"].ToString();
                novoo.Text = dr["ZAPOSLENIID"].ToString();

                add_btn.IsEnabled = false;
                update_btn.IsEnabled = true;
                delete_btn.IsEnabled = true;

            }
        }

        private void nazivTxt_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void nazivTxt_MouseEnter(object sender, MouseEventArgs e)
        {
           
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void kontaktTxt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
