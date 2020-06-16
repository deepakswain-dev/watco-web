var modalMap, source, ol3controls, container, content, closer, overlay, parser, highlightOverlay;
var geoserverURL = "http://emcsolution.in:8080/geoserver/cite/wms?";
var WMSCapabilitiesURL = "http://emcsolution.in:8080/geoserver/wms?request=GetCapabilities&service=WMS&version=1.1.0";

//Create map vector source but let user add them to the map
var vectorSource = new ol.source.Vector({ wrapX: false });
var vectorLayer = new ol.layer.Vector({
    source: vectorSource,
    map: modalMap
});

//base map objects
var layerOSM = new ol.layer.Tile({
    source: new ol.source.OSM()
});
var layerImagery = new ol.layer.Tile({
    source: new ol.source.XYZ({
        url: 'https://services.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}',
        maxZoom: 23
    })
});

//var layerOSM = new ol.layer.Tile({
//    //source: new ol.source.OSM()
//    source: new ol.source.XYZ({
//        //attributions: ['Powered by Esri',
//        //    'Source: Esri, DigitalGlobe, GeoEye, Earthstar Geographics, CNES/Airbus DS, USDA, USGS, AeroGRID, IGN, and the GIS User Community'],
//        //attributionsCollapsible: false,
//        url: 'https://services.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}',
//        maxZoom: 23
//    })
//});
function LoadModalMap() {
    modalMap = new ol.Map({
        target: 'map',
        layers: [layerImagery, vectorLayer],
        view: new ol.View({
            center: [9554619.58, 2305384.53],
            zoom: 19
        })
    });
}
//consumer location feature style
var iconStyle = new ol.style.Style({
    image: new ol.style.Icon({
        anchor: [0.5, 46],
        anchorXUnits: 'fraction',
        anchorYUnits: 'pixels',
        src: '../../Content/Img/location.png'
    })
});
function LoadMarkerToModalMap(latLong) {
    vectorSource.clear();
    var lon = latLong[0].trim();
    var lat = latLong[1].trim();
    //alert(lat+","+lon);
    var pointFeature = new ol.Feature({
        geometry: new ol.geom.Point(ol.proj.transform([lon, lat], 'EPSG:4326', 'EPSG:3857'))
    });
    pointFeature.setStyle(iconStyle);
    vectorSource.addFeature(pointFeature);
    modalMap.getView().setCenter(ol.proj.transform([lon, lat], 'EPSG:4326', 'EPSG:3857'));
    //modalMap.getView().fit(layer.getSource().getExtent(), modalMap.getSize());
}