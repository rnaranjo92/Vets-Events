var EventsController = function () {
    var init = function () {
        $(".js-toggle-attendance").click(toggleAttendance); 
    };
    var toggleAttendance = function (e) {
        var button = $(e.target);
        if (button.hasClass("btn-default")) {
            $.post("/api/attendances", { eventId: button.attr("data-event-id") })
                .done(function () {
                    button
                        .removeClass("btn-default")
                        .addClass("btn-info")
                        .text("Going");
                })
                .fail(function () {
                    alert("Something failed!");
                });
        } else {
            $.ajax({
                url: "/api/attendances/" + button.attr("data-event-id"),
                method: "DELETE"
            })
                .done(function () {
                    button
                        .removeClass("btn-info")
                        .addClass("btn-default")
                        .text("Going?");
                })
                .fail(function () {
                    alert("Something failed");
                });
        }
    };

    return {
        init:init
    }
}();

