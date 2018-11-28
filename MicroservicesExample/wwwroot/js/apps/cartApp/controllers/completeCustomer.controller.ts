angular.module("cartApp").controller("completeCustomerController", [
    '$scope',
    '$rootScope',
    function ($scope: ng.IScope,
        $rootScope: ng.IRootScopeService) {
        $scope.submit = function () {
            $rootScope.$broadcast('onCustomerCompleted', {
                name: $scope.name,
                phone: $scope.phone
            });
        };
    }
]);