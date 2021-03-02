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
    public partial class PassChange : Form
    {
        public PassChange()
        {
            InitializeComponent();
            textBox1.PasswordChar = '*';
            textBox2.PasswordChar = '*';
            textBox3.PasswordChar = '*';
        }

        private void chgnBtn_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                var msg2 = "Do not leave any space.";
                MessageBox.Show(msg2);
            }
            else
            {
                var userAccountService = InstanceFactory.GetInstance<IUserAccountService>();
                Expression<Func<UserAccount, bool>> usraccExpression = user => user.GovId == Program.GovId;
                var user = userAccountService.Get(usraccExpression);
                var oldPassword = Program.Md5Hash(textBox1.Text);
                if (oldPassword == user.Password)
                {
                    var newPassword = Program.Md5Hash(textBox2.Text);
                    var newPassword1 = Program.Md5Hash(textBox3.Text);
                    if (newPassword == newPassword1)
                    {
                        userAccountService.Update(new UserAccount(){GovId = Program.GovId,Name = user.Name,Password = newPassword,SecAns = user.SecAns,SecQue = user.SecQue});
                        var msg1 = "Password Has Been Changed";
                        MessageBox.Show(msg1);
                    }
                    else
                    {
                        var msg2 = "Password Are Not Same";
                        MessageBox.Show(msg2);
                    }
                }
                else
                {
                    var msg3 = "Wrong Password";
                    MessageBox.Show(msg3);
                }
            }
        }
    }
}
