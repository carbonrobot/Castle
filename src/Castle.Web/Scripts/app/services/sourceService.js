// **************
// Source Service
// **************
castle.app.services.factory('sourceService', ['$http', function ($http) {

    var urls = {
        recentHistory: "/api/v1/source/{repositoryKey}/history/recent"
    };

    return {
        getRecentHistory: function (repositoryKey, callback) {
            var url = urls.recentHistory.replace('{repositoryKey}', repositoryKey);
            $http.get(url).success(callback);
        }
    };
}]);