var buttonCommon = {
    exportOptions: {
        columns: ':visible:not(.notexport)',
        format: {
            body: function (data, row, column, node) {
                // if it is select
                if (column == 4 || column == 5 || column == 6) {
                    return $(data).find("option:selected").text()
                }
                else if(column == 7){
                    return $(data).text()
                }
                else return data
            }
        }
    }
};

$(document).ready(function () {
    $("#dashboard_table").DataTable({
        dom: 'Bfrtip',
        buttons: [
            'copy', $.extend(true, {}, buttonCommon, {
                extend: "csv"
            }), $.extend(true, {}, buttonCommon, {
                extend: "excel"
            }), $.extend(true, {}, buttonCommon, {
                extend: "pdf"
            })
        ]
    });
});
