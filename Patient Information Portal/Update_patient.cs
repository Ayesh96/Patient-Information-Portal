using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PIMS
{
    public partial class Update_patient : Form
    {
        connection con = new connection();
        string user, pass, _NIC;
        public Update_patient(string u, string p)
        {
            user = u;
            pass = p;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (maskedTextBox1.MaskFull == false)
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            maskedTextBox1.Text = "";
            name.Text = "";
            mobile.Text = "";
            gender.Text = "";
            dateTimePicker1.Text = "";
            address.Text = "";
            religion.Text = "";
            bloodgroup.Text = "";
            marital.Text = "";
            allergies.Text = "";
            ename.Text = "";
            emobile.Text = "";
            erelation.Text = "";
            eaddress.Text = "";


        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu m = new PIMS.Menu(user, pass);
            m.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void Update_patient_Load(object sender, EventArgs e)
        {

        }
    }
}
