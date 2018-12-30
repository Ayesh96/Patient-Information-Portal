using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PIMS
{
    public partial class Add_patient : Form
    {
        connection con = new connection();
        string user, pass,_NIC;
        public Add_patient(string u,string p)
        {
            user = u;
            pass = p;
        
            InitializeComponent();
        }


        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (maskedTextBox1.MaskFull== false)
            {
                errorProvider1.SetError(maskedTextBox1, "NIC");
            }
            
            if (name.Text.Length <= 1)
            {
                errorProvider1.SetError(name, "enter Name");
            }
            if (gender.Text.Length <= 1)
            {
                errorProvider1.SetError(gender, "select gender");
            }
            if (mobile.Text.Length <= 1)
            {
                errorProvider1.SetError(mobile, "enter mobile number");
            }
          
            if (address.Text.Length <= 1)
            {
                errorProvider1.SetError(address, "enter address");
            }
            if (bloodgroup.Text.Length <= 1)
            {
                errorProvider1.SetError(bloodgroup, "enter blood group");
            }
            if (marital.Text.Length <= 1)
            {
                errorProvider1.SetError(marital, "add marital status");
            }
            if (religion.Text.Length <= 1)
            {
                errorProvider1.SetError(religion, "enter religion");
            }
            if (ename.Text.Length <= 1)
            {
                errorProvider1.SetError(ename, "enter name");
            }
            if (emobile.Text.Length <= 1)
            {
                errorProvider1.SetError(emobile, "enter mobile number");
            }
            if (erelation.Text.Length <= 1)
            {
                errorProvider1.SetError(erelation, "enter relation with patient");
            }
            if (eaddress.Text.Length <= 1)
            {
                errorProvider1.SetError(eaddress, "enter address");
            }
            try
            {
                //con.conn.Close();
                con.con(user,pass);
                

                OleDbCommand comm = new OleDbCommand();
                comm.Connection = con.conn;
                comm.CommandText = "insert_patient";
                comm.CommandType = CommandType.StoredProcedure;

                // input parameters
                comm.Parameters.Add(new OleDbParameter("p_cnic", maskedTextBox1.Text));
                comm.Parameters.Add(new OleDbParameter("p_name", name.Text));
                comm.Parameters.Add(new OleDbParameter("p_mobile", mobile.Text));
                comm.Parameters.Add(new OleDbParameter("p_address",address.Text));
                comm.Parameters.Add(new OleDbParameter("p_dob", dateTimePicker1.Text));
                comm.Parameters.Add(new OleDbParameter("p_blood_group",bloodgroup.Text));
                comm.Parameters.Add(new OleDbParameter("p_emergency_contact_name", ename.Text));
                comm.Parameters.Add(new OleDbParameter("p_emergency_contact_number", emobile.Text));
                comm.Parameters.Add(new OleDbParameter("p_emergency_contact_address", eaddress.Text));
                comm.Parameters.Add(new OleDbParameter("p_emergency_contact_relation", erelation.Text));
                comm.Parameters.Add(new OleDbParameter("marital_status",marital.Text));
                comm.Parameters.Add(new OleDbParameter("p_religion",religion.Text));
                comm.Parameters.Add(new OleDbParameter("p_allergies",allergies.Text));
                comm.Parameters.Add(new OleDbParameter("p_gender",gender.Text));
                comm.ExecuteNonQuery();
                

                //out parameters

                //comm.Parameters.Add(new OleDbParameter("c",OleDbType.VarChar));
                //comm.Parameters["c"].Direction = ParameterDirection.Output;
                //MessageBox.Show(comm.Parameters["c"].Value.ToString());
                con.conn.Close();
                _NIC = maskedTextBox1.Text;
                button4.Enabled = true;
                MessageBox.Show("Record Added");
                maskedTextBox1.Clear();
                name.Clear();
                mobile.Clear();
                address.Clear();
                ename.Clear();
                emobile.Clear();
                eaddress.Clear();
                erelation.Clear();
                religion.Clear();
                allergies.Clear();
                
            }
            catch(OleDbException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.conn.Close();
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
           Menu m = new PIMS.Menu(user,pass);
            m.Show();
        }

        private void nic_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void mobile_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void nic_KeyDown(object sender, KeyEventArgs e)
        {
            //string sVal = nic.Text;

            //if (!string.IsNullOrEmpty(sVal) && e.KeyCode != Keys.Back)
            //{
            //    sVal = sVal.Replace("-", "");
            //    string newst = Regex.Replace(sVal, ".{5}", "$0-");
            //    nic.Text = newst;
            //    nic.SelectionStart = nic.Text.Length;
            //}
        }

        private void Add_patient_Load(object sender, EventArgs e)
        {
            gender.SelectedIndex = 1;
            marital.SelectedIndex = 1;
            bloodgroup.SelectedIndex = 1;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd-MMM-yyyy";
            button4.Enabled = false;
        }

        private void maskedTextBox1_Enter(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Appointment n = new Appointment(user, pass, _NIC);
            n.Show();
            this.Hide();
        }

        private void mobile_KeyDown(object sender, KeyEventArgs e)
        {
            string sVal = mobile.Text;

            if (!string.IsNullOrEmpty(sVal) && e.KeyCode != Keys.Back)
            {
                sVal = sVal.Replace("-", "");
                string newst = Regex.Replace(sVal, ".{4}", "$0-");
                mobile.Text = newst;
                mobile.SelectionStart = mobile.Text.Length;
            }
        }
    }
}
