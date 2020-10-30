$(document).ready(function () {
    $("#student_table").DataTable({
        data: data
    });
});

var data = [
    [
        "John Lowery",
        "n00000001",
        "1/1/2000",
        "McKay"
    ],
    [
        "Other Person",
        "n00000002",
        "2/2/2000",
        "Other Source"
    ]
]