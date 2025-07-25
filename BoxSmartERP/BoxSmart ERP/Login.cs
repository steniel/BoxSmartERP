using BoxSmart_ERP.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Web.WebView2.Core;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BoxSmart_ERP.Services.PostgreSQLServices;

namespace BoxSmart_ERP
{
    public partial class Login : Form
    {        

        private readonly IConfiguration _config;
        public Login(IConfiguration config)
        {
            InitializeComponent();
            this.DoubleBuffered = true;            
            _config = config;
            InitializeWebControl();

        }    

        private async void InitializeWebControl()
        {
            // Initialize main login WebView2
            await webView21.EnsureCoreWebView2Async(null);
            webView21.CoreWebView2.WebMessageReceived += WebView21_WebMessageReceived;
            string htmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/Login.html");
            webView21.Source = new Uri(htmlFilePath);

            // Initialize close button WebView2
            await webView22.EnsureCoreWebView2Async(null);
            webView22.CoreWebView2.WebMessageReceived += WebView22_WebMessageReceived;
            string closeHTMLPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/LoginClose.html");
            webView22.Source = new Uri(closeHTMLPath);
        }

        private int _loginAttemptCounter = 0;
        private bool _isProcessingLogin = false;
        private string systemUserFullname;
        private int systemUserId = 0; // Initialize user ID
        private void WebView21_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            // Prevent concurrent execution
            if (_isProcessingLogin) return;
            _isProcessingLogin = true;

            // Show loading state immediately
            this.Invoke((MethodInvoker)delegate
            {
                webView21.CoreWebView2.PostWebMessageAsString(
                    JsonSerializer.Serialize(new { loading = true }));
            });


            try
            {
                var message = JsonSerializer.Deserialize<WebViewMessage>(e.TryGetWebMessageAsString());

                if (message?.MessageType == "login")
                {
                    var credentials = JsonSerializer.Deserialize<LoginCredentials>(message.Data.GetRawText());
                    // In AuthenticateUser method
                    if (string.IsNullOrEmpty(credentials.Password) || credentials.Password.Length < 8)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            webView21.CoreWebView2.PostWebMessageAsString(
                                JsonSerializer.Serialize(new
                                {
                                    error = "Password must be at least 8 characters"
                                }));
                        });
                        return;
                    }
                    _loginAttemptCounter++;
                    Config.PostgreSQLUsername = credentials.Username; // Store username in Config
                    SecureString securePassword = new SecureString();
                    foreach (char c in credentials.Password)
                    {
                        securePassword.AppendChar(c);
                    }
                    securePassword.MakeReadOnly();
                    Config.PostgreSQLPassword = securePassword;

                    bool isValid = AuthenticateUser(credentials.Username, credentials.Password);

                    if (isValid)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            AppSession.StartSession(credentials.Username, systemUserFullname, systemUserId);                            

                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        });
                        return;
                    }

                    // Handle failed login
                    if (_loginAttemptCounter >= 5)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            webView21.CoreWebView2.PostWebMessageAsString(
                                JsonSerializer.Serialize(new { lockout = true })); // Fix: Added missing ','

                            MessageBox.Show("Too many failed login attempts. Please try again later.",
                                          "Login Failed",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Error);
                            this.DialogResult = DialogResult.Cancel;
                            this.Close();
                        });
                        // For invalid credentials
                        webView21.CoreWebView2.PostWebMessageAsString(
                            JsonSerializer.Serialize(new
                            {
                                error = "Invalid username or password. Please try again."
                            }));
                        return;
                    }

                    // Regular failed attempt
                    this.Invoke((MethodInvoker)delegate
                    {
                        webView21.CoreWebView2.PostWebMessageAsString(
                            JsonSerializer.Serialize(new
                            {
                                error = $"Invalid credentials ({_loginAttemptCounter}/5 attempts)",
                                attempts = _loginAttemptCounter
                            }));
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Login Error: {ex.Message}");
                this.Invoke((MethodInvoker)delegate
                {
                    webView21.CoreWebView2.PostWebMessageAsString(
                        JsonSerializer.Serialize(new
                        {
                            error = "System error. Please try again.",
                            details = ex.Message
                        }));
                });
            }
            finally
            {
                _isProcessingLogin = false;
            }
        }

        private void WebView22_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {

            try
            {
                string message = e.TryGetWebMessageAsString();
                if (message == "btCloseClicked")
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                    });
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Close Button Error: {ex.Message}");
                // Optionally show error to user if close operation fails
                this.Invoke((MethodInvoker)delegate
                {
                    MessageBox.Show("Failed to close the window. Please try again.",
                                  "Error",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error);
                });
            }

        }
        // Helper classes for message parsing
        public class WebViewMessage
        {
            [JsonPropertyName("messageType")]
            public string MessageType { get; set; }

            [JsonPropertyName("data")]
            public JsonElement Data { get; set; } // Flexible data container
        }

        public class LoginCredentials
        {
            [JsonPropertyName("username")]
            public string Username { get; set; }

            [JsonPropertyName("password")]
            public string Password { get; set; }

            [JsonPropertyName("rememberMe")]
            public bool RememberMe { get; set; }
        }


        // Your authentication method
        private string pgsqlHost;
        private string pgsqlPort;
        private string pgsqlDatabase;
        private string pgsqlErrorDetails;
        private bool AuthenticateUser(string username, string password)
        {
            PostgreSQLServices _dbService;
            pgsqlHost = _config["ConnectionStrings:PostgreSQLHost"];
            pgsqlPort = _config["ConnectionStrings:PostgreSQLPort"];
            pgsqlDatabase = _config["ConnectionStrings:PostgreSQLDatabase"];
            pgsqlErrorDetails = _config["ConnectionStrings:ErrorDetails"];

            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(Config.PostgreSQLPassword);
            string plainTextPassword = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            Config.PostgreSQLConnection = $"Host={pgsqlHost};Port={pgsqlPort};Username={Config.PostgreSQLUsername};Password={plainTextPassword};Database={pgsqlDatabase};Include Error Detail={pgsqlErrorDetails}";
            string connectionString = Config.PostgreSQLConnection;
            _dbService = new PostgreSQLServices(connectionString);

            bool isValid = false;
      
            _dbService = new PostgreSQLServices(connectionString);

            UserFullname userFullname = _dbService.GetSystemUserName(username);
            systemUserFullname = userFullname.Fullname; // Store the fullname for session
            systemUserId = userFullname.UserId; // Store the user ID for session
            if (userFullname.Fullname != null) //User exists
            {
                // Check if the password matches
                string UserCredentialValid = _dbService.UserInputPassword(username, password);
                string UserStoredPassword = _dbService.GetUserPassword(username);
                if (UserCredentialValid == UserStoredPassword)
                {
                    isValid = true;
                }
            }
            return isValid; // Return true if valid, false otherwise
        }

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
           
        }

        private void webView22_MouseDown(object sender, MouseEventArgs e)
        {
           
        }
    }
}
