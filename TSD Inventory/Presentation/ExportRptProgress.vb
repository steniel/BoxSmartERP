Imports System.Drawing

Public Class ExportRptProgress
    Private mParentForm As Form
    Private mBaseHeight As Integer

    Private Shared mCurrentProgressForm As ExportRptProgress

    ''' <summary>
    ''' Creates a new ProgressWaitForm and displays it to the user
    ''' </summary>
    ''' <param name="parentForm">The form from which this method is being called</param>
    ''' <returns>A ProgressWaitForm object whose UpdateProgress method can be used to display progress to the user</returns>
    ''' <remarks></remarks>
    Public Shared Function ShowProgress(ByVal parentForm As Form) As ExportRptProgress
        If Not mCurrentProgressForm Is Nothing Then
            Throw New InvalidOperationException("A ProgressWaitForm is already displayed")
        End If
        System.Threading.ThreadPool.QueueUserWorkItem(AddressOf DoLoadProgress, parentForm)
        While mCurrentProgressForm Is Nothing OrElse mCurrentProgressForm.Visible = False
            System.Threading.Thread.Sleep(1)
        End While
        Return mCurrentProgressForm
    End Function

    Private Shared Sub DoLoadProgress(ByVal parentForm As Form)
        Dim f As New ExportRptProgress(parentForm)
        mCurrentProgressForm = f
        f.ShowDialog()
    End Sub

    Private Sub New(ByVal parentForm As Form)
        InitializeComponent()
        mParentForm = parentForm
        Left = mParentForm.Left + (mParentForm.Width / 2) - (Width / 2)
        Top = mParentForm.Top + (mParentForm.Height / 2) - (Height / 2)
        TopMost = True
        mBaseHeight = Height
    End Sub

    Public Shadows Sub Close()
        CloseProgress()
    End Sub

    Private Sub StatusLabel_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProgressInPercent.TextChanged, Status_Label.TextChanged
        Dim gfx As Graphics = ProgressInPercent.CreateGraphics
        Dim textSize As SizeF = gfx.MeasureString(ProgressInPercent.Text, ProgressInPercent.Font, ProgressInPercent.DisplayRectangle.Width)
        gfx.Dispose()
        'Height = mBaseHeight + textSize.Height + StatusLabel.Font.Height
    End Sub

    Private Delegate Sub UpdateProgressDelegate(ByVal progressPercent As Integer, ByVal StatusProgress As String, ByVal statusText As String)
    Private Delegate Sub CloseProgressDelegate()

    Private Sub DoCloseProgress()
        mCurrentProgressForm = Nothing
        MyBase.Close()
    End Sub

    Private Sub DoUpdateProgress(ByVal progressPercent As Integer, ByVal StatusProgress As String, ByVal StatusText As String)
        ProgressBar.Value = progressPercent
        ProgressInPercent.Text = StatusProgress
        Status_Label.Text = StatusText
    End Sub

    ''' <summary>
    ''' Closes the ProgressWaitForm - Be sure to call CloseProgress() when finished with this ProgressWaitForm instance
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CloseProgress()
        If InvokeRequired Then
            Invoke(New CloseProgressDelegate(AddressOf DoCloseProgress))
        Else
            DoCloseProgress()
        End If
    End Sub

    ''' <summary>
    ''' Updates the progress bar value and/or status text
    ''' </summary>
    ''' <param name="progressPercent">A value from 0 to 100 representing the precentage complete</param>
    ''' <param name="statusText">Any status text to accompany the progressbar</param>
    ''' <remarks></remarks>
    Public Sub UpdateProgress(ByVal progressPercent As Integer, ByVal StatusProgress As String, ByVal statusText As String)
        If InvokeRequired Then
            Invoke(New UpdateProgressDelegate(AddressOf DoUpdateProgress), New Object() {progressPercent, StatusProgress, statusText})
        Else
            DoUpdateProgress(progressPercent, StatusProgress, statusText)
        End If
    End Sub
End Class