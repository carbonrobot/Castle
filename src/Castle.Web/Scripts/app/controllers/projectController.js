// ***************
// Project controller
// ***************
castle.app.controllers.controller('ProjectController', ['$scope', '$http', 'projectService', function ($scope, $http, projectService) {

    $scope.loadingProject = true;
    $scope.loadingHistory = false;
    $scope.history = [];
    $scope.projectKey = '';
    $scope.projectPath = '';

    $scope.init = function (key, path) {
        $scope.projectKey = key;
        $scope.projectPath = path;
    };

    // get the commit history
    $scope.getRecentHistory = function () {
        $scope.loadingHistory = true;
        sourceService.getRecentProjectHistory($scope.projectKey, function (data) {
            castle.app.delayPush($scope, data, $scope.history);
            $scope.loadingHistory = false;
        });
    };

}]);