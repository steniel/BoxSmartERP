using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoxSmart_ERP
{
    public partial class ViewReport : Form
    {
        private readonly string _pdfUrl;
        private readonly WebView2 webViewReport;
        private string tempFilePath = string.Empty;

        public ViewReport(string pdfUrl)
        {
            InitializeComponent();
            _pdfUrl = pdfUrl;

            // Initialize WebView2 control
            webViewReport = new WebView2
            {
                Dock = DockStyle.Fill
            };
            this.Controls.Add(webViewReport);

            // Set form properties
            this.Text = "View Report";
            this.Size = new System.Drawing.Size(800, 600);

            // Load PDF when form is shown
            this.Shown += async (s, e) => await LoadPdfAsync();
        }

        private async Task LoadPdfAsync()
        {
            try
            {
                // Download PDF to a temporary file
                tempFilePath = Path.Combine(Path.GetTempPath(), $"report_{Guid.NewGuid()}.pdf");
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(_pdfUrl);
                    response.EnsureSuccessStatusCode();
                    var pdfBytes = await response.Content.ReadAsByteArrayAsync();
                    await File.WriteAllBytesAsync(tempFilePath, pdfBytes);
                }

                // Ensure WebView2 is initialized
                await webViewReport.EnsureCoreWebView2Async(null);

                // Navigate to the PDF file
                webViewReport.CoreWebView2.Navigate($"file:///{tempFilePath.Replace('\\', '/')}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading PDF: {ex.Message}", "Error");
            }
        }
        // Clean up temporary file when form closes
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            // Optionally, delete the temporary PDF file
            if (File.Exists(tempFilePath)) File.Delete(tempFilePath);
        }
    }
}
