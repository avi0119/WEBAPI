angular.module("NorthWindAdmin")
.constant("productUrl", "http://localhost:39402/api/product/")
.config(function ($httpProvider) {
    $httpProvider.defaults.withCredentials = true;
})
.controller("productCtrl", function ($scope, $resource, productUrl, uniqueFilter) {
    //var df = $resource;
    //$scope.screens = ["Products", "Orders"];
   
    $scope.items = [{ name: 'one', age: 30 }, { name: 'two', age: 27 }, { name: 'three', age: 50 }];
    $scope.productsResource = $resource("http://localhost:39402/api/product/" + ":ProductID", { ProductID: "@ProductID" });
    //$scope.productsResource = $resource("http://localhost:2403/products/" + ":id", { id: "@id" });
    $scope.listProducts = function () {
        //$scope.products = $scope.productsResource.query();

        $scope.productsResource.query().$promise.then(

           function(data) {
               $scope.products = data;
               $scope.categories = uniqueFilter($scope.products, 'Category', 'CategoryName');
               $scope.selectedcategory = "Produce";
               var k = 6;
    }


            );
       
       
    }
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
        product.$save();


        product.Category=category ;
        product.Supplier=supplier ;
        $scope.editedProduct = null;
    }
    $scope.startEdit = function (product) {
        $scope.editedProduct = product;
        //$scope.editedProduct.Category.CategoryName = product.Category.CategoryName;
        var r = $scope.editedProduct.Category.CategoryName;
    }
    $scope.cancelEdit = function () {
        $scope.editedProduct = null;
    }
    $scope.listProducts();
});