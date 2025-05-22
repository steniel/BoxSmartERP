Public Class ClassPrintcardForm
    Private Shared PrintcardFormDefaultInstance As PrintCard
    Private Shared m_InitializingDefInstance As Boolean
    Dim _NumberOfColors As Integer
    Dim _Colors(0) As String
    Public Shared Property PrintcardFormInstance() As PrintCard
        Get
            If PrintcardFormDefaultInstance Is Nothing _
                OrElse PrintcardFormDefaultInstance.IsDisposed Then
                m_InitializingDefInstance = True
                PrintcardFormDefaultInstance = New PrintCard()
                m_InitializingDefInstance = False
            End If
            PrintcardFormInstance = PrintcardFormDefaultInstance
        End Get
        Set(ByVal Value As PrintCard)
            PrintcardFormDefaultInstance = Value
        End Set
    End Property

    Public Property CustomerName() As String
        Get
            Return PrintcardFormInstance.tCustomerName.Text
        End Get
        Set(ByVal Value As String)
            PrintcardFormInstance.tCustomerName.Text = Value
        End Set
    End Property
    Public Property BoxDescription() As String
        Get
            Return PrintcardFormInstance.tBoxDescription.Text
        End Get
        Set(ByVal Value As String)
            PrintcardFormInstance.tBoxDescription.Text = Value
        End Set
    End Property
    Public Property BoxFormat() As String
        Get
            Return PrintcardFormInstance.cmbBoxFormat.Text
        End Get
        Set(ByVal Value As String)
            PrintcardFormInstance.cmbBoxFormat.Text = Value
        End Set
    End Property

    Public Property BoxID(ByVal InsideDimensionName As String) As Double
        Get
            Select Case InsideDimensionName
                Case "Length"
                    Return PrintcardFormInstance.tLength.Text
                Case "Width"
                    Return PrintcardFormInstance.tWidth.Text
                Case "Height"
                    Return PrintcardFormInstance.tHeight.Text
                Case Else
                    Throw New Exception("Parameter should be 'Length' or 'Width' or 'Height' only.")
            End Select
        End Get
        Set(ByVal Value As Double)
            Select Case InsideDimensionName
                Case "Length"
                    PrintcardFormInstance.tLength.Text = Value
                Case "Width"
                    PrintcardFormInstance.tWidth.Text = Value
                Case "Height"
                    PrintcardFormInstance.tHeight.Text = Value
            End Select
        End Set
    End Property

    Public Property JointType() As String
        Get
            Return PrintcardFormInstance.cmbJoint.Text
        End Get
        Set(ByVal Value As String)
            PrintcardFormInstance.cmbJoint.Text = Value
        End Set
    End Property

    Public Property BoardType() As String
        Get
            Return PrintcardFormInstance.cmbBoardType.Text
        End Get
        Set(ByVal Value As String)
            PrintcardFormInstance.cmbBoardType.Text = Value
        End Set
    End Property

    Public Property BoxTest() As Integer
        Get
            Return PrintcardFormInstance.cmbTest.Text
        End Get
        Set(ByVal Value As Integer)
            PrintcardFormInstance.cmbTest.Text = Value
        End Set
    End Property

    Public Property PaperCombination() As String
        Get
            Return PrintcardFormInstance.tPaperCombination.Text
        End Get
        Set(ByVal Value As String)
            PrintcardFormInstance.tPaperCombination.Text = Value
        End Set
    End Property
    Public Property NumberOfColors() As UInteger
        Get
            Return _NumberOfColors
        End Get
        Set(ByVal value As UInteger)
            _NumberOfColors = value
            ReDim Preserve _Colors(value)
        End Set
    End Property

    Public Property Colors(ByVal index As Integer) As Integer
        Get
            If (index < NumberOfColors()) And index >= 0 Then
                Return _Colors(index)
            Else
                Throw New Exception("Index should be in the range of zero to 0 to " & NumberOfColors())
            End If
        End Get
        Set(ByVal value As Integer)
            If (index < NumberOfColors()) And index >= 0 Then
                _Colors(index) = value
            Else
                Throw New Exception("Index should be in the range of zero to 0 to " & NumberOfColors())
            End If
        End Set
    End Property

    Public Property ScaleFactor() As String
        Get
            Return PrintcardFormDefaultInstance.cmbScale.Text
        End Get
        Set(ByVal value As String)
            PrintcardFormDefaultInstance.cmbScale.Text = value
        End Set
    End Property

    Public Property PrintcardNumber() As Integer
        Get
            Return PrintcardFormInstance.tPrintcardNumber.Text
        End Get
        Set(ByVal Value As Integer)
            PrintcardFormInstance.tPrintcardNumber.Text = Value
        End Set
    End Property

    Public Property Diecut() As Integer
        Get
            Return PrintcardFormInstance.cmbDiecut.Text
        End Get
        Set(ByVal value As Integer)
            PrintcardFormInstance.cmbDiecut.Text = value
        End Set
    End Property

End Class
