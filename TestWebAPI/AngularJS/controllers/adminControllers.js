angular.module("NorthWindAdmin")
.constant("authUrl", "http://localhost:39402/api/user/")
.constant("ordersUrl", "http://localhost:39402/api/order")
//.config(function ($httpProvider) {
//    logServiceProvider.debugEnabled(true).messageCounterEnabled(false);
//})
.run(function ($http) {
    var base64string = window.btoa('avisemah@gmail.com' + ':' + '12345');
    $http.defaults.headers.common.Authorization = 'Basic ' +base64string;//YmVlcDpib29w';
})
.controller("authCtrl", function ($scope, $http, $location, authUrl, EncodingDecoding) {

    // this is example of inheritance where object is inherited diffeerntly than one value
    var person1 = {
        somenumber: 11111,
        name: "Nicholas",
        name2: {
            firstprop:"hello",
            secondprop:"world",
        },
        sayName: function () {
            console.log(this.name);
        }
    };
    var person2 = Object.create(person1, {
        name: {
            configurable: true,
            enumerable: true,
            value: "Greg",
            writable: true
        },
        somenumber: {
            configurable: true,
            enumerable: true,
            value: 2222,
            writable: true
        },
        
    });
    person2.name2.firstprop = "salut";
    person1.sayName(); // outputs "Nicholas"
    person2.sayName(); // outputs "Greg"
    console.log(person1.hasOwnProperty("sayName")); // true
    console.log(person1.isPrototypeOf(person2)); // true
    console.log(person2.hasOwnProperty("sayName")); // false

    /////////////////////////////////////////////
    ////////////////////////////////////////////////////





    $scope.authenticate = function (user, pass) {
        //$http.get(config.apiUrl + '/api/token', { headers: { 'Authorization': 'Basic ' + base64.encode($scope.username + ':' + $scope.password) } });
        //var gg = EncodingDecoding.Base64Decode(user + ':' + pass);
        var base64string = window.btoa(user + ':' + pass);
        authheader = 'Basic ' + base64string;
        var config = {
            headers: {
                "Authorization": authheader
                , withCredentials: true
            }
            //,            transformRequest: function (data, headers) {
            //    var rootElem = angular.element("<xml>");
            //    for (var i = 0; i < data.length; i++) {
            //        var prodElem = angular.element("<product>");
            //        prodElem.attr("name", data[i].name);
            //        prodElem.attr("category", data[i].category);
            //        prodElem.attr("price", data[i].price);
            //        rootElem.append(prodElem);
            //    }
            //    rootElem.children().wrap("<products>");
            //    return rootElem.html();
            //}
        }
        //$http.post(authUrl,
        //    {
        //        //data: {
        //        //    username: user,
        //        //    password: pass
        //        //},
        //        headers: {
        //            'Authorization': 'Basic ' + gg
        //        }
        //}, {
        //    withCredentials: true
        //}).success(function (data) {
        //    $location.path("/main");
        //}).error(function (error) {
        //    $scope.authenticationError = error;
        //    //$location.path('/login');
        //});
        $http.post(authUrl, {
            username: user,
            password: pass
        },config).success(function (data) {
            $location.path("/main");
        }).error(function (error) {
            $scope.authenticationError = error;
            //$location.path('/login');
        });

        //$http.post(authUrl, {
        //    username: user,
        //    password: pass
        //}, {
        //    withCredentials: true
        //}).success(function (data) {
        //    $location.path("/main");
        //}).error(function (error) {
        //    $scope.authenticationError = error;
        //    //$location.path('/login');
        //});
    }
}).controller("mainCtrl", function ($scope) {
    $scope.screens = ["Products", "Orders", "Users"];
    $scope.current = $scope.screens[0];
    $scope.setScreen = function (index) {
        $scope.current = $scope.screens[index];
    };
    $scope.getScreen = function () {
         var screentoret= $scope.current == "Products"
        ? "/AngularJS/views/adminProducts.html" : "/AngularJS/views/adminOrders.html";



         switch ($scope.current) {
             case "Products":
                 var screentoret = "/AngularJS/views/adminProducts.html";
                 break;
             case "Orders":
                 var screentoret = "/AngularJS/views/adminOrders.html";
                 break;
             case "Users":
                 var screentoret = "/AngularJS/views/adminUsers.html";
                 break;

             default:
                 console.log("Sorry, we are out of " + expr + ".");
         }
         return screentoret;
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