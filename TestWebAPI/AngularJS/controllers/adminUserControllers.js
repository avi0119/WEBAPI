angular.module("NorthWindAdmin")
 .constant("productUrl", "http://localhost:39402/api/claimtype/")
.constant("usersUrl", "http://localhost:39402/api/user")
.constant("claimtypesUrl", "http://localhost:39402/api/claimtype")
.config(function ($httpProvider) {
    $httpProvider.defaults.withCredentials = true;
    $httpProvider.defaults.headers.common.mynewheadertoday = 'C3PO R2D2';
}).controller("usersCtrl", function ($scope, $resource, productUrl, usersUrl, uniqueFilter, claimtypesUrl, uniqueAllChildrenFilter, MinimumPerPropertyFilter, $location, $http) {
    //var df = $resource;
    //$scope.screens = ["Products", "Orders"];

    $scope.selectedclaim = {};
    $scope.productsResource = $resource(usersUrl + ":UserID", { UserID: "@UserID" });
    $scope.calimtypessResource = $resource(claimtypesUrl + ":productID", { productID: "@productID" });

    $scope.listProducts = function () {
        //$scope.products = $scope.productsResource.query();
        /*
        $http.defaults.withCredentials = true;
        $scope.productsResource.query().$promise.then(
           function (data) {
               //$scope.products = data;
               $scope.orders = data;
               //$scope.categories = uniqueAllChildrenFilter($scope.products, 'Category', 'CategoryName');
               $scope.selectedcategory = "Produce";
               var k = 6;
           }, function (reason) {
               var t = reason;
               //alert('Failed: ' + reason);
               $location.path('/login');
           }


            );


    }
    */

        $http.get(usersUrl, { withCredentials: true })
            .success(function (data) {
                //$scope.products = data;
                $scope.orders = data;
                //$scope.categories = uniqueAllChildrenFilter($scope.products, 'Category', 'CategoryName');
                $scope.selectedcategory = "Produce";
                var k = 6;
            })
            .error(function (error) {
                $location.path('/login');
            });
    }



   

    $scope.listClaimTypes = function () {
        //$scope.products = $scope.productsResource.query();
        /*
        $http.defaults.withCredentials = true;
        $scope.calimtypessResource.query().$promise.then(

           function (data) {
               //$scope.products = data;
               var thedata = data;
               $scope.ClaimTypes = data;
               var claimtypes = uniqueAllChildrenFilter(thedata, 'Description');
               $scope.categories = claimtypes;

               //

               //$scope.selectedcategory = "Produce";
               //var k = 6;
           }, function (reason) {
               var t = reason;
               //alert('Failed: ' + reason);
               //$location.path('/login');
           }


            );
            */
        $http.get(claimtypesUrl, { withCredentials: true })
            .success(function (data) {
                //$scope.products = data;
                var thedata = data;
                $scope.ClaimTypes = data;
                var claimtypes = uniqueAllChildrenFilter(thedata, 'Description');
                $scope.categories = claimtypes;
            }).error(function (error) {
                var t = reason;
                //alert('Failed: ' + reason);
                //$location.path('/login');
            });



    }
    /*
    $scope.deleteProduct = function (product) {
        product.$delete().then(function () {
            $scope.products.splice($scope.products.indexOf(product), 1);
        });
    }
    $scope.createProduct = function (product) {
        new $scope.productsResource(product).$save().then(function (newProduct) {
            $scope.products.push(newProduct);
            $scope.editedProduct = null;
        });
    }
    $scope.updateProduct = function (product) {
        category = product.Category;
        supplier = product.Supplier;

        product.Category = null;
        product.Supplier = null;
        delete product.Category;
        delete product.Supplier;
        product.ProductID = category.CategoryID;
        product.$save();


        product.Category = category;
        product.Supplier = supplier;
        $scope.editedProduct = null;
    }
    $scope.startEdit = function (product) {
        $scope.editedProduct = product;
        //$scope.editedProduct.Category.CategoryName = product.Category.CategoryName;
        //$scope.selectedcat = $scope.editedProduct.Category;
        var INDEX = $scope.findcategorydescriptionindex(product.Category.CategoryID);
        $scope.selectedcat = $scope.categories[INDEX];
    }
    $scope.cancelEdit = function () {
        $scope.editedProduct = null;
        $scope.selectedcat = null;
    }
    $scope.findcategorydescription = function (categoryid) {
        var result = "";
        var categories_ = $scope.categories;
        for (var i = 0; i < categories_.length; i++) {
            var cat = categories_[i];
            if (cat.CategoryID = categoryid) {
                result = cat.Description;
                break;
            }
        }
        return result;

    }
    $scope.findcategorydescriptionindex = function (categoryid) {
        var result = -1;
        var categories_ = $scope.categories;
        for (var i = 0; i < categories_.length; i++) {
            var cat = categories_[i];
            if (cat.CategoryID == categoryid) {
                result = i;
                break;
            }
        }
        return result;

    }

    $scope.findcategorydescription2 = function (prod) {

        return prod.Category.Description;;

    }
     */
    $scope.IncludeOnlyClaimTypesOfGivenDescription = function (desiredDescription) {
        return function (ClaimTypeItem) {
            return ClaimTypeItem.ClaimType == desiredDescription;
        }
    }
    $scope.selectedOrder;
    $scope.selectOrder = function (order) {
        $scope.selectedOrder = order;
        $scope.ClaimsOfMinimumValue = MinimumPerPropertyFilter(order.DBClaims, "Rank", "ClaimType");
        $scope.populate_selectedclaim_arrayProperly($scope.ClaimsOfMinimumValue, $scope.ClaimTypes);

    };
    $scope.populate_selectedclaim_arrayProperly = function (ClaimsOfMinimumValuePerCurrentUSer,ReservoirOfClaimTypes) {

        for (var i = 0; i < ClaimsOfMinimumValuePerCurrentUSer.length; i++) {
            var claimID = ClaimsOfMinimumValuePerCurrentUSer[i].ClaimID;
            var object = $scope.findClaimTypeFromReservoirOfClaimTypesGivenClaimID(claimID, ReservoirOfClaimTypes);
            var currClaimTYpe = ClaimsOfMinimumValuePerCurrentUSer[i].ClaimType;
            $scope.selectedclaim[currClaimTYpe] = object;
        }

    };
    $scope.findClaimTypeFromReservoirOfClaimTypesGivenClaimID = function (claimID, ReservoirOfClaimTypes) {
        for (var i = 0; i < ReservoirOfClaimTypes.length; i++) {
            if (ReservoirOfClaimTypes[i].ClaimID == claimID)
            {
                return ReservoirOfClaimTypes[i];
            }
        }


    };
    $scope.listClaimTypes();

   
    $scope.listProducts();

}).controller("usersCtrl_old", function ($scope, $http, usersUrl) {
    $http.get(usersUrl, { withCredentials: true })
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

});

