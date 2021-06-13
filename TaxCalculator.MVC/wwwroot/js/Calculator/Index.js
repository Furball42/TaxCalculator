$(document).ready(function () {

    $('#warning').hide();
    $('#results').hide();
    $('#levelTable').hide();


});

$("#txtIncome").change(function () {
    var val = $("#txtIncome").val();
    $("#txtIncome").val(parseFloat(val).toFixed(2).replace(',', '.'));
});

$('#btnReset').on('click', function () {

    $('#totalTaxValue').text("");
    $('#totalTaxPercentage').text("");
    $('#incomeBefore').text("");
    $('#incomeAfter').text("");
    $('#monthlyTaxes').text("");
    $('#txtIncome').val('');
    $('#txtPostalCode').val('');

    $('#warning').hide();
    $('#results').hide();
    $('#levelTable tbody tr').remove();
    $('#levelTable').hide();
});

$('#btnSubmit').on('click', function () {
    $('#levelTable tbody tr').remove();
    $('#levelTable').hide();

    var income = $('#txtIncome').val();
    var postalCode = $('#txtPostalCode').val();

    if (income == '' || postalCode == '')
        $('#warning').show();
    else {        

        $.ajax({
            url: "http://localhost:5000/Calculation/DoTaxCalculation/" + income + "/" + postalCode,
            type: "GET",
            async: true,
            success: function (data) {

                $('#results').show();

                $('#totalTaxValue').text(data.totalTaxes.toFixed(2));
                $('#totalTaxPercentage').text(data.totalTaxPercentage.toFixed(2) + "%");
                $('#incomeBefore').text(data.originalIncome.toFixed(2));
                $('#incomeAfter').text(data.incomeAfterTax.toFixed(2));
                $('#monthlyTaxes').text(data.totalMonthlyTaxes.toFixed(2));

                var levelList = data.progressiveTaxByLevel;

                if (levelList.length > 0) {
                    $('#levelTable').show();
                    for (var x = 0; x < levelList.length; x++) {

                        var min = levelList[x].min.toFixed(2);
                        var max = levelList[x].max.toFixed(2);
                        var rate = levelList[x].rate.toFixed(2);
                        var taxes = levelList[x].levelTax.toFixed(2);

                        $('#levelTable tbody').append("<tr><td>" + min + "</td><td>" + max + "</td><td>" + rate + "</td><td>" + taxes + "</td></tr>");
                    }
                }
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