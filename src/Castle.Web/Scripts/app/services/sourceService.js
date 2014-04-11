// **************
// Source Service
// **************
castle.app.services.factory('sourceService', ['$http', function ($http) {

    var urls = {
        getFiles:                   "/api/v1/source?path={path}",
        getFileContent:             "/api/v1/source/raw?path={path}",
        recentRepositoryHistory:    "/api/v1/source/repository/{key}/history/recent",
        recentProjectHistory:       "/api/v1/source/project/{key}/history/recent"
    };

    return {

        getFiles : function(path, callback){
            var url = urls.getFiles.replace('{path}', path);
            $http.get(url).success(callback);
        },

        getFileContent: function(path, callback){
            var url = urls.getFileContent.replace('{path}', path);
            $http.get(url).success(callback);
        },

        getRecentRepositoryHistory: function (key, callback) {
            var url = urls.recentRepositoryHistory.replace('{key}', key);
            $http.get(url).success(callback);
        },

        getRecentProjectHistory: function (key, callback) {
            var url = urls.recentProjectHistory.replace('{key}', key);
            $http.get(url).success(callback);
        }
    };
}]);