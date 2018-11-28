angular.module("common").factory("utils", [
    '$q',
    function($q) {
        function setCase(obj, isLoweredCamelCase) {
            if (Array.isArray(obj)) {
                var newArray = [];

                obj.forEach(function(item) {
                    newArray.push(setCase(item, isLoweredCamelCase));
                });

                return newArray;
            } else if (typeof obj === 'object') {
                var newObj = {};

                for (var prop in obj) {
                    var camelCasedProp = (isLoweredCamelCase
                        ? prop[0].toLowerCase()
                        : prop[0].toUpperCase()) + prop.substr(1);

                    newObj[camelCasedProp] = setCase(obj[prop], isLoweredCamelCase);
                }

                return newObj;
            } else {
                return obj;
            }
        }

        return {
            toCamelCase: function(obj) {
                return setCase(obj, true);
            },
            toUpperCamelCase: function(obj) {
                return setCase(obj, false);
            }
        } as Common.IUtilsService;
    }
]);