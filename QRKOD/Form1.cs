using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using ZXing.Aztec;

namespace QRKOD
{
    public partial class Form1 : Form
    { 
        FilterInfoCollection webcam;  //sececeğimiz kamerayı belirliyoruz.
        VideoCaptureDevice cam;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webcam = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo dev in webcam)
            {
                comboBox1.Items.Add(dev.Name); //comboboxa bilgisayarımızda var olan kameraları ekliyoruz.
            }
            comboBox1.SelectedIndex = 0;
        }
        private void cam_NemCamera(object sender ,NewFrameEventArgs eventArgs)
        {
            //kameradan gelen görüntü bitmap,picturebox ın içine atıyoruz
            pictureBox1.Image = ((Bitmap)eventArgs.Frame.Clone());
        }

        private void btnKamera_Click(object sender, EventArgs e)
        {
            cam = new VideoCaptureDevice(webcam[comboBox1.SelectedIndex].MonikerString);
            cam.NewFrame += new NewFrameEventHandler(cam_NemCamera);
            cam.Start();
        }

        private void btnQRTara_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //barkod okuma
            BarcodeReader barkod = new BarcodeReader();
            if (pictureBox1.Image  !=null)
            {
                //decode :şifre çözücü
                //barkodu çözmesi için
                Result resul = barkod.Decode((Bitmap)pictureBox1.Image);
                try
                {

                    string dec = resul.ToString().Trim();
                    if (dec!="")
                    {
                        timer1.Stop();
                        textBox1.Text = dec;
                    }
                }
                catch (Exception exc)
                {

                  
                }
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //cam i kapatması için
            if(cam!=null)
                if (cam.IsRunning == true)
                {
                    cam.Stop();
                }

        }

        private void btnQREkle_Click(object sender, EventArgs e)
        {
            //QROKUTMA FORMUNU EKLEMEK İÇİN
            QROKUTMA ekle = new QROKUTMA();
            ekle.Show();
            this.Hide();
        }
    }
}
