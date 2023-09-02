namespace SPDTProtocolTeste
{
    partial class SPDTProtocolTesteForm
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
            Btn_CreateServer = new Button();
            Btn_CreateClient = new Button();
            SuspendLayout();
            // 
            // Btn_CreateServer
            // 
            Btn_CreateServer.Location = new Point(12, 13);
            Btn_CreateServer.Name = "Btn_CreateServer";
            Btn_CreateServer.Size = new Size(110, 43);
            Btn_CreateServer.TabIndex = 0;
            Btn_CreateServer.Text = "Create Server";
            Btn_CreateServer.UseVisualStyleBackColor = true;
            Btn_CreateServer.Click += Btn_CreateServer_Click;
            // 
            // Btn_CreateClient
            // 
            Btn_CreateClient.Location = new Point(331, 13);
            Btn_CreateClient.Name = "Btn_CreateClient";
            Btn_CreateClient.Size = new Size(110, 43);
            Btn_CreateClient.TabIndex = 1;
            Btn_CreateClient.Text = "Create Client";
            Btn_CreateClient.UseVisualStyleBackColor = true;
            Btn_CreateClient.Click += Btn_CreateClient_Click;
            // 
            // SPDTProtocolTesteForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(453, 259);
            Controls.Add(Btn_CreateClient);
            Controls.Add(Btn_CreateServer);
            Name = "SPDTProtocolTesteForm";
            Text = "SPDT Protocol Server Teste";
            Load += SPDTProtocolTeste_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button Btn_CreateServer;
        private Button Btn_CreateClient;
    }
}