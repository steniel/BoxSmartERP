namespace BoxSmart_ERP
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            splitContainer1 = new SplitContainer();
            webViewSideBar = new Microsoft.Web.WebView2.WinForms.WebView2();
            webViewApps = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webViewSideBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)webViewApps).BeginInit();
            SuspendLayout();
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.BackColor = SystemColors.Control;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.DimGray;
            webView21.Dock = DockStyle.Top;
            webView21.Location = new Point(0, 0);
            webView21.Name = "webView21";
            webView21.Size = new Size(1294, 20);
            webView21.TabIndex = 0;
            webView21.Visible = false;
            webView21.ZoomFactor = 1D;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.BackColor = Color.FromArgb(64, 64, 64);
            splitContainer1.BorderStyle = BorderStyle.FixedSingle;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(webViewSideBar);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(webViewApps);
            splitContainer1.Size = new Size(1294, 762);
            splitContainer1.SplitterDistance = 189;
            splitContainer1.TabIndex = 1;
            // 
            // webViewSideBar
            // 
            webViewSideBar.AllowExternalDrop = true;
            webViewSideBar.CreationProperties = null;
            webViewSideBar.DefaultBackgroundColor = Color.White;
            webViewSideBar.Dock = DockStyle.Fill;
            webViewSideBar.Location = new Point(0, 0);
            webViewSideBar.Name = "webViewSideBar";
            webViewSideBar.Size = new Size(187, 760);
            webViewSideBar.TabIndex = 0;
            webViewSideBar.ZoomFactor = 1D;
            webViewSideBar.NavigationCompleted += webViewSideBar_NavigationCompleted;
            // 
            // webViewApps
            // 
            webViewApps.AllowExternalDrop = true;
            webViewApps.CreationProperties = null;
            webViewApps.DefaultBackgroundColor = Color.White;
            webViewApps.Dock = DockStyle.Fill;
            webViewApps.Location = new Point(0, 0);
            webViewApps.Name = "webViewApps";
            webViewApps.Size = new Size(1099, 760);
            webViewApps.TabIndex = 0;
            webViewApps.ZoomFactor = 1D;
            webViewApps.CoreWebView2InitializationCompleted += webViewApps_CoreWebView2InitializationCompleted;
            webViewApps.NavigationCompleted += webViewApps_NavigationCompleted_1;
            webViewApps.WebMessageReceived += webViewApps_WebMessageReceived;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1294, 762);
            Controls.Add(splitContainer1);
            Controls.Add(webView21);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            Shown += MainForm_Shown;
            MouseDoubleClick += MainForm_MouseDoubleClick;
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)webViewSideBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)webViewApps).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private SplitContainer splitContainer1;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewSideBar;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewApps;
    }
}
