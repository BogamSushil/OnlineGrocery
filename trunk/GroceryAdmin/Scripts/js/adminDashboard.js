function Client() {
    var url = getSiteUrl("/Client/ManageClient");
    window.location.replace(url);
}

function report() {
    var url = getSiteUrl("/AdvantexeReport/ClientReport");
    window.location.replace(url);
}