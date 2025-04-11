document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'dayGridMonth',
        firstDay: 1,
        locale: 'sv',
        events: '/Home/PopulateCalendar',
        eventClick: function (info) {
            const date = info.event.startStr;
            $.get('/Home/PopulateCalendarPopUp', { date: date }, function (data) {
                $('#orderModalBody').html(data);
                $('#orderModal').modal('show');
            });
        }
    });

    calendar.render();
})