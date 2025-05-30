﻿let dailyLabels = [];
let dailySales = [];
let quarterlySales = [];
let monthlySales = [];

document.addEventListener("DOMContentLoaded", function () {
    $('.select2').select2({
        width: '100%',
        placeholder: 'Välj eller sök...',
        allowClear: true,
        language: {
            noResults: function () {
                return "Inga resultat";
            }
        }
    });

    const canvas = document.getElementById("hatChart");
    const ctx = canvas.getContext("2d");
    const rangeButtons = document.querySelectorAll(".range-btn");

    dailyLabels = JSON.parse(canvas.dataset.dailylabels || "[]");
    dailySales = JSON.parse(canvas.dataset.dailysales || "[]");
    quarterlySales = JSON.parse(canvas.dataset.quarterlysales || "[]");
    monthlySales = JSON.parse(canvas.dataset.monthlysales || "[]");

    const customerSelect = document.querySelector('[name="CustomerId"]');
    const hatSelect = document.querySelector('[name="HatId"]');

    async function fetchAndUpdateChart() {
        const customerId = customerSelect.value;
        const hatId = hatSelect.value;

        const url = `/statistics/data?customerId=${customerId}&hatId=${hatId}`;
        const response = await fetch(url);
        const data = await response.json();

        const activeRange = document.querySelector(".range-btn.active").dataset.range;

        dailyLabels = data.dailyLabels;
        dailySales = data.dailySales;
        quarterlySales = data.quarterlySales;
        monthlySales = data.monthlySales;

        updateIntervalData(activeRange);
    }

    $('#customerSelect').on('change', fetchAndUpdateChart);
    $('#hatSelect').on('change', fetchAndUpdateChart);
    $('#customerSelect, #hatSelect').on('select2:open', function () {
        setTimeout(function () {
            let searchField = document.querySelector('.select2-container--open .select2-search__field');
            if (searchField) {
                searchField.focus();
            }
        }, 100);
    });

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
                    borderColor: 'rgba(109, 117, 198, 1)',
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
                        ticks: {
                            stepSize: 1,
                            callback: function (value) {
                                if (Number.isInteger(value)) {
                                    return value;
                                }
                                return null;
                            }
                        }
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

    updateIntervalData("month");

    rangeButtons.forEach(btn => {
        btn.addEventListener("click", function () {
            rangeButtons.forEach(b => b.classList.remove("active"));
            this.classList.add("active");

            const range = this.dataset.range;
            updateIntervalData(range);
        });
    });

    fetchAndUpdateChart();
});