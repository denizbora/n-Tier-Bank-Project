using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;
using Bank.Business.Abstract;
using Bank.Business.DependencyResolvers.Ninject;
using Bank.Entities.Concrete;

namespace Bank.FormApp
{
    public partial class AccInfo : Form
    {
        public AccInfo()
        {
            InitializeComponent();
        }

        private void AccInfo_Load(object sender, EventArgs e)
        {
            var accountService = InstanceFactory.GetInstance<IAccountService>();
            Expression<Func<Account, bool>> usrExpression = user => user.GovId == Program.GovId;
            var ibanList = accountService.GetAll(usrExpression).ToList();
            foreach (var account in ibanList)
            {
                var add = new ListViewItem {Text = account.AccName};
                add.SubItems.Add(account.Iban);
                if (account.Currency == "Dollar")
                {
                    add.SubItems.Add(account.Funds + "$");
                }
                else if (account.Currency == "Euro")
                {
                    add.SubItems.Add(account.Funds + "€");
                }
                else if (account.Currency == "Pound")
                {
                    add.SubItems.Add(account.Funds + "£");
                }
                else
                {
                    add.SubItems.Add(account.Funds + "₺");
                }

                listView1.Items.Add(add);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox2.Checked = false;
                checkBox1.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox1.Checked = false;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox1.Checked = false;
                checkBox5.Checked = false;
            }
        }

        private void newBtn_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                var msg2 = "Do not leave any space.";
                MessageBox.Show(msg2);
            }
            else
            {
                var iban = Program.IbanGenerator();
                var govId = Program.GovId;
                var currency = string.Empty;
                var accName = textBox1.Text;
                if (checkBox2.Checked)
                {
                    currency = "Dollar";
                }
                else if (checkBox3.Checked)
                {
                    currency = "Euro";
                }
                else if (checkBox4.Checked)
                {
                    currency = "Pound";
                }
                else if (checkBox1.Checked)
                {
                    currency = "Lira";
                }
                else if (checkBox5.Checked)
                {
                    currency = "Invest";
                }

                int i = 0;
                var accountService = InstanceFactory.GetInstance<IAccountService>();
                Expression<Func<Account, bool>> usrExpression = user => user.GovId == Program.GovId;
                var ibanList = accountService.GetAll(usrExpression).ToList();
                foreach (var account in ibanList)
                {
                    if (account.AccName == accName)
                    {
                        i++;
                    }
                }

                var funds = "0";
                if (i == 1)
                {
                    var msg = "Try another account name.";
                    MessageBox.Show(msg);
                }
                else
                {
                    if (currency == "Invest")
                    {
                        var now = DateTime.Now;
                        var date = now.ToString();
                        accountService.Add(new Account()
                        {
                            AccName = accName, Currency = currency, Funds = 0, GovId = Program.GovId, Iban = iban,
                            Date = date
                        });
                    }
                    else
                    {
                        accountService.Add(new Account()
                            {AccName = accName, Currency = currency, Funds = 0, GovId = Program.GovId, Iban = iban});
                    }

                    Hide();
                    var msg = "Your new account is opened.";
                    MessageBox.Show(msg);
                    var frm = new AccInfo();
                    frm.Show();
                }
            }

        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                var msg2 = "Do not leave any space.";
                MessageBox.Show(msg2);
            }
            else
            {
                var accName = textBox1.Text;
                var accountService = InstanceFactory.GetInstance<IAccountService>();
                Expression<Func<Account, bool>> usrExpression = user => user.GovId == Program.GovId && user.AccName == accName;
                var account = accountService.Get(usrExpression);
                if (account==null)
                {
                    var message = "Account not found.";
                    MessageBox.Show(message);
                }
                else
                {
                    if (account.Funds <= 0)
                    {
                        accountService.Delete(account);
                        Hide();
                        var msg = "Your account is closed.";
                        MessageBox.Show(msg);
                        var frm = new AccInfo();
                        frm.Show();
                    }
                    else
                    {
                        var message = "You have funds on this account you can't delete it.";
                        MessageBox.Show(message);
                    }
                }
                

            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text=listView1.SelectedItems[0].Text;
        }

        private void AccInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            var frm = new User();
            frm.Show();
        }
    }
}
