
var AttendanceService = function () {
    var createAttendance = function (eventId, done, fail) {
        $.post("/api/attendances", { eventId: eventId })
            .done(done)
            .fail(fail);
    };
    var deleteAttendance = function (eventId, done, fail) {
        $.ajax({
            url: "/api/attendances/" + eventId,
            method: "DELETE"
        })
            .done(done)
            .fail(fail);
    };

    return {
        createAttendance: createAttendance,
        deleteAttendance: deleteAttendance
    }
}();




var EventsController = function (attendanceService) {
    var button;

    var init = function () {
        $(".js-toggle-attendance").click(toggleAttendance); 
    };
    var toggleAttendance = function (e) {
        button = $(e.target);

        var eventId = button.attr("data-event-id");

        if (button.hasClass("btn-default")) 
            attendanceService.createAttendance(eventId, done, fail);
         else 
            attendanceService.deleteAttendance(eventId, done, fail);
    };
    

    var done = function () {
        var text = (button.text() === "Going") ? "Going?" : "Going";
        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };
    var fail = function () {
        alert("Something failed");
    };
    return {
        init: init
    };
}(AttendanceService);

