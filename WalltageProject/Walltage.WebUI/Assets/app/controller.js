
app.controller('WallpaperController', function ($scope, WallpaperService) {

    GetAllWallpaper();

    function GetAllWallpaper() {
        var getData = WallpaperService.getWallpapers();
        getData.then(function (wall) {
            $scope.wallpapers = wall.data;
        }, function () {
            alert("Error in getting records");
        });
    }
});