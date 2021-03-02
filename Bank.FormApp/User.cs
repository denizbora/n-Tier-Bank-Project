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
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
        }

        private void User_Load(object sender, EventArgs e)
        {
            decimal funds0 = 0;
            decimal euro = 0, usd = 0, pound = 0;
            var accountService = InstanceFactory.GetInstance<IAccountService>();
            Expression<Func<Account, bool>> usrExpression = user => user.GovId == Program.GovId;
            var userAccountService = InstanceFactory.GetInstance<IUserAccountService>();
            Expression<Func<UserAccount, bool>> usraccExpression = user => user.GovId == Program.GovId;
            var accountList = accountService.GetAll(usrExpression).ToList();
            foreach (var account in accountList)
            {
                if (account.Currency == "Euro")
                {
                    var funds = account.Funds;
                    euro += funds;
                }
                else if (account.Currency == "Dollar")
                {
                    var funds = account.Funds;
                    usd += funds;
                }
                else if (account.Currency == "Pound")
                {
                    var funds = account.Funds;
                    pound += funds;
                }
                else
                {
                    var funds = account.Funds;
                    funds0 += funds;
                }
            }
            blnLbl1.Text = funds0.ToString("0.##") + @"₺";
            blnLbl2.Text = euro.ToString("0.##") + @"€";
            blnLbl3.Text = usd.ToString("0.##") + @"$";
            blnLbl4.Text = pound.ToString("0.##") + @"£";
            var total = Convert.ToDouble(funds0) + (Convert.ToDouble(euro) * Convert.ToDouble(Program.Euro_Buy()) / 10000) + (Convert.ToDouble(usd) * Convert.ToDouble(Program.Usd_Buy()) / 10000) + (Convert.ToDouble(pound) * Convert.ToDouble(Program.Pound_Buy()) / 10000);
            blnLbl5.Text = total.ToString("0.##") + @"₺";

            var hello = "Hello " + userAccountService.Get(usraccExpression).Name;
            nameLbl.Text = hello;
        }

        private void accinfoBtn_Click(object sender, EventArgs e)
        {
            var frm = new AccInfo();
            frm.Show();
            this.Close();
        }

        private void dpstBtn_Click(object sender, EventArgs e)
        {
            var frm = new Deposit();
            frm.Show();
            Close();
        }

        private void wthBtn_Click(object sender, EventArgs e)
        {
            var frm = new Withdraw();
            frm.Show();
            Close();
        }

        private void trnsBtn_Click(object sender, EventArgs e)
        {
            var frm = new Transfer();
            frm.Show();
            Close();
        }

        private void chngpassBtn_Click(object sender, EventArgs e)
        {
            var frm = new PassChange();
            frm.Show();
        }

        private void actvBtn_Click(object sender, EventArgs e)
        {
            var frm = new Activity();
            frm.Show();
            Close();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void xchngBtn_Click(object sender, EventArgs e)
        {
            var frm = new Exchange();
            frm.Show();
            Close();
        }
    }
}
