var ViewModel = function () {
    var self = this; // make 'this' available to subfunctions or closures
    self.Users = ko.observableArray(); // enables data binding
    var usersUri = "/api/Users";
    function getAllUsers() {
        $.getJSON(usersUri).done(function (data) {
            self.Users(data);
        });
    }
    // Fetch the initial data
    getAllUsers();
};
ko.applyBindings(new ViewModel()); // sets up the data binding