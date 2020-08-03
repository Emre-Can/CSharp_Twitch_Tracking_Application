using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Gecko;
using Twitch.Properties;
using System.Threading;
using System.IO;

namespace Twitch
{
    public partial class Form1 : Form
    {
        public string twitchlink;
        public string twitchname;
        public string twitchtrackerlink;
        public string twitchlinkchat;
        public string twitchlinkactivityfeed;
        public string instalink;
        public string instaname;
        public string youtubelink;
        public string bynogamelink;
        public bool check;
        public Uri url;
        chat chat = new chat();

        public Form1()
        {
            InitializeComponent();
            Xpcom.Initialize("Firefox");
            GeckoPreferences.User["intl.accept_languages"] = "en";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            try
            {
                pictureBox1.Image = Resim(@"");
            }
            catch { }
            elipseControl1.CornerRadius = pictureBox1.Width;
            geckoWebBrowser5.Width = this.Width - panel1.Width;
            if (!File.Exists("details.txt"))
            {
                geckoWebBrowser4.Navigate("https://findl.me/Emre_Can");
                geckoWebBrowser5.Navigate("https://findl.me/Emre_Can");
                MessageBox.Show("• You must fill in the information on the Settings tab !", "Warning");
                buttonAyarlar.PerformClick();
                button2.PerformClick();
            }
            else
            {
                BilgiAl();
            }

            
            elipseControl2.CornerRadius = 10;
            elipseControl3.CornerRadius = 10;
            elipseControl4.CornerRadius = 27;
            elipseControl5.CornerRadius = 27;
            elipseControl6.CornerRadius = 27;
            elipseControl7.CornerRadius = 10;
            elipseControl8.CornerRadius = 10;
        }

        Bitmap Resim(string Url)
        {
            WebRequest rs = WebRequest.Create(Url);
            return (Bitmap)Bitmap.FromStream(rs.GetResponse().GetResponseStream());
            
        }

        int Move;
        int Mouse_X;
        int Mouse_Y;

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Red;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            Environment.Exit(0);            
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackColor = panelUst.BackColor;
        }

        private void panelUst_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;

        }

        private void panelUst_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }

        private void panelUst_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        int altyazix;
        private void timer1_Tick(object sender, EventArgs e)
        {
            altyazix = panelAltyazi.Location.X-1;
            panelAltyazi.Location = new Point(altyazix, 0);
            if (panelAltyazi.Width + panelAltyazi.Location.X <= 0)
                panelAltyazi.Location = new Point(panel2.Width,0);
        }

        private void labelInstagramlink_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(instalink);
        }

        private void labelTwitchlink_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(twitchlink);
        }

        private void labelDiscordlink_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.com/channels/@me");
        }
        private void labelBynogamelink_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start();
        }
        private void labelYoutubelink_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/channel/UCQHGJvpQHMCN01T7zaA8c8A");
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            VeriAlGenel();
        }

        private void BilgiAl()
        {


            FileStream fileStream = new FileStream("details.txt", FileMode.Open, FileAccess.Read);
            StreamReader streamReader = new StreamReader(fileStream);
            string twitchname = streamReader.ReadLine();
            if (twitchname != "null" && twitchname != null)
            {
                check = true;
                labelStreamerName.Text = twitchname;
                twitchtrackerlink = "https://twitchtracker.com/" + twitchname;
                twitchlink = "https://www.twitch.tv/" + twitchname;
                twitchlinkchat = "https://www.twitch.tv/popout/" + twitchname + "/chat?popout=";
                twitchlinkactivityfeed = "https://dashboard.twitch.tv/popout/u/" + twitchname + "/stream-manager/activity-feed";
                instaname = streamReader.ReadLine();
                if (instaname != "null" && instaname != null)
                {
                    labelInstagramlink.Text = "instagram.com/" + instaname;
                    instalink = "https://www.instagram.com/" + instaname;
                }
                else
                {
                    labelInstagramlink.Text = "instagram.com/";
                }



                youtubelink = streamReader.ReadLine();
                if (youtubelink != "null" && youtubelink != null)
                {
                    labelYoutubelink.Text = youtubelink;
                    geckoWebBrowser5.Navigate(youtubelink);
                }
                else
                {
                    labelYoutubelink.Text = "youtube.com/";
                }
                geckoWebBrowser1.Navigate(twitchlinkchat);
                geckoWebBrowser2.Navigate(twitchlinkactivityfeed);
                geckoWebBrowser4.Navigate(twitchlink);
            }
            else
            {
                check = false;
                geckoWebBrowser4.Navigate("https://findl.me/Emre_Can");
                geckoWebBrowser5.Navigate("https://findl.me/Emre_Can");
            }
            streamReader.Close();
            fileStream.Close();
            labelTwitchlink.Text = "twitch.tv/" + twitchname;


            buttonTwitch.PerformClick();

            VeriAlGenel();
            VeriAlTwitch();
        }

        private void VeriAlGenel()
        {

            if (check)
            {
                try
                {
                    var url = new Uri(twitchtrackerlink);
                    var client = new WebClient();
                    var html = client.DownloadString(url);
                    Thread.Sleep(2000);
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(html);
                    var veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[5]/div/div[2]/div[3]/div[1]/div[2]")[0];
                    if (veri != null)
                    {
                        labelTakipci.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[5]/div/div[2]/div[4]/div[1]/div[2]")[0];
                        labelIzlenme.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[5]/div/div[2]/div[1]/div[1]/div[2]")[0];
                        labelToplamyayinsaat.Text = veri.InnerText + " Hour";

                        labelTwitchtakipci.Text = labelTakipci.Text;
                        labelTwitchizlenme.Text = labelIzlenme.Text;
                        labelTwitchyaysaat.Text = labelToplamyayinsaat.Text;
                    }
                }
                catch
                {
                    MessageBox.Show("• Check your internet connection.\n• If the problem persists, contact the producer.", "Can't get data");
                }
            }

        }

        private void VeriAlTwitch()
        {
            if (check)
            {
                int Yay1sure, Yay2sure, Yay3sure, Yay4sure, Yay5sure;
                var url = new Uri(twitchtrackerlink);
                var client = new WebClient();
                var html = client.DownloadString(url);
                Thread.Sleep(2000);
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);
                HtmlAgilityPack.HtmlNode veri;
                try
                {
                    try
                    {
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[1]/ul/li[3]/span")[0];
                    }
                    catch (Exception)
                    {
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[1]/ul/li[3]/span")[0]; 
                    }
                    if (veri.InnerText == "streaming now")
                    {

                        labelCanlibant.Visible = true;
                        labelYay1ay.BackColor = Color.FromArgb(255, 255, 0, 0);
                        labelYay1yil.BackColor = Color.FromArgb(255, 255, 0, 0);
                        labelYay1gun.BackColor = Color.FromArgb(255, 255, 0, 0);
                        labelYay1izle.BackColor = Color.FromArgb(255, 255, 0, 0);
                        labelYay1takip.BackColor = Color.FromArgb(255, 255, 0, 0);
                        labelYay1sure.BackColor = Color.FromArgb(255, 255, 0, 0);
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[2]/div/div[2]/a[1]/div[1]/div[2]")[0];             
                        labelYay1gun.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[2]/div/div[2]/a[2]/div[1]/div[1]")[0];
                        labelYay2gun.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[2]/div/div[2]/a[3]/div[1]/div[1]")[0];
                        labelYay3gun.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[2]/div/div[2]/a[4]/div[1]/div[1]")[0];
                        labelYay4gun.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[2]/div/div[2]/a[5]/div[1]/div[1]")[0];
                        labelYay5gun.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[2]/div/div[2]/a[1]/div[1]/div[3]/div[1]")[0];
                        labelYay1ay.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[2]/div/div[2]/a[2]/div[1]/div[2]/div[1]")[0];
                        labelYay2ay.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[2]/div/div[2]/a[3]/div[1]/div[2]/div[1]")[0];
                        labelYay3ay.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[2]/div/div[2]/a[4]/div[1]/div[2]/div[1]")[0];
                        labelYay4ay.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[2]/div/div[2]/a[5]/div[1]/div[2]/div[1]")[0];
                        labelYay5ay.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[2]/div/div[2]/a[1]/div[1]/div[3]/div[2]")[0];
                        labelYay1yil.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[2]/div/div[2]/a[2]/div[1]/div[2]/div[2]")[0];
                        labelYay2yil.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[2]/div/div[2]/a[3]/div[1]/div[2]/div[2]")[0];
                        labelYay3yil.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[2]/div/div[2]/a[4]/div[1]/div[2]/div[2]")[0];
                        labelYay4yil.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[2]/div/div[2]/a[5]/div[1]/div[2]/div[2]")[0];
                        labelYay5yil.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[2]/div/div[2]/a[1]/div[5]/div[1]")[0];
                        Yay1sure = Convert.ToInt32(veri.InnerText);
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[2]/div/div[2]/a[2]/div[5]/div[1]")[0];
                        Yay2sure = Convert.ToInt32(veri.InnerText);
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[2]/div/div[2]/a[3]/div[5]/div[1]")[0];
                        Yay3sure = Convert.ToInt32(veri.InnerText);
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[2]/div/div[2]/a[4]/div[5]/div[1]")[0];
                        Yay4sure = Convert.ToInt32(veri.InnerText);
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[2]/div/div[2]/a[5]/div[5]/div[1]")[0];
                        Yay5sure = Convert.ToInt32(veri.InnerText);

                        labelYay1sure.Text = (Yay1sure / 60).ToString() + "." + (Yay1sure % 60).ToString() + "hr";
                        labelYay2sure.Text = (Yay2sure / 60).ToString() + "." + (Yay2sure % 60).ToString() + "hr";
                        labelYay3sure.Text = (Yay3sure / 60).ToString() + "." + (Yay3sure % 60).ToString() + "hr";
                        labelYay4sure.Text = (Yay4sure / 60).ToString() + "." + (Yay4sure % 60).ToString() + "hr";
                        labelYay5sure.Text = (Yay5sure / 60).ToString() + "." + (Yay5sure % 60).ToString() + "hr";

                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[2]/div/div[2]/a[1]/div[2]/div[1]")[0];
                        labelYay1izle.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[2]/div/div[2]/a[2]/div[2]/div[1]")[0];
                        labelYay2izle.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[2]/div/div[2]/a[3]/div[2]/div[1]")[0];
                        labelYay3izle.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[2]/div/div[2]/a[4]/div[2]/div[1]")[0];
                        labelYay4izle.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[2]/div/div[2]/a[5]/div[2]/div[1]")[0];
                        labelYay5izle.Text = veri.InnerText;

                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[2]/div/div[2]/a[1]/div[3]/div[1]")[0];
                        labelYay1takip.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[2]/div/div[2]/a[2]/div[3]/div[1]")[0];
                        labelYay2takip.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[2]/div/div[2]/a[3]/div[3]/div[1]")[0];
                        labelYay3takip.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[2]/div/div[2]/a[4]/div[3]/div[1]")[0];
                        labelYay4takip.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[2]/div/div[2]/a[5]/div[3]/div[1]")[0];
                        labelYay5takip.Text = veri.InnerText;
                    }
                }
                catch
                {
                }

                try
                {
                    try
                    {
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[2]/div[1]/ul/li[3]/span")[0];
                    }
                    catch (Exception)
                    {
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[1]/ul/li[3]/span")[0]; 
                    }

                    if (veri.InnerText != "streaming now")
                    {
                        labelCanlibant.Visible = false;
                        labelYay1ay.BackColor = Color.FromArgb(0, 42, 43, 48);
                        labelYay1yil.BackColor = Color.FromArgb(0, 42, 43, 48);
                        labelYay1gun.BackColor = Color.FromArgb(0, 42, 43, 48);
                        labelYay1izle.BackColor = Color.FromArgb(0, 42, 43, 48);
                        labelYay1takip.BackColor = Color.FromArgb(0, 42, 43, 48);
                        labelYay1sure.BackColor = Color.FromArgb(0, 42, 43, 48);

                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[2]/div/div[2]/a[1]/div[1]/div[1]")[0];
                        labelYay1gun.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[2]/div/div[2]/a[2]/div[1]/div[1]")[0];
                        labelYay2gun.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[2]/div/div[2]/a[3]/div[1]/div[1]")[0];
                        labelYay3gun.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[2]/div/div[2]/a[4]/div[1]/div[1]")[0];
                        labelYay4gun.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[2]/div/div[2]/a[5]/div[1]/div[1]")[0];
                        labelYay5gun.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[2]/div/div[2]/a[1]/div[1]/div[2]/div[1]")[0];
                        labelYay1ay.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[2]/div/div[2]/a[2]/div[1]/div[2]/div[1]")[0];
                        labelYay2ay.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[2]/div/div[2]/a[3]/div[1]/div[2]/div[1]")[0];
                        labelYay3ay.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[2]/div/div[2]/a[4]/div[1]/div[2]/div[1]")[0];
                        labelYay4ay.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[2]/div/div[2]/a[5]/div[1]/div[2]/div[1]")[0];
                        labelYay5ay.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[2]/div/div[2]/a[1]/div[1]/div[2]/div[2]")[0];
                        labelYay1yil.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[2]/div/div[2]/a[2]/div[1]/div[2]/div[2]")[0];
                        labelYay2yil.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[2]/div/div[2]/a[3]/div[1]/div[2]/div[2]")[0];
                        labelYay3yil.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[2]/div/div[2]/a[4]/div[1]/div[2]/div[2]")[0];
                        labelYay4yil.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[2]/div/div[2]/a[5]/div[1]/div[2]/div[2]")[0];
                        labelYay5yil.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[2]/div/div[2]/a[1]/div[5]/div[1]")[0];
                        Yay1sure = Convert.ToInt32(veri.InnerText);
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[2]/div/div[2]/a[2]/div[5]/div[1]")[0];
                        Yay2sure = Convert.ToInt32(veri.InnerText);
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[2]/div/div[2]/a[3]/div[5]/div[1]")[0];
                        Yay3sure = Convert.ToInt32(veri.InnerText);
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[2]/div/div[2]/a[4]/div[5]/div[1]")[0];
                        Yay4sure = Convert.ToInt32(veri.InnerText);
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[2]/div/div[2]/a[5]/div[5]/div[1]")[0];
                        Yay5sure = Convert.ToInt32(veri.InnerText);

                        labelYay1sure.Text = (Yay1sure / 60).ToString() + "." + (Yay1sure % 60).ToString() + "hr";
                        labelYay2sure.Text = (Yay2sure / 60).ToString() + "." + (Yay2sure % 60).ToString() + "hr";
                        labelYay3sure.Text = (Yay3sure / 60).ToString() + "." + (Yay3sure % 60).ToString() + "hr";
                        labelYay4sure.Text = (Yay4sure / 60).ToString() + "." + (Yay4sure % 60).ToString() + "hr";
                        labelYay5sure.Text = (Yay5sure / 60).ToString() + "." + (Yay5sure % 60).ToString() + "hr";

                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[2]/div/div[2]/a[1]/div[2]/div[1]")[0];
                        labelYay1izle.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[2]/div/div[2]/a[2]/div[2]/div[1]")[0];
                        labelYay2izle.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[2]/div/div[2]/a[3]/div[2]/div[1]")[0];
                        labelYay3izle.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[2]/div/div[2]/a[4]/div[2]/div[1]")[0];
                        labelYay4izle.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[2]/div/div[2]/a[5]/div[2]/div[1]")[0];
                        labelYay5izle.Text = veri.InnerText;

                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[2]/div/div[2]/a[1]/div[3]/div[1]")[0];
                        labelYay1takip.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[2]/div/div[2]/a[2]/div[3]/div[1]")[0];
                        labelYay2takip.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[2]/div/div[2]/a[3]/div[3]/div[1]")[0];
                        labelYay3takip.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[2]/div/div[2]/a[4]/div[3]/div[1]")[0];
                        labelYay4takip.Text = veri.InnerText;
                        veri = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[7]/section[1]/div[2]/div/div[2]/a[5]/div[3]/div[1]")[0];
                        labelYay5takip.Text = veri.InnerText;
                    }
                }
                catch
                {
                }
            }

        }

        private void buttonHakkimda_Click(object sender, EventArgs e)
        {
            label29.Visible = false;
            label20.Visible = false;
            label19.Visible = true;
            label18.Visible = false;
            label17.Visible = false;
            timerTwitch.Stop();
            geckoWebBrowser5.Visible = false;
            panelTwitch.Visible = false;
            panelVeriler.Visible = true;
            panelYayın.Visible = false;
            panelHakkimda.Visible = true;
            panelAyarlar.Visible = false;

        }

        private void buttonInstagram_Click(object sender, EventArgs e)
        {
            label29.Visible = false;
            label20.Visible = false;
            label19.Visible = false;
            label18.Visible = true;
            label17.Visible = false;
            timerTwitch.Stop();
            panelHakkimda.Visible = false;
            panelTwitch.Visible = false;
            panelVeriler.Visible = true;
            panelYayın.Visible = false;
            geckoWebBrowser5.Visible = true;
            panelAyarlar.Visible = false;

        }

        private void buttonTwitch_Click(object sender, EventArgs e)
        {
            label29.Visible = false;
            label20.Visible = false;
            label19.Visible = false;
            label18.Visible = false;
            label17.Visible = true;
            timerTwitch.Start();
            panelHakkimda.Visible = false;
            geckoWebBrowser5.Visible = false;
            panelVeriler.Visible = false;
            panelYayın.Visible = false;
            panelTwitch.Visible = true;
            panelAyarlar.Visible = false;

        }

        private void buttonYayın_Click(object sender, EventArgs e)
        {
            label29.Visible = false;
            label20.Visible = true;
            label19.Visible = false;
            label18.Visible = false;
            label17.Visible = false;
            timerTwitch.Stop();
            panelHakkimda.Visible = false;
            geckoWebBrowser5.Visible = false;
            panelVeriler.Visible = false;
            panelTwitch.Visible = false;
            panelYayın.Visible = true;
            panelAyarlar.Visible = false;
        }

        private void buttonAyarlar_Click(object sender, EventArgs e)
        {
            label29.Visible = true;
            label20.Visible = false;
            label19.Visible = false;
            label18.Visible = false;
            label17.Visible = false;
            timerTwitch.Stop();
            panelHakkimda.Visible = false;
            geckoWebBrowser5.Visible = false;
            panelVeriler.Visible = true;
            panelTwitch.Visible = false;
            panelYayın.Visible = false;
            panelAyarlar.Visible = true;
        }

        private void timerTwitch_Tick(object sender, EventArgs e)
        {
            VeriAlTwitch();
        }

        private void btnMinimized_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnSohbet_Click(object sender, EventArgs e)
        {
            this.Hide();
            chat.link = twitchlinkchat;
            chat.ShowDialog();
        }

        private void xuıSwitch1_SwitchStateChanged(object sender, EventArgs e)
        {
            if (xuıSwitch1.SwitchState==XanderUI.XUISwitch.State.On)
            {
                chat.TopMost = true;
            }
            else
            {
                chat.TopMost = false;
            }
        }

        byte logo = 0;
        private void timerLogo_Tick(object sender, EventArgs e)
        {
            if (logo == 0)
            {
                pictureBox2.Image = Resources.youtubelogo;
                logo++;
            }
            else if (logo == 1)
            {
                pictureBox2.Image = Resources.TwitchLogo;
                logo = 0;
            }
        }


        private void buttonGiris_Click(object sender, EventArgs e)
        {
            button1.PerformClick();
            geckoWebBrowser3.Navigate("https://www.twitch.tv/login");
            geckoWebBrowser3.Visible = true;
            buttonGiris.Enabled = false;
            buttonGiriskapa.Visible = true;
        }

        private void buttonGiriskapa_Click(object sender, EventArgs e)
        {
            geckoWebBrowser3.Navigate("https://www.google.com/");
            geckoWebBrowser3.Visible = false;
            buttonGiriskapa.Visible = false;
            buttonGiris.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            buttonGiriskapa.PerformClick();
            panel3.Visible = true;
            button2.Enabled = false;
            button1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            button1.Visible = false;
            button2.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBoxTwitchKulAd.Text != "")
            {
                if(File.Exists("details.txt"))
                    File.Delete("details.txt");
                    Thread.Sleep(100);
                    FileStream fileStream = new FileStream("details.txt", FileMode.Create, FileAccess.Write);
                    StreamWriter streamWriter = new StreamWriter(fileStream);
                    streamWriter.WriteLine(textBoxTwitchKulAd.Text);
                    if (textBoxInstaKulAd.Text != "")
                    {
                        streamWriter.WriteLine(textBoxInstaKulAd.Text);
                    }
                    else
                    {
                        streamWriter.WriteLine("emrecimke");
                    }
                    if (textBoxYoutubeKanLink.Text != "")
                    {
                        streamWriter.WriteLine(textBoxYoutubeKanLink.Text);
                    }
                    else
                    {
                        streamWriter.WriteLine("https://findl.me/Emre_Can");
                    }
                    if (textBoxBynoLink.Text != "")
                    {
                        streamWriter.WriteLine(textBoxBynoLink.Text);
                    }
                    else
                    {
                        streamWriter.WriteLine("null");
                    }
                    streamWriter.Flush();
                    streamWriter.Close();
                    fileStream.Close();
            }
            else
            {
                MessageBox.Show("• Please enter your Twitch username", "Warning");
            }
            button1.PerformClick();
            BilgiAl();
        }
    }
}
