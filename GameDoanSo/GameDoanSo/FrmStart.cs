using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameDoanSo
{
    public partial class FrmStart : Form
    {
        public FrmStart()
        {
            InitializeComponent();
        }

        private void btnBatDau_Click(object sender, EventArgs e)
        {
            Program.frPlay = new FrmChoi();
            Program.frPlay.Visible = true;
            this.Visible = false;
            //Program.IDLoaiCH = cmbMucChoi.;
        }

        private void btnThongTin_Click(object sender, EventArgs e)
        {
            Program.frTtin = new FrmThongTin();
            Program.frTtin.Visible = true;
            this.Visible = false;
        }

        private void FrmStart_Load(object sender, EventArgs e)
        {
            List<string>  listItem = new List<string> {"Easy","Medium","Hard" };
            cmbMucChoi.DataSource = listItem;
            lblUsername.Text ="Welcome    "+ Program.UserName + "  !";
        }

        

        private void FrmStart_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.frDN.Close();
        }

        private void cmbMucChoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                if (cmbMucChoi.SelectedValue != null)
                {
                    Program.IDLoaiCH = cmbMucChoi.SelectedIndex +1;

                }

            
        }

        private void btnQuiz_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Program.frDN.Visible = true;
            Program.frDN.Refresh();
        }
    }
}
