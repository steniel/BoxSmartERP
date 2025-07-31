namespace BoxSmart_ERP
{
    partial class EditRecentRequest
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
            webViewRequests = new Microsoft.Web.WebView2.WinForms.WebView2();
            webViewTitlebar = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)webViewRequests).BeginInit();
            ((System.ComponentModel.ISupportInitialize)webViewTitlebar).BeginInit();
            SuspendLayout();
            // 
            // webViewRequests
            // 
            webViewRequests.AllowExternalDrop = true;
            webViewRequests.CreationProperties = null;
            webViewRequests.DefaultBackgroundColor = Color.White;
            webViewRequests.Dock = DockStyle.Bottom;
            webViewRequests.Location = new Point(0, 39);
            webViewRequests.Name = "webViewRequests";
            webViewRequests.Size = new Size(1010, 663);
            webViewRequests.TabIndex = 5;
            webViewRequests.ZoomFactor = 1D;
            webViewRequests.NavigationCompleted += webViewRequests_NavigationCompleted;
            // 
            // webViewTitlebar
            // 
            webViewTitlebar.AllowExternalDrop = true;
            webViewTitlebar.CreationProperties = null;
            webViewTitlebar.DefaultBackgroundColor = Color.White;
            webViewTitlebar.Dock = DockStyle.Top;
            webViewTitlebar.Location = new Point(0, 0);
            webViewTitlebar.Name = "webViewTitlebar";
            webViewTitlebar.Size = new Size(1010, 40);
            webViewTitlebar.TabIndex = 4;
            webViewTitlebar.ZoomFactor = 1D;
            // 
            // EditRecentRequest
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1010, 702);
            Controls.Add(webViewRequests);
            Controls.Add(webViewTitlebar);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            Name = "EditRecentRequest";
            StartPosition = FormStartPosition.CenterParent;
            Text = "EditRecentRequest";
            ((System.ComponentModel.ISupportInitialize)webViewRequests).EndInit();
            ((System.ComponentModel.ISupportInitialize)webViewTitlebar).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webViewRequests;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewTitlebar;
    }
}