app.service("WallpaperService", function ($http) {

    // Get all wallpaper
    this.getWallpapers = function () {
        return $http.get("Home/GetWallpapers");
    };

});