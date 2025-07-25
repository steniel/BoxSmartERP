namespace BoxSmart_ERP
{
    partial class NewRequest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewRequest));
            webViewRequests = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)webViewRequests).BeginInit();
            SuspendLayout();
            // 
            // webViewRequests
            // 
            webViewRequests.AllowExternalDrop = true;
            webViewRequests.CreationProperties = null;
            webViewRequests.DefaultBackgroundColor = Color.White;
            webViewRequests.Dock = DockStyle.Fill;
            webViewRequests.Location = new Point(0, 0);
            webViewRequests.Name = "webViewRequests";
            webViewRequests.Size = new Size(1205, 752);
            webViewRequests.TabIndex = 0;
            webViewRequests.ZoomFactor = 1D;
            webViewRequests.NavigationCompleted += webViewRequests_NavigationCompleted;
            // 
            // NewRequest
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(1205, 752);
            Controls.Add(webViewRequests);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "NewRequest";
            StartPosition = FormStartPosition.CenterParent;
            Text = "NewRequest";
            Shown += NewRequest_Shown;
            ((System.ComponentModel.ISupportInitialize)webViewRequests).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webViewRequests;
    }
}