$(document).ready(function () {
    $("#report_table").DataTable({
        dom: 'Bfrtip',
        buttons: [
                'copy', 'csv', 'excel', 'pdf'
        ],
        exportOptions: {
            columns: ':visible:not(.notexport)'
        }
    });
});
