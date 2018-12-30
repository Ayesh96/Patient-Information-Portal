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
    public partial class medicine : Form
    {
        string user, pass;
        public medicine(string u,string p)
        {
            user = u;
            pass = p;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            errorProvider1.Clear();
            if (Appointment.Text.Length <= 1)
            {
                errorProvider1.SetError(Appointment, "enter");
            }
            if (medicinename.Text.Length <= 1)
            {
                errorProvider1.SetError(medicinename, "enter");
            }
            if (dosage.Text.Length <= 1)
            {
                errorProvider1.SetError(dosage, "enter");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu m = new PIMS.Menu(user,pass);
            m.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
