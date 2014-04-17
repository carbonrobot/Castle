// ***************
// Raw File Content controller
// ***************
castle.app.controllers.controller('RawController', ['$scope', '$http', '$route', 'sourceService', function ($scope, $http, $route, sourceService) {

    $scope.loadingFile = false;
    $scope.path = '';
    $scope.openFile = '';
    $scope.paths = [];

    $scope.init = function (path) {
        $scope.path = path;
        $scope.loadFile();

        // break out the path into links
        $scope.paths = castle.app.utils.generatePaths($scope.path);
    };

    // load a readme file if available
    $scope.loadFile = function () {
        $scope.loadingFile = true;
        sourceService.getFileContent($scope.path, function (data) {

            require(['/scripts/vendor/marked.js'], function (marked) {
                $scope.openFile = marked(data);
                $scope.loadingFile = false;

                // FIX: scope doesnt always update inside require[]
                $scope.$apply(); 
            });

        });
    };

    // initialize using path
    $scope.init($route.current.params.path);

}]);