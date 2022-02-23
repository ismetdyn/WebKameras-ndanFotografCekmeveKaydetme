using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace Proje5._20WebKamerasındanFotografCekmeveKaydetme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private FilterInfoCollection webKamerasiDizisi; //Bilgisayara bağlı kameraları tutan dizi.

        private VideoCaptureDevice webKamerasi; //Kullanılacak kamera cihazı.

        private void Form1_Load(object sender, EventArgs e)
        {
            webKamerasiDizisi = new FilterInfoCollection(FilterCategory.VideoInputDevice); //Diziyi doldur.
            foreach (FilterInfo item in webKamerasiDizisi)
            {
                comboBox1.Items.Add(item.Name); //Kamera cihazlarını ekleme.
            }
        }
        private void btnBaslat_Click(object sender, EventArgs e)
        {
            webKamerasi = new VideoCaptureDevice(webKamerasiDizisi[comboBox1.SelectedIndex].MonikerString);
            //Seçili kamer aktif donanım cihazı olarak tanımlanır.
            webKamerasi.NewFrame += new NewFrameEventHandler(webKamerasi_NewFrame);
            //Kameranın her bir sahnesinde tetiklenen ve sahnenin PictureBox kontrolünde gösterilmesi için
            //olay tanımlanır.
            webKamerasi.Start(); //Kamerayı başlatma.
        }

        void webKamerasi_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
            //Görüntü tanımlama ve PictureBox kontrolüne atama.
            pictureBox1.Image = bmp;
        }
        private void btnDurdur_Click(object sender, EventArgs e)
        {
            if(webKamerasi.IsRunning)
            {
                webKamerasi.Stop(); //Kamerayı durdurma.
            }
        }

        private void btnFotografCek_Click(object sender, EventArgs e)
        {
            SaveFileDialog svf = new SaveFileDialog();
            DialogResult dialog = svf.ShowDialog();
            if(dialog == DialogResult.OK)
            {
                pictureBox1.Image.Save(svf.FileName);
            }
        }
    }
}
