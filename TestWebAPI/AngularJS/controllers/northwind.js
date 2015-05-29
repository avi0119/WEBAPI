angular.module("northWind")
.constant("dataUrl", "http://localhost:39402/api/product")
.controller('northwindCtrl',
function ($scope, $http, dataUrl) {
    $scope.data = {};

    $http({
        method: 'GET',
        url:dataUrl// "http://localhost:39402/api/product?" + 'callback=JSON_CALLBACK'
    }).success(function (data, status) {
        $scope.data.products = data;
        var i;
        for (i = 0; i < data.length; ++i) {
            console.log(data[i]);                    // Do something with `substr[i]`
        }

    }).error(function (error) {
        $scope.data.error = error;
    });
});