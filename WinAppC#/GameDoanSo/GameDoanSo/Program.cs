using System;
using System.Collections.Generic;
using System.IO; //Sử dụng thư viện này để làm việc với Stream
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameDoanSo
{
    static class Program
    {
        public static string IDUser = "";
        public static string UserName = "";
        public static string Password = "";
        public static int IDLoaiCH;
        public static int Diem1;
        public static string url = "http://pmhdv.somee.com/api/";
        public static FrmStart frStart;
        public static FrmDangNhap frDN;
        public static FrmDangKy frDK;
        public static FrmChoi frPlay;
        public static FrmThongTin frTtin;
        public static string ReadTextFromURL(string url)
        {
            using (var client = new WebClient())
            using (var stream = client.OpenRead(url))
            using (var textReader = new StreamReader(stream, Encoding.UTF8, true)) //// read json string from file
            {
                return textReader.ReadToEnd();//ReadToEnd là đọc hết dữ liệu lấy từ url về
            }
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frDN = new FrmDangNhap();
            Application.Run(frDN);
        }
    }
}
