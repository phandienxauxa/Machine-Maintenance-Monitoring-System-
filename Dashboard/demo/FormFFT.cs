using System;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace demo
{
    public partial class FormFFT : DevExpress.XtraEditors.XtraForm
    {
        public FormFFT()
        {
            InitializeComponent();
        }

        // ham public de form1 co the goi va truyen du lieu sang
        public void capNhatPhoFFT(JArray fftData)
        {
            if (chartControlFFT != null && fftData != null)
            {
                // bat invoke tranh loi do dung chung luong giao dien voi form1
                this.BeginInvoke(new Action(() =>
                {
                    chartControlFFT.Series[0].Points.Clear();

                    // tinh do phan giai moi vach tan so 1000hz chia 1024 mau
                    float resolution = 1000.0f / 1024.0f;

                    for (int i = 0; i < fftData.Count; i++)
                    {
                        float freq = (i + 1) * resolution;
                        float mag = (float)fftData[i];
                        chartControlFFT.Series[0].Points.AddPoint(freq, mag);
                    }
                }));
            }
        }
    }
}