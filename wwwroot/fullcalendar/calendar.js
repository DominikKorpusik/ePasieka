
document.addEventListener('DOMContentLoaded', function () {


    const startCalendarEvent = document.getElementById("startCalendarEvent");
    const endCalendarEvent = document.getElementById("endCalendarEvent");
        var calendarEl = document.getElementById('calendar');
        var calendar = new FullCalendar.Calendar(calendarEl, {

            plugins: ['interaction', 'dayGrid', 'timeGrid', 'list'],

            events: '/Home/jSON',

            eventTextColor: 'white',

            locale: 'pl', // Zmiana języka na polski

            navLinks: true, // przenosi do danego dnia

            selectable: true, //Pozwala użytkownikowi wyróżnić wiele dni lub przedziałów czasowych

            selectMirror: true,

            eventLimit: true, // Ustala limit wyświetlanych danych w kalendarzu

            eventTimeFormat: {
                hour: 'numeric',
                minute: '2-digit',
                meridiem: false
            },

            views: {
                dayGrid: {
                    eventLimit: 4
                },
                timeGrid: {
                    eventLimit: 6
                }
            },

            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'dayGridMonth,timeGridWeek,timeGridDay,listMonth'
            },

            //Odpowiada na kliknięcie na datę (pojedyńczo)
            dateClick: function () {
            },

            //Odpowiada za pobranie dat z kalendarza poprzez wybranie zakresu dat
            select: function (info) {
            }

            
            
        });

    //Wprowadzenie pojedynczej daty poprzez kliknięcie na kalendarz

    calendar.on('dateClick', function (info) {
        let data = info.dateStr + "T00:00";
        startCalendarEvent.value = data;
        endCalendarEvent.value = data;
    });

    //Wprowadzenie zakresu daty

    calendar.on('select', function (info) {
        startCalendarEvent.value = info.startStr + "T00:00";
        endCalendarEvent.value = info.endStr + "T00:00";
    });

    calendar.setOption('height', 550);   //Ustawienie wyskokości kalendarza
    calendar.setOption('locale', 'pl');  //Ustawienie języka na PL
    calendar.render();

});


