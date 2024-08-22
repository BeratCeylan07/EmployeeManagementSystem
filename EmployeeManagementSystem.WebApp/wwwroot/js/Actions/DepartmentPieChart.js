export function createDepartmentPieChar(data,charttId) {
    let filteredData = null;
    let values = null;
    let labels =  null;
    if (charttId === "departmentPieChar"){
         filteredData = data.filter(department => department.employeeCount > 0);
         labels = filteredData.map(department => department.name);
         values = filteredData.map(department => department.employeeCount);
    }else if(charttId === "departmentPieChartTotalSalary"){
         filteredData = data.filter(department => department.salaryTotal > 0);
         labels = filteredData.map(department => department.name);
         values = filteredData.map(department => department.salaryTotal);
    }


    // Pie Chart oluşturma
    var ctx = document.getElementById(charttId);
    var myPieChart = new Chart(ctx, {
        type: 'doughnut',
        data: {
            labels: labels,
            datasets: [{
                data: values,
                backgroundColor: ['#4e73df', '#1cc88a', '#36b9cc'], // Renkleri dinamik yapabilirsiniz
                hoverBackgroundColor: ['#2e59d9', '#17a673', '#2c9faf'],
                hoverBorderColor: "rgba(234, 236, 244, 1)",
            }],
        },
        options: {
            maintainAspectRatio: false,
            tooltips: {
                backgroundColor: "rgb(255,255,255)",
                bodyFontColor: "#858796",
                borderColor: '#dddfeb',
                borderWidth: 1,
                xPadding: 15,
                yPadding: 15,
                displayColors: false,
                caretPadding: 10,
            },
            legend: {
                display: true
            },
            cutoutPercentage: 80,
        },
    });
}
