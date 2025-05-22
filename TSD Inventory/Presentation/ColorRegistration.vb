Imports System.IO
Imports System.Xml
Imports System.Drawing

Public Class ColorRegistration
    Dim Colorprops() As Reflection.PropertyInfo
    Private Sub ColorRegistration_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainUI
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ColorDialog1.ShowDialog()
        Button1.ForeColor = ColorDialog1.Color
        Dim myColor As Color = ColorDialog1.Color
        Dim myARGBValue As Integer = myColor.ToArgb
        Dim myOtherColor As Color = Color.FromArgb(myARGBValue)
        Label1.Text = myARGBValue
        Label2.ForeColor = myOtherColor

        '  To get hold of the  Members of the Color Structure
        '  the GetProperties method returns an array of properties in the Color structure
        '  and we store them in the ColorProps array
        Colorprops = GetType(System.Drawing.Color).GetProperties

        '  For demo purposes, display the contents of the array in a listbox
        '  We use the Name property to return the string assigned to each property in turn
        For x As Integer = 0 To Colorprops.Length - 1
            ListBox1.Items.Add((Colorprops(x).Name))
        Next


    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim m_xmlr As XmlTextReader
        'Create the XML Reader
        m_xmlr = New XmlTextReader("Gray100.xml")
        'Disable whitespace so that you don't have to read over whitespaces
        m_xmlr.WhitespaceHandling = WhitespaceHandling.None
        'read the xml declaration and advance to palette resid tag
        m_xmlr.Read()
        'read palette resid
        m_xmlr.Read()
        'read colors tag
        m_xmlr.Read()
        'read page tag
        m_xmlr.Read()
        'Load the Loop
        While Not m_xmlr.EOF
            'Go to the color name tag
            m_xmlr.Read()
            'if not start element exit while loop
            If Not m_xmlr.IsStartElement() Then
                Exit While
            End If
            'Get the name Attribute Value
            Dim GrayValue = m_xmlr.GetAttribute("name")
            Dim cs = m_xmlr.GetAttribute("cs")
            Dim tints = m_xmlr.GetAttribute("tints")

            GrayValue = m_xmlr.GetAttribute("name")
            cs = m_xmlr.GetAttribute("cs")
            tints = m_xmlr.GetAttribute("tints")
            'Write Result to the listview control
            ListView1.Items.Add("Color: " & GrayValue & " Name: " & cs & " Tints: " & tints)




        End While
        'close the reader
        m_xmlr.Close()
    End Sub



    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged

        BackColor = Color.FromName(ListBox1.SelectedItem.ToString)
    End Sub
End Class