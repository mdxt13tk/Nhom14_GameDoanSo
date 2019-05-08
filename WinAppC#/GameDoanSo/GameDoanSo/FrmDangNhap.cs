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
    public partial class FrmDangNhap : Form
    {
        public FrmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            Program.frDK = new FrmDangKy();
            Program.frDK.Visible = true;
            this.Visible = false;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (txtIDUser.Text.Trim() == "")
            {
                txtIDUser.Focus();
                MessageBox.Show("You must enter Username!","",MessageBoxButtons.OK);
            }
            if (txtPassword.Text.Trim() == "")
            {
                txtPassword.Focus();
                MessageBox.Show("You must enter Password!","",MessageBoxButtons.OK);
            }
            //kiểm tra hợp lệ dữ liệu nhập vào so với dữ liệu lấy trên webservice về.
            string idLogin = txtIDUser.Text.Trim(), mkLogin = txtPassword.Text.Trim();
            string url = Program.url+"user/login"; 
 
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json; charset=utf-8"; 
            httpWebRequest.Method = "POST";
            StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());

            string json = "{ \"IDUser\": \"";
            json += idLogin;
            json += "\",\"MK\": \"";
            json += mkLogin;
            json += "\"  }";

            streamWriter.Write(json);//gửi chuỗi lên server
            streamWriter.Flush();
            streamWriter.Close();

            //Gửi HttpWebRequest. Tạo biến Response nhận kết quả trả về.
            HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)//Yêu cầu đã thành công và thông tin được yêu cầu nằm trong phản hồi
            {                
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string result = reader.ReadToEnd();
                result = result.Substring(1, result.Length - 2); // Cắt bỏ 2 dấu " ở 2 đầu chuỗi trả về
                if (result == "IdError")
                {
                    MessageBox.Show("User does not Exists!Please try again!");
                    txtIDUser.Focus();
                }
                else if (result == "PassError")
                {
                    MessageBox.Show("That's not the right password. Please try again!");
                    txtPassword.Focus();
                }
                else
                {
                    Program.IDUser = idLogin;
                    Program.Password = mkLogin;
                    Program.UserName = result;
                    this.Visible = false;
                    Program.frStart = new FrmStart();
                    Program.frStart.Visible = true;
                    this.Visible = false;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        { 
            this.Close();
        }

        
    }
}
