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
    public partial class update_appoint: Form
    {
        string user, pass;
        connection con = new connection();

  

        public update_appoint(string u,string p)
        {

            user = u;
            pass = p;
            InitializeComponent();
        }
        public update_appoint(string u, string p,string n)
        {

            user = u;
            pass = p;

            
            InitializeComponent();
            find_patient(n);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Add_patient m = new PIMS.Add_patient(user,pass);
            m.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu m = new PIMS.Menu(user,pass);
            m.Show();
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
            }catch(OleDbException ex)
            {
                MessageBox.Show(ex.Message);
            }

            }

       

        private void advancedWizardPage2_PageShow(object sender, AdvancedWizardControl.EventArguments.WizardPageEventArgs e)
        {
            if (radioButton1.Checked)
            {
                dateTimePicker2.Enabled = false;
                dateTimePicker3.Enabled = false;
            }

            if (radioButton2.Checked)
            {
                dateTimePicker2.Enabled = true;
                dateTimePicker3.Enabled = true;
            }
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd-MMM-yyyy";
            con.con(user, pass);
          

            //label9.Text = cmd.Parameters["ap"].Value.ToString();
        }

        
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals(string.Empty)){
            }
            else {
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
            
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                dateTimePicker2.Enabled = false;
                dateTimePicker3.Enabled = false;
            }

            if (radioButton2.Checked)
            {
                dateTimePicker2.Enabled = true;
                dateTimePicker3.Enabled = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton1.Checked)
            {
                dateTimePicker2.Enabled = false;
                dateTimePicker3.Enabled = false;
               
            }

            if (radioButton2.Checked)
            {
                dateTimePicker2.Enabled = true;
                dateTimePicker3.Enabled = true;
                panel4.Enabled = true;
            }
        }
        

        private void appoint_no_Leave(object sender, EventArgs e)
        {
            try
            {
                con.con(user, pass);
                OleDbCommand cmd1 = new OleDbCommand();
                cmd1.Connection = con.conn;
                cmd1.CommandText = "find_appointment";
                cmd1.CommandType = CommandType.StoredProcedure;

                cmd1.Parameters.Add(new OleDbParameter("ap_no", Convert.ToInt32(appoint_no.Text)));
                cmd1.Parameters.Add(new OleDbParameter("res", OleDbType.Numeric, 10));
                cmd1.Parameters["res"].Direction = ParameterDirection.Output;
                cmd1.ExecuteNonQuery(); ;

                if (Convert.ToInt32(cmd1.Parameters["res"].Value) > 0)
                {
                    advancedWizard1.NextButtonEnabled = true;
                    advancedWizard1.ClickNext();

                    OleDbCommand getApp = new OleDbCommand();
                    getApp.Connection = con.conn;
                    getApp.CommandType = CommandType.StoredProcedure;
                    getApp.CommandText = "app_details";
                    getApp.Parameters.Add(new OleDbParameter("ap_num", Convert.ToInt32(cmd1.Parameters["res"].Value)));
                    getApp.Parameters.Add(new OleDbParameter("doc_id", OleDbType.Numeric, 5));
                    getApp.Parameters["doc_id"].Direction = ParameterDirection.Output;
                    getApp.Parameters.Add(new OleDbParameter("ap_date", OleDbType.VarChar, 12));
                    getApp.Parameters["ap_date"].Direction = ParameterDirection.Output;
                    getApp.Parameters.Add(new OleDbParameter("op_ndd", OleDbType.Numeric, 1));
                    getApp.Parameters["op_ndd"].Direction = ParameterDirection.Output;
                    getApp.Parameters.Add(new OleDbParameter("adm", OleDbType.Numeric, 1));
                    getApp.Parameters["adm"].Direction = ParameterDirection.Output;
                    getApp.Parameters.Add(new OleDbParameter("diag", OleDbType.VarChar, 30));
                    getApp.Parameters["diag"].Direction = ParameterDirection.Output;
                    getApp.Parameters.Add(new OleDbParameter("comm", OleDbType.VarChar, 100));
                    getApp.Parameters["comm"].Direction = ParameterDirection.Output;
                    getApp.ExecuteNonQuery();

                    try
                    {
                        con.con(user, pass);
                        OleDbCommand cmd = new OleDbCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con.conn;
                        cmd.CommandText = "search_doc";
                        cmd.Parameters.Add(new OleDbParameter("doc_id", Convert.ToInt32(getApp.Parameters["doc_id"].Value)));
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

                        }
                        else
                        {
                            textBox1.Text = getApp.Parameters["doc_id"].Value.ToString();
                            textBox2.Text = cmd.Parameters["doc_name"].Value.ToString();
                            textBox3.Text = cmd.Parameters["speciality"].Value.ToString();
                            textBox4.Text = cmd.Parameters["mobile"].Value.ToString();

                        }

                        dateTimePicker1.Text=getApp.Parameters["ap_date"].Value.ToString();
                        if (Convert.ToInt32(getApp.Parameters["op_ndd"].Value)> 0){
                            radioButton1.Checked = false;
                            radioButton2.Checked = true;
                        }
                        else
                        {
                            radioButton1.Checked = true;
                            radioButton2.Checked = false;

                        }

                        if (Convert.ToInt32(getApp.Parameters["adm"].Value) > 0)
                        {
                            radioButton4.Checked = true;
                            radioButton3.Checked = false;
                        }
                        else
                        {
                            radioButton4.Checked = false;
                            radioButton3.Checked = true;

                        }
                        textBox5.Text = getApp.Parameters["diag"].Value.ToString();
                        textBox6.Text = getApp.Parameters["comm"].Value.ToString();

                        OleDbCommand mt = new OleDbCommand();
                        mt.Connection = con.conn;
                        mt.CommandText = "med_test";
                        mt.CommandType = CommandType.StoredProcedure;
                        mt.Parameters.Add(new OleDbParameter("ap_num", appoint_no.Text));
                        mt.Parameters.Add(new OleDbParameter("med", OleDbType.Numeric, 20));
                        mt.Parameters["med"].Direction = ParameterDirection.Output;
                        mt.Parameters.Add(new OleDbParameter("test", OleDbType.Numeric, 20));
                        mt.Parameters["test"].Direction = ParameterDirection.Output;
                        mt.ExecuteNonQuery();
                        
                        dataGridView2.Rows.Add(Convert.ToInt32(mt.Parameters["test"].Value));
                       
                        dataGridView1.Rows.Add(Convert.ToInt32(mt.Parameters["med"].Value));

                        //getmedicine

                        for (int i = 0; i < Convert.ToInt32(mt.Parameters["med"].Value); i++) {
                            
                            OleDbCommand gm = new OleDbCommand();
                            gm.Connection = con.conn;
                            gm.CommandText = "get_medicine";
                            gm.CommandType = CommandType.StoredProcedure;
                            gm.Parameters.Add(new OleDbParameter("ap_num", appoint_no.Text));

                            gm.Parameters.Add(new OleDbParameter("s_num",i+1));


                            gm.Parameters.Add(new OleDbParameter("mname", OleDbType.VarChar, 20));
                            gm.Parameters["mname"].Direction = ParameterDirection.Output;
                            gm.Parameters.Add(new OleDbParameter("dos", OleDbType.VarChar, 20));
                            gm.Parameters["dos"].Direction = ParameterDirection.Output;
                            gm.ExecuteNonQuery();
                            dataGridView1.Rows[i].Cells["med_name"].Value = gm.Parameters["mname"].Value;
                            dataGridView1.Rows[i].Cells["a"].Value = gm.Parameters["dos"].Value;
                            dataGridView1.Rows.Add();
                            gm.Dispose();

                        }
                        //gettest
                        for (int i = 0; i < Convert.ToInt32(mt.Parameters["test"].Value); i++)
                        {
                            
                            OleDbCommand gt = new OleDbCommand();
                            gt.Connection = con.conn;
                            gt.CommandText = "get_test";
                            gt.CommandType = CommandType.StoredProcedure;
                            gt.Parameters.Add(new OleDbParameter("ap_num", appoint_no.Text));

                            gt.Parameters.Add(new OleDbParameter("s_num", i+1));


                            gt.Parameters.Add(new OleDbParameter("descr", OleDbType.VarChar, 20));
                            gt.Parameters["descr"].Direction = ParameterDirection.Output;
                            gt.Parameters.Add(new OleDbParameter("res", OleDbType.VarChar, 20));
                            gt.Parameters["res"].Direction = ParameterDirection.Output;
                            gt.ExecuteNonQuery();
                            dataGridView2.Rows[i].Cells["desc"].Value = gt.Parameters["descr"].Value;
                            dataGridView2.Rows[i].Cells["result"].Value = gt.Parameters["res"].Value;
                           

                            gt.Dispose();


                        }





                    }
                    catch (OleDbException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    advancedWizard1.NextButtonEnabled = false;


                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void update_appoint_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd-MMM-yyyy";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd-MMM-yyyy";
            dateTimePicker3.Format = DateTimePickerFormat.Custom;
            dateTimePicker3.CustomFormat = "dd-MMM-yyyy";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {

            try
            {
                con.con(user, pass);
                OleDbCommand up_ap = new OleDbCommand();
                up_ap.Connection = con.conn;
                up_ap.CommandText = "update_appoint";
                up_ap.CommandType = CommandType.StoredProcedure;

                up_ap.Parameters.Add(new OleDbParameter("ap_num",appoint_no.Text));

                up_ap.Parameters.Add(new OleDbParameter("doc_id", textBox1.Text));
                up_ap.Parameters.Add(new OleDbParameter("ap_date", dateTimePicker1.Text));
                if (radioButton4.Checked)
                {
                    up_ap.Parameters.Add(new OleDbParameter("op_ndd", Convert.ToInt32("0")));
                }
                if (radioButton2.Checked)
                {
                    up_ap.Parameters.Add(new OleDbParameter("op_ndd", 1));

                }
                if (radioButton1.Checked)
                {
                    up_ap.Parameters.Add(new OleDbParameter("adm", Convert.ToInt32("0")));
                }
                if (radioButton2.Checked)
                {
                    up_ap.Parameters.Add(new OleDbParameter("adm", 1));
                }
                up_ap.Parameters.Add(new OleDbParameter("diag", textBox5.Text));
                up_ap.Parameters.Add(new OleDbParameter("comm", textBox6.Text));
            

                up_ap.Parameters.Add(new OleDbParameter("op_sdate", dateTimePicker2.Text));
                up_ap.Parameters.Add(new OleDbParameter("op_stime", dateTimePicker4.Value.ToShortTimeString()));

                up_ap.Parameters.Add(new OleDbParameter("op_edate", dateTimePicker3.Text));
                up_ap.Parameters.Add(new OleDbParameter("op_etime", dateTimePicker5.Value.ToShortTimeString()));
                if (radioButton5.Checked)
                {
                    up_ap.Parameters.Add(new OleDbParameter("op_succ", 1));
                }
                if (radioButton6.Checked)
                {
                    up_ap.Parameters.Add(new OleDbParameter("op_succ", Convert.ToInt32("0")));
                }
                up_ap.ExecuteNonQuery();

                for(int i = 1; i < dataGridView1.Rows.Count; i++)
                {
                    OleDbCommand up_med = new OleDbCommand();
                    up_med.Connection = con.conn;
                    up_med.CommandType = CommandType.StoredProcedure;
                    up_med.CommandText = "update_medicine";
                    up_med.Parameters.Add(new OleDbParameter("ap_no", appoint_no.Text));
                    up_med.Parameters.Add(new OleDbParameter("m_name", dataGridView1.Rows[i-1].Cells["med_name"].Value));
                    up_med.Parameters.Add(new OleDbParameter("dos", dataGridView1.Rows[i - 1].Cells["a"].Value));
                    up_med.ExecuteNonQuery();


                }

                for (int i = 1; i < dataGridView2.Rows.Count; i++)
                {
                    OleDbCommand up_test = new OleDbCommand();
                    up_test.Connection = con.conn;
                    up_test.CommandType = CommandType.StoredProcedure;
                    up_test.CommandText = "update_medicine";
                    up_test.Parameters.Add(new OleDbParameter("ap_no", appoint_no.Text));
                    up_test.Parameters.Add(new OleDbParameter("test", dataGridView2.Rows[i - 1].Cells["desc"].Value));
                    up_test.Parameters.Add(new OleDbParameter("ress", dataGridView2.Rows[i - 1].Cells["result"].Value));
                    up_test.ExecuteNonQuery();


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            }

        private void button3_Click(object sender, EventArgs e)
        {
           
            Add_patient m = new PIMS.Add_patient(user,pass);
            m.Show();
        }
    }
}
