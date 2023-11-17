namespace ChatService.Client
{
    partial class frmClient
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
            txtMessage = new TextBox();
            lbxMsg = new ListBox();
            txtName = new TextBox();
            label2 = new Label();
            btnStop = new Button();
            btnConnect = new Button();
            label3 = new Label();
            txtAddress = new TextBox();
            label1 = new Label();
            txtPort = new TextBox();
            SuspendLayout();
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(93, 315);
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(287, 23);
            txtMessage.TabIndex = 59;
            // 
            // lbxMsg
            // 
            lbxMsg.FormattingEnabled = true;
            lbxMsg.ItemHeight = 15;
            lbxMsg.Location = new Point(12, 65);
            lbxMsg.Name = "lbxMsg";
            lbxMsg.Size = new Size(368, 244);
            lbxMsg.TabIndex = 58;
            // 
            // txtName
            // 
            txtName.Location = new Point(93, 36);
            txtName.Name = "txtName";
            txtName.Size = new Size(135, 23);
            txtName.TabIndex = 56;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(12, 38);
            label2.Name = "label2";
            label2.Size = new Size(53, 21);
            label2.TabIndex = 55;
            label2.Text = "Name";
            // 
            // btnStop
            // 
            btnStop.ImageAlign = ContentAlignment.TopRight;
            btnStop.Location = new Point(310, 36);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(70, 23);
            btnStop.TabIndex = 53;
            btnStop.Text = "종료";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(234, 36);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(70, 23);
            btnConnect.TabIndex = 52;
            btnConnect.Text = "연결";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(12, 317);
            label3.Name = "label3";
            label3.Size = new Size(82, 21);
            label3.TabIndex = 51;
            label3.Text = "Messages";
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(93, 7);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(184, 23);
            txtAddress.TabIndex = 61;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(72, 21);
            label1.TabIndex = 60;
            label1.Text = "IP / Port";
            // 
            // txtPort
            // 
            txtPort.Location = new Point(283, 7);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(97, 23);
            txtPort.TabIndex = 62;
            // 
            // frmClient
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(398, 352);
            Controls.Add(txtPort);
            Controls.Add(txtAddress);
            Controls.Add(label1);
            Controls.Add(txtMessage);
            Controls.Add(lbxMsg);
            Controls.Add(txtName);
            Controls.Add(label2);
            Controls.Add(btnStop);
            Controls.Add(btnConnect);
            Controls.Add(label3);
            Name = "frmClient";
            Text = "frmClient";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtMessage;
        private ListBox lbxMsg;
        private TextBox txtName;
        private Label label2;
        private Button btnStop;
        private Button btnConnect;
        private Label label3;
        private TextBox txtAddress;
        private Label label1;
        private TextBox txtPort;
    }
}