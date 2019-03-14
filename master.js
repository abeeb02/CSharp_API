// obj is the js array of sql parameter names & parameters values as passed in from dom objects
// cmd is the name of the sql stored proc to be executed in the database (which will update, retrieve, or both)
// ex) CallData([["@SheetID", parseInt($('span[id$="sheetIDLbl"]').text())]], "sp_SelectIdData"))
function CallData(obj, cmd) {
    var data = "";
    $.ajax({
        type: 'POST',
        url: 'https://localhost/Object.aspx/JavascriptDataAccessor',
        dataType: 'json',
        async: false,
        data: JSON.stringify({ paramArray: obj, command: cmd }),
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            data = $.parseJSON(result.d);
        }, error: function (xhr, status, error) {
            alert(error);
        }
    });
    return data;
}