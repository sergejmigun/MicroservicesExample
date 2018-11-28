angular.module("cartApp").factory("cartService", [
    '$http',
    '$rootScope',
    'utils',
    function ($http, $rootScope, utils) {
        return {
            getCart: function () {
                return $http({
                    method: 'GET',
                    url: '/api/CartApi/GetCart'
                }).then(function (resp) {
                    return utils.toCamelCase(resp.data);
                });
            },
            removeCartItem: function (productId) {
                return $http({
                    method: 'DELETE',
                    url: '/api/CartApi/DeleteCartItem',
                    params: {
                        productId: productId
                    }
                }).then(function (resp) {
                    $rootScope.$broadcast('onCartChanged');
                    return resp;
                });
            },
            updateCartItem: function (productId, quantity) {
                return $http({
                    method: 'PUT',
                    url: '/api/CartApi/UpdateCartItem',
                    params: {
                        productId: productId,
                        quantity: quantity
                    }
                }).then(function (resp) {
                    $rootScope.$broadcast('onCartChanged');
                    return resp;
                });
            },
            processOrder: function (submitModel) {
                return $http({
                    method: 'POST',
                    url: '/api/CartApi/ProcessOrder',
                    data: utils.toUpperCamelCase(submitModel)
                }).then(function (resp) {
                    $rootScope.$broadcast('onCartChanged');
                    $rootScope.$broadcast('onOrdersChanged');
                    return resp;
                });
            },
            addToCart: function (productId, quantity) {
                return $http({
                    method: 'PUT',
                    url: '/api/CartApi/AddProduct',
                    params: {
                        productId: productId,
                        quantity: quantity
                    }
                }).then(function (resp) {
                    $rootScope.$broadcast('onCartChanged');
                    return resp;
                });
            }
        };
    }
]);
//# sourceMappingURL=cart.service.js.map