var ViewModel = function () {
    var self = this;
    self.info = ko.observableArray();
    self.error = ko.observable();

    var infoUri = '/api/info/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getSearchResult() {
        ajaxHelper(infoUri, 'GET').done(function (data) {
            self.info(data);
        });
    }

    // Fetch the initial data.
    getInfo();
};

ko.applyBindings(new ViewModel());