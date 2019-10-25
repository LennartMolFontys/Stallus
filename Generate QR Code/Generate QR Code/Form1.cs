﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QRCoder;

namespace Generate_QR_Code
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string generatedString = "Test";


            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(generatedString, QRCodeGenerator.ECCLevel.H);
            QRCode qrCode = new QRCode(qrCodeData);     
            Bitmap customImage = new Bitmap(Properties.Resources.pepe);
            Bitmap qrCodeImage = qrCode.GetGraphic(20, Color.Black, Color.White, customImage, 27);
            pictureBox1.Image = qrCodeImage;
        }
    }
}
