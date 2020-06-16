var map, source, ol3controls, container, content, closer, overlay, parser, highlightOverlay;
var geoserverURL = "";
var WMSCapabilitiesURL = '';

var corineUrl = 'http://filotis.itia.ntua.gr/mapserver?SERVICE=WFS&' +
    'VERSION=1.1.0&REQUEST=GetFeature&TYPENAME=biotopes_corine&' +
    'SRSNAME=EPSG:4326&OUTPUTFORMAT=gml3';
var naturaUrl = 'http://filotis.itia.ntua.gr/mapserver?SERVICE=WFS&' +
    'VERSION=1.1.0&REQUEST=GetFeature&TYPENAME=biotopes_natura&' +
    'SRSNAME=EPSG:4326&OUTPUTFORMAT=gml3';
var cadastreUrl = 'http://gis.ktimanet.gr/wms/wmsopen/wmsserver.aspx';
//ArcGIS REST service URL
var esriImageryURL = 'https://services.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}';
//https://location.sa.gov.au/portal/sharing/servers/19b49fc58df54ffbae1d049a6e5ac59a/rest/services/World_Imagery/MapServer
//bhubaneswar ward booundary
var bbsrwardURL = 'http://35.225.112.210:8080/geoserver/wms?service=WMS&version=1.1.0&request=GetMap&layers=bbsr_map&srs=EPSG:4326';
////////////////google map type tiles///////////////////////////////
var googleBaseLayer = new ol.layer.Tile({
    title: "Google Imagery",
    type: "base",
    source: new ol.source.TileImage({ url: 'http://khm{0-3}.googleapis.com/kh?v=742&hl=pl&&x={x}&y={y}&z={z}' })
});
var googleLayerRoadNames = new ol.layer.Tile({
    title: "Google Road Names",
    type: "base",
    source: new ol.source.TileImage({ url: 'http://mt1.google.com/vt/lyrs=h&x={x}&y={y}&z={z}' })
});
var googleLayerSatellite = new ol.layer.Tile({
    title: "Google Satellite",
    type: "base",
    source: new ol.source.TileImage({ url: 'http://mt1.google.com/vt/lyrs=s&hl=pl&&x={x}&y={y}&z={z}' })
});
var googleLayerHybrid = new ol.layer.Tile({
    title: "Google Satellite & Roads",
    type: "base",
    source: new ol.source.TileImage({ url: 'http://mt1.google.com/vt/lyrs=y&x={x}&y={y}&z={z}' })
});
var googleLayerTerrain = new ol.layer.Tile({
    title: "Google Terrain",
    type: "base",
    source: new ol.source.TileImage({ url: 'http://mt1.google.com/vt/lyrs=t&x={x}&y={y}&z={z}' })
});
var googleLayerHybrid2 = new ol.layer.Tile({
    title: "Google Terrain & Roads",
    type: "base",
    source: new ol.source.TileImage({ url: 'http://mt1.google.com/vt/lyrs=p&x={x}&y={y}&z={z}' })
});
var googleLayerOnlyRoad = new ol.layer.Tile({
    title: "Google Road without Building",
    type: "base",
    source: new ol.source.TileImage({ url: 'http://mt1.google.com/vt/lyrs=r&x={x}&y={y}&z={z}' })
});
var googleSatelliteMap = new ol.layer.Tile({
    title: "Google Satellite",
    type: "base",
    source: new ol.source.TileImage({ url: 'http://mt0.google.com/vt/lyrs=y&hl=en&x={x}&y={y}&z={z}&s=Ga' })
});
var googleLayerRoadmap = new ol.layer.Tile({
    title: "Google Map",//Google Road Map
    type: "base",
    source: new ol.source.TileImage({ url: 'http://mt1.google.com/vt/lyrs=m&x={x}&y={y}&z={z}' })
});

var sourceBingMaps = new ol.source.BingMaps({
    key: 'AiSnqJt9drgqL9NzEz5tVJzDku0GK80_QfyVWaylE_BKY0jL8BQ88jrZZrCiAwfq',
    imagerySet: 'Road',
    culture: 'fr-FR'
});

var bingMapsRoad = new ol.layer.Tile({
    title: 'Bing Road',
    type: 'base',
    visible: false,
    preload: Infinity,
    source: sourceBingMaps
});

var bingMapsAerial = new ol.layer.Tile({
    title: 'Bing Aerial',
    type: 'base',
    visible: false,
    preload: Infinity,
    source: new ol.source.BingMaps({
        key: 'AiSnqJt9drgqL9NzEz5tVJzDku0GK80_QfyVWaylE_BKY0jL8BQ88jrZZrCiAwfq',
        imagerySet: 'Aerial',
    })
});

var baseLayerNatura = new ol.layer.Tile({
    title: 'BBSR Imagery',
    type: 'base',
    visible: false,
    source: new ol.source.TileWMS({
        url: bbsrwardURL
    })
});
var baseLayerESRI = new ol.layer.Tile({
    title: 'ESRI Imagery',
    type: 'base',
    visible: false,
    source: new ol.source.XYZ({
        url: esriImageryURL,
        maxZoom: 23
    })
});
var baseLayerOSM = new ol.layer.Tile({
    title: 'Open Street Map',
    source: new ol.source.OSM(),
    type: 'base'
});
var baseMapGroup = new ol.layer.Group({
    title: 'Base Map',
    layers: [
        googleLayerHybrid,
        googleLayerTerrain,
        baseLayerOSM,
        googleLayerSatellite,
        googleLayerRoadmap
    ]
});

var mapView = new ol.View({
    projection: 'EPSG:3857',
    center: [9554619.58, 2305384.53],
    zoom: 11
});

var vectorSource = new ol.source.Vector({
    wrapX: false
});
var vectorLayer = new ol.layer.Vector({
    source: vectorSource,
    map: map
});

$(document).ready(function () {
    /**
 * Elements that make up the popup.
 */
    container = document.getElementById('popup');
    content = document.getElementById('popup-content');
    closer = document.getElementById('popup-closer');

    /**
     * Create an overlay to anchor the popup to the map.
     */
    overlay = new ol.Overlay(/** @type {olx.OverlayOptions} */({
        element: container,
        autoPan: true,
        autoPanAnimation: {
            duration: 250
        }
    }));
    /**
     * Add a click handler to hide the popup.
     * @return {boolean} Don't follow the href.
     */
    closer.onclick = function () {
        overlay.setPosition(undefined);
        closer.blur();
        return false;
    };

    map = new ol.Map({
        target: 'map',
        layers: [baseMapGroup, vectorLayer],
        overlays: [overlay],
        view: new ol.View({
            center: [9554619.58, 2305384.53],
            zoom: 11
        })
    });

    $('input:checkbox[name="checkboxWMS"]').change(function () {
        if ($(this).prop('checked') == true) {
            var displayName = $(this).closest('li').text();
            ////add WFS layer to map
            //AddwFSLayer($(this)[0].id, $(this).val(), false, "", displayName);
            //add wms layer to map
            AddWMSLayer($(this)[0].id, $(this).val(), false, "",displayName);
        }
        else {
            ////remove wfs layer
            //var selectedLayer = $(this).val();
            //var layersToRemove = [];
            //map.getLayers().forEach(function (layer) {
            //    if (layer.get('name') != undefined && layer.get('name') === selectedLayer) {
            //        layersToRemove.push(layer);
            //    }
            //});
            //var len = layersToRemove.length;
            //for (var i = 0; i < len; i++) {
            //    map.removeLayer(layersToRemove[i]);
            //}

            //remove wms layer
            for (var x = 0; x < layerArray.length; x++) {
                if (layerArray[x].LayerId == $(this).val()) {
                    //debugger;
                    var layerIndex = GetLayerIndexInArray(layerArray[x].LayerId);
                    map.removeLayer(layerArray[x].WMSLayer);
                    layerArray.splice(layerIndex, 1);
                    break;
                }
            }
            dynamicLegend();
        }
    });

    map.addEventListener('click', ShowInfo);

    //function bindInputs(layerid, layer) {
    //    var visibilityInput = $(layerid + ' input.visible');
    //    visibilityInput.on('change', function () {
    //        layer.setVisible(this.checked);
    //    });
    //    visibilityInput.prop('checked', layer.getVisible());

    //    var opacityInput = $(layerid + ' input.opacity');
    //    opacityInput.on('input change', function () {
    //        layer.setOpacity(parseFloat(this.value));
    //    });
    //    opacityInput.val(String(layer.getOpacity()));
    //}
    //map.getLayers().forEach(function (layer, i) {
    //    bindInputs('#layer' + i, layer);
    //    //if (layer instanceof LayerGroup) {
    //    //    layer.getLayers().forEach(function (sublayer, j) {
    //    //        bindInputs('#layer' + i + j, sublayer);
    //    //    });
    //    //}
    //});
    map.addControl(new ol.control.LayerSwitcher());

    //==========Default load layer=======//
    var defaultLayerId = "chkIshaneswarBoundary";
    var defaultLayerName = "cite:bbsr_ishaneswar_basti_boundary";
    AddWMSLayer(defaultLayerId, defaultLayerName, false, "", "Ishaneswar Boundary");
    $("#" + defaultLayerId).prop("checked", true);
    ZoomToLayer(defaultLayerName);
    //================================//

    $('.sidebar-toggle').click(function () {
        document.getElementById("#mapContainer").style.width = "100%";
    })
});

function ZoomToLayer(layerName) {
    //////////set WMS layer bounding box/////////////////
    var parser = new ol.format.WMSCapabilities();
    $.ajax(WMSCapabilitiesURL).then(function (response) {
        //window.alert("word");
        var result = parser.read(response);
        //console.log(result);
        //window.alert(result);
        var Layers = result.Capability.Layer.Layer;
        for (var i = 0, len = Layers.length; i < len; i++) {

            var layerobj = Layers[i];
            //window.alert(layerobj.Name);
            if (layerobj.Name == layerName) {
                var layerextent = new ol.proj.transformExtent(layerobj.BoundingBox[0].extent, 'EPSG:4326', 'EPSG:3857');
                map.getView().fit(layerextent, map.getSize());
                break;
            }
        }
    });
}

function GetLayerIndexInArray(layerId) {
    //debugger;
    for (var i = 0; i < layerArray.length; i++) {
        if (layerArray[i].LayerId == layerId) {
            return i;
            break;
        }
        //else {
        //    return -1;
        //    break;
        //}
    }
}

var dynamicLegend = function () {
    $('#divLegend').html('');
    var legendLine = "";
    for (i = 0, len = layerArray.length; i < len; i++) {
        var legendSrc = geoserverURL + "REQUEST=GetLegendGraphic&VERSION=1.0.0&FORMAT=image/png&transparent=true&WIDTH=20&HEIGHT=20&LAYER=" + layerArray[i].LayerId;
        legendLine = '<img src="' + legendSrc + '" />';
        $('#divLegend').append("<a>" + (i + 1) + ". " + layerArray[i].DisplayName + "-</a>" + legendLine + '<br/>');
    }
    //var legendPoint = '<img src="http://35.225.112.210:8080/geoserver/wms?REQUEST=GetLegendGraphic&VERSION=1.0.0&FORMAT=image/png&WIDTH=20&HEIGHT=20&LAYER=cite:bbsr_consumer" />';    
};


var layerWMS; var layerArray = [];
var tile_load_time_WMSLayer = 0;
var tile_loading = 0, tile_loaded = 0;
function AddWMSLayer(layerId, layerName, mapExport, imagery, displayName) {
    //debugger;
    layerWMS = new ol.layer.Tile({
        source: new ol.source.TileWMS({
            url: geoserverURL,
            params: { 'LAYERS': layerName, 'NAME': layerName, 'TILED': true },
            serverType: 'geoserver',
            crossOrigin: 'anonymous'
        })
    });
    //=============tile load events=============//
    layerWMS.getSource().on('tileloadstart', function () {
        progress.addLoading();
    });

    layerWMS.getSource().on('tileloadend', function () {
        progress.addLoaded();
    });
    layerWMS.getSource().on('tileloaderror', function () {
        progress.addLoaded();
    });
    //==========================================//
    map.addLayer(layerWMS);

    if (mapExport == false) {
        layerArray.push({ "LayerId": layerName, "WMSLayer": layerWMS, "DisplayName": displayName });
    }
    dynamicLegend();
    ZoomToLayer(layerName);
}

function AddwFSLayer(layerId, layerName, mapExport, imagery, displayName) {
    // generate a GetFeature request
    var featureRequest = new ol.format.WFS().writeGetFeature({
        srsName: 'EPSG:3857',
        featureNS: 'www.openplans.org/cite',
        featurePrefix: 'cite',
        featureTypes: [layerName],
        outputFormat: 'application/json'
        //filter: andFilter(
        //    likeFilter('name', 'Mississippi*'),
        //    equalToFilter('waterway', 'riverbank')
        //)
    });

    // then post the request and add the received features to a layer
    fetch('http://emcsolution.in:8080/geoserver/cite/ows?service=WFS', {
        method: 'POST',
        body: new XMLSerializer().serializeToString(featureRequest)
    }).then(function (response) {
        return response.json();
    }).then(function (json) {
        var features = new ol.format.GeoJSON().readFeatures(json);

        // Create separate sources to store the features
        var aSrc = new ol.source.Vector();
        // Loop over the features
        features.forEach(function (feature) {
            var featStyle = new ol.style.Style({
                image: new ol.style.Icon({
                    crossOrigin: 'anonymous',
                    src: '../../Content/Img/location.png'
                })
            });
            feature.setStyle(featStyle);
            aSrc.addFeature(feature);
        });
        // Your separate layers
        var aLayer = new ol.layer.Vector({
            source: aSrc,
            name: layerName
        });
        map.addLayer(aLayer)
        layerArray.push({ "LayerId": layerName, "WMSLayer": "", "DisplayName": displayName });
        //map.getView().fit(vectorSource.getExtent());
    });

    ////==============another method=============//
    //var layerWFS = new ol.layer.Vector({
    //    source: new ol.source.Vector({
    //        loader: function (extent) {
    //            $.ajax('http://35.225.112.210:8080/geoserver/wfs', {
    //                type: 'GET',
    //                data: {
    //                    service: 'WFS',
    //                    version: '1.1.0',
    //                    request: 'GetFeature',
    //                    typename: layerName,
    //                    srsname: 'EPSG:3857',
    //                    bbox: extent.join(',') + ',EPSG:3857'
    //                }
    //            }).done(function (response) {
    //                layerWFS
    //                    .getSource()
    //                    .addFeatures(new ol.format.WFS()
    //                        .readFeatures(response));
    //                // console.log(response);
    //            });
    //        },
    //        strategy: ol.loadingstrategy.bbox,
    //        projection: 'EPSG:3857'
    //    })
    //});
    //map.addLayer(layerWFS);
    ////=========================//
}

function RemoveLayer(layerName) {
    for (var i = 0; i < map.layers.length; i++) {
        if (map.layers[i].name == layerName) {
            map.removeLayer(map.layers[i]);
        }
    }
}
function RemoveAllWMSLayer() {
    if (layerArray.length > 0) {
        for (var i = 0; i < layerArray.length; i++) {
            map.removeLayer(layerArray[i].WMSLayer);
        }
        //uncheck all the corresponding checkboxes from layer array
        $('input:checkbox[name="checkboxWMS"]').each(function () {
            $(this).prop('checked', false);
        });
    }
}

//consumer location feature style
var iconStyle = new ol.style.Style({
    image: new ol.style.Icon({
        anchor: [0.5, 46],
        anchorXUnits: 'fraction',
        anchorYUnits: 'pixels',
        src: '~/Content/Img/location.png'
    })
});
function PlotMarkerOnMap(latLong) {
    vectorSource.clear();
    var pointGeometry = new ol.geom.Point(ol.proj.transform([latLong[0], latLong[1]], 'EPSG:4326', 'EPSG:3857'));
    var pointFeature = new ol.Feature({
        geometry: pointGeometry
    });
    pointFeature.setStyle(iconStyle);
    vectorSource.addFeature(pointFeature);
    //buffer the point
    var radius = parseFloat($('#rangeBuffer').val());
    var point = pointGeometry.getCoordinates();
    var buffer = new ol.Feature({ geometry: new ol.geom.Circle(point, radius) });
    vectorSource.addFeature(buffer)//add the buffer to some source
}
$('select.AllAssets').change(function () {
    //alert($('option:selected', this).index());
    if ($('option:selected', this).index() == 0) {
        bufferRequired = false;
    }
    else {
        bufferRequired = true;
    }
});
///info/////
var clickedLayer = ""; var bufferRequired = false;
var ShowInfo = function (evt) {
    //debugger;
    var clickedLatLng = new ol.proj.transform(evt.coordinate, 'EPSG:3857', 'EPSG:4326');
    //alert(clickedLatLng.toString());
    clickedLng = clickedLatLng.toString().split(',')[0];
    clickedLat = clickedLatLng.toString().split(',')[1];
    $('#spanClickedLonLat').html('[' + parseFloat(clickedLat).toFixed(4) + ',' + parseFloat(clickedLng).toFixed(4) + ']');
    if (bufferRequired) { PlotMarkerOnMap(clickedLatLng); }
    if (layerArray.length > 0) {
        // Attempt to find layer from the clicked pixel
        var requestsArray = [];
        map.forEachLayerAtPixel(evt.pixel, function (layer) {
            //debugger;
            try {
                if (layer.getSource().getParams()) {
                    // log the LAYERS param of the WMS source
                    //console.log(layer.getSource().getParams().LAYERS);
                    var loadedLayerName = layer.getSource().getParams().NAME;
                    for (var i = 0; i < layerArray.length; i++) {
                        if (layerArray[i].LayerId.trim() == loadedLayerName.trim()) {
                            //debugger;
                            var wmsLayerQueryString = layerArray[i].WMSLayer.getSource().getFeatureInfoUrl(evt.coordinate, map.getView().getResolution(), map.getView().getProjection(), { 'INFO_FORMAT': 'application/json' });
                            requestsArray.push({ "LayerName": loadedLayerName, "LayerQueryString": wmsLayerQueryString });
                            break;
                        }
                    }
                    //return layer.values_.source.params_.NAME;      
                }
            } catch (e) {

            }
        });
        //debugger;
        // Attempt to find a feature from the clicked pixel
        var feature = map.forEachFeatureAtPixel(evt.pixel, function (feature, layer) {
            return feature;
        });

        if (feature) {
            var coord = feature.getGeometry().getCoordinates();
            var props = feature.getProperties();
            // Offset the popup so it points at the middle of the marker not the tip
            content.innerHTML = props.desc;
            overlay.setPosition(evt.coordinate);

        } else {
            if (requestsArray.length > 0) {
                var htmlString = '';
                let promises = requestsArray.map(url => {
                    return $.ajax({
                        url: url.LayerQueryString,
                        dataType: 'json',
                        jsonpCallback: 'parseResponse'
                    }).then(function (response) {
                        return response;
                    });
                });

                Promise.all(promises).then(response => {
                    //console.log(response);                
                    for (var k = 0; k < response.length; k++) {
                        if (response[k].features.length > 0) {
                            //debugger;
                            htmlString += ParseJSONResponseToHTML(response[k].features[0].properties, MapLayerTypes.WMSLayer, requestsArray[k].LayerName);
                        }
                    }
                    if (htmlString != "") {
                        content.innerHTML = htmlString;
                        overlay.setPosition(evt.coordinate);
                    }
                    //alert(occurrences(htmlString,'<table'));
                }).catch(e => {
                    console.log('Whoops something went wrong!', e);
                });
            }
        }
    }
    else {

        var f = map.forEachFeatureAtPixel(
            evt.pixel,
            function (ft, layer) { return ft; }
        );
        if (f && f.get('type') == 'click') {
            var geometry = f.getGeometry();
            var coord = geometry.getCoordinates();
            //var content = '<p>' + f.get('desc') + '</p>';
            content.innerHTML = '<p>' + f.get('desc') + '</p>';
            overlay.setPosition(coord);
            //popup.show(coord, content);

        } //else { popup.hide(); }
    }
};
var clickedLat, clickedLng = "";
$('#layertree li > span').click(function () {
    $(this).siblings('fieldset').toggle();
}).siblings('fieldset').hide();


function occurrences(string, substring) {
    //debugger;
    var n = 0;
    var pos = 0;

    while (true) {
        pos = string.indexOf(substring, pos);
        if (pos != -1) { n++; pos += substring.length; }
        else { break; }
    }
    return (n);
}

//========wms loading progress===//
/**
 * Renders a progress bar.
 * @param {HTMLElement} el The target element.
 * @constructor
 */
function Progress(el) {
    this.el = el;
    this.loading = 0;
    this.loaded = 0;
}


/**
 * Increment the count of loading tiles.
 */
Progress.prototype.addLoading = function () {
    if (this.loading === 0) {
        this.show();
    }
    ++this.loading;
    this.update();
};


/**
 * Increment the count of loaded tiles.
 */
Progress.prototype.addLoaded = function () {
    var this_ = this;
    setTimeout(function () {
        ++this_.loaded;
        this_.update();
    }, 100);
};


/**
 * Update the progress bar.
 */
Progress.prototype.update = function () {
    var width = (this.loaded / this.loading * 100).toFixed(1) + '%';
    this.el.style.width = width;
    if (this.loading === this.loaded) {
        this.loading = 0;
        this.loaded = 0;
        var this_ = this;
        setTimeout(function () {
            this_.hide();
        }, 500);
    }
};


/**
 * Show the progress bar.
 */
Progress.prototype.show = function () {
    this.el.style.visibility = 'visible';
};


/**
 * Hide the progress bar.
 */
Progress.prototype.hide = function () {
    if (this.loading === this.loaded) {
        this.el.style.visibility = 'hidden';
        this.el.style.width = 0;
    }
};

var progress = new Progress(document.getElementById('progress'));
//====================================================================//