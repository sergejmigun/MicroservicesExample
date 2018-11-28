angular.module("cartApp").controller("cartController", [
    '$scope',
    '$rootScope',
    'cartService',
    function (
        $scope: ng.IScope,
        $rootScope: ng.IRootScopeService,
        cartService: Cart.ICartService) {
        $scope.layout.activePage = 'cart';

        function initCartView() {
            cartService.getCart().then(function (cart) {
                $scope.orderedItems = cart;
            });
        }

        $scope.$on('onAddProduct', function (arg, item) {
            cartService.addToCart(item.id, item.quantity).then(initCartView);
        });

        $scope.$on('onRemoveOrderedItem', function (arg, item) {
            cartService.removeCartItem(item.productId).then(initCartView);
        });

        $scope.$on('onChangeQuantity', function (arg, item) {
            cartService.updateCartItem(item.productId, item.quantity);
        });

        $scope.$on('onCustomerCompleted', function (arg, item) {
            $scope.orderedItems.customer = item;

            cartService.processOrder($scope.orderedItems).then(initCartView);
            ($('#completeCustomerModal') as any).modal('toggle');
        });

        initCartView();
    }
]);