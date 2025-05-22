Public NotInheritable Class AboutTSDInventory

    Private Sub AboutTSDInventory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Set the title of the form.
        Me.Text = String.Format("About {0}", ApplicationTitle)
        ' Initialize all of the text displayed on the About Box.
        ' TODO: Customize the application's assembly information in the "Application" pane of the project 
        '    properties dialog (under the "Project" menu).
        Me.LabelProductName.Text = My.Application.Info.ProductName
        Me.LabelVersion.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
        Me.LabelCopyright.Text = My.Application.Info.Copyright
        Me.LabelCompanyName.Text = My.Application.Info.CompanyName
        Me.TextBoxDescription.Text = ApplicationTitle & Chr(13) & Chr(10) & "Version #: " & Application.ProductVersion
        Me.TextBoxDescription.Text = Me.TextBoxDescription.Text & Chr(13) & Chr(13) & Chr(10) & "Free Software Components: " & Chr(13) & Chr(10) & _
                                     "PostgreSQL Open Source Database" & Chr(13) & Chr(10) & _
                                     "PostgreSQL ODBC Driver for Windows"
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub

End Class
