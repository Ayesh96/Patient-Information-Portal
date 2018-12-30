
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

namespace PIMS
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();        }

        private void button6_Click(object sender, EventArgs e)
        {
            connection connect = new connection();
            
            try
            {

                connect.con(textBox1.Text, textBox2.Text);
                //OleDbCommand cmd = new OleDbCommand();
                //cmd.Connection = connect.conn;
                //cmd.CommandText = "select dname from dept where deptno = 10";
                //cmd.CommandType = CommandType.Text;
                //OleDbDataReader dr = cmd.ExecuteReader();
                //dr.Read();
                //MessageBox.Show(dr.GetString(0).ToString());
                
                Menu m = new PIMS.Menu(textBox1.Text,textBox2.Text);
                m.Show();
                this.Hide();
             
            }
            catch(OleDbException ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
