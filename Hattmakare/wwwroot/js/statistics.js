//const canvas = document.getElementById('hatChart');
//const labels = JSON.parse(canvas.dataset.hatnames);
//const sales = JSON.parse(canvas.dataset.sales);

//const ctx = canvas.getContext('2d');
//const chart = new Chart(ctx, {
//    type: 'bar',
//    data: {
//        labels: labels,
//        datasets: [{
//            label: 'Antal sålda hattar',
//            data: sales,
//            backgroundColor: 'rgba(54, 162, 235, 0.6)',
//            borderColor: 'rgba(54, 162, 235, 1)',
//            borderWidth: 1
//        }]
//    },
//    options: {
//        scales: {
//            y: {
//                beginAtZero: true,
//                precision: 0
//            }
//        }
//    }
//});

document.addEventListener("DOMContentLoaded", function () {
    // 📊 Hämta canvas och data från attribut
    const canvas = document.getElementById("hatChart");
    if (canvas) {
        const labels = JSON.parse(canvas.dataset.hatnames);
        const sales = JSON.parse(canvas.dataset.sales);

        const ctx = canvas.getContext("2d");
        new Chart(ctx, {
            type: 'bar',
            data: {
                labels: labels,
                datasets: [{
                    label: 'Antal sålda hattar',
                    data: sales,
                    backgroundColor: 'rgba(54, 162, 235, 0.6)',
                    borderColor: 'rgba(54, 162, 235, 1)',
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true,
                        precision: 0
                    }
                }
            }
        });
    }

    // 🧾 Auto-submit för kundfilter
    const customerSelect = document.getElementById("CustomerId");
    if (customerSelect) {
        customerSelect.addEventListener("change", function () {
            this.form.submit();
        });
    }
});
