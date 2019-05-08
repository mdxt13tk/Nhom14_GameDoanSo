using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameDoanSo
{
    public partial class FrmChoi : Form
    {
        int[] arrayId;
        int dem;
        int diem;
        public FrmChoi()
        {
            InitializeComponent();
            arrayId = new int[10];
            dem = 0;
        }

        private void FrmChoi_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Interval = 1000;
            GetCauHoi();
          
           getHighScore();
        }

        

        private void getHighScore()
        {
            
            string url = Program.url + "diem/diemcao";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";

            StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());


            string json = "{ \"IDUser\": \""; //IDUser để biết rằng chính user đó yêu cầu lấy câu hỏi mới
            json += Program.IDUser;
            json += "\",\"IDLoai\": \"";
            json += Program.IDLoaiCH;
            json += "\"  }";


            streamWriter.Write(json);
            streamWriter.Flush();
            streamWriter.Close();
            HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string result = reader.ReadToEnd();
                lblHighScore.Text ="High Scores: "+ result;
            }
        }

        private void GetCauHoi()
        {
            if (dem == 10)
            {
                timer1.Stop();
                progressBar1.Value = 0;
                MessageBox.Show("Wow You are win!");
                return;
            }
            string url = Program.url+"cauhoi/ran";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";

            StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());


            string json = "{ \"IDUser\": \""; //IDUser để biết rằng chính user đó yêu cầu lấy câu hỏi mới
            json += Program.IDUser;
            json += "\",\"IDLoai\": \"";
            json += Program.IDLoaiCH;
            json += "\"  }";


            streamWriter.Write(json);
            streamWriter.Flush();
            streamWriter.Close();
           
            
                HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string result = reader.ReadToEnd();

                    CAUHOI cauhoi = JsonConvert.DeserializeObject<CAUHOI>(result); // Chuyển json về dạng CauHoi

                    arrayId[dem] = cauhoi.getIDCH(); //add id câu hỏi vào mảng

                    lblCauHoi.Text = "Question " + ++dem + ": " + cauhoi.getNoiDung(); dem--;
                    btnCauA.Text = "A. " + cauhoi.getCauA();
                    btnCauB.Text = "B. " + cauhoi.getCauB();
                    btnCauC.Text = "C. " + cauhoi.getCauC();
                    btnCauD.Text = "D. " + cauhoi.getCauD();

                }
           
        }

        void gameOver()
        {
            
            string url = Program.url+"cauhoi/remove";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";

            StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());


            string json = "{ \"IDUser\": \""; 
            json += Program.IDUser;
            json += "\",\"IDLoai\": \"";
            json += Program.IDLoaiCH;
            json += "\",\"DIEM1\": \"";
            json += Program.Diem1;
            json += "\"  }";


            streamWriter.Write(json);
            streamWriter.Flush();
            streamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string result = reader.ReadToEnd();
                    
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 15;
            if (progressBar1.Value == 15)
            {
                btnCauA.Enabled = btnCauB.Enabled = btnCauC.Enabled = btnCauD.Enabled = false;
                timer1.Stop();
                MessageBox.Show("Time out! you lost!", "", MessageBoxButtons.OK);
                
            }
            else progressBar1.Value ++;
        }
        private bool getDapAn(int id, string dapan)
        {
            string url = Program.url+"dapan/kiemtra";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";
            StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());

            string json = "{ \"IDCH\": \"";
            json += id;
            json += "\",\"DapAn1\": \"";
            json += dapan;
            json += "\"  }";
            
            streamWriter.Write(json);
            streamWriter.Flush();
            streamWriter.Close();
            HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string result = reader.ReadToEnd();
                if (result == "true")
                {

                    dem++;
                    Program.Diem1 = dem * 10;
                    lblDiem.Text = "Mask: " + Program.Diem1;
                    timer1.Stop();
                    // MessageBox.Show("That's right! Let's do next question!");
                    btnCauA.BackColor= btnCauB.BackColor = btnCauC.BackColor = btnCauD.BackColor = Color.PaleTurquoise;
                    progressBar1.Value = 0;
                    timer1.Start();
                    GetCauHoi();
                }
                else if (result == "false")
                {
                    timer1.Stop();
                    progressBar1.Value = 0;
                    btnCauA.Enabled = btnCauB.Enabled = btnCauC.Enabled = btnCauD.Enabled = false;
                    MessageBox.Show("Oh, That's not the right answer!", "",MessageBoxButtons.OK);

                }

            }
            return false;
        }
        private void btnQuiz_Click(object sender, EventArgs e)
        {
           
            this.Close();
            Program.frStart.Visible = true;
            Program.frStart.Refresh();
        }

        private void btnCauA_Click(object sender, EventArgs e)
        {
            btnCauA.BackColor = Color.Pink;
            getDapAn(arrayId[dem], "A");
        }

        private void btnCauB_Click(object sender, EventArgs e)
        {
            btnCauB.BackColor = Color.Pink;
            getDapAn(arrayId[dem], "B");
        }

        private void btnCauC_Click(object sender, EventArgs e)
        {
            btnCauC.BackColor = Color.Pink;
            getDapAn(arrayId[dem], "C");
        }

        private void btnCauD_Click(object sender, EventArgs e)
        {
            btnCauD.BackColor = Color.Pink;
            getDapAn(arrayId[dem], "D");
        }
        

        private void FrmChoi_FormClosing(object sender, FormClosingEventArgs e)
        {
            gameOver();
            Program.frStart.Visible = true;
            Program.frStart.Refresh();
        }
    }
}
