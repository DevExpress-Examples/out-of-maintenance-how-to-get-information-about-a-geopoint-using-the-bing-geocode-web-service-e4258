Imports Microsoft.VisualBasic
Imports DevExpress.Xpf.Map
Imports System
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls

Namespace GetLocationInformation

	Partial Public Class MainWindow
		Inherits Window

		Public Sub New()
			InitializeComponent()
			Me.DataContext = Me
		End Sub

		#Region "#requestLocation_Click"
'INSTANT VB NOTE: The field location was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private location_Conflict As New GeoPoint(40, -120)
		Public Property Location() As GeoPoint
			Get
				Return location_Conflict
			End Get
			Set(ByVal value As GeoPoint)
				location_Conflict = value
			End Set
		End Property

		Private Sub requestLocation_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
			geocodeProvider.RequestLocationInformation(Location, Nothing)
		End Sub
		#End Region ' #requestLocation_Click

		#Region "#LocationInformationReceived_Implementation"
		Private Sub geocodeProvider_LocationInformationReceived(ByVal sender As Object, ByVal e As LocationInformationReceivedEventArgs)
			Dim result As GeocodeRequestResult = e.Result
			Dim resultList As New StringBuilder("")
			resultList.Append(String.Format("Status: {0}" & vbLf, result.ResultCode))
			resultList.Append(String.Format("Fault reason: {0}" & vbLf, result.FaultReason))
			resultList.Append(String.Format("______________________________" & vbLf))

			If result.ResultCode <> RequestResultCode.Success Then
				tbResults.Text = resultList.ToString()
				Return
			End If

			Dim resCounter As Integer = 1
			For Each locations As LocationInformation In result.Locations
				resultList.Append(String.Format("Request Result {0}:" & vbLf, resCounter))
				resultList.Append(String.Format("Display Name: {0}" & vbLf, locations.DisplayName))
				resultList.Append(String.Format("Entity Type: {0}" & vbLf, locations.EntityType))
				resultList.Append(String.Format("Address: {0}" & vbLf, locations.Address))
				resultList.Append(String.Format("Location: {0}" & vbLf, locations.Location))
				resultList.Append(String.Format("______________________________" & vbLf))
				resCounter += 1
			Next locations

			tbResults.Text = resultList.ToString()
		End Sub
		#End Region ' #LocationInformationReceived_Implementation

		Private Sub OnError(ByVal sender As Object, ByVal e As ValidationErrorEventArgs)
			If e.Action <> ValidationErrorEventAction.Added Then
				Return
			End If

			MessageBox.Show(e.Error.ErrorContent.ToString())
		End Sub
	End Class

	Friend Class IntervalDoubleValidationRule
		Inherits ValidationRule

		Public Property Max() As Double
		Public Property Min() As Double

		Public Overrides Function Validate(ByVal value As Object, ByVal cultureInfo As System.Globalization.CultureInfo) As ValidationResult
			Dim d As Double = Nothing
			If Not Double.TryParse(TryCast(value, String), d) Then
				Return New ValidationResult(False, "Input value should be floating point number.")
			End If

			If (d > Max) OrElse (d < Min) Then
				Return New ValidationResult(False, String.Format("Input value should be larger than or equals {0} and less that or equals {1}.", Min, Max))
			End If

			Return New ValidationResult(True, Nothing)
		End Function
	End Class
End Namespace
