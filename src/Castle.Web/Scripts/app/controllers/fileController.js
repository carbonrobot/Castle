// ***************
// File controller
// ***************
castle.app.controllers.controller('FileController', ['$scope', '$http', '$route', 'sourceService', function ($scope, $http, $route, sourceService) {

    $scope.loadingFiles = false;
    $scope.isReadmeAvailable = false;
    $scope.files = [];
    $scope.path = '';
    $scope.openFile = '';
    $scope.paths = [];
    $scope.lastChange = 'Gathering Files...';

    $scope.init = function (path) {
        // if no path was passed in, use the project path from the parent scope
        if (path == undefined) {
            $scope.path = $scope.projectPath;
        }
        else {
            $scope.path = path;
        }

        // send a pigeon to get the file listing
        $scope.getFiles();

        // break out the path into links
        $scope.paths = castle.app.utils.generatePaths($scope.path);
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

            // display the latest change
            var last = data[0];
            angular.forEach(data, function (item, key) {
                if (moment(last.ChangeTime).isBefore(item.ChangeTime)) {
                    last = item;
                }
            });
            $scope.lastChange = last.Author + " " + last.ChangeRelativeTime;
        });
    };

    // load a readme file if available
    $scope.loadReadme = function () {
        sourceService.getFileContent($scope.path + '/README.md', function (data) {

            require(['/scripts/vendor/marked.js'], function (marked) {
                $scope.openFile = marked(data);

                // FIX: scope doesnt always update inside require[]
                $scope.$apply(); 
            });
        });
    };

    // initialize using path
    $scope.init($route.current.params.path);

}]);