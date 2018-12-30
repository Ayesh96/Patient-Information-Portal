using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PIMS
{
    public partial class Appointment : Form
    {
        string user, pass;
        connection con = new connection();


        public Appointment(string u, string p)
        {

            user = u;
            pass = p;
            InitializeComponent();
        }
        public Appointment(string u, string p, string n)
        {

            user = u;
            pass = p;


            InitializeComponent();
            find_patient(n);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Add_patient m = new PIMS.Add_patient(user, pass);
            m.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu m = new PIMS.Menu(user, pass);
            m.Show();
        }

        private void maskedTextBox1_Leave(object sender, EventArgs e)
        {
            if (maskedTextBox1.MaskFull == true)
            {
                find_patient(maskedTextBox1.Text);
            }

        }

        public void find_patient(string nic)
        {
            try
            {
                connection con = new connection();
                con.con(user, pass);
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con.conn;
                cmd.CommandText = "find_patient";
                cmd.Parameters.Add(new OleDbParameter("p_nic", nic));
                cmd.Parameters.Add(new OleDbParameter("res", OleDbType.Boolean));
                cmd.Parameters["res"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                if (cmd.Parameters["res"].Value.Equals(false))
                {
                    //if nic is not found
                    advancedWizard1.NextButtonEnabled = false;
                    Add_patient a = new Add_patient(user, pass);
                    a.Show();

                }
                else
                {
                    advancedWizard1.ClickNext();
                }
            } catch (OleDbException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void advancedWizardPage2_Enter(object sender, EventArgs e)
        {

        }

        private void advancedWizardPage2_PageShow(object sender, AdvancedWizardControl.EventArguments.WizardPageEventArgs e)
        {
            //if (radioButton1.Checked)
            //{
            //    dateTimePicker2.Enabled = false;
            //    dateTimePicker3.Enabled = false;
            //}

            //if (radioButton2.Checked)
            //{
            //    dateTimePicker2.Enabled = true;
            //    dateTimePicker3.Enabled = true;
            //}
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd-MMM-yyyy";
            try
            {
                con.con(user, pass);
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con.conn;
                cmd.CommandText = "get_app_num";
                cmd.Parameters.Add(new OleDbParameter("ap", OleDbType.Numeric));
                cmd.Parameters["ap"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                label9.Text = cmd.Parameters["ap"].Value.ToString();

                if (cmd.Parameters["ap"].Value==null)
                {
                    label9.Text = "1001";
                }
            }
            catch(OleDbException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            try
            {
                con.con(user, pass);
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con.conn;
                cmd.CommandText = "search_doc";
                cmd.Parameters.Add(new OleDbParameter("doc_id", Convert.ToInt32(textBox1.Text)));
                cmd.Parameters.Add(new OleDbParameter("doc_name", OleDbType.VarChar, 20));
                cmd.Parameters["doc_name"].Direction = ParameterDirection.Output;
                cmd.Parameters.Add(new OleDbParameter("speciality", OleDbType.VarChar, 20));
                cmd.Parameters["speciality"].Direction = ParameterDirection.Output;
                cmd.Parameters.Add(new OleDbParameter("mobile", OleDbType.VarChar, 20));
                cmd.Parameters["mobile"].Direction = ParameterDirection.Output;
                cmd.Parameters.Add(new OleDbParameter("found", OleDbType.Boolean));
                cmd.Parameters["found"].Direction = ParameterDirection.Output;


                cmd.ExecuteNonQuery();
                if (cmd.Parameters["found"].Value.Equals(false))
                {
                    advancedWizard1.NextButtonEnabled = false;
                }
                else
                {

                    textBox2.Text = cmd.Parameters["doc_name"].Value.ToString();
                    textBox3.Text = cmd.Parameters["speciality"].Value.ToString();
                    textBox4.Text = cmd.Parameters["mobile"].Value.ToString();

                }
                con.conn.Close();
            } catch (OleDbException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void advancedWizardPage2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //if (radioButton1.Checked)
            //{
            //    dateTimePicker2.Enabled = false;
            //    dateTimePicker3.Enabled = false;
            //}

            //if (radioButton2.Checked)
            //{
            //    dateTimePicker2.Enabled = true;
            //    dateTimePicker3.Enabled = true;
            //}
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

            //if (radioButton1.Checked)
            //{
            //    dateTimePicker2.Enabled = false;
            //    dateTimePicker3.Enabled = false;
            //}

            //if (radioButton2.Checked)
            //{
            //    dateTimePicker2.Enabled = true;
            //    dateTimePicker3.Enabled = true;
            //}
        }

        private void advancedWizardPage3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void advancedWizardPage3_PageShow(object sender, AdvancedWizardControl.EventArguments.WizardPageEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            try
            {
                connection c = new connection();
                c.con(user, pass);
                OleDbCommand cm = new OleDbCommand();
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = c.conn;
                cm.CommandText = "insert_appointment";
                cm.Parameters.Add(new OleDbParameter("ap_no", label9.Text));

                cm.Parameters.Add(new OleDbParameter("hos_id", 2 ));
                cm.Parameters.Add(new OleDbParameter("doc_id", Convert.ToInt32(textBox1.Text)));
                cm.Parameters.Add(new OleDbParameter("p_nic", maskedTextBox1.Text));
                cm.Parameters.Add(new OleDbParameter("ap_date", dateTimePicker1.Text));
                if (radioButton1.Checked)
                {
                    cm.Parameters.Add(new OleDbParameter("adm", Convert.ToInt32("0")));
                }
                if (radioButton2.Checked)
                {
                    cm.Parameters.Add(new OleDbParameter("adm", 1));
                }

                if (radioButton4.Checked)
                {
                    cm.Parameters.Add(new OleDbParameter("op_nd", Convert.ToInt32("0")));
                }
                if (radioButton2.Checked)
                {
                    cm.Parameters.Add(new OleDbParameter("op_nd", 1));

                }


                cm.Parameters.Add(new OleDbParameter("diag", textBox5.Text));
                cm.Parameters.Add(new OleDbParameter("comm", textBox6.Text));


                cm.ExecuteNonQuery();
                
                
                for (int i = 1; i < dataGridView1.RowCount; i++)
                {
                   
                    OleDbCommand cm1 = new OleDbCommand();
                    cm1.CommandType = CommandType.StoredProcedure;
                    cm1.Connection = c.conn;
                    cm1.CommandText = "insert_medicine";

                    cm1.Parameters.Add(new OleDbParameter("app_num", Convert.ToInt32(label9.Text)));
                    string dosage = dataGridView1.Rows[i - 1].Cells["a"].Value+"+"+ dataGridView1.Rows[i - 1].Cells["b"].Value+"+"+ dataGridView1.Rows[i - 1].Cells["c"].Value;
                cm1.Parameters.Add(new OleDbParameter("dosage", dosage));
                cm1.Parameters.Add(new OleDbParameter("name", dataGridView1.Rows[i - 1].Cells["med_name"].Value));
                cm1.Parameters.Add(new OleDbParameter("s_no", i));
                    cm1.ExecuteNonQuery();
                    cm1.Dispose();
            }

                
                for (int i = 1; i < dataGridView2.RowCount; i++)
                {
                    OleDbCommand cm2 = new OleDbCommand();
                    cm2.CommandType = CommandType.StoredProcedure;
                    cm2.Connection = c.conn;
                    cm2.CommandText = "insert_test";

                    cm2.Parameters.Add(new OleDbParameter("app_num", Convert.ToInt32(label9.Text)));
                    cm2.Parameters.Add(new OleDbParameter("res", Convert.ToInt32(dataGridView2.Rows[i - 1].Cells["result"].Value)));
                    cm2.Parameters.Add(new OleDbParameter("des", dataGridView2.Rows[i - 1].Cells["desc"].Value));
                    cm2.Parameters.Add(new OleDbParameter("s_no", i));
                    cm2.ExecuteNonQuery();
                    cm2.Dispose();
                }

            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Message+ex.Source);
            }


}

        private void advancedWizardPage1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
           
            Add_patient m = new PIMS.Add_patient(user,pass);
            m.Show();
        }
    }
}
