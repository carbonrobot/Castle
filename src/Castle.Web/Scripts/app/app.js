// namespace
var castle = {};

// application container
castle.app = angular.module('castle.app', ['ngRoute', 'castle.app.services', 'castle.app.controllers', 'ngSanitize', 'ngAnimate'])
    .config(function ($routeProvider, $locationProvider) {
        $routeProvider.
            when('/source', {
                controller: 'ProjectController',
                templateUrl: '/views/project/_index.html'
            }).
            when('/source/:path*', {
                controller: 'ProjectController',
                templateUrl: '/views/project/_index.html'
            }).
            otherwise({
                redirectTo: '/source',
            });
    });

// domain services
castle.app.services = angular.module('castle.app.services', []);

// controllers
castle.app.controllers = angular.module('castle.app.controllers', ['castle.app.services']);

// internal methods
(function (ns, $) {

    // loader
    $(function () {

    });

})(castle, jQuery);