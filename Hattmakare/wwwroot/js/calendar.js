document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'dayGridMonth',
        firstDay: 1,
        locale: 'sv',
        buttonText: {
            today: 'Idag'
        },
        events: '/Home/PopulateCalendar',
        eventClick: function (info) {
            const date = info.event.startStr;
            $.get('/Home/PopulateCalendarPopUp', { date: date }, function (data) {
                $('#orderCalendarModalBody').html(data);
                $('#orderCalendarModal').modal('show');
            });
        }
    });

    calendar.render();

    $(document).on("click", ".calendar-view-order", function () {
        const itemId = $(this).data("item-id");

        $('#orderCalendarModalBody').html("");
        $('#orderCalendarModal').modal('hide');

        $.get('/Order/edit', { oid: itemId }, function (data) {
            $('#editOrderBody').html(data);
            $('#editOrderModal').modal('show');
        });
    });
})