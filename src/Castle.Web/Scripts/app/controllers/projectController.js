// ***************
// Project controller
// ***************
castle.app.controllers.controller('ProjectController', ['$scope', '$http', 'sourceService', function ($scope, $http, sourceService) {

    $scope.loadingFiles = false;
    $scope.loadingHistory = false;
    $scope.loadingReadme = false;
    $scope.history = [];
    $scope.files = [];
    $scope.projectKey = '';
    $scope.branch = '';
    $scope.path = '';
    $scope.openFile = '';

    $scope.init = function (key, path) {
        $scope.projectKey = key;
        $scope.path = path;

        $scope.getFiles();
    };

    // load files at this path
    $scope.getFiles = function () {
        $scope.loadingFiles = true;
        sourceService.getFiles($scope.path, $scope.branch, function (data) {
            delayPush(data, $scope.files);
            $scope.loadingFiles = false;
        });
    }

    // load a readme file if available
    $scope.getFiles = function () {
        $scope.loadingReadme = true;
        sourceService.getFile($scope.path, $scope.branch, function (data) {
            
            var marked = require('marked');
            $scope.openFile = marked(data);

            $scope.loadingReadme = false;
        });
    }

    // get the commit history
    $scope.getRecentHistory = function () {
        $scope.loadingHistory = true;
        sourceService.getRecentProjectHistory($scope.projectKey, function (data) {
            delayPush(data, $scope.history);
            $scope.loadingHistory = false;
        });
    }

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