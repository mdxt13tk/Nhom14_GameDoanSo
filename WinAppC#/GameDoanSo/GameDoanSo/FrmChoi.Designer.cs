namespace GameDoanSo
{
    partial class FrmChoi
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmChoi));
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblCauHoi = new System.Windows.Forms.Label();
            this.btnCauA = new System.Windows.Forms.Button();
            this.btnCauB = new System.Windows.Forms.Button();
            this.btnCauC = new System.Windows.Forms.Button();
            this.btnCauD = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblHighScore = new System.Windows.Forms.Label();
            this.lblDiem = new System.Windows.Forms.Label();
            this.btnQuiz = new System.Windows.Forms.Button();
            this.btnQuitGame = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(200, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Let\'s Play! ";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(81, 79);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(345, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblCauHoi
            // 
            this.lblCauHoi.AutoSize = true;
            this.lblCauHoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblCauHoi.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCauHoi.Location = new System.Drawing.Point(145, 142);
            this.lblCauHoi.Name = "lblCauHoi";
            this.lblCauHoi.Size = new System.Drawing.Size(61, 19);
            this.lblCauHoi.TabIndex = 2;
            this.lblCauHoi.Text = "Câu hỏi:";
            this.lblCauHoi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCauA
            // 
            this.btnCauA.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnCauA.Font = new System.Drawing.Font("Segoe Print", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCauA.Location = new System.Drawing.Point(81, 203);
            this.btnCauA.Name = "btnCauA";
            this.btnCauA.Size = new System.Drawing.Size(345, 44);
            this.btnCauA.TabIndex = 3;
            this.btnCauA.Text = "A:";
            this.btnCauA.UseVisualStyleBackColor = false;
            this.btnCauA.Click += new System.EventHandler(this.btnCauA_Click);
            // 
            // btnCauB
            // 
            this.btnCauB.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnCauB.Font = new System.Drawing.Font("Segoe Print", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCauB.Location = new System.Drawing.Point(81, 259);
            this.btnCauB.Name = "btnCauB";
            this.btnCauB.Size = new System.Drawing.Size(345, 44);
            this.btnCauB.TabIndex = 4;
            this.btnCauB.Text = "B:";
            this.btnCauB.UseVisualStyleBackColor = false;
            this.btnCauB.Click += new System.EventHandler(this.btnCauB_Click);
            // 
            // btnCauC
            // 
            this.btnCauC.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnCauC.Font = new System.Drawing.Font("Segoe Print", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCauC.Location = new System.Drawing.Point(81, 320);
            this.btnCauC.Name = "btnCauC";
            this.btnCauC.Size = new System.Drawing.Size(345, 44);
            this.btnCauC.TabIndex = 5;
            this.btnCauC.Text = "C:";
            this.btnCauC.UseVisualStyleBackColor = false;
            this.btnCauC.Click += new System.EventHandler(this.btnCauC_Click);
            // 
            // btnCauD
            // 
            this.btnCauD.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnCauD.Font = new System.Drawing.Font("Segoe Print", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCauD.Location = new System.Drawing.Point(81, 385);
            this.btnCauD.Name = "btnCauD";
            this.btnCauD.Size = new System.Drawing.Size(345, 44);
            this.btnCauD.TabIndex = 6;
            this.btnCauD.Text = "D:";
            this.btnCauD.UseVisualStyleBackColor = false;
            this.btnCauD.Click += new System.EventHandler(this.btnCauD_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox1.BackgroundImage")));
            this.groupBox1.Controls.Add(this.btnQuitGame);
            this.groupBox1.Controls.Add(this.lblHighScore);
            this.groupBox1.Controls.Add(this.lblDiem);
            this.groupBox1.Controls.Add(this.btnQuiz);
            this.groupBox1.Controls.Add(this.btnCauA);
            this.groupBox1.Controls.Add(this.btnCauD);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnCauC);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.btnCauB);
            this.groupBox1.Controls.Add(this.lblCauHoi);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(504, 511);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // lblHighScore
            // 
            this.lblHighScore.AutoSize = true;
            this.lblHighScore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lblHighScore.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHighScore.Location = new System.Drawing.Point(12, 25);
            this.lblHighScore.Name = "lblHighScore";
            this.lblHighScore.Size = new System.Drawing.Size(109, 23);
            this.lblHighScore.TabIndex = 9;
            this.lblHighScore.Text = "High Scores: ";
            // 
            // lblDiem
            // 
            this.lblDiem.AutoSize = true;
            this.lblDiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lblDiem.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiem.Location = new System.Drawing.Point(379, 25);
            this.lblDiem.Name = "lblDiem";
            this.lblDiem.Size = new System.Drawing.Size(62, 23);
            this.lblDiem.TabIndex = 8;
            this.lblDiem.Text = "Score: ";
            // 
            // btnQuiz
            // 
            this.btnQuiz.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnQuiz.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuiz.Location = new System.Drawing.Point(357, 444);
            this.btnQuiz.Name = "btnQuiz";
            this.btnQuiz.Size = new System.Drawing.Size(118, 36);
            this.btnQuiz.TabIndex = 7;
            this.btnQuiz.Text = "RePlay";
            this.btnQuiz.UseVisualStyleBackColor = false;
            this.btnQuiz.Click += new System.EventHandler(this.btnQuiz_Click);
            // 
            // btnQuitGame
            // 
            this.btnQuitGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnQuitGame.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuitGame.Location = new System.Drawing.Point(16, 444);
            this.btnQuitGame.Name = "btnQuitGame";
            this.btnQuitGame.Size = new System.Drawing.Size(118, 36);
            this.btnQuitGame.TabIndex = 10;
            this.btnQuitGame.Text = "Quit";
            this.btnQuitGame.UseVisualStyleBackColor = false;
            this.btnQuitGame.Click += new System.EventHandler(this.btnQuitGame_Click);
            // 
            // FrmChoi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 511);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmChoi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Play";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmChoi_FormClosing);
            this.Load += new System.EventHandler(this.FrmChoi_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblCauHoi;
        private System.Windows.Forms.Button btnCauA;
        private System.Windows.Forms.Button btnCauB;
        private System.Windows.Forms.Button btnCauC;
        private System.Windows.Forms.Button btnCauD;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnQuiz;
        private System.Windows.Forms.Label lblDiem;
        private System.Windows.Forms.Label lblHighScore;
        private System.Windows.Forms.Button btnQuitGame;
    }
}