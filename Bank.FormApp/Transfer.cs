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
    public partial class Transfer : Form
    {
        public Transfer()
        {
            InitializeComponent();
            label6.Hide();
            textBox4.Hide();
        }

        private void Transfer_Load(object sender, EventArgs e)
        {
            var accountService = InstanceFactory.GetInstance<IAccountService>();
            Expression<Func<Account, bool>> usrExpression = user => user.GovId == Program.GovId;
            var accList = accountService.GetAll(usrExpression).ToList();
            foreach (var acc in accList)
            {
                comboBox2.Items.Add(acc.AccName);
            }
            var remrecService = InstanceFactory.GetInstance<IRemReceiverService>();
            Expression<Func<RemReceiver, bool>> receiverExpression = user => user.GovId == Program.GovId;
            var remList = remrecService.GetAll(receiverExpression);
            foreach (var rem in remList)
            {
                var add = new ListViewItem { Text = rem.RIban};
                add.SubItems.Add(rem.Currency);
                add.SubItems.Add(rem.Name);


                listView1.Items.Add(add);
            }
            var remopService = InstanceFactory.GetInstance<IRemOperationService>();
            Expression<Func<RemOperation, bool>> operationxpression = user => user.GovId == Program.GovId;
            var opList = remopService.GetAll(operationxpression);
            foreach (var op in opList)
            {
                comboBox1.Items.Add(op.OpName);
            }
        }


        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var remopService = InstanceFactory.GetInstance<IRemOperationService>();
            Expression<Func<RemOperation, bool>> remopExpression = user => user.GovId == Program.GovId && user.OpName==comboBox1.Text;
            var remop = remopService.Get(remopExpression);
            textBox1.Text = remop.RIban;
            textBox2.Text = remop.Price.ToString();
            textBox3.Text = remop.Comment;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            var text = string.Empty;
            foreach (var character in textBox2.Text)
            {
                if (char.IsNumber(character))
                    text += character;
            }

            textBox2.Text = text;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                label6.Show();
                textBox4.Show();
            }
            else if (checkBox2.Checked == false)
            {
                label6.Hide();
                textBox4.Hide();
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            var rIban = listView1.SelectedItems[0].SubItems[0].Text;
            textBox1.Text = rIban;
        }

        private void sndBtn_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && textBox2.Text == "")
            {
                var msg2 = "Do not leave any space.";
                MessageBox.Show(msg2);
            }
            else
            {
                var govId = Program.GovId;
                var rIban = "TR" + textBox1.Text; 
                var accountService = InstanceFactory.GetInstance<IAccountService>();
                Expression<Func<Account, bool>> usrExpression = user => user.GovId == Program.GovId && user.AccName==comboBox2.Text;
                var account = accountService.Get(usrExpression);
                var ıban = account.Iban;
                var currency = account.Currency;
                if (rIban == ıban)
                {
                    var msg0 = "Both Iban are same.";
                    MessageBox.Show(msg0);
                }
                else
                {
                    Expression<Func<Account, bool>> ribnExpression = user => user.Iban==rIban;
                    var ribnac = accountService.Get(ribnExpression);
                    var rCurrency = ribnac.Currency;
                    var gov = ribnac.GovId;
                    if (rCurrency == "Dollar" && currency != "Dollar" || rCurrency != "Dollar" && currency == "Dollar")
                    {
                        var msg1 = "You can't send another currency";
                        MessageBox.Show(msg1);
                    }
                    else if (rCurrency == "Euro" && currency != "Euro" || rCurrency != "Euro" && currency == "Euro")
                    {
                        var msg1 = "You can't send another currency";
                        MessageBox.Show(msg1);
                    }
                    else if (rCurrency == "Pound" && currency != "Pound" || rCurrency != "Pound" && currency == "Pound")
                    {
                        var msg1 = "You can't send another currency";
                        MessageBox.Show(msg1);
                    }
                    else if (rCurrency == "Invest" && gov != govId)
                    {
                        var msg1 = "You can't send from this account to anyone else.";
                        MessageBox.Show(msg1);
                    }
                    else if (currency == "Invest" && gov != govId)
                    {
                        var msg1 = "You can't send any money from this account.";
                        MessageBox.Show(msg1);
                    }
                    else
                    {
                        Expression<Func<Account, bool>> ibnExpression = user => user.Iban == ıban;
                        var ibnac = accountService.Get(ibnExpression);
                        var funds = ibnac.Funds;
                        var send = Convert.ToDouble(textBox2.Text);
                        var oldFunds = Convert.ToDouble(funds);
                        var newFunds = oldFunds - send;
                        var newFunds1 = Convert.ToString(newFunds);
                        if (newFunds < 0)
                        {
                            var msg1 = "You don't Have Enough Money";
                            MessageBox.Show(msg1);

                        }
                        else
                        {
                            try
                            {

                                if (gov == "" || gov == null)
                                {
                                    var msg0 = "Can't find receiver";
                                    MessageBox.Show(msg0);
                                }
                                else
                                {
                                    var now = DateTime.Now;
                                    var activity = "Send To " + rIban;
                                    var date = now.ToString();
                                    var price = "-" + send;
                                    var activityService = InstanceFactory.GetInstance<IActivityService>();
                                    activityService.Add(new Entities.Concrete.Activity(){GovId = govId,Iban = ıban,Event = activity,Price = price,Date = date,Comment = textBox3.Text});
                                    var rNow = DateTime.Now;
                                    var rActivity = "Receive From " + ıban;
                                    var rDate = rNow.ToString();
                                    var rPrice = "+" + send;
                                    activityService.Add(new Entities.Concrete.Activity(){GovId = gov,Iban = rIban,Event = rActivity,Price = rPrice,Date = rDate,Comment = textBox3.Text});
                                    var useraccountService = InstanceFactory.GetInstance<IUserAccountService>();
                                    Expression<Func<UserAccount, bool>> usraccExpression = user => user.GovId == gov;

                                    var rName = useraccountService.Get(usraccExpression).Name;
                                    if (checkBox1.Checked)
                                    {
                                        var remrecService = InstanceFactory.GetInstance<IRemReceiverService>();
                                        Expression<Func<RemReceiver, bool>> remrecExpression = user => user.GovId == govId;

                                        var i = 0;
                                        var remrecList = remrecService.GetAll(remrecExpression);
                                        foreach (var remrec in remrecList)
                                        {
                                            if (remrec.RIban==rIban)
                                            {
                                                i++;
                                            }
                                        }
                                        if (i == 0)
                                        {
                                            remrecService.Add(new RemReceiver(){GovId = govId,RIban = rIban,Name = rName,Currency = rCurrency});
                                        }
                                    }
                                    Expression<Func<Account, bool>> ac1Expression = user => user.Iban == ıban;
                                    var ac1 = accountService.Get(ac1Expression);
                                    accountService.Update(new Account(){AccName = ac1.AccName,Currency = ac1.Currency,Funds = Convert.ToDecimal(newFunds1),GovId = govId,Iban = ıban});
                                    Expression<Func<Account, bool>> ac2Expression = user => user.Iban == rIban;
                                    var ac2 = accountService.Get(ac2Expression);
                                    var rOldFunds = Convert.ToDouble(ac2.Funds);
                                    var rNewFunds = rOldFunds + send;
                                    var rNewFunds1 = Convert.ToString(rNewFunds);
                                    accountService.Update(new Account() { AccName = ac2.AccName, Currency = ac2.Currency, Funds = Convert.ToDecimal(rNewFunds1), GovId = gov, Iban = rIban });
                                    if (checkBox2.Checked)
                                    {
                                        var remopService = InstanceFactory.GetInstance<IRemOperationService>();
                                        remopService.Add(new RemOperation(){Comment = textBox3.Text,OpName = textBox4.Text,Price = Convert.ToDecimal(send),GovId = govId,RIban = rIban});
                                    }
                                    Hide();
                                    var msg2 = "Transfer Has Been Successfully";
                                    MessageBox.Show(msg2);
                                    var frm = new Transfer();
                                    frm.Show();
                                }

                            }
                            catch
                            {
                                var msg0 = "Can't find receiver";
                                MessageBox.Show(msg0);
                            }
                        }


                    }
                }
            }
        }

        private void Transfer_FormClosed(object sender, FormClosedEventArgs e)
        {
            var frm = new User();
            frm.Show();
        }
    }
}
