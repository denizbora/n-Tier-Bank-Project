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
    public partial class ChangePass : Form
    {
        public ChangePass()
        {
            InitializeComponent();
            textBox1.PasswordChar = '*';
            textBox2.PasswordChar = '*';
        }

        private void cngBtn_Click(object sender, EventArgs e)
        {
            var newPass = Program.Md5Hash(textBox1.Text);
            var newPass1 = Program.Md5Hash(textBox2.Text);
            var UserAccountService = InstanceFactory.GetInstance<IUserAccountService>();
            Expression<Func<UserAccount, bool>> usraccExpression = user => user.GovId == Program.GovId;
            if (newPass==newPass1)
            {
                UserAccountService.Update(new UserAccount(){GovId = Program.GovId,Password = newPass,Name = UserAccountService.Get(usraccExpression).Name, SecAns = UserAccountService.Get(usraccExpression).SecAns, SecQue = UserAccountService.Get(usraccExpression).SecQue });
                var msg1 = "Password Has Been Changed";
                MessageBox.Show(msg1);
            }
            else
            {
                var msg1 = "Password Are Not Same";
                MessageBox.Show(msg1);
            }

        }
    }
}
