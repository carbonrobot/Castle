// **************
// Project Service
// **************
castle.app.services.factory('projectService', ['$http', function ($http) {

    var urls = {
        getFiles:                   "/api/v1/project/{key}",
    };

    return {

        getProject : function(key, callback){
            var url = urls.getProject.replace('{key}', key);
            $http.get(url).success(callback);
        },
    };
}]);