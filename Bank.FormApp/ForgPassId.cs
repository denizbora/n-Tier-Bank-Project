using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Bank.FormApp
{
    public partial class ForgPassId : Form
    {
        public ForgPassId()
        {
            InitializeComponent();
        }

        private void nxtBtn_Click(object sender, EventArgs e)
        {
            var govId = textBox1.Text;
            Program.GovId = govId;
            var frm1 = new ForgPass();
            frm1.Show();
            this.Close();
        }
    }
}
