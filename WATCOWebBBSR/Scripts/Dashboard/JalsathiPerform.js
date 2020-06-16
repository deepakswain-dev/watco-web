$(function () {

    "use strict";

    /* Morris.js Charts */
    var line = new Morris.Bar({
        element: 'bar-chart',
        data: [
            { Jalsathi: ' Nirmali Kahnar', Amount: 48261 },
            { Jalsathi: 'Anupama Pattanayak', Amount: 46260 },
            { Jalsathi: 'Liza Nayak', Amount: 23741 },
            { Jalsathi: 'Nirupama Balasamanta', Amount: 33397 },
            { Jalsathi: 'Pratima Das', Amount: 61858 },
            { Jalsathi: 'Charulata Das', Amount: 31896 }
        ],
        barColors: ['#00847d'],
        xkey: 'Jalsathi',
        ykeys: ['Amount'],
        labels: ['Amount'],
        hideHover: 'auto',
        behaveLikeLine: true,
        xLabelAngle: 45,
        resize: true
    });
    //Donut Chart
    var donut = new Morris.Donut({
        element: 'donut-chart',
        resize: true,
        colors: ["#3c8dbc", "#f56954", "#00a65a", '#fc8710', '#FF6541', '#A4ADD3'],
        data: [
            { label: ' Nirmali Kahnar', value: 48261 },
            { label: 'Anupama Pattanayak', value: 46260 },
            { label: 'Liza Nayak', value: 23741 },
            { label: 'Nirupama Balasamanta', value: 33397 },
            { label: 'Pratima Das', value: 61858 },
            { label: 'Charulata Das', value: 31896 }
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