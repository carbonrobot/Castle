// ***************
// Project settings controller
// ***************
castle.app.controllers.controller('SettingsController', ['$scope', '$http', 'projectService', function ($scope, $http, projectService) {

    $scope.loadingSettings = false;
    $scope.project = {};

    $scope.init = function () {
        $scope.loadProject();
    };

    // load a readme file if available
    $scope.loadProject = function () {
        $scope.loadingSettings = true;
        projectService.getProject($scope.projectKey, function (data) {
            $scope.project = data;
            $scope.loadingSettings = false;
        });
    };

    $scope.updateProject = function () {
        projectService.updateProject($scope.project, function (data) {
            // TODO: show an update notification
        });
    };

    $scope.deleteProject = function () {
        projectService.deleteProject($scope.project.Key, function (data) {
            // redirect to repository page
            window.location.href = '/source/' + $scope.project.Repository.Key;
        });
    };

    // initialize
    $scope.init();

}]);