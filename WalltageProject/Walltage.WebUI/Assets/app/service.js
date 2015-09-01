app.service("WallpaperService", function ($http) {

    // Get all wallpaper
    this.getWallpapers = function () {
        return $http.get("Home/GetWallpapers");
    };

    this.getCategories = function () {
        return $http.get("Home/GetCategories");
    };


    this.getWallpaper = function (wallpaperId) {
        var response = $http({
            method: "post",
            url: "Home/FindWallpaperById",
            params: { id: wallpaperId }
        });
        return response;
    };

    this.getSearchResult = function (q) {
        var response = $http({
            method: "get",
            url: "Home/Search",
            params: { q: q }
        });
        return response;
    };
});