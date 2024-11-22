// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.
// site.js
// Aquí va el código para generar el gráfico, por ejemplo, utilizando Chart.js
var datos = @Html.Raw(Json.Serialize(datos));  // Datos que se pasan desde el servidor

// Gráfico de Tiempo de Respuesta
var ctx = document.getElementById('graficoTiempoRespuesta').getContext('2d');
var graficoTiempoRespuesta = new Chart(ctx, {
    type: 'bar',
    data: {
        labels: datos.map(d => d.Nombre),
        datasets: [{
            label: 'Tiempo de Respuesta',
            data: datos.map(d => d.TiempoRespuesta),
            backgroundColor: 'rgba(54, 162, 235, 0.2)',
            borderColor: 'rgba(54, 162, 235, 1)',
            borderWidth: 1
        }]
    },
    options: {
        responsive: true,
        scales: {
            y: {
                beginAtZero: true
            }
        }
    }
});

// Gráfico de Mitigación
var ctx2 = document.getElementById('mitigacionChart').getContext('2d');
var mitigacionChart = new Chart(ctx2, {
    type: 'bar',
    data: {
        labels: ['0%', '25%', '50%', '75%', '100%'],
        datasets: [{
            label: 'Porcentaje de Mitigación',
            data: [20, 50, 30, 70, 100],
            backgroundColor: ['rgba(255, 99, 132, 0.2)', 'rgba(54, 162, 235, 0.2)', 'rgba(255, 206, 86, 0.2)', 'rgba(75, 192, 192, 0.2)', 'rgba(153, 102, 255, 0.2)'],
            borderColor: ['rgba(255, 99, 132, 1)', 'rgba(54, 162, 235, 1)', 'rgba(255, 206, 86, 1)', 'rgba(75, 192, 192, 1)', 'rgba(153, 102, 255, 1)'],
            borderWidth: 1
        }]
    },
    options: {
        responsive: true,
        scales: {
            y: {
                beginAtZero: true
            }
        }
    }
});