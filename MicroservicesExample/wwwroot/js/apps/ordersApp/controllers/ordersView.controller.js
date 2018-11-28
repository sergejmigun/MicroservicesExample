angular.module("ordersApp").controller("ordersViewController", [
    '$scope',
    '$rootScope',
    'ordersService',
    function ($scope, $rootScope, ordersService) {
        $scope.layout.activePage = 'orders';
        function initOrders() {
            ordersService.getOrders().then(function (orders) {
                $scope.orders = orders;
            });
        }
        $scope.deleteOrder = function (orderId) {
            ordersService.deleteOrder(orderId).then(initOrders);
        };
        $scope.getProductsCount = function (order) {
            var count = 0;
            order.items.forEach(function (item) {
                count += item.quantity;
            });
            return count;
        };
        initOrders();
    }
]);
//# sourceMappingURL=ordersView.controller.js.map