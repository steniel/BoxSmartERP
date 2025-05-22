<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExportRptProgress
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ProgressInPercent = New System.Windows.Forms.Label()
        Me.ProgressBar = New System.Windows.Forms.ProgressBar()
        Me.Status_Label = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'ProgressInPercent
        '
        Me.ProgressInPercent.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.ProgressInPercent.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ProgressInPercent.Location = New System.Drawing.Point(1, 135)
        Me.ProgressInPercent.Name = "ProgressInPercent"
        Me.ProgressInPercent.Size = New System.Drawing.Size(446, 47)
        Me.ProgressInPercent.TabIndex = 4
        Me.ProgressInPercent.Text = "Pila ka porsyento"
        Me.ProgressInPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ProgressBar
        '
        Me.ProgressBar.Location = New System.Drawing.Point(2, 105)
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(443, 27)
        Me.ProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ProgressBar.TabIndex = 5
        '
        'Status_Label
        '
        Me.Status_Label.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Status_Label.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Status_Label.Location = New System.Drawing.Point(0, 1)
        Me.Status_Label.Name = "Status_Label"
        Me.Status_Label.Size = New System.Drawing.Size(446, 101)
        Me.Status_Label.TabIndex = 4
        Me.Status_Label.Text = "Akong ginabuhat karon..."
        Me.Status_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ExportRptProgress
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.CausesValidation = False
        Me.ClientSize = New System.Drawing.Size(446, 181)
        Me.ControlBox = False
        Me.Controls.Add(Me.ProgressBar)
        Me.Controls.Add(Me.Status_Label)
        Me.Controls.Add(Me.ProgressInPercent)
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ExportRptProgress"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ProgressInPercent As System.Windows.Forms.Label
    Friend WithEvents ProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents Status_Label As System.Windows.Forms.Label
End Class
