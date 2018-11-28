angular.module("productsApp").controller("productsViewController", [
    '$scope',
    '$rootScope',
    'productsService',
    'cartService',
    function ($scope, $rootScope, productsService, cartService) {
        $scope.layout.activePage = 'products';
        function initProducts() {
            productsService.getProducts().then(function (products) {
                $scope.products = products;
            });
        }
        $scope.createProduct = function () {
            $rootScope.$broadcast('onProductManage');
        };
        $scope.editProduct = function (productId) {
            $rootScope.$broadcast('onProductManage', { productId: productId });
        };
        $scope.deleteProduct = function (productId) {
            productsService.deleteProduct(productId).then(function () {
                $rootScope.$broadcast('onProductsChange');
            });
        };
        $scope.addToCart = function (productId) {
            cartService.addToCart(productId);
        };
        $scope.$on('onProductsChange', function () {
            initProducts();
        });
        initProducts();
    }
]);
//# sourceMappingURL=productsView.controller.js.map