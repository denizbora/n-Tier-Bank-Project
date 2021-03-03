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
using Microsoft.Data.SqlClient;

namespace Bank.FormApp
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            var govId = textBox1.Text;
            Program.GovId = govId;
            var password1 = Program.Md5Hash(textBox2.Text);
            var accountService = InstanceFactory.GetInstance<IUserAccountService>();
            Expression<Func<UserAccount, bool>> usrExpression = user => user.GovId == govId;
            var user = accountService.Get(usrExpression);
            if (user!=null)
            {
                var password = user.Password;
                if (password == password1)
                {
                    var frm1 = new User();
                    frm1.Show();
                    this.Close();
                }
                else
                {
                    var msg = "Wrong Password";
                    MessageBox.Show(msg);
                }
            }
            else
            {
                var msg = "User not found";
                MessageBox.Show(msg);
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var text = string.Empty;
            foreach (var character in textBox1.Text)
            {
                if (char.IsNumber(character))
                    text += character;
            }

            textBox1.Text = text;
        }
    }
}
