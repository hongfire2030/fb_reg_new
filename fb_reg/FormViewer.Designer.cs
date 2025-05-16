namespace fb_reg.Viewer
{
    partial class FormViewer
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
            this.panelPhone = new System.Windows.Forms.Panel();
            this.HomeButton = new System.Windows.Forms.Button();
            this.Backbutton = new System.Windows.Forms.Button();
            this.powerbutton = new System.Windows.Forms.Button();
            this.recentbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelPhone
            // 
            this.panelPhone.Location = new System.Drawing.Point(3, 0);
            this.panelPhone.Name = "panelPhone";
            this.panelPhone.Size = new System.Drawing.Size(446, 739);
            this.panelPhone.TabIndex = 0;
            this.panelPhone.Resize += new System.EventHandler(this.panelPhone_Resize);
            // 
            // HomeButton
            // 
            this.HomeButton.Location = new System.Drawing.Point(120, 746);
            this.HomeButton.Name = "HomeButton";
            this.HomeButton.Size = new System.Drawing.Size(75, 23);
            this.HomeButton.TabIndex = 1;
            this.HomeButton.Text = "Home";
            this.HomeButton.UseVisualStyleBackColor = true;
            this.HomeButton.Click += new System.EventHandler(this.HomeButton_Click);
            // 
            // Backbutton
            // 
            this.Backbutton.Location = new System.Drawing.Point(30, 745);
            this.Backbutton.Name = "Backbutton";
            this.Backbutton.Size = new System.Drawing.Size(75, 23);
            this.Backbutton.TabIndex = 2;
            this.Backbutton.Text = "Back";
            this.Backbutton.UseVisualStyleBackColor = true;
            this.Backbutton.Click += new System.EventHandler(this.Backbutton_Click);
            // 
            // powerbutton
            // 
            this.powerbutton.Location = new System.Drawing.Point(317, 747);
            this.powerbutton.Name = "powerbutton";
            this.powerbutton.Size = new System.Drawing.Size(75, 23);
            this.powerbutton.TabIndex = 3;
            this.powerbutton.Text = "Power";
            this.powerbutton.UseVisualStyleBackColor = true;
            this.powerbutton.Click += new System.EventHandler(this.powerbutton_Click);
            // 
            // recentbutton
            // 
            this.recentbutton.Location = new System.Drawing.Point(222, 744);
            this.recentbutton.Name = "recentbutton";
            this.recentbutton.Size = new System.Drawing.Size(75, 23);
            this.recentbutton.TabIndex = 4;
            this.recentbutton.Text = "Recent";
            this.recentbutton.UseVisualStyleBackColor = true;
            this.recentbutton.Click += new System.EventHandler(this.recentbutton_Click);
            // 
            // FormViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 787);
            this.Controls.Add(this.recentbutton);
            this.Controls.Add(this.powerbutton);
            this.Controls.Add(this.Backbutton);
            this.Controls.Add(this.HomeButton);
            this.Controls.Add(this.panelPhone);
            this.Name = "FormViewer";
            this.Text = "FormViewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormViewer_FormClosing);
            this.Load += new System.EventHandler(this.FormViewer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelPhone;
        private System.Windows.Forms.Button HomeButton;
        private System.Windows.Forms.Button Backbutton;
        private System.Windows.Forms.Button powerbutton;
        private System.Windows.Forms.Button recentbutton;
    }
}