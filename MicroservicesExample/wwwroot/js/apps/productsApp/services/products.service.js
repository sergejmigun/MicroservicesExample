angular.module("productsApp").factory("productsService", [
    '$http',
    'utils',
    function ($http, utils) {
        return {
            getProducts: function () {
                return $http({
                    method: 'GET',
                    url: '/api/ProductsApi/GetProducts'
                }).then(function (resp) {
                    return utils.toCamelCase(resp.data);
                });
            },
            getProduct: function (productId) {
                return $http({
                    method: 'GET',
                    url: '/api/ProductsApi/GetProduct/' + productId
                }).then(function (resp) {
                    return utils.toCamelCase(resp.data);
                });
            },
            editProduct: function (product) {
                return $http({
                    method: 'PUT',
                    url: '/api/ProductsApi/EditProduct',
                    data: utils.toUpperCamelCase(product)
                });
            },
            createProduct: function (product) {
                return $http({
                    method: 'POST',
                    url: '/api/ProductsApi/CreateProduct',
                    data: utils.toUpperCamelCase(product)
                });
            },
            deleteProduct: function (productId) {
                return $http({
                    method: 'DELETE',
                    url: '/api/ProductsApi/DeleteProduct/' + productId
                });
            }
        };
    }
]);
//# sourceMappingURL=products.service.js.map