$(document).ready(function () {
    //This function to equally divide the Top Menu   
    var divHeader = $('#divHeader1 > div');
    if (divHeader)
    {     
        var eqWidth = 905 / $(divHeader).length;
        $(divHeader).each(function () {
            $(this).css("width", Math.floor(eqWidth));
        });
    }   
});