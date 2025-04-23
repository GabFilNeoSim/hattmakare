    document.addEventListener("DOMContentLoaded", function () {
    const canvas = document.getElementById("hatChart");
    const ctx = canvas.getContext("2d");
    const rangeButtons = document.querySelectorAll(".range-btn");

    const dailyLabels = JSON.parse(canvas.dataset.dailylabels || "[]");
    const dailySales = JSON.parse(canvas.dataset.dailysales || "[]");
    const quarterlySales = JSON.parse(canvas.dataset.quarterlysales || "[]");
    const monthlySales = JSON.parse(canvas.dataset.monthlysales || "[]");


    let chart;

    function renderLineChart(labels, data) {
        if (chart) chart.destroy();

    chart = new Chart(ctx, {
        type: 'line',
    data: {
        labels: labels,
    datasets: [{
        label: "Antal sålda hattar",
    data: data,
    borderColor: 'rgba(75, 192, 192, 1)',
    fill: false,
    tension: 0.3,
    pointRadius: 3
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

    function updateIntervalData(range) {
        let labelsToUse = [];
        let dataToUse = [];

        if (range === "month") {
            labelsToUse = dailyLabels;
            dataToUse = dailySales;
        } else if (range === "quarter") {
            labelsToUse = ["Q1", "Q2", "Q3", "Q4"];
            dataToUse = quarterlySales;
        } else if (range === "year") {
            labelsToUse = [
                "Jan", "Feb", "Mar", "Apr", "Maj", "Jun",
                "Jul", "Aug", "Sep", "Okt", "Nov", "Dec"
            ];
            dataToUse = monthlySales;
        }

        renderLineChart(labelsToUse, dataToUse);
    }

    // Init with "month"
    updateIntervalData("month");

    rangeButtons.forEach(btn => {
        btn.addEventListener("click", function () {
            rangeButtons.forEach(b => b.classList.remove("active"));
            this.classList.add("active");

            const range = this.dataset.range;
            updateIntervalData(range);
        });
    });
});
