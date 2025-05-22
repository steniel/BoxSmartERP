Imports System.Runtime.InteropServices
Imports Npgsql

Public Class LoginTSD
    Dim LoginSuccessful As Boolean
    Public UserPermission As Integer = 0
    Public SystemUserID As Integer
    Private Declare Auto Function LogonUser Lib "advapi32.dll" (ByVal lpszUsername As [String], _
            ByVal lpszDomain As [String], ByVal lpszPassword As [String], _
            ByVal dwLogonType As Integer, ByVal dwLogonProvider As Integer, _
            ByRef phToken As IntPtr) As Boolean
    <DllImport("kernel32.dll")> _
    Public Shared Function FormatMessage(ByVal dwFlags As Integer, ByRef lpSource As IntPtr, _
        ByVal dwMessageId As Integer, ByVal dwLanguageId As Integer, ByRef lpBuffer As [String], _
        ByVal nSize As Integer, ByRef Arguments As IntPtr) As Integer
    End Function

    Public Declare Auto Function CloseHandle Lib "kernel32.dll" (ByVal handle As IntPtr) As Boolean
    Public Declare Auto Function DuplicateToken Lib "advapi32.dll" (ByVal ExistingTokenHandle As IntPtr, _
            ByVal SECURITY_IMPERSONATION_LEVEL As Integer, _
            ByRef DuplicateTokenHandle As IntPtr) As Boolean

    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See http://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Const logon32_provider_default As Integer = 0
        'this parameter causes logonuser to create a primary token.
        Const logon32_logon_interactive As Integer = 2

        Dim tokenHandle As New IntPtr(0)
        Dim UserValue, lpgszDomain, PasswordValue As String
        UserValue = Trim(LCase(UsernameTextBox.Text))
        lpgszDomain = Trim(DomainTextBox.Text)
        PasswordValue = Trim(PasswordTextBox.Text)

        LoginSuccessful = LogonUser(UserValue, lpgszDomain, PasswordValue, logon32_logon_interactive, logon32_provider_default, tokenHandle)
        If LoginSuccessful Then
            Dim objGetUserPermission As ClassPrintcard = New ClassPrintcard
            UserPermission = objGetUserPermission.GetUserInformation("role_id", UserValue)
            If UserPermission = 0 Then
                MessageBox.Show("User " & UserValue & " is not a registered system user!  Please contact MIS to add this user to the system.", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Hand)
                Return
            End If
            SystemUserID = objGetUserPermission.GetUserInformation("id", UserValue)
            objGetUserPermission = Nothing
            Me.Close()
        Else
            MessageBox.Show("Username or password is incorrect!", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Return
        End If
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        If LoginSuccessful = False Then
            MessageBox.Show("Press OK to close this app.", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Application.Exit()
        End If
        If UserPermission = 0 Then
            Application.Exit()
        End If
    End Sub

    Private Sub LoginTSD_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If LoginSuccessful = False Then
            Application.Exit()
        End If
    End Sub
    Private Sub LoginTSD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim objNameSpace
        objNameSpace = GetObject("WinNT:")
        Me.Text = ApplicationTitle
        For Each objDomain In objNameSpace
            DomainTextBox.Items.Add(objDomain.name)
            DomainTextBox.Text = objDomain.name
        Next

    End Sub
End Class
