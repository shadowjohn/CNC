namespace CNC_選刀
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.INPUT_PATH = new System.Windows.Forms.Label();
            this.GBox = new System.Windows.Forms.GroupBox();
            this.save_btn = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(427, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "步驟1：請將 CNC 的 nc 程式檔拖入此介面。";
            // 
            // INPUT_PATH
            // 
            this.INPUT_PATH.AutoSize = true;
            this.INPUT_PATH.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.INPUT_PATH.Location = new System.Drawing.Point(28, 61);
            this.INPUT_PATH.Name = "INPUT_PATH";
            this.INPUT_PATH.Size = new System.Drawing.Size(0, 24);
            this.INPUT_PATH.TabIndex = 1;
            // 
            // GBox
            // 
            this.GBox.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GBox.Location = new System.Drawing.Point(66, 96);
            this.GBox.Name = "GBox";
            this.GBox.Size = new System.Drawing.Size(441, 360);
            this.GBox.TabIndex = 2;
            this.GBox.TabStop = false;
            this.GBox.Text = "車刀順序編輯區";
            // 
            // save_btn
            // 
            this.save_btn.Location = new System.Drawing.Point(240, 462);
            this.save_btn.Name = "save_btn";
            this.save_btn.Size = new System.Drawing.Size(88, 26);
            this.save_btn.TabIndex = 3;
            this.save_btn.Text = "儲存";
            this.save_btn.UseVisualStyleBackColor = true;
            this.save_btn.Click += new System.EventHandler(this.save_btn_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 513);
            this.Controls.Add(this.save_btn);
            this.Controls.Add(this.GBox);
            this.Controls.Add(this.INPUT_PATH);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "CNC選刀程式";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label INPUT_PATH;
        private System.Windows.Forms.GroupBox GBox;
        private System.Windows.Forms.Button save_btn;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}

