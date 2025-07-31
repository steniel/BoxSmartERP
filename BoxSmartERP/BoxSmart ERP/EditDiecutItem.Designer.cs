namespace BoxSmart_ERP
{
    partial class EditDiecutItem
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
            webViewDiecuts = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)webViewDiecuts).BeginInit();
            SuspendLayout();
            // 
            // webViewDiecuts
            // 
            webViewDiecuts.AllowExternalDrop = true;
            webViewDiecuts.CreationProperties = null;
            webViewDiecuts.DefaultBackgroundColor = Color.White;
            webViewDiecuts.Dock = DockStyle.Fill;
            webViewDiecuts.Location = new Point(0, 0);
            webViewDiecuts.Name = "webViewDiecuts";
            webViewDiecuts.Size = new Size(719, 739);
            webViewDiecuts.TabIndex = 7;
            webViewDiecuts.ZoomFactor = 1D;
            webViewDiecuts.NavigationCompleted += webViewDiecuts_NavigationCompleted;
            // 
            // EditDiecutItem
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(719, 739);
            Controls.Add(webViewDiecuts);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            Name = "EditDiecutItem";
            StartPosition = FormStartPosition.CenterParent;
            Text = "EditDiecutItem";
            ((System.ComponentModel.ISupportInitialize)webViewDiecuts).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webViewDiecuts;
    }
}