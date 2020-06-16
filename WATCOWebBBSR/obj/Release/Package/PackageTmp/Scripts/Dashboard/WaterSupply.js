$(function () {

    "use strict";

    /* Morris.js Charts */    
    var line = new Morris.Line({
        element: 'line-chart',
        data: [
            { period: '2020-05-03', Flow: 9010},
            { period: '2020-05-04', Flow: 9480},
            { period: '2020-05-05', Flow: 8750},
            { period: '2020-05-06', Flow: 9570},
            { period: '2020-05-07', Flow: 9030},
            { period: '2020-05-08', Flow: 8960},
            { period: '2020-05-09', Flow: 9500}
        ],
        lineColors: ['#819C79'],
        xkey: 'period',
        ykeys: ['Flow'],
        ymin: 8500, ymax : 9500,
        labels: ['Flow'],
        xLabels: 'day',
        xLabelAngle: 45,
        xLabelFormat: function (d) {
            var weekdays = new Array(7);
            weekdays[0] = "SUN";
            weekdays[1] = "MON";
            weekdays[2] = "TUE";
            weekdays[3] = "WED";
            weekdays[4] = "THU";
            weekdays[5] = "FRI";
            weekdays[6] = "SAT";

            return weekdays[d.getDay()] + '-' +
                ("0" + (d.getMonth() + 1)).slice(-2) + '-' +
                ("0" + (d.getDate())).slice(-2);
        },
        resize: true
    });

    //Donut Chart
    var donut = new Morris.Donut({
        element: 'donut-chart',
        resize: true,
        colors: ["#3c8dbc", "#f56954", "#00a65a", '#fc8710', '#FF6541', '#A4ADD3', '#766B56'],
        data: [
            { label: '2020-05-03', value: 9010 },
            { label: '2020-05-04', value: 9480 },
            { label: '2020-05-05', value: 8750 },
            { label: '2020-05-06', value: 9570 },
            { label: '2020-05-07', value: 9030 },
            { label: '2020-05-08', value: 8960 },
            { label: '2020-05-09', value: 9500 }
        ],
        hideHover: 'auto'
    });
    //Fix for charts under tabs
    $('.box ul.nav a').on('shown.bs.tab', function () {
        //area.redraw();
        donut.redraw();
        line.redraw();
    });
});