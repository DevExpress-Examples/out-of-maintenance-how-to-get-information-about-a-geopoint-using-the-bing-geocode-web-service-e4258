Imports DevExpress.Xpf.Map
Imports System
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls

Namespace GetLocationInformation

    Public Partial Class MainWindow
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
            DataContext = Me
        End Sub

'#Region "#requestLocation_Click"
        Private locationField As GeoPoint = New GeoPoint(40, -120)

        Public Property Location As GeoPoint
            Get
                Return locationField
            End Get

            Set(ByVal value As GeoPoint)
                locationField = value
            End Set
        End Property

        Private Sub requestLocation_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.geocodeProvider.RequestLocationInformation(Location, Nothing)
        End Sub

'#End Region  ' #requestLocation_Click
'#Region "#LocationInformationReceived_Implementation"
        Private Sub geocodeProvider_LocationInformationReceived(ByVal sender As Object, ByVal e As LocationInformationReceivedEventArgs)
            Dim result As GeocodeRequestResult = e.Result
            Dim resultList As StringBuilder = New StringBuilder("")
            resultList.Append(String.Format("Status: {0}" & Microsoft.VisualBasic.Constants.vbLf, result.ResultCode))
            resultList.Append(String.Format("Fault reason: {0}" & Microsoft.VisualBasic.Constants.vbLf, result.FaultReason))
            resultList.Append(String.Format("______________________________" & Microsoft.VisualBasic.Constants.vbLf))
            If result.ResultCode <> RequestResultCode.Success Then
                Me.tbResults.Text = resultList.ToString()
                Return
            End If

            Dim resCounter As Integer = 1
            For Each locations As LocationInformation In result.Locations
                resultList.Append(String.Format("Request Result {0}:" & Microsoft.VisualBasic.Constants.vbLf, resCounter))
                resultList.Append(String.Format("Display Name: {0}" & Microsoft.VisualBasic.Constants.vbLf, locations.DisplayName))
                resultList.Append(String.Format("Entity Type: {0}" & Microsoft.VisualBasic.Constants.vbLf, locations.EntityType))
                resultList.Append(String.Format("Address: {0}" & Microsoft.VisualBasic.Constants.vbLf, locations.Address))
                resultList.Append(String.Format("Location: {0}" & Microsoft.VisualBasic.Constants.vbLf, locations.Location))
                resultList.Append(String.Format("______________________________" & Microsoft.VisualBasic.Constants.vbLf))
                resCounter += 1
            Next

            Me.tbResults.Text = resultList.ToString()
        End Sub

'#End Region  ' #LocationInformationReceived_Implementation
        Private Sub OnError(ByVal sender As Object, ByVal e As ValidationErrorEventArgs)
            If e.Action <> ValidationErrorEventAction.Added Then Return
            Call MessageBox.Show(e.Error.ErrorContent.ToString())
        End Sub
    End Class

    Friend Class IntervalDoubleValidationRule
        Inherits ValidationRule

        Public Property Max As Double

        Public Property Min As Double

        Public Overrides Function Validate(ByVal value As Object, ByVal cultureInfo As Globalization.CultureInfo) As ValidationResult
            Dim d As Double
            If Not Double.TryParse(TryCast(value, String), d) Then Return New ValidationResult(False, "Input value should be floating point number.")
            If d > Max OrElse d < Min Then Return New ValidationResult(False, String.Format("Input value should be larger than or equals {0} and less that or equals {1}.", Min, Max))
            Return New ValidationResult(True, Nothing)
        End Function
    End Class
End Namespace
