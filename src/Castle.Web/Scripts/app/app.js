// namespace
var castle = {};

// application container
castle.app = angular.module('castle.app', ['ngRoute', 'castle.app.services', 'castle.app.controllers', 'ngSanitize', 'ngAnimate'])
    .config(function ($routeProvider, $locationProvider) {
        $routeProvider.
            when('/source', {
                templateUrl: '/views/source/_List.html'
            }).
            when('/source/:path*', {
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

// internal methods
(function (ns, $) {

    // loader
    $(function () {

    });

})(castle, jQuery);