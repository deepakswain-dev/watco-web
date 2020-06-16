$(function () {
    //only numeric textbox
    jQuery('.numeric').keyup(function () {
        this.value = this.value.replace(/[^0-9\.]/g, '');
    });
    //View button click for consumer info to show data
    $('#btnViewConsumerInfo').click(function () {
        showConsumerInformation();
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

function showConsumerInformation() {
    var mtrFilterValue = $('.mtrNonMtr').val();
    $.ajax({
        type: "POST",
        url: '/MIS/GetConsumerDetails',
        //url: '@Url.Action("GetConsumerDetails", "MIS")',
        contentType: "application/json; charset=utf-8",
        data: "{mtrFilter:'" + mtrFilterValue + "'}",
        success: function (result, status, xhr) {
            //alert(result);debugger;
            var jsonArray = $.parseJSON(result);
            if (jsonArray.length > 0) {
                var tableHTML = '<table class="table table-bordered table-hover watcoTable" id="tblConsumerInfo">';
                tableHTML += '<thead>';
                tableHTML += '<tr>';
                tableHTML += '<th>Sl#</th>';
                tableHTML += '<th>Consumer Name</th>';
                tableHTML += '<th>Consumer No</th>';
                //tableHTML += '<th>Pilot Zone</th>';
                tableHTML += '<th>Address</th>';
                tableHTML += '<th>Mobile No</th>';
                tableHTML += '<th>Consumer Meter No</th>';
                tableHTML += '<th>Connection Type</th>';
                tableHTML += '<th>Application Status</th>';
                tableHTML += '<th>House Type</th>';
                tableHTML += '<th>Map View</th>';
                tableHTML += '</tr>';
                tableHTML += '</thead>';
                tableHTML += '<tbody>';
                for (var i = 0; i < jsonArray.length; i++) {
                    tableHTML += '<tr>';
                    var slNo = parseInt(i) + 1;
                    tableHTML += '<td>' + slNo + '</td><td>' + jsonArray[i].ConsumerName + '</td><td>' + jsonArray[i].ConsumerNo + '</td>';
                    //tableHTML += '<td>' + $("#pilotZone option:selected").text()+'</td>';
                    tableHTML += '<td>' + jsonArray[i].Address + '</td><td>' + jsonArray[i].MobileNo + '</td><td>' + jsonArray[i].ConsumerMeterNo + '</td>';
                    tableHTML += '<td>' + jsonArray[i].ConnectionType + '</td><td>' + jsonArray[i].ApplicationStatus + '</td><td>' + jsonArray[i].HouseType + '</td>';
                    tableHTML += '<td><a onclick="viewMap(this)" style="cursor:pointer;" data-toggle="modal" data-target="#mapModal" name="' + jsonArray[i].Longitude + "," + jsonArray[i].Latitude + '">Map</a></td>';
                    tableHTML += '</tr>';
                }
                tableHTML += '</tbody>';
                tableHTML += '</table>';
                //bind dynamic HTML to table container
                $('#divTableDataConsumerInfo').html(tableHTML);
                //========================================================//
                //data table features
                $('#tblConsumerInfo').DataTable();
                //var tableConsumer = $('#tblConsumerInfo').DataTable({
                //    //pageLength: 9,
                //    lengthChange: false,
                //    buttons: ['excel', 'pdf', 'colvis']
                //});
                //tableConsumer.buttons().container()
                //    .appendTo('#tblConsumerInfo_wrapper .col-sm-6:eq(0)');
                //========================================================///
            }
            else {
                $('#divTableDataConsumerInfo').html("<p class='text-center'><font color='red'><b>No Data !</b></font></p>");
            }
        },
        error: function (xhr, status, error) {
            $("#divTableDataConsumerInfo").html("Result: " + status + " " + error + " " + xhr.status + " " + xhr.statusText);
        }
    });
}

function viewMap(ele) {
    var rowId = $(ele).closest("tr").find("td").eq(1).html();
    var latLong = $(ele).attr('name').trim().split(',');
    //alert(latLong[0]+","+latLong[1]);
    $('#mapModal').on('shown.bs.modal', function () {
        if (modalMap == null || modalMap == undefined) { LoadModalMap(); }
        LoadMarkerToModalMap(latLong);
    })
}