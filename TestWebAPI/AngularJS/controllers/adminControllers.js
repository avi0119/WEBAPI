angular.module("NorthWindAdmin")
.constant("authUrl", "http://localhost:39402/api/user/")
.constant("ordersUrl", "http://localhost:39402/api/order")
.controller("authCtrl", function ($scope, $http, $location, authUrl) {
    $scope.authenticate = function (user, pass) {
        $http.post(authUrl, {
            username: user,
            password: pass
        }, {
            withCredentials: true
        }).success(function (data) {
            $location.path("/main");
        }).error(function (error) {
            $scope.authenticationError = error;
        });
    }
}).controller("mainCtrl", function ($scope) {
    $scope.screens = ["Products", "Orders"];
    $scope.current = $scope.screens[0];
    $scope.setScreen = function (index) {
        $scope.current = $scope.screens[index];
    };
    $scope.getScreen = function () {
        return $scope.current == "Products"
        ? "/AngularJS/views/adminProducts.html" : "/AngularJS/views/adminOrders.html";
    };
}).controller("ordersCtrl", function ($scope, $http, ordersUrl) {
    $http.get(ordersUrl, { withCredentials: true })
    .success(function (data) {
        $scope.orders = data;
    })
    .error(function (error) {
        $scope.error = error;
    });
    $scope.selectedOrder;
    $scope.selectOrder = function (order) {
        $scope.selectedOrder = order;
    };
    $scope.calcTotal = function (order) {
        var total = 0;
        for (var i = 0; i < order.OrderDetails.length; i++) {
            total +=
            order.OrderDetails[i].Quantity * order.OrderDetails[i].UnitPrice;
        }
        return total;
    }
});