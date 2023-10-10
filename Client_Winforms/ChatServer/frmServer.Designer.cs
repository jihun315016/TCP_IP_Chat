namespace ChatServer
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
            btnListen = new Button();
            txtCount = new TextBox();
            listBox1 = new ListBox();
            SuspendLayout();
            // 
            // btnListen
            // 
            btnListen.Location = new Point(12, 13);
            btnListen.Margin = new Padding(3, 4, 3, 4);
            btnListen.Name = "btnListen";
            btnListen.Size = new Size(100, 29);
            btnListen.TabIndex = 1;
            btnListen.Text = "시작";
            btnListen.UseVisualStyleBackColor = true;
            btnListen.Click += btnListen_Click;
            // 
            // txtCount
            // 
            txtCount.Location = new Point(12, 414);
            txtCount.Margin = new Padding(3, 4, 3, 4);
            txtCount.Name = "txtCount";
            txtCount.Size = new Size(100, 23);
            txtCount.TabIndex = 3;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(118, 13);
            listBox1.Margin = new Padding(3, 4, 3, 4);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(670, 424);
            listBox1.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(listBox1);
            Controls.Add(txtCount);
            Controls.Add(btnListen);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnListen;
        private TextBox txtCount;
        private ListBox listBox1;
    }
}