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
    public partial class Deposit : Form
    {
        public Deposit()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var accountService = InstanceFactory.GetInstance<IAccountService>();
            Expression<Func<Account, bool>> usrExpression = user => user.GovId == Program.GovId && user.AccName==comboBox1.Text;
            var account = accountService.Get(usrExpression);
            accountService.Update(new Account(){AccName = account.AccName,Currency = account.Currency,Funds = account.Funds+Convert.ToDecimal(textBox1.Text),Iban = account.Iban,GovId = Program.GovId});
            var now = DateTime.Now;
            var activity = "Deposit";
            var date = now.ToString();
            var price = "+" + textBox1.Text;
            var activityService = InstanceFactory.GetInstance<IActivityService>();
            activityService.Add(new Entities.Concrete.Activity(){Date = date,Event = activity,GovId = Program.GovId,Iban = account.Iban,Price = price});
            var message = "Deposit has been successful, new funds:" + (account.Funds+Convert.ToDecimal(textBox1.Text)) + "";
            MessageBox.Show(message);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && (((TextBox)sender).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void Deposit_Load(object sender, EventArgs e)
        {
            var accountService = InstanceFactory.GetInstance<IAccountService>();
            Expression<Func<Account, bool>> usrExpression = user => user.GovId == Program.GovId;
            var accountList=accountService.GetAll(usrExpression).ToList();
            foreach (var account in accountList)
            {
                if (account.Currency == "Invest")
                {

                }
                else
                {
                    comboBox1.Items.Add(account.AccName);
                }
            }
            
        }

        private void Deposit_FormClosed(object sender, FormClosedEventArgs e)
        {
            var frm = new User();
            frm.Show();
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
