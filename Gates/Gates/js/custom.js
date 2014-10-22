var _dictionary = {};
//Returns the html contents of a url/html page. 
//Stores results in dictionary so that the next time it is called it uses cache
function syncload(urlToLoad) {
    debugger;
    if (_dictionary[urlToLoad]) {
        return _dictionary[urlToLoad];
    }
    else {
        var result;
        jQuery.ajax({
            url: urlToLoad,
            success: function (res) {
                result = res;
            },
            async: false
        });
    }
    _dictionary[urlToLoad] = result;
    return result;
}

function asyncload(urlToLoad, callback) {
    if (_dictionary[urlToLoad]) {
        callback(_dictionary[urlToLoad]);
    }
    else {
        var result;
        jQuery.ajax({
            url: urlToLoad,
            success: function (res) {
                callback(res);
            }
           
        });
    }
    _dictionary[urlToLoad] = result;
    return result;
}