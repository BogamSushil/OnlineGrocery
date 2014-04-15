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

    $("#lhsPanelBar").kendoPanelBar({
        expandMode: "single"
    });
});

function addFilterDiv(_thiz)
{
    var thiz = $(_thiz);
    var top = Math.floor(thiz.position().top);
    var left = Math.floor(thiz.position().left) + 300;
    $('#divLHSContainer').css("top", top);
    $('#divLHSContainer').css("left", left);
    
}