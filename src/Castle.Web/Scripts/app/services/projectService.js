// **************
// Project Service
// **************
castle.app.services.factory('projectService', ['$http', function ($http) {

    var urls = {
        getProject: "/api/v1/project/{key}",
        deleteProject: "/api/v1/project/{key}/delete",
        updateProject: "/api/v1/project/{key}/update"
    };

    return {

        getProject : function(key, callback){
            var url = urls.getProject.replace('{key}', key);
            $http.get(url).success(callback);
        },

        deleteProject: function (key, callback) {
            var url = urls.deleteProject.replace('{key}', key);
            $http.post(url).success(callback);
        },

        updateProject: function (project, callback) {
            var url = urls.updateProject.replace('{key}', project.Key);
            $http.post(url, project).success(callback);
        }
    };
}]);