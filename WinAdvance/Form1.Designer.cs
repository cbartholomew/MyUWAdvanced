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
            this.lblExcelFileStatus = new System.Windows.Forms.Label();
            this.lblConfirmedWorksheet = new System.Windows.Forms.Label();
            this.btnConfirmWorksheet = new System.Windows.Forms.Button();
            this.lblAdvancedOptions = new System.Windows.Forms.Label();
            this.btnKill = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(53, 197);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(257, 80);
            this.button1.TabIndex = 0;
            this.button1.Text = "Advance";
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
            this.radioEmailActive.Text = "Active Emails";
            this.radioEmailActive.UseVisualStyleBackColor = true;
            this.radioEmailActive.CheckedChanged += new System.EventHandler(this.radioEmailActive_CheckedChanged);
            // 
            // radioEmailInactive
            // 
            this.radioEmailInactive.AutoSize = true;
            this.radioEmailInactive.Location = new System.Drawing.Point(6, 19);
            this.radioEmailInactive.Name = "radioEmailInactive";
            this.radioEmailInactive.Size = new System.Drawing.Size(96, 17);
            this.radioEmailInactive.TabIndex = 2;
            this.radioEmailInactive.Text = "Inactive Emails";
            this.radioEmailInactive.UseVisualStyleBackColor = true;
            this.radioEmailInactive.CheckedChanged += new System.EventHandler(this.radioEmailInactive_CheckedChanged);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(53, 6);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(98, 37);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Load Excel";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioEmailInactive);
            this.groupBox1.Controls.Add(this.radioEmailActive);
            this.groupBox1.Location = new System.Drawing.Point(53, 112);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 74);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Advanced Options";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // lblFileStatus
            // 
            this.lblFileStatus.AutoSize = true;
            this.lblFileStatus.Location = new System.Drawing.Point(157, 18);
            this.lblFileStatus.Name = "lblFileStatus";
            this.lblFileStatus.Size = new System.Drawing.Size(0, 13);
            this.lblFileStatus.TabIndex = 4;
            // 
            // txtWorksheetName
            // 
            this.txtWorksheetName.Location = new System.Drawing.Point(157, 75);
            this.txtWorksheetName.Name = "txtWorksheetName";
            this.txtWorksheetName.Size = new System.Drawing.Size(120, 20);
            this.txtWorksheetName.TabIndex = 5;
            this.txtWorksheetName.Text = "CSE_ALUMS_10-16";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Confirm Worksheet Name";
            // 
            // lblExcelFileStatus
            // 
            this.lblExcelFileStatus.BackColor = System.Drawing.Color.Red;
            this.lblExcelFileStatus.Location = new System.Drawing.Point(12, 6);
            this.lblExcelFileStatus.Name = "lblExcelFileStatus";
            this.lblExcelFileStatus.Size = new System.Drawing.Size(35, 37);
            this.lblExcelFileStatus.TabIndex = 7;
            this.lblExcelFileStatus.Click += new System.EventHandler(this.label2_Click);
            // 
            // lblConfirmedWorksheet
            // 
            this.lblConfirmedWorksheet.BackColor = System.Drawing.Color.Red;
            this.lblConfirmedWorksheet.Location = new System.Drawing.Point(12, 58);
            this.lblConfirmedWorksheet.Name = "lblConfirmedWorksheet";
            this.lblConfirmedWorksheet.Size = new System.Drawing.Size(35, 37);
            this.lblConfirmedWorksheet.TabIndex = 8;
            // 
            // btnConfirmWorksheet
            // 
            this.btnConfirmWorksheet.Location = new System.Drawing.Point(53, 73);
            this.btnConfirmWorksheet.Name = "btnConfirmWorksheet";
            this.btnConfirmWorksheet.Size = new System.Drawing.Size(98, 23);
            this.btnConfirmWorksheet.TabIndex = 9;
            this.btnConfirmWorksheet.Text = "Confirm";
            this.btnConfirmWorksheet.UseVisualStyleBackColor = true;
            this.btnConfirmWorksheet.Click += new System.EventHandler(this.btnConfirmWorksheet_Click);
            // 
            // lblAdvancedOptions
            // 
            this.lblAdvancedOptions.BackColor = System.Drawing.Color.Red;
            this.lblAdvancedOptions.Location = new System.Drawing.Point(12, 112);
            this.lblAdvancedOptions.Name = "lblAdvancedOptions";
            this.lblAdvancedOptions.Size = new System.Drawing.Size(35, 37);
            this.lblAdvancedOptions.TabIndex = 10;
            // 
            // btnKill
            // 
            this.btnKill.Location = new System.Drawing.Point(3, 197);
            this.btnKill.Name = "btnKill";
            this.btnKill.Size = new System.Drawing.Size(44, 80);
            this.btnKill.TabIndex = 11;
            this.btnKill.Text = "Stop";
            this.btnKill.UseVisualStyleBackColor = true;
            this.btnKill.Click += new System.EventHandler(this.btnKill_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(316, 197);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(257, 80);
            this.button2.TabIndex = 12;
            this.button2.Text = "Sales Force";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 294);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnKill);
            this.Controls.Add(this.lblAdvancedOptions);
            this.Controls.Add(this.btnConfirmWorksheet);
            this.Controls.Add(this.lblConfirmedWorksheet);
            this.Controls.Add(this.lblExcelFileStatus);
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
        private System.Windows.Forms.Label lblExcelFileStatus;
        private System.Windows.Forms.Label lblConfirmedWorksheet;
        private System.Windows.Forms.Button btnConfirmWorksheet;
        private System.Windows.Forms.Label lblAdvancedOptions;
        private System.Windows.Forms.Button btnKill;
        private System.Windows.Forms.Button button2;
    }
}

