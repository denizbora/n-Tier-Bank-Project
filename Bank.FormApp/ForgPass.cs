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
    public partial class ForgPass : Form
    {
        public ForgPass()
        {
            InitializeComponent();
        }

        private void ForgPass_Load(object sender, EventArgs e)
        {
            var UserAccountService = InstanceFactory.GetInstance<IUserAccountService>();
            Expression<Func<UserAccount, bool>> usraccExpression = user => user.GovId == Program.GovId;
            qLbl.Text=UserAccountService.Get(usraccExpression).SecQue;
        }

        private void nxtBtn_Click(object sender, EventArgs e)
        {
            var secAns1 = Program.Md5Hash(textBox1.Text);
            var UserAccountService = InstanceFactory.GetInstance<IUserAccountService>();
            Expression<Func<UserAccount, bool>> usraccExpression = user => user.GovId == Program.GovId;
            if (UserAccountService.Get(usraccExpression).SecAns == secAns1)
            {
                var frm = new ChangePass();
                frm.Show();
                this.Close();
            }
            else
            {
                var msg = "Wrong Answer";
                MessageBox.Show(msg);
            }
        }
    }
}
