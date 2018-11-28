angular.module("ordersApp").controller("ordersDetailsController", [
    '$scope',
    '$rootScope',
    '$timeout',
    'ordersService',
    function ($scope: ng.IScope,
        $rootScope: ng.IRootScopeService,
        $timeout: ng.ITimeoutService,
        ordersService: Orders.IOrdersService) {
        $scope.layout.activePage = 'orders';

        function initOrderItemsView() {
            ordersService.getOrder($scope.orderId).then(function (order) {
                $scope.order = order;
                $scope.orderedItems = order;
                $scope.orderedItems.readonly = order.status === 'Processed';
            });
        }

        $scope.completeOrder = function () {
            ordersService.completeOrder($scope.orderId).then(initOrderItemsView);
        };

        $scope.$on('onAddProduct', function (arg, item) {
            ordersService.addProduct($scope.orderId, item.id, item.quantity).then(initOrderItemsView);
        });

        $scope.$on('onRemoveOrderedItem', function (arg, item) {
            ordersService.deleteProduct($scope.orderId, item.productId).then(initOrderItemsView);
        });

        $scope.$on('onChangeQuantity', function (arg, item) {
            ordersService.updateProduct($scope.orderId, item.productId, item.quantity);
        });

        $timeout(initOrderItemsView);
    }
]);