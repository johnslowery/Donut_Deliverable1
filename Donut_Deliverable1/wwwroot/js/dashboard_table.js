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
                else if (column == 2 || column == 3) {
                    return node.firstChild.tagName === "INPUT" ?
                        node.firstElementChild.value :
                        data;
                }
                else return data
            }
        }
    }
};

var table = $(document).ready(function () {
    $("#dashboard_table").DataTable({
        dom: 'Bfrtip',
        paging: false,
        buttons: [
            'copy', $.extend(true, {}, buttonCommon, {
                extend: "csv"
            }), $.extend(true, {}, buttonCommon, {
                extend: "excel"
            }), $.extend(true, {}, buttonCommon, {
                extend: "pdf"
            }), $.extend(true, {}, buttonCommon, {
                extend: "print"
            }),
        ]
    });
});
