//document.addEventListener("DOMContentLoaded", function () {
//    const canvas = document.getElementById("hatChart");
//    const chartButtons = document.querySelectorAll(".chart-btn");

//    if (!canvas) return;

//    const ctx = canvas.getContext("2d");

//    // Hämta data från attribut
//    const labels = JSON.parse(canvas.dataset.hatnames || "[]");
//    const sales = JSON.parse(canvas.dataset.sales || "[]");
//    const dailyLabels = JSON.parse(canvas.dataset.dailylabels || "[]");
//    const dailySales = JSON.parse(canvas.dataset.dailysales || "[]");

//    let chart;

//    const createChart = (type) => {
//        if (chart) chart.destroy();

//        let data;
//        if (type === "line") {
//            data = {
//                labels: dailyLabels,
//                datasets: [{
//                    label: "Daglig försäljning",
//                    data: dailySales,
//                    fill: false,
//                    borderColor: "rgba(75, 192, 192, 1)",
//                    tension: 0.3,
//                    pointRadius: 3,
//                    pointBackgroundColor: "rgba(75, 192, 192, 1)",
//                }]
//            };
//        } else {
//            data = {
//                labels: labels,
//                datasets: [{
//                    label: "Antal sålda hattar",
//                    data: sales,
//                    backgroundColor: [
//                        'rgba(54, 162, 235, 0.6)',
//                        'rgba(255, 99, 132, 0.6)',
//                        'rgba(255, 206, 86, 0.6)',
//                        'rgba(75, 192, 192, 0.6)',
//                        'rgba(153, 102, 255, 0.6)',
//                        'rgba(255, 159, 64, 0.6)'
//                    ],
//                    borderColor: 'rgba(54, 162, 235, 1)',
//                    borderWidth: 1
//                }]
//            };
//        }

//        chart = new Chart(ctx, {
//            type: type,
//            data: data,
//            options: {
//                responsive: true,
//                maintainAspectRatio: false,
//                scales: type === "bar" || type === "line" ? {
//                    y: {
//                        beginAtZero: true,
//                        precision: 0
//                    },
//                    x: type === "line" ? {
//                        ticks: {
//                            maxRotation: 45,
//                            minRotation: 45
//                        }
//                    } : {}
//                } : {}
//            }
//        });
//    };

//    createChart("bar");

//    // 🟦 Klicka på knapp = byt diagram
//    chartButtons.forEach(btn => {
//        btn.addEventListener("click", function () {
//            const selectedType = this.dataset.type;

//            chartButtons.forEach(b => b.classList.remove("active"));
//            this.classList.add("active");

//            createChart(selectedType);
//        });
//    });

//    function aggregateQuarterly(labels, data) {
//        const quarters = ['Q1', 'Q2', 'Q3', 'Q4'];
//        const quarterMap = [[], [], [], []];

//        labels.forEach((dateStr, i) => {
//            const month = new Date(dateStr).getMonth() + 1;
//            const quarter = Math.floor((month - 1) / 3);
//            quarterMap[quarter].push(data[i]);
//        });

//        return {
//            labels: quarters,
//            data: quarterMap.map(q => q.reduce((a, b) => a + b, 0))
//        };
//    }

//    function aggregateMonthly(labels, data) {
//        const monthMap = Array(12).fill(0);

//        labels.forEach((dateStr, i) => {
//            const month = new Date(dateStr).getMonth();
//            monthMap[month] += data[i];
//        });

//        const monthNames = [
//            "Jan", "Feb", "Mar", "Apr", "Maj", "Jun",
//            "Jul", "Aug", "Sep", "Okt", "Nov", "Dec"
//        ];

//        return {
//            labels: monthNames,
//            data: monthMap
//        };
//    }

//    // Init med månadsvy (dag för dag)
//    renderLineChart(labels, data);

//    // Knapp för att växla intervall
//    document.querySelectorAll(".range-btn").forEach(btn => {
//        btn.addEventListener("click", function () {
//            document.querySelectorAll(".range-btn").forEach(b => b.classList.remove("active"));
//            this.classList.add("active");

//            const range = this.dataset.range;

//            if (range === "month") {
//                renderLineChart(labels, data);
//            } else if (range === "quarter") {
//                const q = aggregateQuarterly(labels, data);
//                renderLineChart(q.labels, q.data);
//            } else if (range === "year") {
//                const y = aggregateMonthly(labels, data);
//                renderLineChart(y.labels, y.data);
//            }
//        });
//    });

//    // 🧾 Auto-submit för filter
//    ["CustomerId", "HatId"].forEach(id => {
//        const el = document.getElementById(id);
//        if (el) {
//            el.addEventListener("change", function () {
//                this.form.submit();
//            });
//        }
//    });
//});

document.addEventListener("DOMContentLoaded", function () {
    const canvas = document.getElementById("hatChart");
    const chartButtons = document.querySelectorAll(".chart-btn");
    const rangeButtons = document.querySelectorAll(".range-btn");

    if (!canvas) return;

    const ctx = canvas.getContext("2d");

    // Hämta data från attribut
    const labels = JSON.parse(canvas.dataset.hatnames || "[]");
    const sales = JSON.parse(canvas.dataset.sales || "[]");
    const dailyLabels = JSON.parse(canvas.dataset.dailylabels || "[]");
    const dailySales = JSON.parse(canvas.dataset.dailysales || "[]");

    let chart;

    // Funktion för att rita linjediagram
    function renderLineChart(labels, data) {
        if (chart) chart.destroy();  // Förstör det gamla diagrammet om det finns.

        chart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: labels,
                datasets: [{
                    label: "Försäljning",
                    data: data,
                    borderColor: 'rgba(75, 192, 192, 1)',
                    fill: false,
                    tension: 0.3,
                    pointRadius: 3,
                    pointBackgroundColor: 'rgba(75, 192, 192, 1)',
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    y: {
                        beginAtZero: true,
                        precision: 0
                    },
                    x: {
                        ticks: {
                            maxRotation: 45,
                            minRotation: 45
                        }
                    }
                }
            }
        });
    }

    // Funktion för att uppdatera data baserat på val av intervall (månad, kvartal, år)
    function updateIntervalData(range) {
        let labelsToUse = [];
        let dataToUse = [];

        if (range === "month") {
            labelsToUse = dailyLabels;
            dataToUse = dailySales;
        } else if (range === "quarter") {
            const aggregated = aggregateQuarterly(labels, sales);
            labelsToUse = aggregated.labels;
            dataToUse = aggregated.data;
        } else if (range === "year") {
            const aggregated = aggregateMonthly(labels, sales);
            labelsToUse = aggregated.labels;
            dataToUse = aggregated.data;
        }

        renderLineChart(labelsToUse, dataToUse);
    }

    // Klickhanterare för range-knappar (månad, kvartal, år)
    rangeButtons.forEach(btn => {
        btn.addEventListener("click", function () {
            rangeButtons.forEach(b => b.classList.remove("active"));
            this.classList.add("active");

            const selectedRange = this.dataset.range;
            updateIntervalData(selectedRange);
        });
    });

    // Funktion för att aggregera data per kvartal
    function aggregateQuarterly(labels, data) {
        const quarters = ['Q1', 'Q2', 'Q3', 'Q4'];
        const quarterMap = [[], [], [], []];

        labels.forEach((dateStr, i) => {
            const month = new Date(dateStr).getMonth();
            const quarter = Math.floor(month / 3);
            quarterMap[quarter].push(data[i]);
        });

        return {
            labels: quarters,
            data: quarterMap.map(q => q.reduce((a, b) => a + b, 0))
        };
    }

    // Funktion för att aggregera data per månad
    function aggregateMonthly(labels, data) {
        const monthMap = Array(12).fill(0);

        labels.forEach((dateStr, i) => {
            const month = new Date(dateStr).getMonth();
            monthMap[month] += data[i];
        });

        const monthNames = [
            "Jan", "Feb", "Mar", "Apr", "Maj", "Jun",
            "Jul", "Aug", "Sep", "Okt", "Nov", "Dec"
        ];

        return {
            labels: monthNames,
            data: monthMap
        };
    }

    // Init med daglig vy (börjar med att visa dagliga data)
    renderLineChart(dailyLabels, dailySales);

    // Klickhanterare för att byta diagramtyp (ex. linjediagram, stapeldiagram)
    chartButtons.forEach(btn => {
        btn.addEventListener("click", function () {
            const selectedType = this.dataset.type;

            chartButtons.forEach(b => b.classList.remove("active"));
            this.classList.add("active");

            createChart(selectedType);
        });
    });

    // Funktion för att skapa diagram baserat på valt diagramtyp (bar, line, pie)
    function createChart(type) {
        if (chart) chart.destroy();  // Förstör det gamla diagrammet om det finns.

        let data;
        if (type === "line") {
            data = {
                labels: dailyLabels,
                datasets: [{
                    label: "Försäljning",
                    data: dailySales,
                    borderColor: 'rgba(75, 192, 192, 1)',
                    fill: false,
                    tension: 0.3,
                    pointRadius: 3,
                    pointBackgroundColor: 'rgba(75, 192, 192, 1)',
                }]
            };
        } else {
            data = {
                labels: labels,
                datasets: [{
                    label: "Antal sålda hattar",
                    data: sales,
                    backgroundColor: [
                        'rgba(54, 162, 235, 0.6)',
                        'rgba(255, 99, 132, 0.6)',
                        'rgba(255, 206, 86, 0.6)',
                        'rgba(75, 192, 192, 0.6)',
                        'rgba(153, 102, 255, 0.6)',
                        'rgba(255, 159, 64, 0.6)'
                    ],
                    borderColor: 'rgba(54, 162, 235, 1)',
                    borderWidth: 1
                }]
            };
        }

        chart = new Chart(ctx, {
            type: type,
            data: data,
            options: {
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    y: {
                        beginAtZero: true,
                        precision: 0
                    },
                    x: type === "line" ? {
                        ticks: {
                            maxRotation: 45,
                            minRotation: 45
                        }
                    } : {}
                }
            }
        });
    }
});

