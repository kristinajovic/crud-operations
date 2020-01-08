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
    public partial class MainWindow3 : Window
    {
        OracleConnection con = null;
        public MainWindow3()
        {
            this.setConnection();
            InitializeComponent();
        }
        private void updateDataGrid()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM RezervisaneSobe";
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
            sobaIdTxt.IsReadOnly = false;

            String sql = "INSERT INTO REZERVISANESOBE(BROJREZERVACIJE, SOBAID, BROJODRASLIH, BROJDECE, IZNOS,NAPOMENA,BROJSOBE) " +
               "VALUES(:BROJREZERVACIJE, :SOBAID, :BROJODRASLIH, :BROJDECE, :IZNOS, :NAPOMENA,:BROJSOBE)";

            this.AUD(sql, 0);
            add_btn.IsEnabled = false;
            update_btn.IsEnabled = true;
            delete_btn.IsEnabled = true;
        }

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {
            String sql = "UPDATE REZERVISANESOBE SET BROJREZERVACIJE=:BROJREZERVACIJE, BROJODRASLIH= :BROJODRASLIH," +
               "BROJDECE=:BROJDECE, IZNOS=:IZNOS, BROJSOBE=:BROJSOBE " +
               "WHERE SOBAID = :SOBAID";
            this.AUD(sql, 1);




        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            String sql = "DELETE FROM REZERVISANESOBE " +
                "WHERE BROJREZERVACIJE = :BROJREZERVACIJE AND SOBAID=:SOBAID";
            this.AUD(sql, 2);
            this.resetAll();
        }
        private void resetAll()
        {
            employee_id_txtbx.Text = "";
         
            napomenaTxt.Text = "";
            kontaktTxt.Text = "";
            novoo.Text = "";
            sobaIdTxt.Text = "";
            brSobeTxt.Text = "";
            napTxt.Text = "";
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
                    cmd.Parameters.Add("BROJREZERVACIJE", OracleDbType.Int32, 6).Value = Int32.Parse(employee_id_txtbx.Text);
                    cmd.Parameters.Add("SOBAID", OracleDbType.Varchar2, 25).Value = Int32.Parse(sobaIdTxt.Text);
                    cmd.Parameters.Add("BROJODRASLIH", OracleDbType.Varchar2, 25).Value = Int32.Parse(napomenaTxt.Text);
                    cmd.Parameters.Add("BROJDECE", OracleDbType.Varchar2, 25).Value = Int32.Parse(kontaktTxt.Text);
                    cmd.Parameters.Add("IZNOS", OracleDbType.Varchar2, 25).Value = Int32.Parse(novoo.Text);
                    cmd.Parameters.Add("NAPOMENA", OracleDbType.Varchar2, 25).Value = napTxt.Text;
                    cmd.Parameters.Add("BROJSOBE", OracleDbType.Varchar2, 25).Value = Int32.Parse(brSobeTxt.Text);




                    break;
                case 1:


                    msg = "Row Updated Successfully!";
                    cmd.Parameters.Add("BROJREZERVACIJE", OracleDbType.Int32, 6).Value = Int32.Parse(employee_id_txtbx.Text);
                    cmd.Parameters.Add("BROJODRASLIH", OracleDbType.Varchar2, 25).Value = Int32.Parse(napomenaTxt.Text);
                    cmd.Parameters.Add("BROJDECE", OracleDbType.Varchar2, 25).Value = Int32.Parse(kontaktTxt.Text);
                    cmd.Parameters.Add("IZNOS", OracleDbType.Varchar2, 25).Value = Int32.Parse(novoo.Text);
                   // cmd.Parameters.Add("NAPOMENA", OracleDbType.Varchar2, 25).Value = napTxt.Text;
                    cmd.Parameters.Add("BROJSOBE", OracleDbType.Varchar2, 25).Value = Int32.Parse(brSobeTxt.Text);
                    cmd.Parameters.Add("SOBAID", OracleDbType.Varchar2, 25).Value = Int32.Parse(sobaIdTxt.Text);




                    break;
                case 2:
                    msg = "Row Deleted Successfully!";

                    cmd.Parameters.Add("BROJREZERVACIJE", OracleDbType.Int32, 6).Value = Int32.Parse(employee_id_txtbx.Text);
                    cmd.Parameters.Add("SOBAID", OracleDbType.Varchar2, 25).Value = Int32.Parse(sobaIdTxt.Text);

                    break;

                case 3:
                    cmd.Parameters.Add("BROJREZERVACIJE", OracleDbType.Int32, 6).Value = Int32.Parse(employee_id_txtbx.Text);
                    cmd.Parameters.Add("BROJODRASLIH", OracleDbType.Varchar2, 25).Value = Int32.Parse(napomenaTxt.Text);
                    cmd.Parameters.Add("BROJDECE", OracleDbType.Varchar2, 25).Value = Int32.Parse(kontaktTxt.Text);
                    cmd.Parameters.Add("IZNOS", OracleDbType.Varchar2, 25).Value = Int32.Parse(novoo.Text);
                    cmd.Parameters.Add("NAPOMENA", OracleDbType.Varchar2, 25).Value = napTxt.Text;
                    cmd.Parameters.Add("BROJSOBE", OracleDbType.Varchar2, 25).Value = Int32.Parse(brSobeTxt.Text);

                    cmd.Parameters.Add("SOBAID", OracleDbType.Varchar2, 25).Value = Int32.Parse(sobaIdTxt.Text);


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
                


                employee_id_txtbx.Text = dr["BROJREZERVACIJE"].ToString();
                sobaIdTxt.Text = dr["SOBAID"].ToString();


                napomenaTxt.Text = dr["BROJODRASLIH"].ToString();
                kontaktTxt.Text = dr["BROJDECE"].ToString();
                novoo.Text = dr["IZNOS"].ToString();

                napTxt.Text = dr["NAPOMENA"].ToString();

                brSobeTxt.Text = dr["BROJSOBE"].ToString();


              //  brSobeTxt.IsReadOnly = true;
              //  employee_id_txtbx.IsReadOnly = true;
               // sobaIdTxt.IsReadOnly = true;
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

        private void napTxt_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            /*   String sql = "UPDATE REZERVISANESOBE SET BROJODRASLIH= :BROJODRASLIH," +
                 "BROJDECE=:BROJDECE, IZNOS=:IZNOS, NAPOMENA:=NAPOMENA, BROJSOBE=:BROJSOBE " +
                 "WHERE SOBAID = :SOBAID AND BROJREZERVACIJE=:BROJREZERVACIJE"; */


            String sql = "UPDATE REZERVISANESOBE SET  BROJREZERVACIJE=:BROJREZERVACIJE, BROJODRASLIH= :BROJODRASLIH," +
              "BROJDECE=:BROJDECE, IZNOS=:IZNOS, NAPOMENA=:NAPOMENA, BROJSOBE=:BROJSOBE " +
              "WHERE SOBAID = :SOBAID";

            this.AUD(sql, 3);
        }
    }
}
