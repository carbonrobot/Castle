﻿// namespace
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
            when('/raw/:path*', {
                controller: 'RawController',
                templateUrl: '/views/source/_Raw.html'
            }).
            when('/settings', {
                controller: 'SettingsController',
                templateUrl: '/views/project/_settings.html'
            }).
            when('/history', {
                controller: 'HistoryController',
                templateUrl: '/views/project/_history.html'
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
        },
        
        // generate path links for navigation
        generatePaths : function (path) {
            var parts = path.split('/');
            var links = [];
            for (i = 0; i < parts.length; i++) {
                var linkPath = parts.slice(0, i + 1).join('/');
                links.push({ "name": parts[i], "path": linkPath });
            }
            return links;
        }

    };

})(jQuery);

// internal methods
(function (ns, $) {

    // loader
    $(function () {

    });

})(castle, jQuery);