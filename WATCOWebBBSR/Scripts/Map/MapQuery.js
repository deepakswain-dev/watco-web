geoserverURL = '@(ViewBag.GeoserverURL)';
WMSCapabilitiesURL = '@(ViewBag.WMSCapabilitiesURL)';
$(document).ready(function () {
    var screenHeight = $(window).height();
    $('#map').height(screenHeight - 58 + 'px');
    //hide attribution div
    $('.ol-attribution').css('display', 'none');

    $('#rangeBuffer').change(function () {
        var spanBufferValue = this.value;
        $('#spanBufferValue').html('Buffer ' + spanBufferValue + ' Mtr.');
    });
});

function openSidebarLeft(btn) {
    document.getElementById("mySidebar").style.width = "300px";
    //document.getElementById("map").style.marginLeft = "300px";
    //$('#map').width($('#map').width() - 300 + "px");
    $(btn).css('display', 'none');
}
function closeSidebarLeft() {
    document.getElementById("mySidebar").style.width = "0";
    //document.getElementById("map").style.marginLeft = "0";
    //$('#map').width($(window).width() - 5 + 'px');
    $('button.btnleftSidebar').css('display', 'block');
}

function openNavRight() {
    document.getElementById("mySidenavRight").style.width = "300px";
    $('a.querybuilder').css('display', 'none');
    //querybuilder
}

function closeNavRight() {
    document.getElementById("mySidenavRight").style.width = "0";
    $('a.querybuilder').css('display', 'block');
}
function showAssetMap() {
    //vectorSource.clear();
    if (clickedLat != "" && clickedLng != "") {
        var assetId = $("#AllAssets").val();
        var buffer = $('#rangeBuffer').val();
        var myStyle = new ol.style.Style({
            image: new ol.style.Circle({
                radius: 7,
                fill: new ol.style.Fill({ color: 'olive' }),
                stroke: new ol.style.Stroke({
                    color: [255, 0, 0], width: 2
                })
            })
        });
        $.ajax({
            type: "POST",
            url: "/Map/GetAssetDetails",
            //url: '@Url.Action("GetAssetDetails", "Map")',
            contentType: "application/json; charset=utf-8",
            data: "{AssetID:'" + assetId + "',Buffer:'" + buffer + "',ClickedLat:'" + clickedLat + "',ClickedLng:'" + clickedLng + "'}",
            success: function (result, status, xhr) {
                //alert(result);debugger;
                var jsonArray = $.parseJSON(result);
                for (var i = 0; i < jsonArray.length; i++) {
                    var point = new ol.geom.Point([jsonArray[i].Longitude, jsonArray[i].Latitude]).transform('EPSG:4326', 'EPSG:3857');
                    var get_desc = ParseJSONResponseToHTML(jsonArray[i], MapLayerTypes.VectorLayer, assetId);
                    var featurePoint = new ol.Feature({
                        type: 'click',
                        desc: get_desc,
                        geometry: point
                    });
                    featurePoint.setStyle(myStyle);
                    vectorSource.addFeature(featurePoint);
                }
                map.getView().fit(vectorSource.getExtent(), map.getSize());
            },
            error: function (xhr, status, error) {
                $("#dataDiv").html("Result: " + status + " " + error + " " + xhr.status + " " + xhr.statusText);
                $('#myModal').modal('show');
            }
        });
    }
    else {
        alert('Click on map');
    }
}

function showAssetQuery() {
    vectorSource.clear();
    var assetId = $("#AllAssetsQuery").val();
    var assetColumn = $('#assetValueBBSRQuery').val();
    var assetValue = $('#txtAssetQueryValue').val()
    var myStyle = new ol.style.Style({
        image: new ol.style.Circle({
            radius: 7,
            fill: new ol.style.Fill({ color: 'olive' }),
            stroke: new ol.style.Stroke({
                color: [255, 0, 0], width: 2
            })
        })
    });
    $.ajax({
        type: "POST",
        //url: "/Map/GetAssetDetailsQuery",
        url: '@Url.Action("GetAssetDetailsQuery", "Map")',
        contentType: "application/json; charset=utf-8",
        data: "{AssetID:'" + assetId + "',AssetColumn:'" + assetColumn + "',AssetCriteria:'" + setCriteriaVal + "',AssetValue:'" + assetValue + "'}",
        success: function (result, status, xhr) {
            //alert(result);debugger;
            var jsonArray = $.parseJSON(result);
            for (var i = 0; i < jsonArray.length; i++) {
                var point = new ol.geom.Point([jsonArray[i].Longitude, jsonArray[i].Latitude]).transform('EPSG:4326', 'EPSG:3857')
                var get_desc = ParseJSONResponseToHTML(jsonArray[i], MapLayerTypes.VectorLayer);
                var featurePoint = new ol.Feature({
                    type: 'click',
                    desc: get_desc,
                    geometry: point
                });
                featurePoint.setStyle(myStyle);
                vectorSource.addFeature(featurePoint);
            }
            map.getView().fit(vectorSource.getExtent(), map.getSize());
        },
        error: function (xhr, status, error) {
            $("#dataDiv").html("Result: " + status + " " + error + " " + xhr.status + " " + xhr.statusText);
            $('#myModal').modal('show');
        }
    });
}

var setCriteriaVal = "";
function setCriteria(ele) {
    setCriteriaVal = ele.id;
    //alert(setCriteriaVal);
}
function bindAssetValue(sel) {
    var serviceArray; $('#assetValueBBSRQuery').empty();
    switch ($(sel).val()) {
        case "W04":
            serviceArray = [["-Select-", "0"], ["Installation year", "yr_instl"], ["Efficiency", "efficiency"], ["Manual/Auto", "man_auto"]];
            break;
        case "W40":
            serviceArray = [["-Select-", "0"], ["Location", "location"], ["Type", "type"], ["Material", "material"], ["Construction year", "yr_const"], ["Capacity", "capacity"], ["Inflow Diameter", "inflow_dia"], ["Outflow Diameter", "outflow_dia"]];
            break;
        case "W06":
            serviceArray = [["-Select-", "0"], ["Installation year", "yr_instl"], ["Diameter Size", "dia_size"], ["Manual/Auto", "man_auto"]];
            break;
        default:
            break;
    }
    for (i = 0; i < serviceArray.length; i++) {
        var data = '<option value=' + serviceArray[i][1] + '>' + serviceArray[i][0] + '</option>'
        $('#assetValueBBSRQuery').append(data);
    }
}

var clearMap = function () {
    vectorSource.clear();
    bufferRequired = false;
};