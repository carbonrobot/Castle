// ***************
// Project controller
// ***************
castle.app.controllers.controller('ProjectController', ['$scope', '$http', 'sourceService', function ($scope, $http, sourceService) {

    $scope.loadingHistory = false;
    $scope.history = [];
    $scope.files = [];
    $scope.projectKey = '';
    $scope.branch = '';
    $scope.path = '';

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