<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128571505/12.1.7%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4258)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [MainWindow.xaml](./CS/GetLocationInformation/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/GetLocationInformation/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/GetLocationInformation/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/GetLocationInformation/MainWindow.xaml.vb))
<!-- default file list end -->
# How to get information about a geopoint using the Bing Geocode web service  


<p>This example  demonstrates how to obtain information about a geographical point from the Bing Geocode web service using the <a href="http://documentation.devexpress.com/#WPF/DevExpressXpfMapBingGeocodeDataProvider_RequestLocationInformationtopic"><u>BingGeocodeDataProvider.RequestLocationInformation</u></a> method.</p><p>To do this, specify a geographical point (<a href="http://documentation.devexpress.com/#WPF/DevExpressXpfMapGeoPoint_Longitudetopic"><u>GeoPoint.Longitude</u></a> and <a href="http://documentation.devexpress.com/#WPF/DevExpressXpfMapGeoPoint_Latitudetopic"><u>GeoPoint.Latitude</u></a>) in the <strong>TextBox</strong> elements. Then, click the<strong> Request Location</strong> button. It handles the <strong>requestLocation_Click </strong>event and all parameters are passed to the <strong>RequestLocationInformation </strong>method. </p><p>Results contain a location name (<a href="http://documentation.devexpress.com/#WPF/DevExpressXpfMapLocationInformation_DisplayNametopic"><u>LocationInformation.DisplayName</u></a>), entity type (<a href="http://documentation.devexpress.com/#WPF/DevExpressXpfMapLocationInformation_EntityTypetopic"><u>LocationInformation.EntityType</u></a> ), address (<a href="http://documentation.devexpress.com/#WPF/DevExpressXpfMapLocationInformation_Addresstopic"><u>LocationInformation.Address</u></a>) and exact coordinates ( <a href="http://documentation.devexpress.com/#WPF/DevExpressXpfMapLocationInformation_Locationtopic"><u>LocationInformation.Location</u></a>) that are shown in the <strong>TextBlock</strong> element below. </p><p>Note that if you run this sample as is, you will get a warning message informing that the specified Bing Maps key is invalid. To learn more about Bing Map keys, please refer to the <a href="http://documentation.devexpress.com/#WPF/CustomDocument10974"><u>How to: Get a Bing Maps Key</u></a> tutorial.</p><p><br />
</p><p><br />
</p>

<br/>


