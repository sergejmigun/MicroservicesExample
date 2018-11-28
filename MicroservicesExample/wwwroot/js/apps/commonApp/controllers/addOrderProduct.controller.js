angular.module("common").controller("addOrderProductController", [
    '$scope',
    '$rootScope',
    'productsService',
    function ($scope, $rootScope, productsService) {
        productsService.getProducts().then(function (products) {
            $scope.products = products;
        });
        $scope.addProduct = function () {
            $rootScope.$broadcast('onAddProduct', {
                id: $scope.orderProduct,
                quantity: $scope.quantity
            });
        };
    }
]);
//# sourceMappingURL=addOrderProduct.controller.js.map