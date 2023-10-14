namespace SampleChat.Server
{
    partial class frmServer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnStop = new Button();
            btnStart = new Button();
            lbxMsg = new ListBox();
            lbxClients = new ListBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnStop
            // 
            btnStop.Location = new Point(87, 43);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(70, 23);
            btnStop.TabIndex = 27;
            btnStop.Text = "중지";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(11, 43);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(70, 23);
            btnStart.TabIndex = 26;
            btnStart.Text = "시작";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // lbxMsg
            // 
            lbxMsg.FormattingEnabled = true;
            lbxMsg.ItemHeight = 15;
            lbxMsg.Location = new Point(158, 93);
            lbxMsg.Name = "lbxMsg";
            lbxMsg.Size = new Size(286, 199);
            lbxMsg.TabIndex = 25;
            // 
            // lbxClients
            // 
            lbxClients.FormattingEnabled = true;
            lbxClients.ItemHeight = 15;
            lbxClients.Location = new Point(12, 93);
            lbxClients.Name = "lbxClients";
            lbxClients.Size = new Size(127, 199);
            lbxClients.TabIndex = 24;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("맑은 고딕", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(158, 69);
            label3.Name = "label3";
            label3.Size = new Size(69, 19);
            label3.TabIndex = 23;
            label3.Text = "Messages";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(12, 69);
            label2.Name = "label2";
            label2.Size = new Size(50, 19);
            label2.TabIndex = 22;
            label2.Text = "Clients";
            // 
            // label1
            // 
            label1.BackColor = SystemColors.Highlight;
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("맑은 고딕", 15F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.FromArgb(241, 241, 241);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(456, 40);
            label1.TabIndex = 21;
            label1.Text = " Server";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // frmServer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(456, 301);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            Controls.Add(lbxMsg);
            Controls.Add(lbxClients);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "frmServer";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnStop;
        private Button btnStart;
        private ListBox lbxMsg;
        private ListBox lbxClients;
        private Label label3;
        private Label label2;
        private Label label1;
    }
}