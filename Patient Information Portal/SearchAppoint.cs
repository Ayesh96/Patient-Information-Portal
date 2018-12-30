using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess;
using System.Data.OleDb;
using System.Data.OracleClient;

namespace PIMS
{
    public partial class SearchAppoint : Form
    {
        string user, pass;
        connection con = new connection();
        public SearchAppoint(string u, string p)
        {
            user = u;
            pass = p;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.con(user, pass);

                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = con.conn;
                cmd.CommandText = "search_appointment";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OleDbParameter("p_appointment_no", textBox1.Text));
                //out
                cmd.Parameters.Add(new OleDbParameter("p_cnic", OleDbType.VarChar, 20));
                cmd.Parameters["p_cnic"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add(new OleDbParameter("p_doctor_id", OleDbType.VarChar, 20));
                cmd.Parameters["p_doctor_id"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add(new OleDbParameter("p_hospital_id", OleDbType.VarChar, 20));
                cmd.Parameters["p_hospital_id"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add(new OleDbParameter("p_appointment_date", OleDbType.VarChar, 20));
                cmd.Parameters["p_appointment_date"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add(new OleDbParameter("p_admitted", OleDbType.VarChar, 20));
                cmd.Parameters["p_admitted"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add(new OleDbParameter("p_operation_need", OleDbType.VarChar, 20));
                cmd.Parameters["p_operation_need"].Direction = ParameterDirection.Output;


                cmd.Parameters.Add(new OleDbParameter("p_operation_done", OleDbType.VarChar, 20));
                cmd.Parameters["p_operation_done"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add(new OleDbParameter("p_operation_start_date", OleDbType.VarChar, 20));
                cmd.Parameters["p_operation_start_date"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add(new OleDbParameter("p_operation_start_time", OleDbType.VarChar, 20));
                cmd.Parameters["p_operation_start_time"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add(new OleDbParameter("p_operation_end_date", OleDbType.VarChar, 20));
                cmd.Parameters["p_operation_end_date"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add(new OleDbParameter("p_operation_end_time", OleDbType.VarChar, 20));
                cmd.Parameters["p_operation_end_time"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add(new OleDbParameter("p_operation_successful", OleDbType.VarChar, 20));
                cmd.Parameters["p_operation_successful"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add(new OleDbParameter("p_diagnosis", OleDbType.VarChar, 20));
                cmd.Parameters["p_diagnosis"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add(new OleDbParameter("p_comments", OleDbType.VarChar, 20));
                cmd.Parameters["p_comments"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add(new OleDbParameter("p_app_date", OleDbType.VarChar, 20));
                cmd.Parameters["p_app_date"].Direction = ParameterDirection.Output;


                cmd.ExecuteNonQuery();

                textBox4.Text = cmd.Parameters["p_cnic"].Value.ToString();
                textBox3.Text = cmd.Parameters["p_doctor_id"].Value.ToString();
                textBox2.Text = cmd.Parameters["p_hospital_id"].Value.ToString();
                textBox6.Text = cmd.Parameters["p_admitted"].Value.ToString();
                textBox7.Text = cmd.Parameters["p_appointment_date"].Value.ToString();
                textBox5.Text = cmd.Parameters["p_operation_need"].Value.ToString();
                textBox14.Text = cmd.Parameters["p_operation_done"].Value.ToString();
                textBox13.Text = cmd.Parameters["p_operation_start_date"].Value.ToString();
                textBox8.Text = cmd.Parameters["p_operation_start_time"].Value.ToString();
                textBox9.Text = cmd.Parameters["p_operation_end_date"].Value.ToString();
                textBox11.Text = cmd.Parameters["p_operation_end_time"].Value.ToString();
                textBox15.Text = cmd.Parameters["p_diagnosis"].Value.ToString();
                textBox16.Text = cmd.Parameters["p_comments"].Value.ToString();
                textBox17.Text = cmd.Parameters["p_app_date"].Value.ToString();
                textBox12.Text = cmd.Parameters["p_operation_successful"].Value.ToString();
                con.conn.Close();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu m = new PIMS.Menu(user,pass);
            m.Show();
        }
    }
}
