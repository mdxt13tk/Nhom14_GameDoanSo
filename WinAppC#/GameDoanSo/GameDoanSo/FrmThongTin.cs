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
    public partial class FrmThongTin : Form
    {
        public FrmThongTin()
        {
            InitializeComponent();
        }

        private void FrmThongTin_Load(object sender, EventArgs e)
        {
            txtID.Text = Program.IDUser;
            txtPassword.Text = Program.Password;
            txtUsername.Text = Program.UserName;
            lblRePass.Visible = txtRePass.Visible = false;

            btnOK.Visible = false;
            txtID.Enabled = txtUsername.Enabled = txtPassword.Enabled = txtRePass.Enabled = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Program.frStart = new FrmStart();
            Program.frStart.Visible = true;
            this.Visible = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            lblRePass.Visible = txtRePass.Visible = true;
            txtID.Enabled =  false;
            txtUsername.Enabled = txtPassword.Enabled = txtRePass.Enabled = true;
            btnOK.Visible = true;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string sID, sUserName, sPass, sRePass;
            sID = txtID.Text.Trim();
            sUserName = txtUsername.Text.Trim();
            sPass = txtPassword.Text.Trim();
            sRePass = txtRePass.Text.Trim();
            if (txtUsername.Text.Trim() == "")
            {
                txtUsername.Focus();
                MessageBox.Show("You must enter Username!", "", MessageBoxButtons.OK);
            }
            else if (txtPassword.Text.Trim() == "")
            {
                txtPassword.Focus();
                MessageBox.Show("You must enter Password!", "", MessageBoxButtons.OK);
            }
            else if (txtRePass.Text.Trim() == "")
            {
                txtRePass.Focus();
                MessageBox.Show("You must re-enter Password!", "", MessageBoxButtons.OK);
            }
            else if (txtPassword.Text.Trim() != txtRePass.Text.Trim())
            {
                txtRePass.Focus();
                MessageBox.Show("That's not the right password. Please try again!", "", MessageBoxButtons.OK);
            }
            else
            {
                string url = Program.url+ "user/update";
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "PUT";
                StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());

                string json = "{ \"IDUser\": \"";
                json += sID;
                json += "\",\"MK\": \"";
                json += sPass;
                json += "\",\"HoTen\": \"";
                json += sUserName;
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
                        MessageBox.Show("Successed!");


                    }
                    else if (result == "false")
                    {
                        MessageBox.Show("Fail!");
                    }

                }
                btnOK.Visible = false;
                txtID.Enabled = txtUsername.Enabled = txtPassword.Enabled = txtRePass.Enabled = false;
                lblRePass.Visible = txtRePass.Visible = false;
                
            }
        }
    }
}
