$(function () {
    //only numeric textbox
    jQuery('.numeric').keyup(function () {
        this.value = this.value.replace(/[^0-9\.]/g, '');
    });
    //View button click for asset info to show data
    $('#btnViewAssetInfo').click(function () {
        showAssetInformation();
    });
});

//debugger;
//=======ajax response indicator===========//
$(document).ajaxSend(function (event, request, settings) {
    $('.loading-indicator').show();
    $('.btn-Report').prop('disabled', true);
});
$(document).ajaxComplete(function (event, request, settings) {
    $('.loading-indicator').hide();
    $('.btn-Report').prop('disabled', false);
});

function showAssetInformation() {
    var assetName = $('.bbsrasset').val();
    var assetYear = '1995';
    $.ajax({
        type: "POST",
        url:'/MIS/GetAssetDetails',
        //url: '@Url.Action("GetAssetDetails", "MIS")',
        contentType: "application/json; charset=utf-8",
        data: "{AssetName:'" + assetName + "'}",
        success: function (result, status, xhr) {
            //alert(result);debugger;
            var jsonArray = $.parseJSON(result);
            if (jsonArray.length > 0) {
                var tableHTML = '<table class="table table-bordered table-striped table-hover watcoTable" id="tblAssetInfo">';
                tableHTML += '<thead>';
                tableHTML += '<tr>';
                tableHTML += '<th>Sl#</th>';
                tableHTML += '<th>Name Of Asset</th>';
                tableHTML += '<th>Asset Code</th>';
                tableHTML += '<th>Type</th>';
                tableHTML += '<th>Material Type</th>';
                //tableHTML += '<th>Location [Zone]</th>';
                tableHTML += '<th>Make</th>';
                tableHTML += '<th>Dia Size</th>';
                tableHTML += '<th>Capacity</th>';
                tableHTML += '<th>Installation Year</th>';
                tableHTML += '<th>Shape</th>';
                tableHTML += '<th>Last Repair Date</th>';
                tableHTML += '<th>Map View</th>';
                tableHTML += '</tr>';
                tableHTML += '</thead>';
                tableHTML += '<tbody>';
                for (var i = 0; i < jsonArray.length; i++) {
                    tableHTML += '<tr>';
                    var slNo = parseInt(i) + 1;
                    tableHTML += '<td>' + slNo + '</td><td>' + jsonArray[i].AssetName + '</td><td>' + jsonArray[i].AssetCode + '</td>';
                    tableHTML += '<td>' + jsonArray[i].AssetType + '</td><td>' + jsonArray[i].AssetMaterial + '</td><td>' + jsonArray[i].AssetMake + '</td>';
                    tableHTML += '<td>' + jsonArray[i].AssetDiaSize + '</td><td>' + jsonArray[i].AssetCapacity + '</td>';
                    tableHTML += '<td>' + jsonArray[i].AssetInstallYear + '</td><td>' + jsonArray[i].AssetShape + '</td>';
                    tableHTML += '<td>' + jsonArray[i].AssetLastRepairDate + '</td>';
                    tableHTML += '<td><a class="viewMapAssetInfo" style="cursor:pointer;" data-toggle="modal" data-target="#mapModal" name="' + jsonArray[i].Longitude + "," + jsonArray[i].Latitude + '">Map</a></td>';
                    tableHTML += '</tr>';
                }
                tableHTML += '</tbody>';
                tableHTML += '</table>';
                //bind dynamic HTML to table container
                $('#divTableDataAssetInfo').html(tableHTML);
                //========================================================///
                //datatable operations initialize for consumer information
                $('#tblAssetInfo').DataTable();
                //map link click initialize for consumer information
                $('.viewMapAssetInfo').click(function () {
                    //var rowIndex = $(this).closest("tr")[0].rowIndex;
                    var rowId = $(this).closest("tr").find("td").eq(1).html();
                    var latLong = $(this).attr('name').trim().split(',');
                    //alert(latLong[0]+","+latLong[1]);
                    $('#mapModal').on('shown.bs.modal', function () {
                        if (modalMap == null || modalMap == undefined) { LoadModalMap(); }
                        LoadMarkerToModalMap(latLong);
                    })
                });
                //========================================================///
            }
            else {
                $('#divTableDataAssetInfo').html("<p class='text-center'><font color='red'><b>No Data !</b></font></p>");
            }
        },
        error: function (xhr, status, error) {
            $("#divTableDataAssetInfo").html("Result: " + status + " " + error + " " + xhr.status + " " + xhr.statusText);
        }
    });
}