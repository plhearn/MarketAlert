namespace MarketAlert
{
    partial class Form1
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
            this.lblMessage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAlertThreshold = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMinutes = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lblDebug = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.RichTextBox();
            this.lblCurrentPrice = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(18, 174);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(74, 20);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "message";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 82);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Alert when price changes by ";
            // 
            // txtAlertThreshold
            // 
            this.txtAlertThreshold.Location = new System.Drawing.Point(228, 77);
            this.txtAlertThreshold.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtAlertThreshold.Name = "txtAlertThreshold";
            this.txtAlertThreshold.Size = new System.Drawing.Size(31, 26);
            this.txtAlertThreshold.TabIndex = 2;
            this.txtAlertThreshold.Text = "0.5";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(270, 82);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "percent within";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(428, 82);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "minutes.";
            // 
            // txtMinutes
            // 
            this.txtMinutes.Location = new System.Drawing.Point(388, 77);
            this.txtMinutes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMinutes.Name = "txtMinutes";
            this.txtMinutes.Size = new System.Drawing.Size(30, 26);
            this.txtMinutes.TabIndex = 5;
            this.txtMinutes.Text = "5";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(18, 14);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 35);
            this.button1.TabIndex = 1;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblDebug
            // 
            this.lblDebug.AutoSize = true;
            this.lblDebug.Location = new System.Drawing.Point(598, 45);
            this.lblDebug.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDebug.Name = "lblDebug";
            this.lblDebug.Size = new System.Drawing.Size(54, 20);
            this.lblDebug.TabIndex = 7;
            this.lblDebug.Text = "debug";
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(22, 233);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(630, 320);
            this.txtStatus.TabIndex = 8;
            this.txtStatus.Text = "";
            // 
            // lblCurrentPrice
            // 
            this.lblCurrentPrice.AutoSize = true;
            this.lblCurrentPrice.Location = new System.Drawing.Point(18, 127);
            this.lblCurrentPrice.Name = "lblCurrentPrice";
            this.lblCurrentPrice.Size = new System.Drawing.Size(109, 20);
            this.lblCurrentPrice.TabIndex = 9;
            this.lblCurrentPrice.Text = "Current Price: ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 586);
            this.Controls.Add(this.lblCurrentPrice);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.lblDebug);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtMinutes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAlertThreshold);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMessage);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAlertThreshold;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMinutes;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblDebug;
        private System.Windows.Forms.RichTextBox txtStatus;
        private System.Windows.Forms.Label lblCurrentPrice;
    }
}

