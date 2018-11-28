angular.module("common").controller("orderedProductsViewController", [
    '$scope',
    '$rootScope',
    function ($scope, $rootScope) {
        $scope.getTotal = function () {
            if ($scope.orderedItems) {
                var total = 0;
                $scope.orderedItems.items.forEach(function (orderItem) {
                    var subTotal = orderItem.quantity * orderItem.price;
                    total += subTotal;
                });
                return total;
            }
        };
        $scope.removeItem = function (orderedItem) {
            var index = $scope.orderedItems.items.indexOf(orderedItem);
            $rootScope.$broadcast('onRemoveOrderedItem', $scope.orderedItems.items[index]);
            $scope.orderedItems.items.splice(index, 1);
        };
        $scope.onChangeQuantity = function (orderedItem) {
            $rootScope.$broadcast('onChangeQuantity', orderedItem);
        };
    }
]);
//# sourceMappingURL=orderedProductsView.controller.js.map