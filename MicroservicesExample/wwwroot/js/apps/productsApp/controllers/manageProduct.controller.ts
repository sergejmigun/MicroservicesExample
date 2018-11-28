angular.module("productsApp").controller("manageProductController", [
    '$scope',
    '$rootScope',
    'productsService',
    function ($scope: ng.IScope,
        $rootScope: ng.IRootScopeService,
        productsService: Products.IProductsService) {
        function initEditing(productId) {
            $scope.actionTitle = 'Edit product';

            productsService.getProduct(productId).then(function (product) {
                $scope.product = product;
            });
        }

        function initCreation() {
            $scope.product = {};
            $scope.actionTitle = 'Create product';
        }

        $scope.$on('onProductManage', function (ignore, data) {
            if (data && data.productId) {
                initEditing(data.productId);
            } else {
                initCreation();
            }
        });

        $scope.saveProduct = function () {
            if ($scope.product.id) {
                productsService.editProduct($scope.product).then(function () {
                    $('#manageProductModal').modal('toggle');
                });
            } else {
                productsService.createProduct($scope.product).then(function (id) {
                    $('#manageProductModal').modal('toggle');
                });
            }

            $rootScope.$broadcast('onProductsChange');
        };
    }
]);