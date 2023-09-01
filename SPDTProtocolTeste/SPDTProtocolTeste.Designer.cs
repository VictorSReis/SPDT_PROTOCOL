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
            Btn_CreateSPDTCore = new Button();
            Btn_CreateStreamOnUser1 = new Button();
            SuspendLayout();
            // 
            // Btn_CreateSPDTCore
            // 
            Btn_CreateSPDTCore.Location = new Point(28, 16);
            Btn_CreateSPDTCore.Name = "Btn_CreateSPDTCore";
            Btn_CreateSPDTCore.Size = new Size(125, 63);
            Btn_CreateSPDTCore.TabIndex = 0;
            Btn_CreateSPDTCore.Text = "Create SPDTCore";
            Btn_CreateSPDTCore.UseVisualStyleBackColor = true;
            Btn_CreateSPDTCore.Click += Btn_CreateSPDTCore_Click;
            // 
            // Btn_CreateStreamOnUser1
            // 
            Btn_CreateStreamOnUser1.Location = new Point(159, 16);
            Btn_CreateStreamOnUser1.Name = "Btn_CreateStreamOnUser1";
            Btn_CreateStreamOnUser1.Size = new Size(125, 63);
            Btn_CreateStreamOnUser1.TabIndex = 1;
            Btn_CreateStreamOnUser1.Text = "Create Stream";
            Btn_CreateStreamOnUser1.UseVisualStyleBackColor = true;
            Btn_CreateStreamOnUser1.Click += Btn_CreateStreamOnUser1_Click;
            // 
            // SPDTProtocolTesteForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(Btn_CreateStreamOnUser1);
            Controls.Add(Btn_CreateSPDTCore);
            Name = "SPDTProtocolTesteForm";
            Text = "Form1";
            Load += SPDTProtocolTeste_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button Btn_CreateSPDTCore;
        private Button Btn_CreateStreamOnUser1;
    }
}