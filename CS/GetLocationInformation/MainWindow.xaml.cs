using DevExpress.Xpf.Map;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace GetLocationInformation {

    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            this.DataContext = this;
        }

        #region #requestLocation_Click
        GeoPoint location = new GeoPoint(40, -120);     
        public GeoPoint Location {
            get { return location; }
            set { location = value;} 
        }
        
        private void requestLocation_Click(object sender, RoutedEventArgs e) {
            geocodeProvider.RequestLocationInformation(Location, null);
        }
        #endregion #requestLocation_Click

        #region #LocationInformationReceived_Implementation
        private void geocodeProvider_LocationInformationReceived(object sender, LocationInformationReceivedEventArgs e) {
            GeocodeRequestResult result = e.Result;
            StringBuilder resultList = new StringBuilder("");
            resultList.Append(String.Format("Status: {0}\n", result.ResultCode));
            resultList.Append(String.Format("Fault reason: {0}\n", result.FaultReason));
            resultList.Append(String.Format("______________________________\n"));

            if (result.ResultCode != RequestResultCode.Success) {
                tbResults.Text = resultList.ToString();
                return; 
            }
            
            int resCounter = 1;
            foreach (LocationInformation locations in result.Locations) {
                resultList.Append(String.Format("Request Result {0}:\n", resCounter));
                resultList.Append(String.Format("Display Name: {0}\n", locations.DisplayName));
                resultList.Append(String.Format("Entity Type: {0}\n", locations.EntityType));
                resultList.Append(String.Format("Address: {0}\n", locations.Address));
                resultList.Append(String.Format("Location: {0}\n", locations.Location));
                resultList.Append(String.Format("______________________________\n"));
                resCounter++;
            }

            tbResults.Text = resultList.ToString();
        }
        #endregion #LocationInformationReceived_Implementation

        private void OnError(object sender, ValidationErrorEventArgs e) {
            if (e.Action != ValidationErrorEventAction.Added) return;

            MessageBox.Show(e.Error.ErrorContent.ToString());
        }
    }

    class IntervalDoubleValidationRule : ValidationRule {
        public double Max { get; set; }
        public double Min { get; set; }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo) {
            double d;
            if (!Double.TryParse(value as string, out d)) 
                return new ValidationResult(false, "Input value should be floating point number.");

            if ((d > Max) || (d < Min))
                return new ValidationResult(
                    false, 
                    String.Format("Input value should be larger than or equals {0} and less that or equals {1}.", Min, Max));

            return new ValidationResult(true, null);
        }
    }
}
