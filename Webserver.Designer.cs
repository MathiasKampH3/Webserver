using System;
using System.ComponentModel;

namespace Webserver
{
    partial class Webserver
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxMaxConnections = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxContent = new System.Windows.Forms.TextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.textBoxIpaddress = new System.Windows.Forms.TextBox();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(410, 208);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 19;
            this.label4.Text = "Max Connections";
            // 
            // textBoxMaxConnections
            // 
            this.textBoxMaxConnections.Location = new System.Drawing.Point(410, 234);
            this.textBoxMaxConnections.Name = "textBoxMaxConnections";
            this.textBoxMaxConnections.Size = new System.Drawing.Size(292, 20);
            this.textBoxMaxConnections.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(99, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 17;
            this.label3.Text = "Content";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(410, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 16;
            this.label2.Text = "Port eg. http(80)";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(99, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 23);
            this.label1.TabIndex = 15;
            this.label1.Text = "Ipaddress eg.(192.168.1.1)";
            // 
            // textBoxContent
            // 
            this.textBoxContent.Location = new System.Drawing.Point(99, 234);
            this.textBoxContent.Name = "textBoxContent";
            this.textBoxContent.Size = new System.Drawing.Size(289, 20);
            this.textBoxContent.TabIndex = 14;
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(410, 175);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(292, 20);
            this.textBoxPort.TabIndex = 13;
            // 
            // textBoxIpaddress
            // 
            this.textBoxIpaddress.Location = new System.Drawing.Point(99, 175);
            this.textBoxIpaddress.Name = "textBoxIpaddress";
            this.textBoxIpaddress.Size = new System.Drawing.Size(292, 20);
            this.textBoxIpaddress.TabIndex = 12;
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(289, 278);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(99, 23);
            this.buttonStop.TabIndex = 10;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(99, 278);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(99, 23);
            this.buttonStart.TabIndex = 10;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // Webserver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxMaxConnections);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxContent);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.textBoxIpaddress);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Name = "Webserver";
            this.Text = "Webserver";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxMaxConnections;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxContent;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.TextBox textBoxIpaddress;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonStart;

        #endregion
    }
}