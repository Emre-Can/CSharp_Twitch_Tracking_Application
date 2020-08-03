using Gecko;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace Twitch
{
    public partial class chat : Form
    {
        public chat()
        {
            InitializeComponent();
        }

        public string link;

        private void chat_Load(object sender, EventArgs e)
        {
            geckoWebBrowser1.Navigate(link);
        }

        private void chat_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 anaform = new Form1();
            anaform.Show();
        }

    }
}
