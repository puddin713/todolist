angular.module('myApp', []).controller('CrudCtrl', ['$scope', 'crudService', function ($scope, crudService) {

    $scope.createVM = {}
    $scope.updateVM = {}
    $scope.showAlert = false;

    $scope.loadTableData = function () {
        crudService.serviceCall("GET", "", "").then(function (response) {
            if (response.data.length < 1) {
                $scope.showAlert = true;
                $scope.alertName = "Note!"
                $scope.alertMessage = "These Is No Record To Show."
            }
            $scope.listData = response.data;
        });
    }
    // add new task
    $scope.addTask = function (isValid) {
        if (isValid) {
            $scope.addPhoneNumberForm.$setPristine();
            crudService.serviceCall("POST", "", $scope.createVM).then(function (response) {
                $scope.showAlert = true;
                $scope.alertName = "Success!"
                $scope.alertMessage = "Task Is Added Successfully."
                $scope.createVM = {};
                $scope.loadTableData();
            });
        }
    }
    // update task model
    $scope.openUpdateTaskModal = function (Id) {
        crudService.serviceCall("GET", Id, "").then(function (response) {
            $scope.updateVM = response.data;
        });
    }
    
    $scope.updateTask = function (Id) {

        crudService.serviceCall("PUT", Id, $scope.updateVM).then(function (response) {
            $scope.showAlert = true;
            $scope.alertName = "Success!"
            $scope.alertMessage = "Task Is Updated Successfully."
            $scope.loadTableData();
        });

    }

     //delete task
    $scope.openDeleteTaskModal = function (Id) {
        $scope.recordIdToDelete = Id;
    }
   
    $scope.deleteTask = function (Id) {
        crudService.serviceCall("Delete", Id, "").then(function (response) {
            $scope.showAlert = true;
            $scope.alertName = "Success!"
            $scope.alertMessage = "Task Is Deleted Successfully."
            $scope.loadTableData();
        });
    }

    $scope.loadTableData();

}])
    .service('crudService', ['$http', function ($http) {
        return {
            serviceCall: function (method, parameter, data) {
                return $http({
                    method: method,
                    url: 'https://localhost:44312/api/mylists/' + parameter,
                    data: data
                });
            }
        };
    }]);