using System;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace demo
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        private TcpListener tcpServer;
        private Thread listenThread;
        private bool isRunning = false;

        public Form1()
        {
            InitializeComponent();

            // khoi chay server lang nghe ket noi tu esp32 qua wifi
            isRunning = true;
            listenThread = new Thread(new ThreadStart(langNgheKetNoiWifi));
            listenThread.IsBackground = true;
            listenThread.Start();
        }

        // ham nay de fix loi cs0103 bi thieu file form1 load
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void langNgheKetNoiWifi()
        {
            // bat cong 8888 mo rong cho moi ip trong mang noi bo ket noi den
            tcpServer = new TcpListener(IPAddress.Any, 8888);
            tcpServer.Start();

            while (isRunning)
            {
                try
                {
                    if (tcpServer.Pending())
                    {
                        TcpClient client = tcpServer.AcceptTcpClient();
                        StreamReader reader = new StreamReader(client.GetStream());
                        string data = reader.ReadLine();

                        if (!string.IsNullOrEmpty(data))
                        {
                            this.BeginInvoke(new Action(() =>
                            {
                                hienThiLenDashboard(data);
                            }));
                        }
                        client.Close();
                    }
                    Thread.Sleep(10); 
                }
                catch
                {

                }
            }
        }

        // ham doc va ve bieu do dev express
        private void hienThiLenDashboard(string chuoiJson)
        {
            try
            {
                JObject data = JObject.Parse(chuoiJson);

                // kiem tra loai goi tin type de xu ly phu hop
                string loaiGoi = (string)data["type"];

                if (data["vbat"] != null && data["pbat"] != null)
                {
                    float vbat = (float)data["vbat"];
                    float pbat = (float)data["pbat"];

                   if (progBattery != null)
                    {
                        progBattery.Position = Convert.ToInt32(pbat);

                        if (pbat <= 20)
                        {
                            progBattery.ForeColor = System.Drawing.Color.Red; // Dưới 20% -> Đỏ cảnh báo
                        }
                        else if (pbat <= 50)
                        {
                            progBattery.ForeColor = System.Drawing.Color.Orange; // Dưới 50% -> Vàng cam
                        }
                        else
                        {
                            progBattery.ForeColor = System.Drawing.Color.Green; // Trên 50% -> Xanh an toàn
                        }
                    }
                }

                if (data["totalWakes"] != null && data["motionWakes"] != null)
                {
                    int totalWakes = (int)data["totalWakes"];
                    int motionWakes = (int)data["motionWakes"];
                    int timerWakes = totalWakes - motionWakes;

                    if (chartControl3 != null)
                    {
                        chartControl3.Series["Thức định kỳ"].Points.Clear();
                        chartControl3.Series["Thức do rung"].Points.Clear();

                        chartControl3.Series["Thức định kỳ"].Points.AddPoint("Định kỳ", timerWakes);
                        chartControl3.Series["Thức do rung"].Points.AddPoint("Do Rung", motionWakes);
                    }
                }

                if (loaiGoi == "vib")
                {
                    // goi tin rung
                    float ax = (float)data["ax"];
                    float ay = (float)data["ay"];
                    float az = (float)data["az"];

                    // cap nhat bieu do mpu6050
                    chartControl1.Series["truc_x"].Points.AddPoint(DateTime.Now, ax);
                    chartControl1.Series["truc_y"].Points.AddPoint(DateTime.Now, ay);
                    chartControl1.Series["truc_z"].Points.AddPoint(DateTime.Now, az);

                    // nhuong bo bot diem cu de bieu do khong bi tran
                    if (chartControl1.Series["truc_x"].Points.Count > 100)
                    {
                        chartControl1.Series["truc_x"].Points.RemoveAt(0);
                        chartControl1.Series["truc_y"].Points.RemoveAt(0);
                        chartControl1.Series["truc_z"].Points.RemoveAt(0);
                    }
                }
                else if (loaiGoi == "status")
                {
                    float nhietDo = (float)data["temp"];
                    float doAm = (float)data["hum"];

                    int totalWakes = data["totalWakes"] != null ? (int)data["totalWakes"] : 0;
                    int motionWakes = data["motionWakes"] != null ? (int)data["motionWakes"] : 0;
                    int timerWakes = totalWakes - motionWakes;

                    chartControl2.Series["nhiet_do"].Points.AddPoint(DateTime.Now, nhietDo);
                    chartControl2.Series["do_am"].Points.AddPoint(DateTime.Now, doAm);

                    if (chartControl2.Series["nhiet_do"].Points.Count > 100)
                    {
                        chartControl2.Series["nhiet_do"].Points.RemoveAt(0);
                        chartControl2.Series["do_am"].Points.RemoveAt(0);
                    }

                    if (chartControl3 != null)
                    {
                        chartControl3.Series["Thức định kỳ"].Points.Clear();
                        chartControl3.Series["Thức do rung"].Points.Clear();

                        chartControl3.Series["Thức định kỳ"].Points.AddPoint("Định kỳ", timerWakes);
                        chartControl3.Series["Thức do rung"].Points.AddPoint("Do Rung", motionWakes);
                    }
                }
            }

            catch
            {

            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}