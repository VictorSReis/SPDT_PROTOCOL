namespace SPDTProtocolTeste
{
    partial class UserSocketForm
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
            Lst_ResponseServer = new ListBox();
            Btn_SendStreamData1 = new Button();
            Btn_SendStreamData2 = new Button();
            Btn_SendMalformedPkt = new Button();
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
            LabelStream1.BackColor = Color.Transparent;
            LabelStream1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            LabelStream1.ForeColor = Color.Firebrick;
            LabelStream1.Location = new Point(492, 45);
            LabelStream1.Name = "LabelStream1";
            LabelStream1.Size = new Size(85, 15);
            LabelStream1.TabIndex = 1;
            LabelStream1.Text = "Stream 1: 000";
            // 
            // LabelStream2
            // 
            LabelStream2.AutoSize = true;
            LabelStream2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            LabelStream2.ForeColor = Color.Firebrick;
            LabelStream2.Location = new Point(492, 69);
            LabelStream2.Name = "LabelStream2";
            LabelStream2.Size = new Size(85, 15);
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
            // Lst_ResponseServer
            // 
            Lst_ResponseServer.FormattingEnabled = true;
            Lst_ResponseServer.ItemHeight = 15;
            Lst_ResponseServer.Location = new Point(492, 87);
            Lst_ResponseServer.Name = "Lst_ResponseServer";
            Lst_ResponseServer.Size = new Size(363, 109);
            Lst_ResponseServer.TabIndex = 5;
            // 
            // Btn_SendStreamData1
            // 
            Btn_SendStreamData1.Location = new Point(122, 98);
            Btn_SendStreamData1.Name = "Btn_SendStreamData1";
            Btn_SendStreamData1.Size = new Size(104, 40);
            Btn_SendStreamData1.TabIndex = 6;
            Btn_SendStreamData1.Text = "Send Stream 1";
            Btn_SendStreamData1.UseVisualStyleBackColor = true;
            Btn_SendStreamData1.Click += Btn_SendStreamData1_Click;
            // 
            // Btn_SendStreamData2
            // 
            Btn_SendStreamData2.Location = new Point(122, 156);
            Btn_SendStreamData2.Name = "Btn_SendStreamData2";
            Btn_SendStreamData2.Size = new Size(104, 40);
            Btn_SendStreamData2.TabIndex = 7;
            Btn_SendStreamData2.Text = "Send Stream 2";
            Btn_SendStreamData2.UseVisualStyleBackColor = true;
            Btn_SendStreamData2.Click += Btn_SendStreamData2_Click;
            // 
            // Btn_SendMalformedPkt
            // 
            Btn_SendMalformedPkt.Location = new Point(232, 98);
            Btn_SendMalformedPkt.Name = "Btn_SendMalformedPkt";
            Btn_SendMalformedPkt.Size = new Size(104, 40);
            Btn_SendMalformedPkt.TabIndex = 8;
            Btn_SendMalformedPkt.Text = "Send Malformed Packet";
            Btn_SendMalformedPkt.UseVisualStyleBackColor = true;
            Btn_SendMalformedPkt.Click += Btn_SendMalformedPkt_Click;
            // 
            // UserSocketForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(867, 208);
            Controls.Add(Btn_SendMalformedPkt);
            Controls.Add(Btn_SendStreamData2);
            Controls.Add(Btn_SendStreamData1);
            Controls.Add(Lst_ResponseServer);
            Controls.Add(Btn_CreateStream2);
            Controls.Add(Btn_CreateStream1);
            Controls.Add(LabelStream2);
            Controls.Add(LabelStream1);
            Controls.Add(Btn_Connect);
            Name = "UserSocketForm";
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
        private ListBox Lst_ResponseServer;
        private Button Btn_SendStreamData1;
        private Button Btn_SendStreamData2;
        private Button Btn_SendMalformedPkt;
    }
}