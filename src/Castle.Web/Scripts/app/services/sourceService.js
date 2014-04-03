// **************
// Source Service
// **************
castle.app.services.factory('sourceService', ['$http', function ($http) {

    var urls = {
        recentHistory: "/api/v1/source/history/recent"
    };

    return {
        getRecentHistory: function (callback) {
            $http.get(urls.recentHistory).success(callback);
        }
    };
}]);