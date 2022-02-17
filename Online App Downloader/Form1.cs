using System;
using System.Windows.Forms;
using System.Drawing;
using System.Net;
using System.ComponentModel;
using System.Diagnostics;

#pragma warning disable 0168

namespace Online_App_Downloader
{
    public partial class Form1 : Form
    {
        //current code from https://stackoverflow.com/questions/25830079/how-to-make-the-background-of-a-label-transparent-in-c-sharp
        void TransparentBackground(Control C)
        {
            C.Visible = false;

            C.Refresh();
            Application.DoEvents();

            Rectangle screenRectangle = RectangleToScreen(ClientRectangle);
            int titleHeight = screenRectangle.Top - Top;
            int Right = screenRectangle.Left - Left;

            Bitmap bmp = new Bitmap(Width, Height);
            DrawToBitmap(bmp, new Rectangle(0, 0, Width, Height));
            Bitmap bmpImage = new Bitmap(bmp);
            bmp = bmpImage.Clone(new Rectangle(C.Location.X + Right, C.Location.Y + titleHeight, C.Width, C.Height), bmpImage.PixelFormat);
            C.BackgroundImage = bmp;
            C.Visible = true;
        }
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            TransparentBackground(label1);
            TransparentBackground(label2);
            TransparentBackground(label3);
            TransparentBackground(label4);
            TransparentBackground(label5);
            TransparentBackground(label6);
            TransparentBackground(button2);
            SetStyle(ControlStyles.SupportsTransparentBackColor,true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if(fbd.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = fbd.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(webClient_Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webClient_inProgress);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                webClient.DownloadFileAsync(new Uri(textBox1.Text), textBox2.Text+"\\"+textBox3.Text);
            }
            catch (UriFormatException ufe)
            {
                label6.Text = "Ⓧ URL is invalid (wrong or empty)";
            }
            catch (ArgumentException ae)
            {
                label6.Text = "Ⓧ File name or path is not valid (wrong or empty).";
            }
            catch (WebException we)
            {
                label6.Text = "Ⓧ Cannot connect to suggested URL; Try turning off your firewall or allow network activities of this application.";
            }
            catch (InvalidOperationException ioe)
            {
                label6.Text = "Ⓧ Your file to overwrite is in use; Change its name or turn off the app that using suggested file.";
            }
            finally
            {

            }
        }

        private void Button2_keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                button2_Click(sender, e);
                e.SuppressKeyPress = true; 
             }
        }
        private void webClient_Completed(object sender, AsyncCompletedEventArgs e)
        {
            label5.Text = "Completed";
            progressBar1.Value = 0;
            textBox1.Text = textBox3.Text = "";
        }
        private void webClient_inProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label5.Text = e.ProgressPercentage.ToString()+"%";
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/HWKevin2009/Online-App-Downloader");
        }
    }
}
