function Callback(response){
    var responseObj = JSON.parse(response);
    var coordinates = responseObj.results["0"].geometry.location;
    var input = document.getElementById("crdInput");
    input.value = JSON.stringify(coordinates)
}

function APICaller(){
    var apiKey = "AIzaSyAweW76XxiA6Tsd66YnWbIiOhqTmWwBxOY";
    var apiURL = "https://maps.googleapis.com/maps/api/geocode/";
    var outputFormat = "json";
    var address = "";

    this.GetCoordinates = function(){
        var xhr=new XMLHttpRequest();
        xhr.onreadystatechange=function() {
            if (xhr.readyState == 4) {
                if (xhr.status == 200) {
                    Callback(xhr.responseText);
                }
            }
        };
        address = document.getElementById("adr").value;
        var params = "?address=" + address;
        params += "&key=" + apiKey;
        xhr.open("GET", apiURL + outputFormat + params,true);
        xhr.send(null);
    }
}

var GoogleAPI = new APICaller();
var timeout = null;

function AdrChangeHandler(){
    clearTimeout(timeout);
    timeout = setTimeout(function () {
        GoogleAPI.GetCoordinates();
    }, 500);
}
