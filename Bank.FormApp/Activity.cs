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
    public partial class Activity : Form
    {
        public Activity()
        {
            InitializeComponent();
        }

        private void Activity_Load(object sender, EventArgs e)
        {
            var activityService = InstanceFactory.GetInstance<IActivityService>();
            Expression<Func<Entities.Concrete.Activity, bool>> actExpression = user => user.GovId == Program.GovId;
            var actList = activityService.GetAll(actExpression).OrderByDescending(a=>a.Date).ToList();
            foreach (var act in actList)
            {
                var add = new ListViewItem { Text = (act.Event) };
                add.SubItems.Add(act.Comment);
                add.SubItems.Add(act.Price);
                add.SubItems.Add(act.Date);

                listView1.Items.Add(add);
            }
        }

        private void Activity_FormClosed(object sender, FormClosedEventArgs e)
        {
            var frm = new User();
            frm.Show();
        }
    }
}
