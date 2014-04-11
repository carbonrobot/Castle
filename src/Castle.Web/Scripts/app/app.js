// namespace
var castle = {};

// application container
castle.app = angular.module('castle.app', ['castle.app.services', 'castle.app.controllers', 'ngSanitize']);

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