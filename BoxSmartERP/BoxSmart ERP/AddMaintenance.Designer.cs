namespace BoxSmart_ERP
{
    partial class AddMaintenance
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
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            webView22 = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            ((System.ComponentModel.ISupportInitialize)webView22).BeginInit();
            SuspendLayout();
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Dock = DockStyle.Bottom;
            webView21.Location = new Point(0, 40);
            webView21.Name = "webView21";
            webView21.Size = new Size(714, 675);
            webView21.TabIndex = 1;
            webView21.ZoomFactor = 1D;
            webView21.NavigationCompleted += webView21_NavigationCompleted;
            // 
            // webView22
            // 
            webView22.AllowExternalDrop = true;
            webView22.CreationProperties = null;
            webView22.DefaultBackgroundColor = Color.White;
            webView22.Dock = DockStyle.Top;
            webView22.Location = new Point(0, 0);
            webView22.Name = "webView22";
            webView22.Size = new Size(714, 40);
            webView22.TabIndex = 2;
            webView22.ZoomFactor = 1D;
            // 
            // AddMaintenance
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(714, 715);
            Controls.Add(webView22);
            Controls.Add(webView21);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AddMaintenance";
            StartPosition = FormStartPosition.CenterParent;
            Text = "AddMaintenance";
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            ((System.ComponentModel.ISupportInitialize)webView22).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView22;
    }
}