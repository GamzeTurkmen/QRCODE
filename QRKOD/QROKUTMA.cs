using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessagingToolkit.QRCode;
using MessagingToolkit.QRCode.Codec;

namespace QRKOD
{
    public partial class QROKUTMA : Form
    {

        public QROKUTMA()
        {
            InitializeComponent();
        }

        QRCodeEncoder code = new QRCodeEncoder();
        Image resim;
        private void QROKUTMA_Load(object sender, EventArgs e)
        {

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            //öncelikle kodu resime atamamız lazım
            resim = code.Encode(textBox1.Text);
            pictureBox1.Image = resim;


        }
    }
}
