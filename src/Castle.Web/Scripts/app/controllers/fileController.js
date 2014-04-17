// ***************
// File controller
// ***************
castle.app.controllers.controller('FileController', ['$scope', '$http', '$route', 'sourceService', function ($scope, $http, $route, sourceService) {

    $scope.loadingFiles = false;
    $scope.loadingReadme = false;
    $scope.isReadmeAvailable = false;
    $scope.files = [];
    $scope.path = '';
    $scope.openFile = '';

    $scope.init = function (path) {
        // if no path was passed in, use the project path from the parent scope
        if (path == undefined) {
            $scope.path = $scope.projectPath;
        }
        else {
            $scope.path = path;
        }

        $scope.getFiles();
    };

    // load files at this path
    $scope.getFiles = function () {
        $scope.loadingFiles = true;
        sourceService.getFiles($scope.path, function (data) {
            castle.app.utils.delayPush($scope, data, $scope.files);
            $scope.loadingFiles = false;

            // if there is a readme file, load er up. bangarang.
            angular.forEach(data, function (item, key) {
                if (item.Name == 'README.md') {
                    $scope.isReadmeAvailable = true;
                    $scope.loadReadme();
                    return;
                }
            });
        });
    };

    // load a readme file if available
    $scope.loadReadme = function () {
        $scope.loadingReadme = true;
        sourceService.getFileContent($scope.path + '/README.md', function (data) {

            require(['/scripts/vendor/marked.js'], function (marked) {
                $scope.openFile = marked(data);
                $scope.loadingReadme = false;

                // FIX: scope doesnt always update inside require[]
                $scope.$apply(); 
            });
        });
    };

    // initialize using path
    $scope.init($route.current.params.path);

}]);