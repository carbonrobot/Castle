// ***************
// Project controller
// ***************
castle.app.controllers.controller('ProjectController', ['$scope', '$http', 'sourceService', 'projectService', function ($scope, $http, sourceService, projectService) {

    $scope.loadingProject = true;
    $scope.loadingFiles = false;
    $scope.loadingHistory = false;
    $scope.loadingReadme = false;
    $scope.history = [];
    $scope.files = [];
    $scope.projectKey = '';
    $scope.path = '';
    $scope.openFile = '';

    $scope.init = function (key, path) {
        $scope.projectKey = key;
        $scope.path = path;

        $scope.getFiles();
        $scope.getReadme();
    };

    // get project information
    $scope.getProject = function () {
        $scope.loadingProject = true;
        projectService.getProjectByKey(key, function (data) {

        });
    };

    // load files at this path
    $scope.getFiles = function () {
        $scope.loadingFiles = true;
        sourceService.getFiles($scope.path, function (data) {
            delayPush(data, $scope.files);
            $scope.loadingFiles = false;
        });
    };

    // load a readme file if available
    $scope.getReadme = function () {
        $scope.loadingReadme = true;
        sourceService.getFileContent($scope.path + '/README.md', function (data) {

            require(['/scripts/vendor/marked.js'], function (marked) {
                $scope.openFile = marked(data);
                $scope.loadingReadme = false;
            });
        });
    };

    // get the commit history
    $scope.getRecentHistory = function () {
        $scope.loadingHistory = true;
        sourceService.getRecentProjectHistory($scope.projectKey, function (data) {
            delayPush(data, $scope.history);
            $scope.loadingHistory = false;
        });
    };

    // make it look pretty when it loads
    var delayPush = function (source, dest) {
        angular.forEach(source, function (value, key) {
            setTimeout(function () {
                dest.push(value);
                $scope.$apply();
            }, key * 25);
        });
    };
}]);