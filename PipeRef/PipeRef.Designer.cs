namespace PipeRef
{
    partial class PipeRef
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            this.m_cbxCost = new System.Windows.Forms.ComboBox();
            this.m_pbCreate = new System.Windows.Forms.Button();
            this.m_pbAdd1 = new System.Windows.Forms.Button();
            this.m_pbAdd5 = new System.Windows.Forms.Button();
            this.m_tbLog = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.m_allowQueueAbort = new System.Windows.Forms.CheckBox();
            this.m_ebThreadCount = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(596, 181);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(141, 20);
            label1.TabIndex = 0;
            label1.Tag = "";
            label1.Text = "Listener Task Cost";
            // 
            // m_cbxCost
            // 
            this.m_cbxCost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cbxCost.FormattingEnabled = true;
            this.m_cbxCost.Items.AddRange(new object[] {
            "None",
            "1 μs",
            "5 μs",
            "10 μs",
            "100 μs",
            "1 ms",
            "5 ms",
            "10 ms",
            "100 ms",
            "1 s",
            "5 s",
            "10 s"});
            this.m_cbxCost.Location = new System.Drawing.Point(600, 206);
            this.m_cbxCost.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.m_cbxCost.Name = "m_cbxCost";
            this.m_cbxCost.Size = new System.Drawing.Size(180, 28);
            this.m_cbxCost.TabIndex = 1;
            this.m_cbxCost.SelectedIndexChanged += new System.EventHandler(this.DoChangeWorkCost);
            // 
            // m_pbCreate
            // 
            this.m_pbCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pbCreate.Location = new System.Drawing.Point(600, 83);
            this.m_pbCreate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.m_pbCreate.Name = "m_pbCreate";
            this.m_pbCreate.Size = new System.Drawing.Size(175, 35);
            this.m_pbCreate.TabIndex = 2;
            this.m_pbCreate.Text = "Create Pipeline";
            this.m_pbCreate.UseVisualStyleBackColor = true;
            this.m_pbCreate.Click += new System.EventHandler(this.DoCreatePipeline);
            // 
            // m_pbAdd1
            // 
            this.m_pbAdd1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pbAdd1.Location = new System.Drawing.Point(600, 286);
            this.m_pbAdd1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.m_pbAdd1.Name = "m_pbAdd1";
            this.m_pbAdd1.Size = new System.Drawing.Size(175, 35);
            this.m_pbAdd1.TabIndex = 3;
            this.m_pbAdd1.Text = "Add 1 Item";
            this.m_pbAdd1.UseVisualStyleBackColor = true;
            this.m_pbAdd1.Click += new System.EventHandler(this.DoAdd1);
            // 
            // m_pbAdd5
            // 
            this.m_pbAdd5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pbAdd5.Location = new System.Drawing.Point(600, 331);
            this.m_pbAdd5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.m_pbAdd5.Name = "m_pbAdd5";
            this.m_pbAdd5.Size = new System.Drawing.Size(175, 35);
            this.m_pbAdd5.TabIndex = 4;
            this.m_pbAdd5.Text = "Add 5 Items";
            this.m_pbAdd5.UseVisualStyleBackColor = true;
            this.m_pbAdd5.Click += new System.EventHandler(this.DoAdd5);
            // 
            // m_tbLog
            // 
            this.m_tbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tbLog.Location = new System.Drawing.Point(22, 20);
            this.m_tbLog.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.m_tbLog.Multiline = true;
            this.m_tbLog.Name = "m_tbLog";
            this.m_tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.m_tbLog.Size = new System.Drawing.Size(566, 475);
            this.m_tbLog.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(600, 460);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(175, 35);
            this.button1.TabIndex = 6;
            this.button1.Text = "Terminate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.DoTerminatePipeline);
            // 
            // m_allowQueueAbort
            // 
            this.m_allowQueueAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_allowQueueAbort.AutoSize = true;
            this.m_allowQueueAbort.Location = new System.Drawing.Point(600, 242);
            this.m_allowQueueAbort.Name = "m_allowQueueAbort";
            this.m_allowQueueAbort.Size = new System.Drawing.Size(162, 24);
            this.m_allowQueueAbort.TabIndex = 7;
            this.m_allowQueueAbort.Text = "Allow queue abort";
            this.m_allowQueueAbort.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(596, 23);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(176, 20);
            label2.TabIndex = 8;
            label2.Tag = "";
            label2.Text = "Consumer thread count";
            // 
            // m_ebThreadCount
            // 
            this.m_ebThreadCount.Location = new System.Drawing.Point(600, 46);
            this.m_ebThreadCount.Name = "m_ebThreadCount";
            this.m_ebThreadCount.Size = new System.Drawing.Size(172, 26);
            this.m_ebThreadCount.TabIndex = 9;
            this.m_ebThreadCount.Text = "1";
            this.m_ebThreadCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // PipeRef
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 515);
            this.Controls.Add(this.m_ebThreadCount);
            this.Controls.Add(label2);
            this.Controls.Add(this.m_allowQueueAbort);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.m_tbLog);
            this.Controls.Add(this.m_pbAdd5);
            this.Controls.Add(this.m_pbAdd1);
            this.Controls.Add(this.m_pbCreate);
            this.Controls.Add(this.m_cbxCost);
            this.Controls.Add(label1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PipeRef";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox m_cbxCost;
        private System.Windows.Forms.Button m_pbCreate;
        private System.Windows.Forms.Button m_pbAdd1;
        private System.Windows.Forms.Button m_pbAdd5;
        private System.Windows.Forms.TextBox m_tbLog;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox m_allowQueueAbort;
        private System.Windows.Forms.TextBox m_ebThreadCount;
    }
}

