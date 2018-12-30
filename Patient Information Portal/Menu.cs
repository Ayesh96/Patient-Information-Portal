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
    public partial class Menu : Form
    {
        String user, pass;
        public Menu(string u,string p)
        {
            user = u;
            pass = p;
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Add_patient m = new PIMS.Add_patient(user,pass);
            m.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
           SearchPatient m = new PIMS.SearchPatient(user,pass);
            m.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Appointment m = new PIMS.Appointment(user,pass);
            m.Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            test m = new PIMS.test(user,pass);
            m.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            login m = new PIMS.login();
            m.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Update_patient m = new PIMS.Update_patient(user,pass);
            m.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            update_appoint m = new update_appoint(user, pass);
            m.Show();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            SearchAppoint m = new PIMS.SearchAppoint(user,pass);
            m.Show();
        }
    }
}
