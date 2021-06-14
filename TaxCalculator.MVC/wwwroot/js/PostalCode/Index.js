var dataTable;

$(document).ready(function () {

    $('#warning').hide();

    dataTable = $('#postalCodeTable').DataTable({
        ajax: {
            url: "http://localhost:5000/PostalCode/GetAllForDatables",
            type: "GET"
        },
        columns: [
            { data: 'description' },
            { data: 'calculationTypeDescription'},
        ],
        processing: true,
        serverSide: false,
        ordering: false,
        paging: true,
        searching: true,
        lengthChange: false,
    });

    $.ajax({
        type: "GET",
        url: "http://localhost:5000/PostalCode/GetCalculationTypes",
        data: "{}",
        success: function (data) {
            var s = '<option value="-1">Please Select Calculation Type</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].id + '">' + data[i].description + '</option>';
            }
            $("#ddlCalculationType").html(s);
        }
    });

});

$('#btnReset').on('click', function () {
    reset();
});

$('#btnSubmit').on('click', function () {

    var postalCode = $('#txtPostalCode').val();
    var type = $('#ddlCalculationType').find(":selected").val();

    if (postalCode == '' || type == -1)

        $('#warning').show();

    else {

        var postalCodeObj = new Object();
        postalCodeObj.Description = postalCode;
        postalCodeObj.CalculationType = parseInt(type);
        postalCodeObj.ReferenceId = 0;
        postalCodeObj.Id = 0;

        $.ajax({
            url: "http://localhost:5000/PostalCode/",
            type: "POST",
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify(postalCodeObj),
            async: true,
            success: function (data) {

                if (data == true) {
                    dataTable.ajax.reload();
                    $('#messageModalText').text("Postal Code Added");
                    $('#messegeModalHeader').html("Add");
                    $('.modal-header').addClass('bg-success');
                    $('#messageModal').modal('show');
                }

                reset();
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {

                var json = XMLHttpRequest.responseJSON.error;
                $('#messageModalText').text(json);
                $('#messegeModalHeader').html("Warnings");
                $('.modal-header').addClass('bg-danger');
                $('#messageModal').modal('show');

            }
        });
    }
});

function reset() {
    $('#warning').hide();
    $('#txtPostalCode').val('');
    $("#ddlCalculationType").val(-1);
}