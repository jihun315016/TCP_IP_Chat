namespace SampleChat.Client
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
            txtName = new TextBox();
            label2 = new Label();
            label4 = new Label();
            btnStop = new Button();
            btnConnect = new Button();
            label3 = new Label();
            label1 = new Label();
            txtRoomId = new TextBox();
            lbxMsg = new ListBox();
            txtMessage = new TextBox();
            SuspendLayout();
            // 
            // txtName
            // 
            txtName.Location = new Point(93, 81);
            txtName.Name = "txtName";
            txtName.Size = new Size(135, 23);
            txtName.TabIndex = 44;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(12, 83);
            label2.Name = "label2";
            label2.Size = new Size(53, 21);
            label2.TabIndex = 43;
            label2.Text = "Name";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(12, 50);
            label4.Name = "label4";
            label4.Size = new Size(75, 21);
            label4.TabIndex = 42;
            label4.Text = "Room ID";
            // 
            // btnStop
            // 
            btnStop.Location = new Point(310, 81);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(70, 23);
            btnStop.TabIndex = 41;
            btnStop.Text = "종료";
            btnStop.UseVisualStyleBackColor = true;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(234, 81);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(70, 23);
            btnConnect.TabIndex = 40;
            btnConnect.Text = "연결";
            btnConnect.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(12, 118);
            label3.Name = "label3";
            label3.Size = new Size(82, 21);
            label3.TabIndex = 38;
            label3.Text = "Messages";
            // 
            // label1
            // 
            label1.BackColor = Color.Maroon;
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("맑은 고딕", 15F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.FromArgb(241, 241, 241);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(395, 40);
            label1.TabIndex = 37;
            label1.Text = " Client";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtRoomId
            // 
            txtRoomId.Location = new Point(93, 48);
            txtRoomId.Name = "txtRoomId";
            txtRoomId.Size = new Size(135, 23);
            txtRoomId.TabIndex = 47;
            // 
            // lbxMsg
            // 
            lbxMsg.FormattingEnabled = true;
            lbxMsg.ItemHeight = 15;
            lbxMsg.Location = new Point(12, 155);
            lbxMsg.Name = "lbxMsg";
            lbxMsg.Size = new Size(368, 244);
            lbxMsg.TabIndex = 48;
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(93, 116);
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(287, 23);
            txtMessage.TabIndex = 49;
            // 
            // frmClient
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(395, 413);
            Controls.Add(txtMessage);
            Controls.Add(lbxMsg);
            Controls.Add(txtRoomId);
            Controls.Add(txtName);
            Controls.Add(label2);
            Controls.Add(label4);
            Controls.Add(btnStop);
            Controls.Add(btnConnect);
            Controls.Add(label3);
            Controls.Add(label1);
            Name = "frmClient";
            Text = "frmClient";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtName;
        private Label label2;
        private Label label4;
        private Button btnStop;
        private Button btnConnect;
        private Label label3;
        private Label label1;
        private TextBox txtRoomId;
        private ListBox lbxMsg;
        private TextBox txtMessage;
    }
}