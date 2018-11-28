angular.module("cartApp").controller("completeCustomerController", [
    '$scope',
    '$rootScope',
    function ($scope, $rootScope) {
        $scope.submit = function () {
            $rootScope.$broadcast('onCustomerCompleted', {
                name: $scope.name,
                phone: $scope.phone
            });
        };
    }
]);
//# sourceMappingURL=completeCustomer.controller.js.map