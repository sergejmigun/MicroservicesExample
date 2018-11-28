angular.module("common").controller("addOrderProductController", [
    '$scope',
    '$rootScope',
    'productsService',
    function ($scope: ng.IScope,
        $rootScope: ng.IRootScopeService,
        productsService: Products.IProductsService) {
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