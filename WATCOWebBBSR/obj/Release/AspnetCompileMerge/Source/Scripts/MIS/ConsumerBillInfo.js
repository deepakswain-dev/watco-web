$(function () {
    //===============================//
    $('#dtPickConsumerBillInfo').datepicker();
    //View button click for consumer billing info to show data
    $('#btnViewConsumerBillInfo').click(function () {
        showConsumerBillInformation();
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

function showConsumerBillInformation() {
    $.ajax({
        type: "POST",
        url:'/MIS/GetConsumerBillingDetails',
        //url: '@Url.Action("GetConsumerBillingDetails", "MIS")',
        contentType: "application/json; charset=utf-8",
        data: "",
        success: function (result, status, xhr) {
            //alert(result);debugger;
            var jsonArray = $.parseJSON(result);
            if (jsonArray.length > 0) {
                var tableHTML = '<table class="table table-bordered table-striped table-hover watcoTable" id="tblConsumerBillInfo">';
                tableHTML += '<thead>';
                tableHTML += '<tr>';
                tableHTML += '<th>Sl#</th>';
                tableHTML += '<th>Consumer Name</th>';
                tableHTML += '<th>Consumer No</th>';
                tableHTML += '<th>House Type</th>';
                tableHTML += '<th>Connection Status</th>';
                tableHTML += '<th>Payment Status</th>';
                tableHTML += '<th>Bill Amount</th>';
                tableHTML += '<th>Map View</th>';
                tableHTML += '</tr>';
                tableHTML += '</thead>';
                tableHTML += '<tbody>';
                for (var i = 0; i < jsonArray.length; i++) {
                    tableHTML += '<tr>';
                    var slNo = parseInt(i) + 1;
                    tableHTML += '<td>' + slNo + '</td><td>' + jsonArray[i].ConsumerName + '</td><td>' + jsonArray[i].ConsumerNo + '</td>';
                    tableHTML += '<td>' + jsonArray[i].HouseType + '</td><td>' + jsonArray[i].ConnectionStatus + '</td><td>' + jsonArray[i].PaymentStatus + '</td><td>0.00</td>';
                    tableHTML += '<td><a class="viewMapConsumerBillInfo" style="cursor:pointer;" data-toggle="modal" data-target="#mapModal" name="' + jsonArray[i].Longitude + "," + jsonArray[i].Latitude + '">Map</a></td>';
                    tableHTML += '</tr>';
                }
                tableHTML += '</tbody>';
                tableHTML += '</table>';
                //bind dynamic HTML to table container
                $('#divTableDataConsumerBillInfo').html(tableHTML);
                //========================================================///
                //datatable features
                $('#tblConsumerBillInfo').DataTable();
                //var tableConsumerBill = $('#tblConsumerBillInfo').DataTable({
                //    //pageLength: 9,
                //    lengthChange: false,
                //    buttons: ['excel', 'pdf', 'colvis']
                //});
                //tableConsumerBill.buttons().container()
                //    .appendTo('#tblConsumerBillInfo_wrapper .col-sm-6:eq(0)');
                //map link click initialize for consumer information
                $('.viewMapConsumerBillInfo').click(function () {
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
                $('#divTableDataConsumerBillInfo').html("<p class='text-center'><font color='red'><b>No Data !</b></font></p>");
            }
        },
        error: function (xhr, status, error) {
            $("#divTableDataConsumerBillInfo").html("Result: " + status + " " + error + " " + xhr.status + " " + xhr.statusText);
        }
    });
}