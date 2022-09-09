using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Updater
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BeforeLoad();
            Application.DoEvents();
            //NEWS
            pictureBox1.Image = Image.FromFile("./updater_img/0.jpg");
            pictureBox1.Controls.Add(panel8);
            pictureBox1.Controls.Add(arrow_right);
            pictureBox1.Controls.Add(arrow_left);
            arrow_right.Location = new Point(630, 120);
            arrow_left.Location = new Point(10, 120);
            panel8.Location = new Point(0, 225);
            pictureBox3.Image = Image.FromFile("./updater_img/1.jpg");
            pictureBox4.Image = Image.FromFile("./updater_img/2.jpg");

            //CONFIG
            label1.Text = Config.ServerName;

            //MAJ
            CheckMaj();
            panel7.Controls.Add(pictureBox2);
            pictureBox2.Location = new Point(2, 3);
            pictureBox2.BringToFront();
            pictureBox2.Controls.Add(majState);
            majState.Location = new Point(pictureBox2.Width / 2 - majState.Size.Width / 2, majState.Location.Y);

            //FUNCTIONS
            ModifyNewsInfo(0);
        }

        private void BeforeLoad()
        {
            if(Directory.Exists("./updater_img"))
                Directory.Delete("./updater_img", true);

            Directory.CreateDirectory("./updater_img");
            WebClient wc = new WebClient();
            wc.DownloadFile(new Uri(Config.UpdaterFolder + "/updater_img/0.jpg"), "./updater_img/0.jpg");
            wc.DownloadFile(new Uri(Config.UpdaterFolder + "/updater_img/1.jpg"), "./updater_img/1.jpg");
            wc.DownloadFile(new Uri(Config.UpdaterFolder + "/updater_img/2.jpg"), "./updater_img/2.jpg");
            wc.DownloadFile(new Uri(Config.UpdaterFolder + "/updater_img/news.xml"), "./updater_img/news.xml");
            wc.DownloadFile(new Uri(Config.UpdaterFolder + "/client.xml"), "./client.xml");
        }

        private void CheckMaj()
        {
            WebClient wc = new WebClient();

            if (!File.Exists("./Updater.version"))
            {
                wc.DownloadFileAsync(new Uri(Config.UpdaterFolder + "Updater.version"), "./Updater.version");
            }

            if (File.Exists(Application.StartupPath + "/app/Dofus.exe"))
            {
                Finish();
            }
            else
            {
                majState.Text = "Installation en cours...";
                panel5.Enabled = false;
                Install();
                return;
            }

            var objreader = new StreamReader("./Updater.version");
            var version = objreader.ReadToEnd();
            objreader.Close();

            wc.DownloadFile(new Uri(Config.UpdaterFolder + "Updater.version"), "./Last.version");
            var request = new StreamReader ("./Last.version");
            var lastversion = request.ReadToEnd();
            request.Close();

            if (version != lastversion)
            {
                majState.Text = "Installation de la mise à jour...";
                panel5.Enabled = false;
                InstallMaj();
            }
        }

        private void Finish()
        {
            if (File.Exists(Application.StartupPath + "/app/Dofus.exe"))
            {
                majState.Text = "Votre jeu est à jour.";
                panel5.Enabled = true;
            }
            else
            {
                MessageBox.Show("une erreur est survenu veuillez réinstaller le jeu.");
            }
        }

        private void OnDownloadFileProgress2(object obj, DownloadProgressChangedEventArgs e)
        {
            Thread.Sleep(0);
            majState.Text = "Installation en cours... (" + e.ProgressPercentage + "/100%)";
        }

        private void Install()
        {
            if (Directory.Exists(Application.StartupPath + "/app/"))
            {
                if(!InstallWorker.IsBusy)
                    InstallWorker.RunWorkerAsync();
            }
            else
            {
                Directory.CreateDirectory(Application.StartupPath + "/app/");
                Install();
            }
        }

        private void InstallMaj()
        {
            WebClient wc = new WebClient();

            if (Directory.Exists(Application.StartupPath + "/app/"))
            {
                if (!InstallWorker.IsBusy)
                    MajWorker.RunWorkerAsync();
            }
            else
            {
                Directory.CreateDirectory(Application.StartupPath + "/app/");
                InstallMaj();
            }
        }

        private void ModifyNewsInfo(int index)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Application.StartupPath + "/updater_img/news.xml");
            XmlNode node = doc.SelectSingleNode($"/configuration/news[@id = '{index}']"); ;
            title.Text = node["title"].InnerText;
            var location = new Point(panel8.Width / 2 - title.Size.Width / 2, title.Location.Y);
            title.Location = location;
            content.Text = node["content"].InnerText;
            var location2 = new Point(panel8.Width / 2 - content.Size.Width / 2, content.Location.Y);
            content.Location = location2;
        }

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void ChangeNews()
        {
            if (pictureBox1.Visible)
            {
                pictureBox1.Controls.Remove(panel8);
                pictureBox1.Controls.Remove(arrow_right);
                pictureBox1.Controls.Remove(arrow_left);
                pictureBox3.Controls.Add(panel8);
                pictureBox3.Controls.Add(arrow_right);
                pictureBox3.Controls.Add(arrow_left);
                pictureBox3.Visible = true;
                pictureBox1.Visible = false;
                ModifyNewsInfo(1);
            }
            else if (pictureBox3.Visible)
            {
                pictureBox3.Controls.Remove(panel8);
                pictureBox3.Controls.Remove(arrow_right);
                pictureBox3.Controls.Remove(arrow_left);
                pictureBox4.Controls.Add(panel8);
                pictureBox4.Controls.Add(arrow_right);
                pictureBox4.Controls.Add(arrow_left);
                pictureBox4.Visible = true;
                pictureBox3.Visible = false;
                ModifyNewsInfo(2);
            }
            else if (pictureBox4.Visible)
            {
                pictureBox4.Controls.Remove(panel8);
                pictureBox4.Controls.Remove(arrow_right);
                pictureBox4.Controls.Remove(arrow_left);
                pictureBox1.Controls.Add(panel8);
                pictureBox1.Controls.Add(arrow_right);
                pictureBox1.Controls.Add(arrow_left);
                pictureBox1.Visible = true;
                pictureBox4.Visible = false;
                ModifyNewsInfo(0);
            }
        }

        private void ChangeNewsReverse()
        {
            if (pictureBox1.Visible)
            {
                pictureBox1.Controls.Remove(panel8);
                pictureBox1.Controls.Remove(arrow_right);
                pictureBox1.Controls.Remove(arrow_left);
                pictureBox4.Controls.Add(panel8);
                pictureBox4.Controls.Add(arrow_right);
                pictureBox4.Controls.Add(arrow_left);
                pictureBox4.Visible = true;
                pictureBox1.Visible = false;
                ModifyNewsInfo(2);
            }
            else if (pictureBox3.Visible)
            {
                pictureBox3.Controls.Remove(panel8);
                pictureBox3.Controls.Remove(arrow_right);
                pictureBox3.Controls.Remove(arrow_left);
                pictureBox1.Controls.Add(panel8);
                pictureBox1.Controls.Add(arrow_right);
                pictureBox1.Controls.Add(arrow_left);
                pictureBox1.Visible = true;
                pictureBox3.Visible = false;
                ModifyNewsInfo(0);
            }
            else if (pictureBox4.Visible)
            {
                pictureBox4.Controls.Remove(panel8);
                pictureBox4.Controls.Remove(arrow_right);
                pictureBox4.Controls.Remove(arrow_left);
                pictureBox3.Controls.Add(panel8);
                pictureBox3.Controls.Add(arrow_right);
                pictureBox3.Controls.Add(arrow_left);
                pictureBox3.Visible = true;
                pictureBox4.Visible = false;
                ModifyNewsInfo(1);
            }
        }

        private void sliderTimer_Tick(object sender, EventArgs e)
        {
            ChangeNews();
        }

        private void arrow_right_Click(object sender, EventArgs e)
        {
            sliderTimer.Stop();
            ChangeNews();
            sliderTimer.Start();
        }

        private void arrow_left_Click(object sender, EventArgs e)
        {
            sliderTimer.Stop();
            ChangeNewsReverse();
            sliderTimer.Start();
        }

        private void panel5_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                ArkanSound.Initialization.Init();
                Process.Start(Application.StartupPath + "/app/reg/Reg.exe");
                Process.Start(Application.StartupPath + "/app/Dofus.exe");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Impossible de trouver app/Dofus.exe, veuillez réinstaller le jeu.");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Process.Start(Config.SiteLink);
        }

        private void panel9_Click(object sender, EventArgs e)
        {
            Process.Start(Config.DiscordLink);
        }

        public int NodesCount
        {
            get;
            set;
        }

        private void InstallWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            XmlDocument clientfiles = new XmlDocument();
            clientfiles.Load(Application.StartupPath + "/client.xml");
            XmlNodeList nodes = clientfiles.SelectNodes("/configuration/path");
            var count = 0;
            NodesCount = nodes.Count;
            foreach (var node in nodes)
            {
                WebClient wc = new WebClient();

                var directory = Path.GetDirectoryName((node as XmlNode).InnerText);
                if (!Directory.Exists("./app/" + directory))
                    Directory.CreateDirectory("./app/" + directory);

                wc.DownloadFile(new Uri(Config.UpdaterFolder + "dofus/" + (node as XmlNode).InnerText), "./app/" + (node as XmlNode).InnerText);
                Thread.Sleep(5);
                count++;
                InstallWorker.ReportProgress(count);
            }
        }

        private void InstallWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Thread.Sleep(0);
            majState.Text = "Installation en cours... (" + e.ProgressPercentage + " /  " + NodesCount + ")";
        }

        private void InstallWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Finish();
        }

        private void MajWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            XmlDocument clientfiles = new XmlDocument();
            clientfiles.Load(Application.StartupPath + "/client.xml");
            XmlNodeList nodes = clientfiles.SelectNodes("/configuration/path");
            var count = 0;
            NodesCount = nodes.Count;
            foreach (var node in nodes)
            {
                WebClient wc = new WebClient();

                var directory = Path.GetDirectoryName((node as XmlNode).InnerText);
                if (!Directory.Exists("./app/" + directory))
                    Directory.CreateDirectory("./app/" + directory);

                //DISTANCE
                Stream stream = wc.OpenRead(new Uri(Config.UpdaterFolder + "dofus/" + (node as XmlNode).InnerText));                

                Int64 bytes_total = Convert.ToInt64(wc.ResponseHeaders["Content-Length"]);
                stream.Close();
                
                //LOCAL FILE
                if(!File.Exists("./app/" + (node as XmlNode).InnerText))
                {
                    wc.DownloadFile(new Uri(Config.UpdaterFolder + "dofus/" + (node as XmlNode).InnerText), "./app/" + (node as XmlNode).InnerText);
                }
                else
                {
                    FileInfo file = new FileInfo("./app/" + (node as XmlNode).InnerText);
                    if (file.Length != bytes_total)
                    {
                        wc.DownloadFile(new Uri(Config.UpdaterFolder + "dofus/" + (node as XmlNode).InnerText), "./app/" + (node as XmlNode).InnerText);
                    }
                }
                Thread.Sleep(5);
                count++;
                MajWorker.ReportProgress(count);
            }
        }

        private void MajWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Thread.Sleep(0);
            majState.Text = "Installation de la MAJ cours... (" + e.ProgressPercentage + " /  " + NodesCount + ")";
        }

        private void MajWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var request = new StreamReader("./Last.version");
            var lastversion = request.ReadToEnd();
            request.Close();
            File.Delete("./Last.version");
            var objwriter = new StreamWriter("./Updater.version");
            objwriter.Write(lastversion);
            objwriter.Close();
            Finish();
        }
    }
}
