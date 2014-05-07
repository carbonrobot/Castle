// ***************
// History controller
// ***************
castle.app.controllers.controller('HistoryController', ['$scope', '$http', '$route', 'sourceService', function ($scope, $http, $route, sourceService) {

    $scope.loadingHistory = false;
    $scope.history = [];
    $scope.path = '';

    $scope.init = function (path) {
        // if no path was passed in, use the project path from the parent scope
        if (path == undefined) {
            $scope.path = $scope.projectPath;
        }
        else {
            $scope.path = path;
        }
        $scope.getRecentHistory();
    };

    // get the commit history
    $scope.getRecentHistory = function () {
        $scope.loadingHistory = true;
        sourceService.getPathHistory($scope.path, 30, function (data) {
            castle.app.utils.delayPush($scope, data, $scope.history);
            $scope.loadingHistory = false;
        });
    };

    // initialize using path
    $scope.init($route.current.params.path);

}]);