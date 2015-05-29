angular.module("northWind")
.constant("productListActiveClass", "btn-primary")
.constant("productListPageCount", 6)
.controller("productListCtrl", function ($scope, $filter, productListActiveClass, productListPageCount,cart)
{
    $scope.selectedPage = 1;
    $scope.pageSize = productListPageCount;
    var selectedCategory = null;
    $scope.selectCategory = function (newCategory) {
        selectedCategory = newCategory;
        $scope.selectedPage = 1;
    }
    $scope.selectPage = function (newPage) {
        $scope.selectedPage = newPage;
    }
    $scope.categoryFilterFn = function (product) {
        return selectedCategory == null ||
        product.Category.CategoryName == selectedCategory;
    }
    $scope.getCategoryClass = function (category) {
        return selectedCategory == category ? productListActiveClass : "";
    }
    $scope.getPageClass = function (page) {
        return $scope.selectedPage == page ? productListActiveClass : "";
    }
    $scope.addProductToCart = function (product) {
        cart.addProduct(product.ProductID, product.ProductName, product.UnitPrice);
    }
});