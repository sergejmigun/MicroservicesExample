angular.module("ordersApp").factory("ordersService", [
    '$q',
    '$http',
    '$rootScope',
    'utils',
    function (
        $q: ng.IQService,
        $http: ng.IHttpService,
        $rootScope: ng.IRootScopeService,
        utils: Common.IUtilsService) {
        return {
            getOrders: function () {
                return $http({
                    method: 'GET',
                    url: '/api/OrdersApi/GetOrders'
                }).then(function (resp) {
                    return utils.toCamelCase(resp.data);
                });
            },
            getOrder: function (orderId) {
                return $http({
                    method: 'GET',
                    url: '/api/OrdersApi/GetOrder/' + orderId
                }).then(function (resp) {
                    return utils.toCamelCase(resp.data);
                });
            },
            deleteOrder: function (orderId) {
                return $http({
                    method: 'DELETE',
                    url: '/api/OrdersApi/DeleteOrder/' + orderId
                }).then(function (resp) {
                    $rootScope.$broadcast('onOrdersChanged');

                    return utils.toCamelCase(resp.data);
                });
            },
            completeOrder: function (orderId) {
                return $http({
                    method: 'PUT',
                    url: '/api/OrdersApi/CompleteOrder/' + orderId
                }).then(function (resp) {
                    $rootScope.$broadcast('onOrdersChanged');

                    return utils.toCamelCase(resp.data);
                });
            },
            addProduct: function (orderId, productId, quantity) {
                return $http({
                    method: 'PUT',
                    url: '/api/OrdersApi/AddProduct',
                    params: {
                        orderId: orderId,
                        productId: productId,
                        quantity: quantity
                    }
                }).then(function (resp) {
                    return utils.toCamelCase(resp.data);
                });
            },
            updateProduct: function (orderId, productId, quantity) {
                return $http({
                    method: 'PUT',
                    url: '/api/OrdersApi/UpdateProduct',
                    params: {
                        orderId: orderId,
                        productId: productId,
                        quantity: quantity
                    }
                }).then(function (resp) {
                    return utils.toCamelCase(resp.data);
                });
            },
            deleteProduct: function (orderId, productId) {
                return $http({
                    method: 'DELETE',
                    url: '/api/OrdersApi/DeleteProduct',
                    params: {
                        orderId: orderId,
                        productId: productId
                    }
                }).then(function (resp) {
                    return utils.toCamelCase(resp.data);
                });
            }
        } as Orders.IOrdersService;
    }
]);