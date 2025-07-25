namespace BoxSmart_ERP
{
    partial class MoreActions
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
            webViewTitlebar = new Microsoft.Web.WebView2.WinForms.WebView2();
            webViewRequests = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)webViewTitlebar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)webViewRequests).BeginInit();
            SuspendLayout();
            // 
            // webViewTitlebar
            // 
            webViewTitlebar.AllowExternalDrop = true;
            webViewTitlebar.CreationProperties = null;
            webViewTitlebar.DefaultBackgroundColor = Color.White;
            webViewTitlebar.Dock = DockStyle.Top;
            webViewTitlebar.Location = new Point(0, 0);
            webViewTitlebar.Name = "webViewTitlebar";
            webViewTitlebar.Size = new Size(1205, 40);
            webViewTitlebar.TabIndex = 2;
            webViewTitlebar.ZoomFactor = 1D;
            webViewTitlebar.NavigationCompleted += webViewTitlebar_NavigationCompleted;
            // 
            // webViewRequests
            // 
            webViewRequests.AllowExternalDrop = true;
            webViewRequests.CreationProperties = null;
            webViewRequests.DefaultBackgroundColor = Color.White;
            webViewRequests.Dock = DockStyle.Bottom;
            webViewRequests.Location = new Point(0, 37);
            webViewRequests.Name = "webViewRequests";
            webViewRequests.Size = new Size(1205, 662);
            webViewRequests.TabIndex = 3;
            webViewRequests.ZoomFactor = 1D;
            webViewRequests.NavigationCompleted += webViewRequests_NavigationCompleted;
            webViewRequests.WebMessageReceived += webViewRequests_WebMessageReceived;
            // 
            // MoreActions
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1205, 699);
            Controls.Add(webViewRequests);
            Controls.Add(webViewTitlebar);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MoreActions";
            StartPosition = FormStartPosition.CenterParent;
            Text = "MoreActions";
            ((System.ComponentModel.ISupportInitialize)webViewTitlebar).EndInit();
            ((System.ComponentModel.ISupportInitialize)webViewRequests).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webViewTitlebar;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewRequests;
    }
}