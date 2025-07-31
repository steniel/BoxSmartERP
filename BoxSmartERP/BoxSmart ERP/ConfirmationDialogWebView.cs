using Microsoft.Web.WebView2.Core;
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
    public partial class ConfirmationDialogWebView : UserControl
    {
        // Event to communicate the dialog result back to the parent form
        public event EventHandler<DialogResultEventArgs> DialogResultConfirmed;
        public ConfirmationDialogWebView()
        {
            InitializeComponent();
            this.Load += ConfirmationDialogWebView_Load;
        }
        public class DialogResultEventArgs : EventArgs
        {
            public bool Confirmed { get; }
            public string Message { get; }

            public DialogResultEventArgs(bool confirmed, string message = "")
            {
                Confirmed = confirmed;
                Message = message;
            }
        }

        private string _dialogMessage = "Are you sure you want to proceed?";
        private string _confirmationPhrase = "Yes, I want to dispose this diecut.";

        // Public method to set the dialog message and confirmation phrase
        public void SetDialogContent(string message, string confirmationPhrase)
        {
            _dialogMessage = message;
            _confirmationPhrase = confirmationPhrase;

            // If WebView2 is already initialized, update the content
            if (webView2Control.CoreWebView2 != null)
            {
                UpdateWebViewContent();
            }
        }

        private async void ConfirmationDialogWebView_Load(object sender, EventArgs e)
        {
            await InitializeWebView2();
        }

        private async Task InitializeWebView2()
        {
            // Ensure the environment is created before navigating or setting HTML
            if (webView2Control.CoreWebView2 == null)
            {
                // Create a WebView2Environment to specify user data folder
                // This is important for deployment and avoiding conflicts
                var env = await CoreWebView2Environment.CreateAsync(
                    userDataFolder: Path.Combine(Path.GetTempPath(), "YourAppWebView2Data"));
                await webView2Control.EnsureCoreWebView2Async(env);
            }

            // Set up event handler for messages from JavaScript
            webView2Control.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;

            // Load the HTML content
            UpdateWebViewContent();
        }

        private void UpdateWebViewContent()
        {
            string htmlContent = GenerateHtmlContent(_dialogMessage, _confirmationPhrase);
            webView2Control.CoreWebView2.NavigateToString(htmlContent);
        }

        private void CoreWebView2_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            string message = e.WebMessageAsJson;
            // Parse the JSON message from JavaScript
            // Example: {"action": "yes", "value": "..."} or {"action": "no"}

            // Simple parsing for this example, a JSON library would be better for complex messages
            if (message.Contains("\"action\":\"yes\""))
            {
                DialogResultConfirmed?.Invoke(this, new DialogResultEventArgs(true, _confirmationPhrase));
                // Optionally close the dialog or hide the user control
                this.ParentForm?.Close(); // Close the parent form if this is a dialog
            }
            else if (message.Contains("\"action\":\"no\"") || message.Contains("\"action\":\"close\""))
            {
                DialogResultConfirmed?.Invoke(this, new DialogResultEventArgs(false));
                this.ParentForm?.Close(); // Close the parent form
            }
        }

        // Method to generate the HTML content dynamically
        private string GenerateHtmlContent(string message, string confirmationPhrase)
        {
            // The HTML content is embedded as a string here.
            // In a larger application, you might load it from a resource file.
            return $@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Confirmation Dialog</title>
    <link href=""https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css"" rel=""stylesheet"">
    <link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.2/css/all.min.css"">
    <style>
        body {{
            background-color: #1a1a1a; /* Dark background */
            color: #e0e0e0; /* Light text color */
            font-family: 'Inter', sans-serif; /* Using Inter font */
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
            margin: 0;
            padding: 20px;
            box-sizing: border-box;
            overflow: hidden; /* Prevent scrollbars */
        }}

        .dialog-container {{
            background-color: #2c2c2c; /* Slightly lighter dark background for the container */
            border-radius: 12px;
            padding: 30px;
            box-shadow: 0 8px 30px rgba(0, 0, 0, 0.5);
            max-width: 500px; /* Compact width */
            width: 100%;
            position: relative;
            text-align: center;
            border: 1px solid #444; /* Subtle border */
        }}

        .close-button {{
            position: absolute;
            top: 15px;
            left: 15px;
            width: 24px; /* Slightly larger for better touch target */
            height: 24px;
            background-color: #ff5f56; /* Red for close */
            border-radius: 50%;
            display: flex;
            justify-content: center;
            align-items: center;
            font-size: 16px;
            color: #fff;
            cursor: pointer;
            transition: background-color 0.2s, transform 0.2s;
            z-index: 10;
            font-weight: bold;
        }}

        .close-button:hover {{
            background-color: #e04a40;
            transform: scale(1.05);
        }}

        h2 {{
            color: #f0f0f0;
            font-weight: 600;
            margin-bottom: 20px;
            font-size: 1.5em;
        }}

        p.message-text {{
            color: #b0b0b0;
            font-size: 1em;
            margin-bottom: 25px;
            line-height: 1.5;
        }}

        .form-group {{
            margin-bottom: 25px;
        }}

        .form-control {{
            background-color: #3a3a3a;
            border: 1px solid #555;
            color: #f0f0f0;
            padding: 10px 15px;
            border-radius: 8px;
            font-size: 0.95em;
            width: 100%;
            box-sizing: border-box;
            transition: border-color 0.2s, box-shadow 0.2s;
        }}

        .form-control:focus {{
            background-color: #4a4a4a;
            border-color: #007bff;
            box-shadow: 0 0 0 0.2rem rgba(0, 123, 255, 0.25);
            outline: none;
        }}

        .button-group {{
            display: flex;
            justify-content: center;
            gap: 15px;
            margin-top: 20px;
        }}

        .btn {{
            padding: 10px 25px;
            font-size: 1em;
            border-radius: 8px;
            transition: background-color 0.2s, border-color 0.2s, transform 0.2s, opacity 0.2s;
            font-weight: 500;
        }}

        .btn-primary {{
            background-color: #007bff;
            border-color: #007bff;
        }}

        .btn-primary:hover:not(:disabled) {{
            background-color: #0056b3;
            border-color: #004085;
            transform: translateY(-2px);
        }}

        .btn-primary:disabled {{
            background-color: #004085;
            border-color: #00356b;
            cursor: not-allowed;
            opacity: 0.6;
        }}

        .btn-secondary {{
            background-color: #6c757d;
            border-color: #6c757d;
        }}

        .btn-secondary:hover {{
            background-color: #5a6268;
            border-color: #545b62;
            transform: translateY(-2px);
        }}
    </style>
</head>
<body>
    <div class=""dialog-container"">
        <div class=""close-button"" id=""closeButton"" title=""Close"">×</div>
        <h2>Confirmation Required</h2>
        <p class=""message-text"">{message}</p>

        <div class=""form-group"">
            <input type=""text"" class=""form-control"" id=""confirmationInput"" placeholder=""Type '{confirmationPhrase}' to enable Yes button"">
        </div>

        <div class=""button-group"">
            <button type=""button"" class=""btn btn-primary"" id=""yesButton"" disabled>Yes</button>
            <button type=""button"" class=""btn btn-secondary"" id=""noButton"">No</button>
        </div>
    </div>

    <script>
        const confirmationInput = document.getElementById('confirmationInput');
        const yesButton = document.getElementById('yesButton');
        const noButton = document.getElementById('noButton');
        const closeButton = document.getElementById('closeButton');
        const requiredPhrase = '{confirmationPhrase}';

        // Function to send message to C# host
        function sendMessageToHost(action, value = '') {{
            if (window.chrome && window.chrome.webview) {{
                window.chrome.webview.postMessage(JSON.stringify({{ action: action, value: value }}));
            }} else {{
                console.warn('WebView2 host object not found. Running in browser context.');
                // For testing in a browser, you might use alerts or console logs
                alert(`Action: ${{action}}, Value: ${{value}}`);
            }}
        }}

        // Input field event listener for enabling/disabling Yes button
        confirmationInput.addEventListener('input', function() {{
            if (this.value === requiredPhrase) {{
                yesButton.disabled = false;
            }} else {{
                yesButton.disabled = true;
            }}
        }});

        // Button click listeners
        yesButton.addEventListener('click', function() {{
            if (!yesButton.disabled) {{
                sendMessageToHost('yes', confirmationInput.value);
            }}
        }});

        noButton.addEventListener('click', function() {{
            sendMessageToHost('no');
        }});

        closeButton.addEventListener('click', function() {{
            sendMessageToHost('close');
        }});
    </script>
</body>
</html>
";
        }

    }
}
