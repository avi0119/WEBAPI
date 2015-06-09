angular.module("northWind")
.constant("dataUrl", "http://localhost:39402/api/product")
.constant("orderUrl", "http://localhost:39402/api/order")
.controller('northwindCtrl',
function ($scope, $http, $location, dataUrl, orderUrl, cart) {
    $scope.placeorderbuttonpressed = false;
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
    $scope.sendOrder = function (shippingDetails) {
        $scope.placeorderbuttonpressed = true;
        var order = angular.copy(shippingDetails);
        order.products = cart.getProducts();
        $http.post(orderUrl +'/x', order)
        .success(function (data) {
            $scope.data.orderId = data;
            cart.getProducts().length = 0;
        })
        .error(function (error) {
            $scope.data.orderError = error;
        }).finally(function () {
            $location.path("/complete");
        });
    }
});