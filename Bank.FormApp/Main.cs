using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bank.Business.Abstract;
using Bank.Business.DependencyResolvers.Ninject;
using Bank.DataAccess.Abstract;
using Microsoft.Data.SqlClient;

namespace Bank.FormApp
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            var frm = new Login();
            frm.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            var accountService = InstanceFactory.GetInstance<IAccountService>();
            var accountList = accountService.GetAll().ToList();
            foreach (var account in accountList)
            {
                if (account.Date==String.Empty || account.Date =="" || account.Date==null)
                {
                }
                else
                {
                    var day = Convert.ToInt32((now - Convert.ToDateTime(account.Date)).TotalDays);
                    for (var i = 0; i <= day; i++)
                    {
                        if (i==0)
                        {
                            
                        }
                        else
                        {
                            account.Funds += account.Funds * (decimal)0.00031;
                        }
                        
                    }
                    account.Date=DateTime.Now.ToString();
                    accountService.Update(account);
                }
            }
        }

        private void forqPassBtn_Click(object sender, EventArgs e)
        {
            var frm = new ForgPassId();
            frm.Show();
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            var frm = new Register();
            frm.Show();
        }
    }
}
