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
    public partial class MainWindow : Window
    {
        OracleConnection con = null;
        public MainWindow()
        {
            this.setConnection();
            InitializeComponent();
        }
        private void updateDataGrid()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM Rezervacija";
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


            String sql = "INSERT INTO REZERVACIJA(BROJREZERVACIJE, DATUMOD, DATUMDO, NACINREZERVISANJA, NAPOMENA,TIPPANSIONA,NAZIVGOSTA,ZAPOSLENIID,GOSTID) " +
                "VALUES(:BROJREZERVACIJE, :DATUMOD, :DATUMDO, :NACINREZERVISANJA, :NAPOMENA,:TIPPANSIONA,:NAZIVGOSTA,:ZAPOSLENIID,:GOSTID)";

            this.AUD(sql, 0);
            add_btn.IsEnabled = false;
            update_btn.IsEnabled = true;
            delete_btn.IsEnabled = true;
        }

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {

             String sql = "UPDATE REZERVACIJA SET DATUMOD = :DATUMOD," +
               "DATUMDO=:DATUMDO, NACINREZERVISANJA=:NACINREZERVISANJA, NAPOMENA=:NAPOMENA, TIPPANSIONA=:TIPPANSIONA, ZAPOSLENIID=:ZAPOSLENIID,GOSTID=:GOSTID " +
               "WHERE BROJREZERVACIJE = :BROJREZERVACIJE";
                this.AUD(sql, 1);
           
          
        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            String sql = "DELETE FROM REZERVACIJA " +
                "WHERE BROJREZERVACIJE = :BROJREZERVACIJE";
            this.AUD(sql, 2);
            this.resetAll();
        }
        private void resetAll()
        {
            employee_id_txtbx.Text = "";
            datumOD_picker.SelectedDate = null;
            datumDO_picker.SelectedDate = null;
            nacinTxt.Text = "";
            napomenaTxt.Text = "";
            tipTxt_novi.Text = "";
            nazivTxt.Text = "";
            zapTxt.Text = "";
            GostTxt.Text = "";

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
                    cmd.Parameters.Add("DATUMOD", OracleDbType.Date, 7).Value = datumOD_picker.SelectedDate;
                    cmd.Parameters.Add("DATUMDO", OracleDbType.Date, 7).Value = datumDO_picker.SelectedDate;

                    cmd.Parameters.Add("NACINREZERVISANJA", OracleDbType.Varchar2, 25).Value = nacinTxt.Text;
                    cmd.Parameters.Add("Napomena", OracleDbType.Varchar2, 25).Value = napomenaTxt.Text;
                    cmd.Parameters.Add("TIPPANSIONA", OracleDbType.Varchar2, 25).Value = tipTxt_novi.Text;
                    cmd.Parameters.Add("NAZIVGOSTA", OracleDbType.Varchar2, 25).Value = nazivTxt.Text;

                    cmd.Parameters.Add("ZAPOSLENIID", OracleDbType.Int32, 6).Value = Int32.Parse(zapTxt.Text);
                    cmd.Parameters.Add("GOSTID", OracleDbType.Int32, 6).Value = Int32.Parse(GostTxt.Text);


                    break;
                case 1:


                    msg = "Row Updated Successfully!";
                   
                        cmd.Parameters.Add("DATUMOD", OracleDbType.Date, 7).Value = datumOD_picker.SelectedDate;
                        cmd.Parameters.Add("DATUMDO", OracleDbType.Date, 7).Value = datumDO_picker.SelectedDate;

                        cmd.Parameters.Add("NACINREZERVISANJA", OracleDbType.Varchar2, 25).Value = nacinTxt.Text;
                        cmd.Parameters.Add("Napomena", OracleDbType.Varchar2, 25).Value = napomenaTxt.Text;
                        cmd.Parameters.Add("TIPPANSIONA", OracleDbType.Varchar2, 25).Value = tipTxt_novi.Text;
                       // cmd.Parameters.Add("NAZIVGOSTA", OracleDbType.Varchar2, 25).Value = nazivTxt.Text;

                        cmd.Parameters.Add("ZAPOSLENIID", OracleDbType.Int32, 6).Value = Int32.Parse(zapTxt.Text);
                        cmd.Parameters.Add("GOSTID", OracleDbType.Int32, 6).Value = Int32.Parse(GostTxt.Text);
                        cmd.Parameters.Add("BROJREZERVACIJE", OracleDbType.Int32, 6).Value = Int32.Parse(employee_id_txtbx.Text);


                    


                    break;
                case 2:
                    msg = "Row Deleted Successfully!";

                    cmd.Parameters.Add("BROJREZERVACIJE", OracleDbType.Int32, 6).Value = Int32.Parse(employee_id_txtbx.Text);

                    break;


                case 3:

                    cmd.Parameters.Add("DATUMOD", OracleDbType.Date, 7).Value = datumOD_picker.SelectedDate;
                    cmd.Parameters.Add("DATUMDO", OracleDbType.Date, 7).Value = datumDO_picker.SelectedDate;

                    cmd.Parameters.Add("NACINREZERVISANJA", OracleDbType.Varchar2, 25).Value = nacinTxt.Text;
                    cmd.Parameters.Add("Napomena", OracleDbType.Varchar2, 25).Value = napomenaTxt.Text;
                    cmd.Parameters.Add("TIPPANSIONA", OracleDbType.Varchar2, 25).Value = tipTxt_novi.Text;
                     cmd.Parameters.Add("NAZIVGOSTA", OracleDbType.Varchar2, 25).Value = nazivTxt.Text;

                    cmd.Parameters.Add("ZAPOSLENIID", OracleDbType.Int32, 6).Value = Int32.Parse(zapTxt.Text);
                    cmd.Parameters.Add("GOSTID", OracleDbType.Int32, 6).Value = Int32.Parse(GostTxt.Text);
                    cmd.Parameters.Add("BROJREZERVACIJE", OracleDbType.Int32, 6).Value = Int32.Parse(employee_id_txtbx.Text);
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
                datumOD_picker.SelectedDate = DateTime.Parse(dr["DATUMOD"].ToString());
                datumDO_picker.SelectedDate = DateTime.Parse(dr["DATUMDO"].ToString());

                nacinTxt.Text = dr["NACINREZERVISANJA"].ToString();
                napomenaTxt.Text = dr["NAPOMENA"].ToString();

                tipTxt_novi.Text = dr["TIPPANSIONA"].ToString();
                nazivTxt.Text = dr["NAZIVGOSTA"].ToString();
                zapTxt.Text = dr["ZAPOSLENIID"].ToString();
                GostTxt.Text = dr["GOSTID"].ToString();

              //  employee_id_txtbx.IsReadOnly = true;
                add_btn.IsEnabled = false;
                update_btn.IsEnabled = true;
                delete_btn.IsEnabled = true;

            }
        }

        private void nazivTxt_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            String sql = "UPDATE REZERVACIJA SET DATUMOD = :DATUMOD," +
                    "DATUMDO=:DATUMDO, NACINREZERVISANJA=:NACINREZERVISANJA, NAPOMENA=:NAPOMENA, TIPPANSIONA=:TIPPANSIONA, NAZIVGOSTA=:NAZIVGOSTA, ZAPOSLENIID=:ZAPOSLENIID,GOSTID=:GOSTID " +
                    "WHERE BROJREZERVACIJE = :BROJREZERVACIJE";
            this.AUD(sql, 3);
        }

        private void nazivTxt_MouseEnter(object sender, MouseEventArgs e)
        {
           /* String sql = "UPDATE REZERVACIJA SET DATUMOD = :DATUMOD," +
                   "DATUMDO=:DATUMDO, NACINREZERVISANJA=:NACINREZERVISANJA, NAPOMENA=:NAPOMENA, TIPPANSIONA=:TIPPANSIONA, NAZIVGOSTA=:NAZIVGOSTA, ZAPOSLENIID=:ZAPOSLENIID,GOSTID=:GOSTID " +
                   "WHERE BROJREZERVACIJE = :BROJREZERVACIJE";
            this.AUD(sql, 3); 
            */
        } 
    }
}
