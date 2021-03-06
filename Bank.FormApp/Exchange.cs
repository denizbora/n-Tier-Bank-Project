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
    public partial class Exchange : Form
    {
        public Exchange()
        {
            InitializeComponent();
        }

        private void Exchange_Load(object sender, EventArgs e)
        {
            string[] row = { "Dollar", Program.Usd_Buy(), Program.Usd_Sell() };
            string[] row1 = { "Euro", Program.Euro_Buy(), Program.Euro_Sell() };
            string[] row2 = { "Pound", Program.Pound_Buy(), Program.Pound_Sell() };
            var listViewItem = new ListViewItem(row);
            var listViewItem1 = new ListViewItem(row1);
            var listViewItem2 = new ListViewItem(row2);
            listView1.Items.Add(listViewItem);
            listView1.Items.Add(listViewItem1);
            listView1.Items.Add(listViewItem2);
            comboBox1.Items.Add("Lira");
            comboBox1.Items.Add("Dollar");
            comboBox1.Items.Add("Euro");
            comboBox1.Items.Add("Pound");
            comboBox2.Items.Add("Lira");
            comboBox2.Items.Add("Dollar");
            comboBox2.Items.Add("Euro");
            comboBox2.Items.Add("Pound");
        }

        private void xchngBtn_Click(object sender, EventArgs e)
        {
            var activityService = InstanceFactory.GetInstance<IActivityService>();
            var accountService = InstanceFactory.GetInstance<IAccountService>();
            Expression<Func<Account, bool>> usr3Expression = user => user.GovId == Program.GovId && user.AccName == comboBox3.Text;
            Expression<Func<Account, bool>> usr4Expression = user => user.GovId == Program.GovId && user.AccName == comboBox4.Text;
            if (comboBox1.Text == comboBox2.Text)
            {
                var msg = "You can't exchange same currency";
                MessageBox.Show(msg);
            }
            else
            {
                if (comboBox1.Text == "Lira")
                {
                    var account3 = accountService.Get(usr3Expression);
                    var lira = account3.Funds;
                    var lira1 = Convert.ToDouble(lira);
                    if (comboBox2.Text == "Dollar")
                    {
                        
                        var account4 = accountService.Get(usr4Expression);
                        var rIban = account4.Iban;
                        var dollar = account4.Funds;
                        var dollar1 = Convert.ToDouble(dollar);
                        var addDollar = Convert.ToDouble(textBox1.Text) / Convert.ToDouble(Program.Usd_Sell()) * 10000 + dollar1;
                        var fundsDollar = addDollar.ToString();
                        var minusLira = lira1 - Convert.ToDouble(textBox1.Text);
                        var fundsLira = minusLira.ToString();
                        if (minusLira < 0)
                        {
                            var msg1 = "You don't Have Enough Money";
                            MessageBox.Show(msg1);

                        }
                        else
                        {
                            accountService.Update(new Account(){AccName = account3.AccName,Currency = account3.Currency,Funds = Convert.ToDecimal(fundsLira),GovId = Program.GovId,Iban = account3.Iban});
                            accountService.Update(new Account(){AccName = account4.AccName,Currency = account4.Currency,Funds = Convert.ToDecimal(fundsDollar),GovId = Program.GovId,Iban = account4.Iban});
                            var now = DateTime.Now;
                            var activity = "Exchange To: " + rIban;
                            var date = now.ToString();
                            var price = "-" + textBox1.Text + "₺";
                            activityService.Add(new Entities.Concrete.Activity(){Date = date,Event = activity,GovId = Program.GovId,Iban = account3.Iban,Price = price});
                            var msg1 = "Exchange has been successful";
                            MessageBox.Show(msg1);
                        }


                    }
                    else if (comboBox2.Text == "Euro")
                    {
                        var account4 = accountService.Get(usr4Expression);
                        var rIban = account4.Iban;
                        var euro = account4.Funds;
                        var euro1 = Convert.ToDouble(euro);
                        var addEuro = Convert.ToDouble(textBox1.Text) / Convert.ToDouble(Program.Euro_Sell()) * 10000 + euro1;
                        var fundsEuro = addEuro.ToString();
                        var minusLira = lira1 - Convert.ToDouble(textBox1.Text);
                        var fundsLira = minusLira.ToString();
                        if (minusLira < 0)
                        {
                            var msg1 = "You don't Have Enough Money";
                            MessageBox.Show(msg1);

                        }
                        else
                        {
                            accountService.Update(new Account() { AccName = account3.AccName, Currency = account3.Currency, Funds = Convert.ToDecimal(fundsLira), GovId = Program.GovId, Iban = account3.Iban });
                            accountService.Update(new Account() { AccName = account4.AccName, Currency = account4.Currency, Funds = Convert.ToDecimal(fundsEuro), GovId = Program.GovId, Iban = account4.Iban });
                            var now = DateTime.Now;
                            var activity = "Exchange To: " + rIban;
                            var date = now.ToString();
                            var price = "-" + textBox1.Text + "₺";
                            activityService.Add(new Entities.Concrete.Activity() { Date = date, Event = activity, GovId = Program.GovId, Iban = account3.Iban, Price = price });
                            var msg1 = "Exchange has been successful";
                            MessageBox.Show(msg1);
                        }
                    }
                    else if (comboBox2.Text == "Pound")
                    {
                        var account4 = accountService.Get(usr4Expression);
                        var rIban = account4.Iban;
                        var pound = account4.Funds;
                        var pound1 = Convert.ToDouble(pound);
                        var addPound = Convert.ToDouble(textBox1.Text) / Convert.ToDouble(Program.Pound_Sell()) * 10000 + pound1;
                        var fundsPound = addPound.ToString();
                        var minusLira = lira1 - Convert.ToDouble(textBox1.Text);
                        var fundsLira = minusLira.ToString();
                        if (minusLira < 0)
                        {
                            var msg1 = "You don't Have Enough Money";
                            MessageBox.Show(msg1);

                        }
                        else
                        {
                            accountService.Update(new Account() { AccName = account3.AccName, Currency = account3.Currency, Funds = Convert.ToDecimal(fundsLira), GovId = Program.GovId, Iban = account3.Iban });
                            accountService.Update(new Account() { AccName = account4.AccName, Currency = account4.Currency, Funds = Convert.ToDecimal(fundsPound), GovId = Program.GovId, Iban = account4.Iban });
                            var now = DateTime.Now;
                            var activity = "Exchange To: " + rIban;
                            var date = now.ToString();
                            var price = "-" + textBox1.Text + "₺";
                            activityService.Add(new Entities.Concrete.Activity() { Date = date, Event = activity, GovId = Program.GovId, Iban = account3.Iban, Price = price });

                            var msg1 = "Exchange has been successful";
                            MessageBox.Show(msg1);
                        }

                    }

                }
                else if (comboBox1.Text == "Dollar")
                {
                    var account3 = accountService.Get(usr3Expression);
                    var dollar = account3.Funds;
                    var dollar1 = Convert.ToDouble(dollar);
                    if (comboBox2.Text == "Lira")
                    {
                        var account4 = accountService.Get(usr4Expression);
                        var rIban = account4.Iban;
                        var lira = account4.Funds;
                        var lira1 = Convert.ToDouble(lira);
                        var addLira = Convert.ToDouble(textBox1.Text) * Convert.ToDouble(Program.Usd_Buy()) / 10000 + lira1;
                        var fundsLira = addLira.ToString();
                        var minusDollar = dollar1 - Convert.ToDouble(textBox1.Text);
                        var fundsDollar = minusDollar.ToString();
                        if (minusDollar < 0)
                        {
                            var msg1 = "You don't Have Enough Money";
                            MessageBox.Show(msg1);

                        }
                        else
                        {
                            accountService.Update(new Account() { AccName = account3.AccName, Currency = account3.Currency, Funds = Convert.ToDecimal(fundsDollar), GovId = Program.GovId, Iban = account3.Iban });
                            accountService.Update(new Account() { AccName = account4.AccName, Currency = account4.Currency, Funds = Convert.ToDecimal(fundsLira), GovId = Program.GovId, Iban = account4.Iban });
                            var now = DateTime.Now;
                            var activity = "Exchange To: " + rIban;
                            var date = now.ToString();
                            var price = "-" + textBox1.Text + "$";
                            activityService.Add(new Entities.Concrete.Activity() { Date = date, Event = activity, GovId = Program.GovId, Iban = account3.Iban, Price = price });
                            var msg1 = "Exchange has been successful";
                            MessageBox.Show(msg1);
                        }
                    }
                    else if (comboBox2.Text == "Euro")
                    {
                        var account4 = accountService.Get(usr4Expression);
                        var rIban = account4.Iban;
                        var euro = account4.Funds;
                        var euro1 = Convert.ToDouble(euro);
                        var addEuro = Convert.ToDouble(textBox1.Text) * Convert.ToDouble(Program.Usd_Buy()) / Convert.ToDouble(Program.Euro_Sell()) + euro1;
                        var fundsEuro = addEuro.ToString();
                        var minusDollar = dollar1 - Convert.ToDouble(textBox1.Text);
                        var fundsDollar = minusDollar.ToString();
                        if (minusDollar < 0)
                        {
                            var msg1 = "You don't Have Enough Money";
                            MessageBox.Show(msg1);

                        }
                        else
                        {
                            accountService.Update(new Account() { AccName = account3.AccName, Currency = account3.Currency, Funds = Convert.ToDecimal(fundsDollar), GovId = Program.GovId, Iban = account3.Iban });
                            accountService.Update(new Account() { AccName = account4.AccName, Currency = account4.Currency, Funds = Convert.ToDecimal(fundsEuro), GovId = Program.GovId, Iban = account4.Iban });
                            var now = DateTime.Now;
                            var activity = "Exchange To: " + rIban;
                            var date = now.ToString();
                            var price = "-" + textBox1.Text + "$";
                            activityService.Add(new Entities.Concrete.Activity() { Date = date, Event = activity, GovId = Program.GovId, Iban = account3.Iban, Price = price });
                            var msg1 = "Exchange has been successful";
                            MessageBox.Show(msg1);
                        }
                    }
                    else if (comboBox2.Text == "Pound")
                    {
                        var account4 = accountService.Get(usr4Expression);
                        var rIban = account4.Iban;
                        var pound = account4.Funds;
                        var pound1 = Convert.ToDouble(pound);
                        var addPound = Convert.ToDouble(textBox1.Text) * Convert.ToDouble(Program.Usd_Buy()) / Convert.ToDouble(Program.Pound_Sell()) + pound1;
                        var fundsPound = addPound.ToString();
                        var minusDollar = dollar1 - Convert.ToDouble(textBox1.Text);
                        var fundsDollar = minusDollar.ToString();
                        if (minusDollar < 0)
                        {
                            var msg1 = "You don't Have Enough Money";
                            MessageBox.Show(msg1);

                        }
                        else
                        {
                            accountService.Update(new Account() { AccName = account3.AccName, Currency = account3.Currency, Funds = Convert.ToDecimal(fundsDollar), GovId = Program.GovId, Iban = account3.Iban });
                            accountService.Update(new Account() { AccName = account4.AccName, Currency = account4.Currency, Funds = Convert.ToDecimal(fundsPound), GovId = Program.GovId, Iban = account4.Iban });
                            var now = DateTime.Now;
                            var activity = "Exchange To: " + rIban;
                            var date = now.ToString();
                            var price = "-" + textBox1.Text + "$";
                            activityService.Add(new Entities.Concrete.Activity() { Date = date, Event = activity, GovId = Program.GovId, Iban = account3.Iban, Price = price });
                            var msg1 = "Exchange has been successful";
                            MessageBox.Show(msg1);
                        }
                    }
                }
                else if (comboBox1.Text == "Euro")
                {
                    var account3 = accountService.Get(usr3Expression);
                    var euro = account3.Funds;
                    var euro1 = Convert.ToDouble(euro);
                    if (comboBox2.Text == "Dollar")
                    {
                        var account4 = accountService.Get(usr4Expression);
                        var rIban = account4.Iban;
                        var dollar = account4.Funds;
                        var dollar1 = Convert.ToDouble(dollar);
                        var addDollar = Convert.ToDouble(textBox1.Text) * Convert.ToDouble(Program.Euro_Buy()) / Convert.ToDouble(Program.Usd_Sell()) + dollar1;
                        var fundsDollar = addDollar.ToString();
                        var minusEuro = euro1 - Convert.ToDouble(textBox1.Text);
                        var fundsEuro = minusEuro.ToString();
                        if (minusEuro < 0)
                        {
                            var msg1 = "You don't Have Enough Money";
                            MessageBox.Show(msg1);

                        }
                        else
                        {
                            accountService.Update(new Account() { AccName = account3.AccName, Currency = account3.Currency, Funds = Convert.ToDecimal(fundsEuro), GovId = Program.GovId, Iban = account3.Iban });
                            accountService.Update(new Account() { AccName = account4.AccName, Currency = account4.Currency, Funds = Convert.ToDecimal(fundsDollar), GovId = Program.GovId, Iban = account4.Iban });
                            var now = DateTime.Now;
                            var activity = "Exchange To: " + rIban;
                            var date = now.ToString();
                            var price = "-" + textBox1.Text + "€";
                            activityService.Add(new Entities.Concrete.Activity() { Date = date, Event = activity, GovId = Program.GovId, Iban = account3.Iban, Price = price });
                            var msg1 = "Exchange has been successful";
                            MessageBox.Show(msg1);
                        }
                    }
                    else if (comboBox2.Text == "Lira")
                    {
                        var account4 = accountService.Get(usr4Expression);
                        var rIban = account4.Iban;
                        var lira = account4.Funds;
                        var lira1 = Convert.ToDouble(lira);
                        var addLira = Convert.ToDouble(textBox1.Text) * Convert.ToDouble(Program.Euro_Buy()) / 10000 + lira1;
                        var fundsLira = addLira.ToString();
                        var minusEuro = euro1 - Convert.ToDouble(textBox1.Text);
                        var fundsEuro = minusEuro.ToString();
                        if (minusEuro < 0)
                        {
                            var msg1 = "You don't Have Enough Money";
                            MessageBox.Show(msg1);

                        }
                        else
                        {
                            accountService.Update(new Account() { AccName = account3.AccName, Currency = account3.Currency, Funds = Convert.ToDecimal(fundsEuro), GovId = Program.GovId, Iban = account3.Iban });
                            accountService.Update(new Account() { AccName = account4.AccName, Currency = account4.Currency, Funds = Convert.ToDecimal(fundsLira), GovId = Program.GovId, Iban = account4.Iban });
                            var now = DateTime.Now;
                            var activity = "Exchange To: " + rIban;
                            var date = now.ToString();
                            var price = "-" + textBox1.Text + "€";
                            activityService.Add(new Entities.Concrete.Activity() { Date = date, Event = activity, GovId = Program.GovId, Iban = account3.Iban, Price = price });
                            var msg1 = "Exchange has been successful";
                            MessageBox.Show(msg1);
                        }
                    }
                    else if (comboBox2.Text == "Pound")
                    {
                        var account4 = accountService.Get(usr4Expression);
                        var rIban = account4.Iban;
                        var pound = account4.Funds;
                        var pound1 = Convert.ToDouble(pound);
                        var addPound = Convert.ToDouble(textBox1.Text) * Convert.ToDouble(Program.Euro_Buy()) / Convert.ToDouble(Program.Pound_Sell()) + pound1;
                        var fundsPound = addPound.ToString();
                        var minusEuro = euro1 - Convert.ToDouble(textBox1.Text);
                        var fundsEuro = minusEuro.ToString();
                        if (minusEuro < 0)
                        {
                            var msg1 = "You don't Have Enough Money";
                            MessageBox.Show(msg1);

                        }
                        else
                        {
                            accountService.Update(new Account() { AccName = account3.AccName, Currency = account3.Currency, Funds = Convert.ToDecimal(fundsEuro), GovId = Program.GovId, Iban = account3.Iban });
                            accountService.Update(new Account() { AccName = account4.AccName, Currency = account4.Currency, Funds = Convert.ToDecimal(fundsPound), GovId = Program.GovId, Iban = account4.Iban });
                            var now = DateTime.Now;
                            var activity = "Exchange To: " + rIban;
                            var date = now.ToString();
                            var price = "-" + textBox1.Text + "€";
                            activityService.Add(new Entities.Concrete.Activity() { Date = date, Event = activity, GovId = Program.GovId, Iban = account3.Iban, Price = price });
                            var msg1 = "Exchange has been successful";
                            MessageBox.Show(msg1);
                        }
                    }
                }
                else if (comboBox1.Text == "Pound")
                {
                    var account3 = accountService.Get(usr3Expression);
                    var pound = account3.Funds;
                    var pound1 = Convert.ToDouble(pound);
                    if (comboBox2.Text == "Dollar")
                    {
                        var account4 = accountService.Get(usr4Expression);
                        var rIban = account4.Iban;
                        var dollar = account4.Funds;
                        var dollar1 = Convert.ToDouble(dollar);
                        var addDollar = Convert.ToDouble(textBox1.Text) * Convert.ToDouble(Program.Pound_Buy()) / Convert.ToDouble(Program.Usd_Sell()) + dollar1;
                        var fundsDollar = addDollar.ToString();
                        var minusPound = pound1 - Convert.ToDouble(textBox1.Text);
                        var fundsPound = minusPound.ToString();
                        if (minusPound < 0)
                        {
                            var msg1 = "You don't Have Enough Money";
                            MessageBox.Show(msg1);

                        }
                        else
                        {
                            accountService.Update(new Account() { AccName = account3.AccName, Currency = account3.Currency, Funds = Convert.ToDecimal(fundsPound), GovId = Program.GovId, Iban = account3.Iban });
                            accountService.Update(new Account() { AccName = account4.AccName, Currency = account4.Currency, Funds = Convert.ToDecimal(fundsDollar), GovId = Program.GovId, Iban = account4.Iban });
                            var now = DateTime.Now;
                            var activity = "Exchange To: " + rIban;
                            var date = now.ToString();
                            var price = "-" + textBox1.Text + "£";
                            activityService.Add(new Entities.Concrete.Activity() { Date = date, Event = activity, GovId = Program.GovId, Iban = account3.Iban, Price = price });
                            var msg1 = "Exchange has been successful";
                            MessageBox.Show(msg1);
                        }
                    }
                    else if (comboBox2.Text == "Euro")
                    {
                        var account4 = accountService.Get(usr4Expression);
                        var rIban = account4.Iban;
                        var euro = account4.Funds;
                        var euro1 = Convert.ToDouble(euro);
                        var addEuro = Convert.ToDouble(textBox1.Text) * Convert.ToDouble(Program.Pound_Buy()) / Convert.ToDouble(Program.Euro_Sell()) + euro1;
                        var fundsEuro = addEuro.ToString();
                        var minusPound = pound1 - Convert.ToDouble(textBox1.Text);
                        var fundsPound = minusPound.ToString();
                        if (minusPound < 0)
                        {
                            var msg1 = "You don't Have Enough Money";
                            MessageBox.Show(msg1);

                        }
                        else
                        {
                            accountService.Update(new Account() { AccName = account3.AccName, Currency = account3.Currency, Funds = Convert.ToDecimal(fundsPound), GovId = Program.GovId, Iban = account3.Iban });
                            accountService.Update(new Account() { AccName = account4.AccName, Currency = account4.Currency, Funds = Convert.ToDecimal(fundsEuro), GovId = Program.GovId, Iban = account4.Iban });
                            var now = DateTime.Now;
                            var activity = "Exchange To: " + rIban;
                            var date = now.ToString();
                            var price = "-" + textBox1.Text + "£";
                            activityService.Add(new Entities.Concrete.Activity() { Date = date, Event = activity, GovId = Program.GovId, Iban = account3.Iban, Price = price });
                            var msg1 = "Exchange has been successful";
                            MessageBox.Show(msg1);
                        }
                    }
                    else if (comboBox2.Text == "Lira")
                    {
                        var account4 = accountService.Get(usr4Expression);
                        var rIban = account4.Iban;
                        var lira = account4.Funds;
                        var lira1 = Convert.ToDouble(lira);
                        var addLira = Convert.ToDouble(textBox1.Text) * Convert.ToDouble(Program.Pound_Buy()) / 10000 + lira1;
                        var fundsLira = addLira.ToString();
                        var minusPound = pound1 - Convert.ToDouble(textBox1.Text);
                        var fundsPound = minusPound.ToString();
                        if (minusPound < 0)
                        {
                            var msg1 = "You don't Have Enough Money";
                            MessageBox.Show(msg1);

                        }
                        else
                        {
                            accountService.Update(new Account() { AccName = account3.AccName, Currency = account3.Currency, Funds = Convert.ToDecimal(fundsPound), GovId = Program.GovId, Iban = account3.Iban });
                            accountService.Update(new Account() { AccName = account4.AccName, Currency = account4.Currency, Funds = Convert.ToDecimal(fundsLira), GovId = Program.GovId, Iban = account4.Iban });
                            var now = DateTime.Now;
                            var activity = "Exchange To: " + rIban;
                            var date = now.ToString();
                            var price = "-" + textBox1.Text + "£";
                            activityService.Add(new Entities.Concrete.Activity() { Date = date, Event = activity, GovId = Program.GovId, Iban = account3.Iban, Price = price });
                            var msg1 = "Exchange has been successful";
                            MessageBox.Show(msg1);
                        }
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            comboBox3.Text = null;
            var accountService = InstanceFactory.GetInstance<IAccountService>();
            Expression<Func<Account, bool>> usrExpression = user => user.GovId == Program.GovId && user.Currency==comboBox1.Text;
            var accList = accountService.GetAll(usrExpression).ToList();
            foreach (var acc in accList)
            {
                comboBox3.Items.Add(acc.AccName);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            comboBox4.Text = null;
            var accountService = InstanceFactory.GetInstance<IAccountService>();
            Expression<Func<Account, bool>> usrExpression = user => user.GovId == Program.GovId && user.Currency == comboBox2.Text;
            var accList = accountService.GetAll(usrExpression).ToList();
            foreach (var acc in accList)
            {
                comboBox4.Items.Add(acc.AccName);
            }
        }

        private void Exchange_FormClosed(object sender, FormClosedEventArgs e)
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
            if (textBox1.Text=="")
            {
                label4.Text = "";
            }
            if (textBox1.Text!="")
            {
                if (comboBox1.Text == "Lira")
                {
                    if (comboBox2.Text == "Dollar")
                    {

                        label4.Text = "You will take: " + string.Format("{0:0.##}", Convert.ToDouble(textBox1.Text) / Convert.ToDouble(Program.Usd_Sell()) * 10000) + "$";

                    }
                    else if (comboBox2.Text == "Euro")
                    {
                        label4.Text = "You will take: " + string.Format("{0:0.##}", Convert.ToDouble(textBox1.Text) / Convert.ToDouble(Program.Euro_Sell()) * 10000) + "€";
                    }
                    else if (comboBox2.Text == "Pound")
                    {
                        label4.Text = "You will take: " + string.Format("{0:0.##}", Convert.ToDouble(textBox1.Text) / Convert.ToDouble(Program.Pound_Sell()) * 10000) + "£";

                    }

                }
                else if (comboBox1.Text == "Dollar")
                {
                    if (comboBox2.Text == "Lira")
                    {
                        label4.Text = "You will take: " + string.Format("{0:0.##}", Convert.ToDouble(textBox1.Text) * Convert.ToDouble(Program.Usd_Buy()) / 10000) + "₺";
                    }
                    else if (comboBox2.Text == "Euro")
                    {
                        label4.Text = "You will take: " + string.Format("{0:0.##}", Convert.ToDouble(textBox1.Text) * Convert.ToDouble(Program.Usd_Buy()) / Convert.ToDouble(Program.Euro_Sell())) + "€";
                    }
                    else if (comboBox2.Text == "Pound")
                    {
                        label4.Text = "You will take: " + string.Format("{0:0.##}", Convert.ToDouble(textBox1.Text) * Convert.ToDouble(Program.Usd_Buy()) / Convert.ToDouble(Program.Pound_Sell())) + "£";
                    }
                }
                else if (comboBox1.Text == "Euro")
                {
                    if (comboBox2.Text == "Dollar")
                    {
                        label4.Text = "You will take: " + string.Format("{0:0.##}", Convert.ToDouble(textBox1.Text) * Convert.ToDouble(Program.Euro_Buy()) / Convert.ToDouble(Program.Usd_Sell())) + "$";
                    }
                    else if (comboBox2.Text == "Lira")
                    {
                        label4.Text = "You will take: " + string.Format("{0:0.##}", Convert.ToDouble(textBox1.Text) * Convert.ToDouble(Program.Euro_Buy()) / 10000) + "₺";
                    }
                    else if (comboBox2.Text == "Pound")
                    {
                        label4.Text = "You will take: " + string.Format("{0:0.##}", Convert.ToDouble(textBox1.Text) * Convert.ToDouble(Program.Euro_Buy()) / Convert.ToDouble(Program.Pound_Sell())) + "£";
                    }
                }
                else if (comboBox1.Text == "Pound")
                {
                    if (comboBox2.Text == "Dollar")
                    {
                        label4.Text = "You will take: " + string.Format("{0:0.##}", Convert.ToDouble(textBox1.Text) * Convert.ToDouble(Program.Pound_Buy()) / Convert.ToDouble(Program.Usd_Sell())) + "$";
                    }
                    else if (comboBox2.Text == "Euro")
                    {
                        label4.Text = "You will take: " + string.Format("{0:0.##}", Convert.ToDouble(textBox1.Text) * Convert.ToDouble(Program.Pound_Buy()) / Convert.ToDouble(Program.Euro_Sell())) + "€";
                    }
                    else if (comboBox2.Text == "Lira")
                    {
                        label4.Text = "You will take: " + string.Format("{0:0.##}", Convert.ToDouble(textBox1.Text) * Convert.ToDouble(Program.Pound_Buy()) / 10000) + "₺";
                    }
                }
            }
            
        }
    }
}
