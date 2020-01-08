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
    public partial class MainWindow5 : Window
    {
        OracleConnection con = null;
        public MainWindow5()
        {
            this.setConnection();
            InitializeComponent();
        }
        private void updateDataGrid()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM GRAD";
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


            String sql = "INSERT INTO GRAD(GRADID, NAZIVGRADA, DRZAVAID) " +
               "VALUES(:GRADID, :NAZIVGRADA, :DRZAVAID)";

            this.AUD(sql, 0);
            add_btn.IsEnabled = false;
            update_btn.IsEnabled = true;
            delete_btn.IsEnabled = true;
        }

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {



            /*  String sql = "UPDATE NASELJE SET POSTANSKIBR=:POSTANSKIBR, GRADID= :GRADID," +
                  "DRZAVAID=:DRZAVAID" +
                  "WHERE NASELJEID = :NASELJEID";
              this.AUD(sql, 1);

      */


            int gr = Convert.ToInt32(employee_id_txtbx.Text);
            String naz = nacinTxt.Text;
            int dr = Convert.ToInt32(napomenaTxt.Text);



            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "update  GRAD set GRADID='" + gr + "' , NAZIVGRADA='" + naz + "', DRZAVAID='" + dr +"' where GRADID='" + gr + "'";

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
            String sql = "DELETE FROM GRAD " +
                "WHERE GRADID = :GRADID";
            this.AUD(sql, 2);
            this.resetAll();
        }
        private void resetAll()
        {
            employee_id_txtbx.Text = "";
         
            nacinTxt.Text = "";
            napomenaTxt.Text = "";
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
                    cmd.Parameters.Add("GRADID", OracleDbType.Int32, 6).Value = Int32.Parse(employee_id_txtbx.Text);
     

                    cmd.Parameters.Add("NAZIVGRADA", OracleDbType.Varchar2, 25).Value = nacinTxt.Text;
                    cmd.Parameters.Add("DRZAVAID", OracleDbType.Varchar2, 25).Value = Int32.Parse(napomenaTxt.Text);
                   // cmd.Parameters.Add("DRZAVAID", OracleDbType.Varchar2, 25).Value = Int32.Parse(kontaktTxt.Text);



                    break;
                case 1:


                    msg = "Row Updated Successfully!";

                   /* cmd.Parameters.Add("POSTANSKIBR", OracleDbType.Varchar2, 25).Value = Int32.Parse(nacinTxt.Text);
                    cmd.Parameters.Add("GRADID", OracleDbType.Varchar2, 25).Value = Int32.Parse(napomenaTxt.Text);
                   // cmd.Parameters.Add("DRZAVAID", OracleDbType.Varchar2, 25).Value = Int32.Parse(kontaktTxt.Text);


                    cmd.Parameters.Add("NASELJEID", OracleDbType.Int32, 6).Value = Int32.Parse(employee_id_txtbx.Text);
                    */


                    break;
                case 2:
                    msg = "Row Deleted Successfully!";

                    cmd.Parameters.Add("GRADID", OracleDbType.Int32, 6).Value = Int32.Parse(employee_id_txtbx.Text);

                    break;
                case 3:

                    cmd.Parameters.Add("POSTANSKIBR", OracleDbType.Varchar2, 25).Value = Int32.Parse(nacinTxt.Text);
                    cmd.Parameters.Add("GRADID", OracleDbType.Varchar2, 25).Value = Int32.Parse(napomenaTxt.Text);


                    cmd.Parameters.Add("NASELJEID", OracleDbType.Int32, 6).Value = Int32.Parse(employee_id_txtbx.Text);

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
                


                employee_id_txtbx.Text = dr["GRADID"].ToString();
                

                nacinTxt.Text = dr["NAZIVGRADA"].ToString();
                napomenaTxt.Text = dr["DRZAVAID"].ToString();


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

        private void kontaktTxt_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
