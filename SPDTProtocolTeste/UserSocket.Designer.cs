namespace SPDTProtocolTeste
{
    partial class UserSocket
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
            Btn_Connect = new Button();
            LabelStream1 = new Label();
            LabelStream2 = new Label();
            Btn_CreateStream1 = new Button();
            Btn_CreateStream2 = new Button();
            SuspendLayout();
            // 
            // Btn_Connect
            // 
            Btn_Connect.Location = new Point(12, 12);
            Btn_Connect.Name = "Btn_Connect";
            Btn_Connect.Size = new Size(104, 40);
            Btn_Connect.TabIndex = 0;
            Btn_Connect.Text = "Connect";
            Btn_Connect.UseVisualStyleBackColor = true;
            Btn_Connect.Click += Btn_Connect_Click;
            // 
            // LabelStream1
            // 
            LabelStream1.AutoSize = true;
            LabelStream1.Location = new Point(365, 37);
            LabelStream1.Name = "LabelStream1";
            LabelStream1.Size = new Size(77, 15);
            LabelStream1.TabIndex = 1;
            LabelStream1.Text = "Stream 1: 000";
            // 
            // LabelStream2
            // 
            LabelStream2.AutoSize = true;
            LabelStream2.Location = new Point(365, 72);
            LabelStream2.Name = "LabelStream2";
            LabelStream2.Size = new Size(77, 15);
            LabelStream2.TabIndex = 2;
            LabelStream2.Text = "Stream 2: 000";
            // 
            // Btn_CreateStream1
            // 
            Btn_CreateStream1.Location = new Point(12, 98);
            Btn_CreateStream1.Name = "Btn_CreateStream1";
            Btn_CreateStream1.Size = new Size(104, 40);
            Btn_CreateStream1.TabIndex = 3;
            Btn_CreateStream1.Text = "Create Stream 1";
            Btn_CreateStream1.UseVisualStyleBackColor = true;
            Btn_CreateStream1.Click += Btn_CreateStream1_Click;
            // 
            // Btn_CreateStream2
            // 
            Btn_CreateStream2.Location = new Point(12, 156);
            Btn_CreateStream2.Name = "Btn_CreateStream2";
            Btn_CreateStream2.Size = new Size(104, 40);
            Btn_CreateStream2.TabIndex = 4;
            Btn_CreateStream2.Text = "Create Stream 2";
            Btn_CreateStream2.UseVisualStyleBackColor = true;
            Btn_CreateStream2.Click += Btn_CreateStream2_Click;
            // 
            // UserSocket
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(543, 208);
            Controls.Add(Btn_CreateStream2);
            Controls.Add(Btn_CreateStream1);
            Controls.Add(LabelStream2);
            Controls.Add(LabelStream1);
            Controls.Add(Btn_Connect);
            Name = "UserSocket";
            Text = "UserSocket";
            Load += UserSocket_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Btn_Connect;
        private Label LabelStream1;
        private Label LabelStream2;
        private Button Btn_CreateStream1;
        private Button Btn_CreateStream2;
    }
}