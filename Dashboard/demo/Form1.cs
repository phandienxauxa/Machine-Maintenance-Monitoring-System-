using System;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
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

        // ham thuc thi gui email qua giao thuc smtp cua google
        private void guiEmailCanhBao(string thongBaoLoi)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

                // thiet lap dia chi nguoi gui va nguoi nhan
                mail.From = new MailAddress("thaiducthienthcs@gmail.com");
                mail.To.Add("thaiducthienthcs@gmail.com");
                mail.Subject = "Cảnh báo khẩn cấp Hệ thống Giám sát IIoT";
                mail.Body = $@"Phát hiện rung động bất thường!!!. 
                
                Chi tiết: {thongBaoLoi} 
                Thời gian: {DateTime.Now}

                Vị trí: Phòng 1016, Tòa BA4, Kí túc xá khu B - Đại học Quốc gia Tp.HCM";

                // cau hinh server gmail theo chuan RFC 6409
                smtpServer.Port = 587;

                // su dung mat khau ung dung chu khong phai mat khau dang nhap
                smtpServer.Credentials = new NetworkCredential(ThongTin.EmailGui, ThongTin.MatkhauGui);
                smtpServer.EnableSsl = true;

                // thuc hien gui
                smtpServer.Send(mail);
            }
            catch
            {
                // bo qua ngoai le de tranh treo chuong trinh neu rot mang
            }
        }


        // ham doc va ve bieu do dev express
        private void hienThiLenDashboard(string chuoiJson)
        {
            try
            {
                JObject data = JObject.Parse(chuoiJson);
                string loaiGoi = (string)data["type"];

                // 1. Xu ly khoi pin va % pin
                if (data["vbat"] != null && data["pbat"] != null)
                {
                    float vbat = (float)data["vbat"];
                    float pbat = (float)data["pbat"];
                    if (progBattery != null)
                    {
                        progBattery.Position = Convert.ToInt32(pbat);
                        if (pbat <= 20) progBattery.ForeColor = System.Drawing.Color.Red;
                        else if (pbat <= 50) progBattery.ForeColor = System.Drawing.Color.Orange;
                        else progBattery.ForeColor = System.Drawing.Color.Green;
                    }
                }


                // 2. Xu ly so lan thuc do dinh ky cua he thong hay do rung dong may moc
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

                // 3. Nhiet do & do am
                if (data["temp"] != null && data["hum"] != null)
                {
                    float nhietDo = (float)data["temp"];
                    float doAm = (float)data["hum"];

                    if (chartControl2 != null)
                    {
                        chartControl2.Series["nhiet_do"].Points.AddPoint(DateTime.Now, nhietDo);
                        chartControl2.Series["do_am"].Points.AddPoint(DateTime.Now, doAm);

                        // Giới hạn số điểm để không làm nặng màn hình Dashboard
                        if (chartControl2.Series["nhiet_do"].Points.Count > 100)
                        {
                            chartControl2.Series["nhiet_do"].Points.RemoveAt(0);
                            chartControl2.Series["do_am"].Points.RemoveAt(0);
                        }
                    }
                }

                // 4. Gia tri x, y, z va thuat toan FFT
                if (loaiGoi == "vib")
                {
                    float ax = (float)data["ax"];
                    float ay = (float)data["ay"];
                    float az = (float)data["az"];

                    if (chartControl1 != null)
                    {
                        chartControl1.Series["truc_x"].Points.AddPoint(DateTime.Now, ax);
                        chartControl1.Series["truc_y"].Points.AddPoint(DateTime.Now, ay);
                        chartControl1.Series["truc_z"].Points.AddPoint(DateTime.Now, az);

                        if (chartControl1.Series["truc_x"].Points.Count > 100)
                        {
                            chartControl1.Series["truc_x"].Points.RemoveAt(0);
                            chartControl1.Series["truc_y"].Points.RemoveAt(0);
                            chartControl1.Series["truc_z"].Points.RemoveAt(0);
                        }
                    }

                    // lay trang thai canh bao tu json do esp32 gui len
                    string trangThai = (string)data["status"];

                    if (trangThai == "ALARM")
                    {
                        // tang 1 hien thi popup truot len
                        if (alertControl1 != null)
                        {
                            alertControl1.Show(this, "Cảnh báo khẩn cấp!!!", "Phát hiện máy rung lắc vượt ngưỡng an toàn.");
                        }

                        // tang 2 gui email thong bao cho ky su
                        guiEmailCanhBao("Độ lớn gia tốc chạm ngưỡng ALARM. Cần kiểm tra cơ khí gấp!!!.");
                    }
                }

                else if (loaiGoi == "fft")
                {
                    JArray fftData = (JArray)data["fft_data"];

                    if (formPhanTichFFT != null && !formPhanTichFFT.IsDisposed)
                    {
                        formPhanTichFFT.capNhatPhoFFT(fftData);
                    }
                }
            }
            catch
            {
                // Tránh treo ứng dụng nếu chuỗi JSON lỗi định dạng cục bộ
            }
        }

        // khai bao mot bien toan cuc chua cua so fft o dau class form1
        private FormFFT formPhanTichFFT = null;

        // su kien nhap dup chuot vao bieu do rung
        private void chartControl1_DoubleClick(object sender, EventArgs e)
        {
            // kiem tra neu form chua mo hoac da bi tat thi tao moi
            if (formPhanTichFFT == null || formPhanTichFFT.IsDisposed)
            {
                formPhanTichFFT = new FormFFT();
                formPhanTichFFT.Show();
            }
            else
            {
                // neu form dang mo ma bi khuat phia sau thi mang no len truoc
                formPhanTichFFT.BringToFront();
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}