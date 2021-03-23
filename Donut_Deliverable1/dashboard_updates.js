function updateExcused(nNumber, presentDateTime, excOriginal, excUpdate) {
    if (excOriginal == excUpdate.val) {
        console.log("hi!");
        return;
    }
    else {
        console.log("hi!");
        var jsonData = JSON.stringify({
            "nNumber": nNumber,
            "presentDateTime": presentDateTime,
            "update": excUpdate.val
        });
        $.ajax({
            type:"POST",
            url: `/Admin/UpdateExcused`,
            data: jsonData,
            dataType: "json",
            contentType: "application/json",
            success: function () {
                alert("Data has been updated successfully.");
            },
            error: function () {
                alert("Error while updating data");
            }
        });
    }
}
function updateTardy(nNumber, presentDateTime, tarOriginal, tarUpdate) {
    if (tarOriginal == tarUpdate.val) {
        console.log("hi!");
        return;
    }
    else {
        console.log("hi!");
        var jsonData = JSON.stringify({
            "nNumber": nNumber,
            "presentDateTime": presentDateTime,
            "update": tarUpdate.val
        });
        $.ajax({
            type:"POST",
            url: `Admin/UpdateTardy`,
            data: jsonData,
            dataType: "json",
            contentType: "application/json",
            success: function () {
                alert("Data has been updated successfully.");
            },
            error: function () {
                alert("Error while updating data");
            }
        });
    }
}
function updateAbsent(nNumber, presentDateTime, absOriginal, absUpdate) {
    if (absOriginal == absUpdate.val) {
        console.log("hi!");
        return;
    }
    else {
        console.log("hi!");
        var jsonData = JSON.stringify({
            "nNumber": nNumber,
            "presentDateTime": presentDateTime,
            "update": absUpdate.val
        });
        $.ajax({
            type:"POST",
            url: `/Admin/UpdateAbsent`,
            data: jsonData,
            dataType: "json",
            contentType: "application/json",
            success: function () {
                alert("Data has been updated successfully.");
            },
            error: function () {
                alert("Error while updating data");
            }
        });
    }
}
function updateNote(nNumber, presentDateTime, noteOriginal, noteUpdate) {
    if (noteOriginal == noteUpdate.val) {
        console.log("hi!");
        return;
    }
    else {
        console.log("hi!");
        var jsonData = JSON.stringify({
            "nNumber": nNumber,
            "presentDateTime": presentDateTime,
            "update": noteUpdate.val
        });
        $.ajax({
            type:"POST",
            url: `/Admin/UpdateNote`,
            data: jsonData,
            dataType: "json",
            contentType: "application/json",
            success: function () {
                alert("Data has been updated successfully.");
            },
            error: function () {
                alert("Error while updating data");
            }
        });
    }
}

