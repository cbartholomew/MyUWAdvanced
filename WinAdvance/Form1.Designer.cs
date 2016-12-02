namespace WinAdvance
{
    partial class frmMain
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
            this.button1 = new System.Windows.Forms.Button();
            this.radioEmailActive = new System.Windows.Forms.RadioButton();
            this.radioEmailInactive = new System.Windows.Forms.RadioButton();
            this.btnLoad = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblFileStatus = new System.Windows.Forms.Label();
            this.txtWorksheetName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 226);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(260, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Do it Donna!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // radioEmailActive
            // 
            this.radioEmailActive.AutoSize = true;
            this.radioEmailActive.Location = new System.Drawing.Point(6, 42);
            this.radioEmailActive.Name = "radioEmailActive";
            this.radioEmailActive.Size = new System.Drawing.Size(88, 17);
            this.radioEmailActive.TabIndex = 1;
            this.radioEmailActive.TabStop = true;
            this.radioEmailActive.Text = "Active Emails";
            this.radioEmailActive.UseVisualStyleBackColor = true;
            // 
            // radioEmailInactive
            // 
            this.radioEmailInactive.AutoSize = true;
            this.radioEmailInactive.Checked = true;
            this.radioEmailInactive.Location = new System.Drawing.Point(6, 19);
            this.radioEmailInactive.Name = "radioEmailInactive";
            this.radioEmailInactive.Size = new System.Drawing.Size(96, 17);
            this.radioEmailInactive.TabIndex = 2;
            this.radioEmailInactive.TabStop = true;
            this.radioEmailInactive.Text = "Inactive Emails";
            this.radioEmailInactive.UseVisualStyleBackColor = true;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(10, 12);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(98, 23);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Load Excel";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioEmailInactive);
            this.groupBox1.Controls.Add(this.radioEmailActive);
            this.groupBox1.Location = new System.Drawing.Point(12, 90);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 74);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Advanced Options";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // lblFileStatus
            // 
            this.lblFileStatus.AutoSize = true;
            this.lblFileStatus.Location = new System.Drawing.Point(114, 17);
            this.lblFileStatus.Name = "lblFileStatus";
            this.lblFileStatus.Size = new System.Drawing.Size(0, 13);
            this.lblFileStatus.TabIndex = 4;
            // 
            // txtWorksheetName
            // 
            this.txtWorksheetName.Location = new System.Drawing.Point(13, 64);
            this.txtWorksheetName.Name = "txtWorksheetName";
            this.txtWorksheetName.Size = new System.Drawing.Size(256, 20);
            this.txtWorksheetName.TabIndex = 5;
            this.txtWorksheetName.Text = "CSE_ALUMS_10-16";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Worksheet";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtWorksheetName);
            this.Controls.Add(this.lblFileStatus);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.button1);
            this.Name = "frmMain";
            this.Text = "Donna\'s EzAdvance";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton radioEmailActive;
        private System.Windows.Forms.RadioButton radioEmailInactive;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblFileStatus;
        private System.Windows.Forms.TextBox txtWorksheetName;
        private System.Windows.Forms.Label label1;
    }
}

