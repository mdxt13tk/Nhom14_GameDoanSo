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
    public partial class FrmDangKy : Form
    {
        public FrmDangKy()
        {
            InitializeComponent();
        }

        
        private void btnDangKy_Click(object sender, EventArgs e)
        {

            string sID, sUserName, sPass, sRePass;
            sID = txtID.Text.Trim();
            sUserName = txtUsername.Text.Trim();
            sPass = txtPassword.Text.Trim();
            sRePass = txtRePass.Text.Trim();
            
            if ( sID== "")
            {
                txtID.Focus();
                MessageBox.Show("You must enter ID!", "", MessageBoxButtons.OK);
            }
            else if (sUserName == "")
            {
                txtUsername.Focus();
                MessageBox.Show("You must enter Username!", "", MessageBoxButtons.OK);
            }
            else if (sPass == "")
            {
                txtPassword.Focus();
                MessageBox.Show("You must enter password!", "", MessageBoxButtons.OK);
            }
            else if (sRePass == "")
            {
                txtRePass.Focus();
                MessageBox.Show("You must re-enter password!", "", MessageBoxButtons.OK);
            }
            else if (sRePass != sPass)
            {
                txtRePass.Focus();
                MessageBox.Show("That's not the right password. Please try again!", "", MessageBoxButtons.OK);
            }

            else
            {
                string url = Program.url+"user/register";
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "POST";
                StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());

                string json = "{ \"IDUser\": \"";
                json += sID;
                json += "\",\"MK\": \"";
                json += sPass;
                json += "\",\"HoTen\": \"";
                json += sUserName;
                json += "\"  }";

                //Debug.Write(json);
                //Ghi chuỗi json 
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
                HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string result = reader.ReadToEnd();
                    //MessageBox.Show(result);
                    if (result == "true")
                    {
                        MessageBox.Show("Success!");
                        txtID.Text = txtPassword.Text = txtRePass.Text = txtUsername.Text = "";
                        txtID.Focus();
                    }
                    else if (result == "false")
                    {
                        MessageBox.Show("User exists!Choose a different!");
                    }

                }
            }

            
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Program.frDN = new FrmDangNhap();
            Program.frDN.Visible = true;
            Program.frDN.Refresh();
            this.Close();
        }
    }
}
