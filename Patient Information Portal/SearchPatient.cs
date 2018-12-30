using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess;
using System.Data.OleDb;
using System.Data.OracleClient;

namespace PIMS
{
    public partial class SearchPatient : Form
    {
        connection con = new connection();
        string user, pass;

        public SearchPatient(string u,string p)
        {
            user = u;
            pass = p;
            InitializeComponent();
        }

        //private void textBox1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    string sVal = textBox1.Text;

        //    if (!string.IsNullOrEmpty(sVal) && e.KeyCode != Keys.Back)
        //    {
        //        sVal = sVal.Replace("-", "");
        //        string newst = Regex.Replace(sVal, ".{5}", "$0-");
        //        textBox1.Text = newst;
        //        textBox1.SelectionStart = textBox1.Text.Length;
        //    }
        //}

        private void validateTextInteger(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu m = new PIMS.Menu(user,pass);
            m.Show();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            maskedTextBox1.Clear();
            textBox2.Text = "";
            textBox7.Text = "";
            textBox9.Text = "";
            textBox8.Text = "";
            textBox6.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox3.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.MaskFull == true)
            {
                try
                {
                    con.con(user, pass);

                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = con.conn;
                    cmd.CommandText = "search_patient";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OleDbParameter("p_cnic", maskedTextBox1.Text));
                    //out
                    cmd.Parameters.Add(new OleDbParameter("p_name", OleDbType.VarChar, 20));
                    cmd.Parameters["p_name"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(new OleDbParameter("p_mobile", OleDbType.VarChar, 20));
                    cmd.Parameters["p_mobile"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(new OleDbParameter("p_ADDRESS", OleDbType.VarChar, 20));
                    cmd.Parameters["p_ADDRESS"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(new OleDbParameter("p_DOB", OleDbType.VarChar, 20));
                    cmd.Parameters["p_DOB"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(new OleDbParameter("p_BLOOD_GROUP", OleDbType.VarChar, 20));
                    cmd.Parameters["p_BLOOD_GROUP"].Direction = ParameterDirection.Output;


                    cmd.Parameters.Add(new OleDbParameter("p_EMERGENCY_CONTACT_NAME", OleDbType.VarChar, 20));
                    cmd.Parameters["p_EMERGENCY_CONTACT_NAME"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(new OleDbParameter("p_EMERGENCY_CONTACT_NUMBER", OleDbType.VarChar, 20));
                    cmd.Parameters["p_EMERGENCY_CONTACT_NUMBER"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(new OleDbParameter("p_EMERGENCY_CONTACT_ADDRESS", OleDbType.VarChar, 20));
                    cmd.Parameters["p_EMERGENCY_CONTACT_ADDRESS"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(new OleDbParameter("p_EMERGENCY_CONTACT_RELATION", OleDbType.VarChar, 20));
                    cmd.Parameters["p_EMERGENCY_CONTACT_RELATION"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(new OleDbParameter("p_MARITAL_STATUS", OleDbType.VarChar, 20));
                    cmd.Parameters["p_MARITAL_STATUS"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(new OleDbParameter("p_RELIGION", OleDbType.VarChar, 20));
                    cmd.Parameters["p_RELIGION"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(new OleDbParameter("p_ALLERGIES", OleDbType.VarChar, 20));
                    cmd.Parameters["p_ALLERGIES"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(new OleDbParameter("p_timestamp", OleDbType.VarChar, 20));
                    cmd.Parameters["p_timestamp"].Direction = ParameterDirection.Output;


                    cmd.ExecuteNonQuery();




                    textBox2.Text = cmd.Parameters["p_name"].Value.ToString();
                    textBox7.Text = cmd.Parameters["p_mobile"].Value.ToString();
                    textBox9.Text = cmd.Parameters["p_DOB"].Value.ToString();
                    textBox8.Text = cmd.Parameters["p_blood_group"].Value.ToString();
                    textBox6.Text = cmd.Parameters["p_EMERGENCY_CONTACT_NAME"].Value.ToString();
                    textBox3.Text = cmd.Parameters["p_EMERGENCY_CONTACT_NUMBER"].Value.ToString();
                    textBox4.Text = cmd.Parameters["p_EMERGENCY_CONTACT_ADDRESS"].Value.ToString();
                    textBox5.Text = cmd.Parameters["p_EMERGENCY_CONTACT_RELATION"].Value.ToString();
                    textBox10.Text = cmd.Parameters["p_MARITAL_STATUS"].Value.ToString();
                    textBox11.Text = cmd.Parameters["p_RELIGION"].Value.ToString();
                    textBox12.Text = cmd.Parameters["p_ALLERGIES"].Value.ToString();
                    textBox13.Text = cmd.Parameters["p_timestamp"].Value.ToString();
                    con.conn.Close();

                }
                catch (OleDbException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
           
        }
    }

