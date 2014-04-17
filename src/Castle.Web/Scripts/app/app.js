// namespace
var castle = {};

// application container
castle.app = angular.module('castle.app', ['ngRoute', 'castle.app.services', 'castle.app.controllers', 'ngSanitize', 'ngAnimate'])
    .config(function ($routeProvider, $locationProvider) {
        $routeProvider.
            when('/source', {
                controller: 'FileController',
                templateUrl: '/views/source/_List.html'
            }).
            when('/source/:path*', {
                controller: 'FileController',
                templateUrl: '/views/source/_List.html'
            }).
            otherwise({
                redirectTo: '/source',
            });
    });

// domain services
castle.app.services = angular.module('castle.app.services', []);

// controllers
castle.app.controllers = angular.module('castle.app.controllers', ['castle.app.services']);

// utils
castle.app.utils = (function ($) {

    return {

        // incrementally load an array for a visual effect
        delayPush : function ($scope, source, dest) {
            angular.forEach(source, function (value, key) {
                setTimeout(function () {
                    dest.push(value);
                    $scope.$apply();
                }, key * 25);
            });
        }

    };

})(jQuery);

// internal methods
(function (ns, $) {

    // loader
    $(function () {

    });

})(castle, jQuery);