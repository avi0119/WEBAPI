angular.module("customFilters", [])
.filter("unique", function () {
    return function (data, propertyName, finalPropertyToTakeValueFrom) {
        if (angular.isArray(data) && angular.isString(propertyName)) {
            var results = [];
            var keys = {};
            for (var i = 0; i < data.length; i++) {
                var val;

                if (angular.isUndefined(data[i][propertyName])) {
                    continue;
                } else {
                    val = data[i][propertyName];

                    if (angular.isUndefined(finalPropertyToTakeValueFrom) || val == null) {
                        continue;;
                    } else {
                        console.log("item " + i + " is " + val.CategoryName);
                        angular.isString(finalPropertyToTakeValueFrom)
                        {
                            if (angular.isUndefined(val[finalPropertyToTakeValueFrom])) {
                                continue;
                            }
                            else {
                                val = val[finalPropertyToTakeValueFrom];
                            }

                        }
                    }
                    if (val == '' || angular.isUndefined(val)) {
                        continue;
                    }
                    if (angular.isUndefined(keys[val])) {
                        keys[val] = true;
                        results.push(val);
                    }
                }
            }
            return results;
        } else {
            return data;
        }
    }
}).filter("range", function ($filter) {
    return function (data, page, size) {
        if (angular.isArray(data) && angular.isNumber(page) && angular.isNumber(size)) {
            var start_index = (page - 1) * size;
            if (data.length < start_index) {
                return [];
            } else {
                return $filter("limitTo")(data.splice(start_index), size);
            }
        } else {
            return data;
        }
    }
}).filter("pageCount", function () {
    return function (data, size) {
        if (angular.isArray(data)) {
            var result = [];
            for (var i = 0; i < Math.ceil(data.length / size) ; i++) {
                result.push(i);
            }
            return result;
        } else {
            return data;
        }
    }
}).filter("uniqueAllChildren", function () {
    return function (data, propertyName, finalPropertyToTakeValueFrom) {
        if (angular.isArray(data) && angular.isString(propertyName)) {
            var results = [];
            var keys = {};
            var currObject;
            for (var i = 0; i < data.length; i++) {
                var val;

                if (angular.isUndefined(data[i][propertyName])) {
                    continue;
                } else {
                    val = data[i][propertyName];
                    currObject = val;
                    if (angular.isUndefined(finalPropertyToTakeValueFrom) && (!(val == null))) {
                        if (angular.isUndefined(keys[val])) {
                            keys[val] = true;
                            results.push(currObject);
                        }
                        continue;;
                    }

                    if (angular.isUndefined(finalPropertyToTakeValueFrom) || val == null) {

                        continue;;
                    } else {
                        console.log("item " + i + " is " + val.CategoryName);
                        angular.isString(finalPropertyToTakeValueFrom)
                        {
                            if (angular.isUndefined(val[finalPropertyToTakeValueFrom])) {
                                continue;
                            }
                            else {
                                val = val[finalPropertyToTakeValueFrom];
                            }

                        }
                    }
                    if (val == '' || angular.isUndefined(val)) {
                        continue;
                    }
                    if (angular.isUndefined(keys[val])) {
                        keys[val] = true;
                        results.push(currObject);
                    }
                }
            }
            return results;
        } else {
            return data;
        }
    }
})
.filter("MinimumPerProperty", function (AllItemsOfOneTypeFilter) {

    
    return function (data, PropertToAscertainMinimum, TypeProperty) {
        var minFunc = function (myArray, propname) {
            //var lowest = Number.POSITIVE_INFINITY;
            //var highest = Number.NEGATIVE_INFINITY;
            var tmp;
            var indexl
            for (var i = myArray.length - 1; i >= 0; i--) {
                if (i == (myArray.length - 1)) {
                    lowest = (myArray[i])[propname];
                    index = myArray.length - 1;
                    continue;
                }
                tmp = (myArray[i])[propname];
                if (tmp < lowest) {
                    owest = tmp;
                    index = i;
                }
                //if (tmp > highest) highest = tmp;
            }
            return index;
        }
        var resultsHash = {};
        var results = [];
        for (var i = 0; i < data.length; i++) {
            var curType = (data[i])[TypeProperty];
            //var cond = angular.isUndefined( resultsHash );
            var cond;
            if (angular.isUndefined( resultsHash )==false) {
                cond= (!(resultsHash.hasOwnProperty(curType)));
            } else 
            {
            cond=true;
           }
            if (cond==true)
            {
                var currentArrayOfOneType = AllItemsOfOneTypeFilter(data, TypeProperty, curType);
                var requiredindex = minFunc(currentArrayOfOneType, PropertToAscertainMinimum);
                resultsHash[curType] = true;
                results.push(currentArrayOfOneType[requiredindex]);
            } 
        }
        return results;
    }
}).filter("AllItemsOfOneType", function () {
    return function (data,  PropertyName,value) {
        var results=[];
        for (i = 0; i < data.length; i++) {
            var currValue = (data[i])[PropertyName];
            if (currValue == value) {
                results.push(data[i]);
            }
        }
        return results;
    }
})
;