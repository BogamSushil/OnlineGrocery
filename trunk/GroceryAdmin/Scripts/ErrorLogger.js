var loggError = function (error) {
    $.ajax({
        type: "POST",
        url: '@Url.Action("LoggClientSideErrors", "AdvantexeAccount")',
        data: { error: error }
    });
};

var unhandledException = function (error) {
    onError();   
};