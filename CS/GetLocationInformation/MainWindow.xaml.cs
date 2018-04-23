using DevExpress.Xpf.Map;
using System;
using System.Text;
using System.Windows;

namespace GetLocationInformation {

    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        #region #requestLocation_Click
        double latitude;
        double longitude;

        private bool GetLocationArguments() {
            latitude = String.IsNullOrEmpty(tbLatitude.Text) ? 0 : Double.Parse(tbLatitude.Text);
            if ((latitude > 90) || (latitude < -90)) {
                MessageBox.Show("Latitude must be less than or equal to 90 and greater than or equal to - 90. Please, correct the input value.");
                return false;
            }

            longitude = String.IsNullOrEmpty(tbLongitude.Text) ? 0 : Double.Parse(tbLongitude.Text);
            if ((longitude > 180) || (longitude < -180)) {
                MessageBox.Show("Longitude must be less than or equal to 180 and greater than or equal to - 180. Please, correct the input value.");
                return false;
            }

            return true;
        }

        private void requestLocation_Click(object sender, RoutedEventArgs e) {
            if (GetLocationArguments()) {
                LocationRequest();
            }
        }

        private void LocationRequest() {
            LocationInformationRequest(new GeoPoint(latitude, longitude));
        }

        private void LocationInformationRequest(GeoPoint geoPoint) {
            try {
                #region #RequestLocation
                geocodeProvider.RequestLocationInformation(new GeoPoint(latitude, longitude), 0);
                #endregion #RequestLocation
            }
            catch (Exception ex) {
                MessageBox.Show("An error occurs: " + ex.ToString());
            }
        }
        #endregion #requestLocation_Click

        #region #LocationInformationReceived_Implementation
        private void geocodeProvider_LocationInformationReceived(object sender, LocationInformationReceivedEventArgs e) {
            mapControl1.CenterPoint = new GeoPoint(latitude, longitude);
            mapControl1.ZoomLevel = 10;
            GeocodeRequestResult result = e.Result;
            StringBuilder resultList = new StringBuilder("");
            resultList.Append(String.Format("Status: {0}\n", result.ResultCode));
            resultList.Append(String.Format("Fault reason: {0}\n", result.FaultReason));

            if (result.ResultCode == RequestResultCode.Success) {
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

            }

            tbResults.Text = resultList.ToString();
        }
        #endregion #LocationInformationReceived_Implementation
    }
}
