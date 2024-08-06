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
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            this.m_cbxCost = new System.Windows.Forms.ComboBox();
            this.m_pbCreate = new System.Windows.Forms.Button();
            this.m_pbAdd1 = new System.Windows.Forms.Button();
            this.m_pbAdd5 = new System.Windows.Forms.Button();
            this.m_tbLog = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.m_allowQueueAbort = new System.Windows.Forms.CheckBox();
            this.m_ebThreadCount = new System.Windows.Forms.TextBox();
            this.m_workCookie = new System.Windows.Forms.ComboBox();
            this.m_pbAccelerate = new System.Windows.Forms.Button();
            this.m_ebMaxBatchSize = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(792, 303);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(141, 20);
            label1.TabIndex = 0;
            label1.Tag = "";
            label1.Text = "Listener Task Cost";
            // 
            // label2
            // 
            label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(792, 23);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(176, 20);
            label2.TabIndex = 8;
            label2.Tag = "";
            label2.Text = "Consumer thread count";
            // 
            // label3
            // 
            label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(792, 405);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(99, 20);
            label3.TabIndex = 10;
            label3.Tag = "";
            label3.Text = "Work Cookie";
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
            this.m_cbxCost.Location = new System.Drawing.Point(796, 328);
            this.m_cbxCost.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.m_cbxCost.Name = "m_cbxCost";
            this.m_cbxCost.Size = new System.Drawing.Size(180, 28);
            this.m_cbxCost.TabIndex = 1;
            this.m_cbxCost.SelectedIndexChanged += new System.EventHandler(this.DoChangeWorkCost);
            // 
            // m_pbCreate
            // 
            this.m_pbCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pbCreate.Location = new System.Drawing.Point(796, 182);
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
            this.m_pbAdd1.Location = new System.Drawing.Point(796, 512);
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
            this.m_pbAdd5.Location = new System.Drawing.Point(796, 557);
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
            this.m_tbLog.Size = new System.Drawing.Size(762, 744);
            this.m_tbLog.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(796, 729);
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
            this.m_allowQueueAbort.Location = new System.Drawing.Point(796, 364);
            this.m_allowQueueAbort.Name = "m_allowQueueAbort";
            this.m_allowQueueAbort.Size = new System.Drawing.Size(162, 24);
            this.m_allowQueueAbort.TabIndex = 7;
            this.m_allowQueueAbort.Text = "Allow queue abort";
            this.m_allowQueueAbort.UseVisualStyleBackColor = true;
            // 
            // m_ebThreadCount
            // 
            this.m_ebThreadCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ebThreadCount.Location = new System.Drawing.Point(796, 46);
            this.m_ebThreadCount.Name = "m_ebThreadCount";
            this.m_ebThreadCount.Size = new System.Drawing.Size(172, 26);
            this.m_ebThreadCount.TabIndex = 9;
            this.m_ebThreadCount.Text = "1";
            this.m_ebThreadCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // m_workCookie
            // 
            this.m_workCookie.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_workCookie.FormattingEnabled = true;
            this.m_workCookie.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.m_workCookie.Location = new System.Drawing.Point(796, 430);
            this.m_workCookie.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.m_workCookie.Name = "m_workCookie";
            this.m_workCookie.Size = new System.Drawing.Size(180, 28);
            this.m_workCookie.TabIndex = 11;
            this.m_workCookie.Text = "1";
            // 
            // m_pbAccelerate
            // 
            this.m_pbAccelerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pbAccelerate.Location = new System.Drawing.Point(796, 620);
            this.m_pbAccelerate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.m_pbAccelerate.Name = "m_pbAccelerate";
            this.m_pbAccelerate.Size = new System.Drawing.Size(175, 35);
            this.m_pbAccelerate.TabIndex = 12;
            this.m_pbAccelerate.Text = "Accelerate Cookie";
            this.m_pbAccelerate.UseVisualStyleBackColor = true;
            this.m_pbAccelerate.Click += new System.EventHandler(this.DoAccelerate);
            // 
            // m_ebMaxBatchSize
            // 
            this.m_ebMaxBatchSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ebMaxBatchSize.Location = new System.Drawing.Point(799, 108);
            this.m_ebMaxBatchSize.Name = "m_ebMaxBatchSize";
            this.m_ebMaxBatchSize.Size = new System.Drawing.Size(172, 26);
            this.m_ebMaxBatchSize.TabIndex = 14;
            this.m_ebMaxBatchSize.Text = "0";
            this.m_ebMaxBatchSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(795, 85);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(114, 20);
            label4.TabIndex = 13;
            label4.Tag = "";
            label4.Text = "Max batch size";
            // 
            // PipeRef
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 784);
            this.Controls.Add(this.m_ebMaxBatchSize);
            this.Controls.Add(label4);
            this.Controls.Add(this.m_pbAccelerate);
            this.Controls.Add(this.m_workCookie);
            this.Controls.Add(label3);
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
        private System.Windows.Forms.ComboBox m_workCookie;
        private System.Windows.Forms.Button m_pbAccelerate;
        private System.Windows.Forms.TextBox m_ebMaxBatchSize;
    }
}

