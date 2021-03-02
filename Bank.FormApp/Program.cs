using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Bank.FormApp
{
    static class Program
    {
        public static string GovId = "";
        public static string IbanGenerator()
        {
            var country = "TR";
            var rnd = new Random();
            var first0 = rnd.Next(10, 99);
            var first = first0.ToString();
            var bankNum = "6060";
            var second = "1000000000";
            var last0 = rnd.Next(10000000, 99999999);
            var last = last0.ToString();
            var ýban = country + first + bankNum + second + last;
            return ýban;
        }

        public static string Pound_Buy()
        {
            var today = "http://www.tcmb.gov.tr/kurlar/today.xml";

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(today);

            var poundSell = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='GBP']/BanknoteBuying")?.InnerXml;

            return poundSell;
        }
        public static string Pound_Sell()
        {
            var today = "http://www.tcmb.gov.tr/kurlar/today.xml";

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(today);

            var poundSell = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='GBP']/BanknoteSelling")?.InnerXml;

            return poundSell;
        }
        public static string Euro_Buy()
        {
            var today = "https://www.tcmb.gov.tr/kurlar/today.xml";

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(today);

            var euroSell = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteBuying")?.InnerXml;

            return euroSell;
        }
        public static string Euro_Sell()
        {
            var today = "http://www.tcmb.gov.tr/kurlar/today.xml";

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(today);

            var euroSell = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteSelling")?.InnerXml;

            return euroSell;
        }
        public static string Usd_Buy()
        {
            var today = "http://www.tcmb.gov.tr/kurlar/today.xml";

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(today);

            var usdSell = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteBuying")?.InnerXml;

            return usdSell;
        }
        public static string Usd_Sell()
        {
            var today = "http://www.tcmb.gov.tr/kurlar/today.xml";

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(today);

            var usdSell = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteSelling")?.InnerXml;

            return usdSell;
        }
        public static string Md5Hash(string encryptedWords)
        {

            var md5 = new MD5CryptoServiceProvider();
            var array = Encoding.UTF8.GetBytes(encryptedWords);
            array = md5.ComputeHash(array);
            var sb = new StringBuilder();

            foreach (var ba in array)
            {
                sb.Append(ba.ToString("x2").ToLower());
            }

            return sb.ToString();
        }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
