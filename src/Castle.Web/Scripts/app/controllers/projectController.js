// ***************
// Project controller
// ***************
castle.app.controllers.controller('ProjectController', ['$scope', '$http', 'sourceService', function ($scope, $http, sourceService) {

    $scope.loadingHistory = true;
    $scope.history = [];
    $scope.projectKey = '';

    $scope.init = function (key) {
        $scope.projectKey = key;
    };

    $scope.getRecentHistory = function () {
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