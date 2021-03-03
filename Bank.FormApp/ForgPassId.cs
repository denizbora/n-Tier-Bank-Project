using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;
using Bank.Business.Abstract;
using Bank.Business.DependencyResolvers.Ninject;
using Bank.Entities.Concrete;

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
            var accountService = InstanceFactory.GetInstance<IUserAccountService>();
            Expression<Func<UserAccount, bool>> usrExpression = user => user.GovId == govId;
            var user = accountService.Get(usrExpression);
            if (user != null)
            {
                Program.GovId = govId;
                var frm1 = new ForgPass();
                frm1.Show();
                this.Close();
            }
            else
            {
                var msg = "User not found";
                MessageBox.Show(msg);
            }
            
        }
    }
}
