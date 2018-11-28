angular.module("layoutApp").controller("layoutController", [
    '$scope',
    'cartService',
    'ordersService',
    function ($scope, cartService, ordersService) {
        $scope.layout = {};
        function initCartItemsAmount() {
            cartService.getCart().then(function (cart) {
                $scope.layout.cartItemsAmount = cart.items.length;
            });
        }
        function initActiveOrdersAmount() {
            ordersService.getOrders().then(function (orders) {
                $scope.layout.activeOrdersAmount = orders.filter(function (order) {
                    return order.status === 'Pending';
                }).length;
            });
        }
        initCartItemsAmount();
        initActiveOrdersAmount();
        $scope.$on('onCartChanged', initCartItemsAmount);
        $scope.$on('onOrdersChanged', initActiveOrdersAmount);
    }
]);
//# sourceMappingURL=layout.controller.js.map