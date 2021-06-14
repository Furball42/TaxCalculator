$(document).ready(function () {
    $('#calcsTable').DataTable({
        ajax: {
            url: "http://localhost:5000/Calculation/GetAllForDatables/",
            type: "GET"
        },
        columns: [
            { data: 'postalCode' },
            { data: 'annualIncome', className: "text-right", render: $.fn.dataTable.render.number(' ', '.', 2, '') },
            { data: 'calculatedTax', className: "text-right", render: $.fn.dataTable.render.number(' ', '.', 2, '') },
            { data: 'dateTimeCreated' },
        ],
        columnDefs: [{
            targets: [3], render: function (data) {
                return moment(data).format('D MMMM YYYY');
            }
        }],
        processing: true,
        serverSide: false,
        ordering: false,
        paging: true,
        searching: false,
        lengthChange: false,
        language: {
            "decimal": ",",
            "thousands": " "
        }
    });
});