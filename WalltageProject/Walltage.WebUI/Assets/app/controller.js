
app.controller('WallpaperController', function ($scope, WallpaperService) {

    GetAllWallpaper();
    GetAllCategory();

    function GetAllWallpaper() {
        var getData = WallpaperService.getWallpapers();
        getData.then(function (wall) {
            $scope.wallpapers = wall.data;
        }, function () {
            alert("Error in getting records");
        });
    };

    function GetAllCategory() {
        var getData = WallpaperService.getCategories();
        getData.then(function (category) {
            $scope.categories = category.data;
        }, function () {
            $scope.message = "Error in getting Categories";
        });
    };


});

app.controller('SearchController', function ($scope, WallpaperService, $location) {

    
    $scope.query = '';
    $location.path("Home/Search?q=" + $scope.query);


    //$scope.submit = function () {
    //    var getData = WallpaperService.getSearchResult($scope.query);
    //    getData.then(function (wallpaper) {
    //        $scope.location = $scope.go + $scope.q;
    //        console.log($scope.go);
    //    }, function () {
    //        alert("Not found");
    //    });
    //};


});
