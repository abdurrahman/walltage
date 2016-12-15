function CategoryViewModel() {
    var self = this;
    self.Id = ko.observable("");
    self.Name = ko.observable("");

    var Category = {
        Id: self.Id,
        Name: self.Name
    };

    self.Category = ko.observable();
    self.Categories = ko.observableArray();

    $.ajax({
        url: '/Home/GetCategories',
        cache: false,
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        data: {},
        success: function (data) {
            self.Categories(data);
        }
    });

}