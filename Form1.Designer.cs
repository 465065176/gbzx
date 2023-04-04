namespace 贵州省干部在线学习助手
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cefWebBrowser1 = new Xilium.CefGlue.WindowsForms.CefWebBrowser();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cefWebBrowser1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.comboBox1);
            this.splitContainer1.Panel2.Controls.Add(this.button2);
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Size = new System.Drawing.Size(1810, 928);
            this.splitContainer1.SplitterDistance = 1535;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 0;
            // 
            // cefWebBrowser1
            // 
            this.cefWebBrowser1.BrowserSettings = null;
            this.cefWebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cefWebBrowser1.Location = new System.Drawing.Point(0, 0);
            this.cefWebBrowser1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cefWebBrowser1.Name = "cefWebBrowser1";
            this.cefWebBrowser1.Size = new System.Drawing.Size(1535, 928);
            this.cefWebBrowser1.StartUrl = "http://222.178.69.178:8118/jbzf";
            this.cefWebBrowser1.TabIndex = 1;
            this.cefWebBrowser1.Text = "cefWebBrowser1";
            this.cefWebBrowser1.BrowserCreated += new System.EventHandler(this.cefWebBrowser1_BrowserCreated);
            this.cefWebBrowser1.BeforePopup += new System.EventHandler<Xilium.CefGlue.WindowsForms.BeforePopupEventArgs>(this.cefWebBrowser1_BeforePopup);
            this.cefWebBrowser1.Click += new System.EventHandler(this.cefWebBrowser1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "http://2020ynxd.yanxiuonline.com",
            "http://www.rufa.gov.cn",
            "http://edu.xjcxedu.com/desktop-web/login.action",
            "https://qdjj.59iedu.com",
            "http://wlcb.zgrsw.cn",
            "20qhxkb.study.teacheredu.cn",
            "http://www.ejxjy.com/",
            "http://guizhou.zxjxjy.com/",
            "https://hn.webtrn.cn/",
            "http://www.hnsydwpx.cn/",
            "https://gzwy.gov.cn/",
            "https://hn.ischinese.cn/",
            "https://newnhc-p.webtrn.cn/cms/",
            "http://61.128.194.102:8118/jbzf/",
            "https://mchtraweb.chinawch.org.cn/",
            "https://gz-smch-home.chengdumaixun.com/#/me/index"});
            this.comboBox1.Location = new System.Drawing.Point(18, 50);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(220, 32);
            this.comboBox1.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(48, 269);
            this.button2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 46);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(48, 120);
            this.button1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 46);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1810, 928);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private Xilium.CefGlue.WindowsForms.CefWebBrowser cefWebBrowser1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

