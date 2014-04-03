// ***************
// home page controller
// ***************
castle.app.controllers.controller('HomeController', ['$scope', '$http', 'sourceService', function ($scope, $http, sourceService) {

    $scope.loading = true;
    $scope.history = [];

    $scope.init = function () {

        sourceService.getRecentHistory(function (data) {
            delayPush(data, $scope.history);
            $scope.loading = false;
        });

    };

    // make it look pretty when it loads
    var delayPush = function (source, dest) {
        angular.forEach(source, function (value, key) {
            setTimeout(function () {
                dest.push(value);
                $scope.$apply();
            }, key * 50);
        });
    };

    $scope.init();
}]);