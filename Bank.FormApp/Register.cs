using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Bank.Business.Abstract;
using Bank.Business.DependencyResolvers.Ninject;
using Bank.Entities.Concrete;
using TCKN;

namespace Bank.FormApp
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
            textBox5.PasswordChar = '*';
        }

        private void Register_Load(object sender, EventArgs e)
        {
            var SecQueService= InstanceFactory.GetInstance<ISecQueService>();
            SecQueService.GetAll().ForEach(s=>comboBox1.Items.Add(s.Question));
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            long govId1 = long.Parse(textBox1.Text);
            int birth = int.Parse(textBox4.Text);
            TCKimlikNoDogrulaResponse response;
            bool? result;

            try
            {
                TCKN.KPSPublicSoapClient webService = new TCKN.KPSPublicSoapClient(KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap12);
                var asd = webService.TCKimlikNoDogrulaAsync(govId1, textBox2.Text.ToUpper(), textBox3.Text.ToUpper(), birth).Result;
                result = asd.Body.TCKimlikNoDogrulaResult;
            }
            catch (Exception ex)
            {
                result = null;
                MessageBox.Show(ex.Message);
            }

            if (result==true)
            {
                try
                {
                    if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
                    {
                        var msg2 = "Do not leave any space.";
                        MessageBox.Show(msg2);
                    }
                    else
                    {
                        var govId = textBox1.Text;
                        var name = textBox2.Text +" "+ textBox3.Text;
                        var password = Program.Md5Hash(textBox5.Text);
                        var funds = 0;
                        var secQue = comboBox1.Text.Trim();
                        var secAns = Program.Md5Hash(textBox6.Text);
                        var accName = "Main";
                        var currency = "Lira";
                        var ıban = Program.IbanGenerator();
                        var UserAccountService = InstanceFactory.GetInstance<IUserAccountService>();
                        UserAccountService.Add(new UserAccount(){GovId = govId,Name = name,Password = password,SecAns = secAns,SecQue = secQue});
                        var AccountService = InstanceFactory.GetInstance<IAccountService>();
                        AccountService.Add(new Account(){AccName = accName,Currency = currency,Funds = funds,GovId = govId,Iban = ıban});
                        this.Hide();
                        var msg = "Registered Successfully";
                        MessageBox.Show(msg);
                    }

                }
                catch (Exception exception)
                {
                    var msg = "This Government Id Has Already Been Used";
                    MessageBox.Show(msg);
                }
            }
            else
            {
                var msg = "Government Id couldn't verified";
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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            var text = string.Empty;
            foreach (var character in textBox6.Text)
            {
                if (char.IsNumber(character))
                    text += character;
            }

            textBox6.Text = text;
        }
    }
}
