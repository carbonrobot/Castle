// ***************
// Repository controller
// ***************
castle.app.controllers.controller('RepositoryController', ['$scope', '$http', 'sourceService', function ($scope, $http, sourceService) {

    $scope.loadingHistory = true;
    $scope.history = [];
    $scope.repositoryKey = '';

    $scope.init = function (repositoryKey) {

        $scope.repositoryKey = repositoryKey;

        sourceService.getRecentHistory(repositoryKey, function (data) {
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