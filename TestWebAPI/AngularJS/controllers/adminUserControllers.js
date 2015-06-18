angular.module("NorthWindAdmin")
 .constant("productUrl", "http://localhost:39402/api/claimtype/")
.constant("usersUrl", "http://localhost:39402/api/user")
.constant("claimtypesUrl", "http://localhost:39402/api/claimtype")
.config(function ($httpProvider) {
    $httpProvider.defaults.withCredentials = true;
    $httpProvider.defaults.headers.common.mynewheadertoday = 'C3PO R2D2';
}).controller("usersCtrl", function ($scope,$rootScope, $resource, productUrl, usersUrl, uniqueFilter, claimtypesUrl, uniqueAllChildrenFilter, MinimumPerPropertyFilter,AllItemsOfOneTypeFilter, $location) {
    $scope.alerterflag = {};

    $scope.alerterflag.alerterflag = true;
    $scope.data = {};
    $scope.data.name = "avi semah and erit gridnev";
    $scope.createmode;
    $scope.editmode;
    $scope.entitlementtype = {};
    $scope.entitlementlevel;

    $scope.ActivateCreateMode = function (val) {
        $scope.createmode = val;
        $scope.editmode = !val;
    }
    $scope.NeutralizeCreateAndEdit = function () {
        $scope.createmode = false;
        $scope.editmode = false;
    }
    $scope.createorder = function () {
        
        $scope.ActivateCreateMode(true);
    }
    //var df = $resource;
    //$scope.screens = ["Products", "Orders"];

    $scope.selectedclaim = {};
    $scope.selectedclaimv2 = [];
    $scope.productsResource = $resource(usersUrl + ":UserID", { UserID: "@UserID" });
    $scope.calimtypessResource = $resource(claimtypesUrl + ":productID", { productID: "@productID" });

    $scope.listProducts = function () {
        //$scope.products = $scope.productsResource.query();

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



    $scope.listClaimTypes = function () {
        //$scope.products = $scope.productsResource.query();

        $scope.calimtypessResource.query().$promise.then(

           function (data) {
               //$scope.products = data;
               var thedata = data;
               $scope.ClaimTypes = data;
               //$scope.alerterflag.alerterflag = !$scope.alerterflag.alerterflag
               //$scope.alerterflag.alerterflag = !$scope.alerterflag.alerterflag;
               var claimtypes = uniqueAllChildrenFilter(thedata, 'Description');
               $scope.categories = claimtypes;
               $scope.claimtypesstrinarray = uniqueAllChildrenFilter(thedata, 'ClaimType');;
               //

               //$scope.selectedcategory = "Produce";
               //var k = 6;
           }, function (reason) {
               var t = reason;
               //alert('Failed: ' + reason);
               //$location.path('/login');
           }


            );


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
    $scope.updateselectedclaimPerLastItem = function () {
        if ($scope.addbuttonpressed == true) {
            var lastindex = $scope.ClaimsOfMinimumValue.length - 1;
            var lastMinValueItem = $scope.ClaimsOfMinimumValue[lastindex];
            $scope.selectedclaim[lastMinValueItem.ClaimType] = lastMinValueItem;
            varz = 1;
        }
    }
    $scope.removeclaimtype = function (index) {
        $scope.updateselectedclaimPerLastItem();
        $scope.ClaimsOfMinimumValue.splice(index, 1);
        $scope.addbuttonpressed = false;// $scope.ClaimsOfMinimumValue.length -1 > $scope.originalcountofuserclaimtypes;
    }
    $scope.addClaimType = function () {
        $scope.updateselectedclaimPerLastItem();
        $scope.addbuttonpressed = $scope.ClaimsOfMinimumValue.length+1 > $scope.originalcountofuserclaimtypes;
        $scope.addbuttonpressed=true;
        var newClaimType = {};
        $scope.ClaimsOfMinimumValue.push(newClaimType);
        
    }

    $scope.validatetype = function (index) {
        if ($scope.ClaimsOfMinimumValue[index].ClaimType) {
            if ($scope.isClaimTypeInClaimsOfMinimumValue($scope.ClaimsOfMinimumValue[index].ClaimType, index)) {
                alert($scope.ClaimsOfMinimumValue[index].ClaimType + " has already been selected")
                //$scope.ClaimsOfMinimumValue[index].ClaimType = "";
                //$scope.ClaimsOfMinimumValue[index].Description = "";
                var tempClaimType = $scope.ClaimsOfMinimumValue[index];
                $scope.ClaimsOfMinimumValue[index] = {};
                //$scope.removeclaimtype(index);
                //$scope.addClaimType();

                //$scope.ClaimsOfMinimumValue.splice(index, 1);
                //$scope.addbuttonpressed = false;// $scope.ClaimsOfMinimumValue.length -1 > $scope.originalcountofuserclaimtypes;

                //$scope.addbuttonpressed = $scope.ClaimsOfMinimumValue.length + 1 > $scope.originalcountofuserclaimtypes;
                //$scope.addbuttonpressed = true;
                //var newClaimType = {};
                //$scope.ClaimsOfMinimumValue.push(newClaimType);

                var t = index;
                //$scope.$apply();
            }
        }
    }
    $scope.isClaimTypeInClaimsOfMinimumValue = function (claimtype,indextoexclude) {
        for (var i = 0; i < $scope.ClaimsOfMinimumValue.length; i++) {
            if (indextoexclude == i)
            {
                continue;
            }
            if ($scope.ClaimsOfMinimumValue[i].ClaimType == claimtype) {

                return true;
            }
        }
        return false;
    }
    $rootScope.triggerRelink = function () {
        $rootScope.$broadcast('myEventName');
    };
    $scope.canceledit = function () {
        $scope.editeduser = {};

        $scope.ActivateCreateMode(true);

    };
    $scope.IncludeOnlyClaimTypesOfGivenDescription = function (desiredDescription) {
        return function (ClaimTypeItem) {
            var res = ClaimTypeItem.ClaimType == desiredDescription;
            return res
        }
    }
    $scope.selectedOrder;
    $scope.selectOrder = function (order) {
        $scope.alerterflag.alerterflag = !$scope.alerterflag.alerterflag;
        $scope.addbuttonpressed = false;
        $scope.ActivateCreateMode(false);
        $scope.selectedOrder = order;

        $scope.editeduser = order;
        $scope.ClaimsOfMinimumValue = MinimumPerPropertyFilter(order.DBClaims, "Rank", "ClaimType");
        $scope.deletepropertiesPerEachItemInArry($scope.ClaimsOfMinimumValue, ['StartDate','UserID']);
        $scope.populate_selectedclaim_arrayProperly($scope.ClaimsOfMinimumValue, $scope.ClaimTypes);
        $scope.populate_selectedclaim_arrayProperly_v2($scope.ClaimsOfMinimumValue, $scope.ClaimTypes);
        $scope.originalcountofuserclaimtypes = $scope.ClaimsOfMinimumValue.length;

    };
    $scope.deletepropertiesPerEachItemInArry = function (arr, arrayofproperties) {
        for (var i = 0; i < arr.length; i++) {
            var curritem = arr[i];
            for (var j = 0; j < arrayofproperties.length; j++) {
                delete curritem[arrayofproperties[j]];
            }
        }
    }
    $scope.populate_selectedclaim_arrayProperly = function (ClaimsOfMinimumValuePerCurrentUSer, ReservoirOfClaimTypes) {

        for (var i = 0; i < ClaimsOfMinimumValuePerCurrentUSer.length; i++) {
            var claimID = ClaimsOfMinimumValuePerCurrentUSer[i].ClaimID;
            var object = $scope.findClaimTypeFromReservoirOfClaimTypesGivenClaimID(claimID, ReservoirOfClaimTypes);
            var currClaimTYpe = ClaimsOfMinimumValuePerCurrentUSer[i].ClaimType;
            $scope.selectedclaim[currClaimTYpe] = object;
        }

    };

    $scope.populate_selectedclaim_arrayProperly_v2 = function (ClaimsOfMinimumValuePerCurrentUSer, ReservoirOfClaimTypes) {

        for (var i = 0; i < ClaimsOfMinimumValuePerCurrentUSer.length; i++) {
            var claimID = ClaimsOfMinimumValuePerCurrentUSer[i].ClaimID;
            var object = $scope.findClaimTypeFromReservoirOfClaimTypesGivenClaimID(claimID, ReservoirOfClaimTypes);
            var currClaimTYpe = ClaimsOfMinimumValuePerCurrentUSer[i].ClaimType;
            $scope.selectedclaimv2[i] = object;
        }

    };
    $scope.findClaimTypeFromReservoirOfClaimTypesGivenClaimID = function (claimID, ReservoirOfClaimTypes) {
        for (var i = 0; i < ReservoirOfClaimTypes.length; i++) {
            if (ReservoirOfClaimTypes[i].ClaimID == claimID) {
                return ReservoirOfClaimTypes[i];
            }
        }


    };
    $scope.ActivateCreateMode(true);
    $scope.listClaimTypes();


    $scope.listProducts();


}).directive('relinkEvent', function ($rootScope) {
    return {
        transclude: 'element',
        restrict: 'A',
        link: function (scope, element, attr, ctrl, transclude) {
            var previousContent = null;

            var triggerRelink = function () {
                if (previousContent) {
                    previousContent.remove();
                    previousContent = null;
                }

                transclude(function (clone) {
                    console.log('relinking');
                    element.parent().append(clone);
                    previousContent = clone;
                });

            };

            triggerRelink();
            $rootScope.$on(attr.relinkEvent, triggerRelink);

        }
    };

}).directive("scopeDemo", function () {
    return {
        template: function () {
            return angular.element(
            document.querySelector("#scopeTemplate")).html();
        },
        scope: {
            local: "=nameprop"
        }
    }
}).directive("claimtypeamddescriptionlevel", function () {
        return {
            link: function (scope, element, attrs) {
                //scope.data = scope[attrs["unorderedList"]];

                scope.somefunc = function () {
                    var t = 1;
                    this.alerterflag = true;
                }
                scope.IncludeOnlyClaimTypesOfGivenDescription = function (desiredDescription) {
                    return function (ClaimTypeItem) {
                        var res = ClaimTypeItem.ClaimType == desiredDescription;
                        return res
                    }
                }
                scope.$watch("currentclaimsofminimumvalueitem", function () {
                    scope.currentclaimsofminimumvalueitemObject = JSON.parse(scope.currentclaimsofminimumvalueitem);
                    //ctrl.updateTotal();
                });
                scope.$watch("claimtypesarray", function () {
                    scope.claimtypesarrayObject = JSON.parse(scope.claimtypesarray);
                    scope.populateselecteditemv2();
                    //ctrl.updateTotal();
                });
                scope.findClaimTypeFromReservoirOfClaimTypesGivenClaimID = function (claimID, ReservoirOfClaimTypes) {
                    for (var i = 0; i < ReservoirOfClaimTypes.length; i++) {
                        if (ReservoirOfClaimTypes[i].ClaimID == claimID) {
                            return ReservoirOfClaimTypes[i];
                        }
                    }


                };
                scope.populateselecteditemv2 = function () {
                    var index = scope.indexinarrayofminvalues;
                    scope.selectedclaimv2[index] = scope.returnClaimTypeCorerspondingToIndexInMinimumCalueArray(scope.claimsofminimumvaluearray, scope.claimtypesarrayObject, index);
                }
                scope.returnClaimTypeCorerspondingToIndexInMinimumCalueArray= function (ClaimsOfMinimumValuePerCurrentUSer, ReservoirOfClaimTypes,index) {

                         var claimID = ClaimsOfMinimumValuePerCurrentUSer[index].ClaimID;
                        var object = scope.findClaimTypeFromReservoirOfClaimTypesGivenClaimID(claimID, ReservoirOfClaimTypes);
                        var currClaimTYpe = ClaimsOfMinimumValuePerCurrentUSer[index].ClaimType;
                        return   object;


                };
                scope.islastfunc = function () {
                    if (angular.isUndefined(this.islast)) {
                        return false;
                    }
                    var val = this.islast;
                    if (val == "false") {
                        return false;

                    } else if (val == "true") {
                        return true;
                    }
                    return !!val;
                }
                scope.isaddbuttonpressedfunc = function () {
                    if (angular.isUndefined(this.isaddbuttonpressed)) {
                        return false;
                    }
                    var val = this.isaddbuttonpressed;
                    if (val == "false") {
                        return false;

                    } else if (val == "true") {
                        return true;
                    }
                    return !!val;
                }
            },
            restrict: "EA",
            //replace: true,
            scope:{
                alerterflag: "="
                , islast: "@"
                , isaddbuttonpressed: "@"
                , indexinarrayofminvalues: "@"
                , claimtypesarray: "@"
                //, claimtypesarraysensitive: "="
                , claimtypesstringsonnlyarray: "@"
                , claimsofminimumvaluearray: "="
                , currentclaimsofminimumvalueitem: "@"
                , selectedclaimv2:"="
            },
            //templateUrl: "../DirectiveTemplates/TwoTableCells.html"
            templateUrl: "http://localhost:39402/AngularJS/DirectiveTemplates/ClaimLevelCell.html"
        }
}).directive("claimtypeamddescriptiontype", function () {
    return {
        link: function (scope, element, attrs, ctrl) {
            //scope.data = scope[attrs["unorderedList"]];
            //scope.currentclaimsofminimumvalueitem = JSON.parse(scope.currentclaimsofminimumvalueitem);
            //scope.tagid = attrs["id"];
            scope.thelement = element;
            scope.somefunc = function () {
                var t = scope.alerterflag;
                var topicButton = document.getElementById('rightcellspan0');
                this.alerterflag = true;
            }
            
            scope.$watch("claimtypesstringsonnlyarray", function () {
                scope.claimtypesstringsonnlyarrayObject = JSON.parse(scope.claimtypesstringsonnlyarray);
                //ctrl.updateTotal();
            });
            scope.$watch("currentclaimsofminimumvalueitem", function () {
                scope.currentclaimsofminimumvalueitemObject = JSON.parse(scope.currentclaimsofminimumvalueitem);
                //ctrl.updateTotal();
            });
            var topicButton = document.getElementById('aaa' );
            $('#aaa').change(function () {
                var topicButton = document.getElementById('rightcellspan' + scope.indexinarrayofminvalues);
                var val = $("#rightcellspan" + scope.indexinarrayofminvalues + " option:selected").text();
                alert(val);
            });
            scope.$watch("alerterflag", function () {
                setSelected(ctrl.$viewValue);
                var topicButton2 = document.getElementById('rightcellspan0');
                var topicButton = document.getElementById('rightcellspan' + scope.indexinarrayofminvalues);
                var b=$('#rightcellspan' + scope.indexinarrayofminvalues).change(function () {
                    var topicButton = document.getElementById('rightcellspan' + scope.indexinarrayofminvalues);
                    var val = $("#rightcellspan" + scope.indexinarrayofminvalues + " option:selected").text();
                    alert(val);
                });
                //ctrl.updateTotal();
            });
            var setSelected = function (value) {
                var span = document.getElementById('rightcellspan' + scope.indexinarrayofminvalues)
                if (!(span==null) ){
                    $("#rightcellspan" + scope.indexinarrayofminvalues).html(value);
                }
            }
            ctrl.$render = function () {
                setSelected(ctrl.$viewValue );
            }
            var topicButton = document.getElementById('ddd0');
            //$('#rightcellspan' + scope.indexinarrayofminvalues).change(function () {
                
            //    var val = $("#rightcellspan" + scope.indexinarrayofminvalues + " option:selected").text();
            //    alert(val);
            //});
            scope.islastfunc = function () {
                if (angular.isUndefined(this.islast)) {
                    return false;
                }
                var val = this.islast;
                if (val == "false") {
                    return false;

                } else if (val == "true") {
                    return true;
                }
                return !!val;
            }
            scope.isaddbuttonpressedfunc = function () {
                if (angular.isUndefined(this.isaddbuttonpressed)) {
                    return false;
                }
                var val = this.isaddbuttonpressed;
                if (val == "false") {
                    return false;

                } else if (val == "true") {
                    return true;
                }
                return !!val;
            }
        },
        restrict: "EA",
        require: "ngModel",
        //replace: true,
        scope: {
            alerterflag: "="
            , islast: "@"
            , isaddbuttonpressed: "@"
            , indexinarrayofminvalues: "@"
            , claimtypesarray: "@"
            , claimtypesstringsonnlyarray: "@"
            , claimsofminimumvaluearray: "="
            , currentclaimsofminimumvalueitem: "@"
        },
        //templateUrl: "../DirectiveTemplates/TwoTableCells.html"
        templateUrl: "http://localhost:39402/AngularJS/DirectiveTemplates/ClaimTypeCell.html"
    }
}).directive("claimtypeamddescriptiontypeonerow", function () {
    return {
        link: function (scope, element, attrs) {
            //scope.data = scope[attrs["unorderedList"]];
            scope.$watch("currentclaimsofminimumvalueitem", function () {
                scope.currentclaimsofminimumvalueitemObject = JSON.parse(scope.currentclaimsofminimumvalueitem);
                //ctrl.updateTotal();
            });
            //scope.currentclaimsofminimumvalueitem = JSON.parse(scope.currentclaimsofminimumvalueitem);
            
            scope.somefunc = function () {
                var t = 1;
                this.alerterflag = true;
            }
            scope.islastfunc = function () {
                if (angular.isUndefined(this.islast)) {
                    return false;
                }
                var val = this.islast;
                if (val == "false") {
                    return false;

                } else if (val == "true") {
                    return false;
                }
                return !!val;
            }
            scope.isaddbuttonpressedfunc = function () {
                if (angular.isUndefined(this.isaddbuttonpressed)) {
                    return false;
                }
                var val = this.isaddbuttonpressed;
                if (val == "false") {
                    return false;

                } else if (val == "true") {
                    return false;
                }
                return !!val;
            }
        },
        restrict: "EA",
        //replace: true,
        scope: {
            alerterflag: "="
            , islast: "@"
            , isaddbuttonpressed: "@"
            , indexinArrayOfMinValues: "@"
            , claimtypesarray: "@"
            , claimtypesstringsonnlyarray: "@"
            , claimsofminimumvaluearray: "="
            , currentclaimsofminimumvalueitem: "@"
        },
        //templateUrl: "../DirectiveTemplates/TwoTableCells.html"
        templateUrl: "http://localhost:39402/AngularJS/DirectiveTemplates/onerow.html"
    }
});
