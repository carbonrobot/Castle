// ***************
// File controller
// ***************
castle.app.controllers.controller('FileController', ['$scope', '$http', '$route', 'sourceService', function ($scope, $http, $route, sourceService) {

    $scope.loadingFiles = false;
    $scope.loadingReadme = false;
    $scope.files = [];
    $scope.path = '';
    $scope.openFile = '';

    $scope.init = function (path) {
        if (path == undefined) {
            $scope.path = $scope.projectPath; // from parent scope
        }
        else {
            $scope.path = path;
        }

        $scope.getFiles();
        $scope.getReadme();
    };

    // load files at this path
    $scope.getFiles = function () {
        $scope.loadingFiles = true;
        sourceService.getFiles($scope.path, function (data) {
            castle.app.utils.delayPush($scope, data, $scope.files);
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

    // initialize using path
    $scope.init($route.current.params.path);

}]);