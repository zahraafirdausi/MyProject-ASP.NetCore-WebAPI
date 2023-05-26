$(document).ready(function () {
    //-------------
    //- BAR CHART -
    //-------------
    // Fetch data from the "Departments" table
    $(function () {
        debugger;
        $.ajax({

            url: 'http://localhost:8082/Rows',
            type: 'GET',
            headers: {
                'Authorization': 'Bearer ' + sessionStorage.getItem('token')
            },
            dataType: 'json',
            
            success: function (result) {
                debugger;
                // Prepare the chart data
                var departmentCounts = {}; //Definisikan Objek
                var objDept = result.data;
                var nameDept = objDept.map(function (department) {
                    return department.departmentName;
                });

                // Iterasi melalui data
                for (var i = 0; i < result.data.length; i++) {
                    var employee = result.data[i];
                    var departmentName = employee.departmentName;

                    // Periksa apakah departmentName sudah ada dalam departmentCounts
                    if (departmentCounts.hasOwnProperty(departmentName)) {
                        // Jika sudah ada, tambahkan 1 ke jumlahnya
                        departmentCounts[departmentName]++;
                    } else {
                        // Jika belum ada, inisialisasi dengan 1
                        departmentCounts[departmentName] = 1;
                    }
                }

                var departmentNames = Object.keys(departmentCounts).map(function (key) {
                    return key; //Object terdiri dari Key dan Values (Ambil Key dari Object DepartmentCounts)
                });
                var totalEmpBasedOnDept = Object.values(departmentCounts).map(function (value) {
                    return value; //Object terdiri dari Key dan Values (Ambil Value dari Object DepartmentCounts)
                });

                var chartData = {
                    labels: departmentNames, // nameDept
                    datasets: [{
                        label: 'Departments', //'Departments'
                        backgroundColor: 'rgba(60,141,188,0.9)',
                        borderColor: 'rgba(60,141,188,0.8)',
                        pointRadius: false,
                        pointColor: '#3b8bba',
                        pointStrokeColor: 'rgba(60,141,188,1)',
                        pointHighlightFill: '#fff',
                        pointHighlightStroke: 'rgba(60,141,188,1)',
                        data: totalEmpBasedOnDept //data_dept.map(function (department) {
                        //return department.tempt_count;
                        //}) 
                    }]
                }

                var chartOptions = {
                    maintainAspectRatio: false,
                    responsive: true,
                    legend: {
                        display: false
                    },
                    scales: {
                        xAxes: [{
                            gridLines: {
                                display: false,
                            }
                        }],
                        yAxes: [{
                            gridLines: {
                                display: true,
                            }
                        }]
                    }
                };

                var chartCanvas = $('#barChart').get(0).getContext('2d');
                var chart = new Chart(chartCanvas, {
                    type: 'bar',
                    data: chartData,
                    options: chartOptions
                });


                //GetCountDept(function (countDept) {
                //    var data_dept = [];
                //    var objDept = result.data;
                //    var idDept = objDept.map(function (department) {
                //        return department.idDept;
                //    });
                //    var nameDept = objDept.map(function (department) {
                //        return department.nameDept;
                //    });

                //    //var valueDept = nameDept.map(function (deptName) {
                //    //    return countDept[deptName] || 0;
                //    //});
                //    console.log(nameDept);
                //    idDept.forEach(function (result) {
                //        var tempt_count = countDept[result] || 0;
                //        data_dept.push({ result: result, tempt_count: tempt_count })
                //    });

                //    var chartData = {
                //        labels: nameDept,
                //        datasets: [{
                //            label: 'Departments', //'Departments'
                //            backgroundColor: 'rgba(60,141,188,0.9)',
                //            borderColor: 'rgba(60,141,188,0.8)',
                //            pointRadius: false,
                //            pointColor: '#3b8bba',
                //            pointStrokeColor: 'rgba(60,141,188,1)',
                //            pointHighlightFill: '#fff',
                //            pointHighlightStroke: 'rgba(60,141,188,1)',
                //            data: [3, 4, 2, 8] //data_dept.map(function (department) {
                //            //return department.tempt_count;
                //            //})
                //        }]
                //    }

                //    var chartOptions = {
                //        maintainAspectRatio: false,
                //        responsive: true,
                //        legend: {
                //            display: false
                //        },
                //        scales: {
                //            xAxes: [{
                //                gridLines: {
                //                    display: false,
                //                }
                //            }],
                //            yAxes: [{
                //                gridLines: {
                //                    display: true,
                //                }
                //            }]
                //        }
                //    };

                //    var chartCanvas = $('#barChart').get(0).getContext('2d');
                //    var chart = new Chart(chartCanvas, {
                //        type: 'bar',
                //        data: chartData,
                //        options: chartOptions
                //    });
                //});
                Piechart();
                stakedbar();
                barchart();
            },
            error: function (error) {
                console.log(error);
            }
        });
    })
    
});
    

function Piechart() {
    $.ajax({
        url: 'http://localhost:8082/api/Employees',
        type: 'GET',
        //headers: {
        //    'Authorization': 'Bearer ' + sessionStorage.getItem('token')
        //},
        dataType: 'json',
        success: function (result) {
            //Calculate gender counts // Array untuk menghitung jumlah karyawan berdasarkan gender
            var genderCounts = { 
                //Male: 0,
                //Female: 0
            };

            var objDept = result.data;
            debugger;
            /*console.log(keys(data).length);*/
            for (var i = 0; i < result.data.length; i++) {
                var employee = result.data[i];
                var gender = employee.gender;
                //console.log(employee)
                //console.log(gender)
                
                if (genderCounts.hasOwnProperty(gender)) {
                    if (gender == 0) {
                        genderCounts[0]++; // Jumlah karyawan laki-laki
                    } else {
                        genderCounts[1]++; // Jumlah karyawan perempuan
                    }
                     
                } else {
                    genderCounts[gender] = 1;
                }
                
            }
            console.log(genderCounts)
            var totalEmpBasedOnEmp = Object.values(genderCounts).map(function (value) {
                return value; //Object terdiri dari Key dan Values (Ambil Value dari Object DepartmentCounts)
            });
            
            // Prepare the chart data
            var pieData = {
                labels: ['Male', 'Female'],
                datasets: [{
                    data: totalEmpBasedOnEmp,//[20, 30],
                    backgroundColor: ['#cbd5e0', '#f56954'],
                }]
            };

            var pieChartCanvas = $('#pieChart').get(0).getContext('2d');
            var pieOptions = {
                maintainAspectRatio: false,
                responsive: true,
            };

            // Create pie chart
            new Chart(pieChartCanvas, {
                type: 'pie',
                data: pieData,
                options: pieOptions
            });
        },
        error: function (error) {
            console.log(error);
        }
    });
};

function stakedbar() {
    //---------------------
    //- STACKED BAR CHART -
    //---------------------
    //var areaChartData = {
    //    labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
    //    datasets: [
    //        {
    //            label: 'Digital Goods',
    //            backgroundColor: 'rgba(60,141,188,0.9)',
    //            borderColor: 'rgba(60,141,188,0.8)',
    //            pointRadius: false,
    //            pointColor: '#3b8bba',
    //            pointStrokeColor: 'rgba(60,141,188,1)',
    //            pointHighlightFill: '#fff',
    //            pointHighlightStroke: 'rgba(60,141,188,1)',
    //            data: [28, 48, 40, 19, 86, 27, 90]
    //        },
    //        {
    //            label: 'Electronics',
    //            backgroundColor: 'rgba(210, 214, 222, 1)',
    //            borderColor: 'rgba(210, 214, 222, 1)',
    //            pointRadius: false,
    //            pointColor: 'rgba(210, 214, 222, 1)',
    //            pointStrokeColor: '#c1c7d1',
    //            pointHighlightFill: '#fff',
    //            pointHighlightStroke: 'rgba(220,220,220,1)',
    //            data: [65, 59, 80, 81, 56, 55, 40]
    //        },
    //    ]
    //}

    //var barChartData = $.extend(true, {}, areaChartData)
    //var stackedBarChartCanvas = $('#stackedBarChart').get(0).getContext('2d')
    //var stackedBarChartData = $.extend(true, {}, barChartData)

    //var stackedBarChartOptions = {
    //    responsive: true,
    //    maintainAspectRatio: false,
    //    scales: {
    //        xAxes: [{
    //            stacked: true,
    //        }],
    //        yAxes: [{
    //            stacked: true
    //        }]
    //    }
    //}

    //new Chart(stackedBarChartCanvas, {
    //    type: 'bar',
    //    data: stackedBarChartData,
    //    options: stackedBarChartOptions
    //})

    var temp_gender = {};
    var departmentCounts = {};
    $.ajax({
        url: 'http://localhost:8082/api/Departments',
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + sessionStorage.getItem('token')
        },
        dataType: 'json',
        success: function (departmentsData) {
            // Fetch data from the "Employees" API
            $.ajax({
                url: 'http://localhost:8082/api/Employees',
                type: 'GET',
                //headers: {
                //    'Authorization': 'Bearer ' + sessionStorage.getItem('token')
                //},
                dataType: 'json',
                success: function (employeesData) {
                     
                    var obj = employeesData.data;
                    debugger;
                    // Iterasi melalui data karyawan
                    for (var i = 0; i < obj.length; i++) {
                        console.log(i)
                        var employee = employeesData.data[i];
                        var departmentId = employee.departmentId;
                        var gender = employee.gender;
                        var male = 0;
                        var female = 1;
                        if (!temp_gender.hasOwnProperty(departmentId)) {
                            temp_gender[departmentId] = {};
                        }
                        // Periksa apakah departmentId sudah ada dalam departmentCounts
                        if (!temp_gender[departmentId].hasOwnProperty(gender)) {
                            temp_gender[departmentId][gender] = 1;
                            
                        } else {
                            temp_gender[departmentId][gender]++;
                        }
                    }
                    var objDept = departmentsData.data;
                    var departmentNames = objDept.map(function (department) {
                        return department.name;
                    });
                    debugger;
                    var maleCounts = [];
                    var femaleCounts = [];

                    // Ambil jumlah karyawan laki-laki dan perempuan berdasarkan departemen
                    for (var j = 0; j < departmentNames.length; j++) {
                        var departmentId = objDept[j].id;
                        if (temp_gender.hasOwnProperty(departmentId)) {
                            var counts = temp_gender[departmentId];
                            console.log(counts)
                            maleCounts.push(counts[0]);
                            femaleCounts.push(counts[1]);
                        } else {
                            maleCounts.push(0);
                            femaleCounts.push(0);
                        }
                    }
                    debugger;
                    var chartData = {
                        labels: departmentNames,
                        datasets: [
                            {
                                label: 'Male',
                                backgroundColor: 'rgba(60,141,188,0.9)',
                                borderColor: 'rgba(60,141,188,0.8)',
                                pointRadius: false,
                                pointColor: '#3b8bba',
                                pointStrokeColor: 'rgba(60,141,188,1)',
                                pointHighlightFill: '#fff',
                                pointHighlightStroke: 'rgba(60,141,188,1)',
                                data: maleCounts
                            },
                            {
                                label: 'Female',
                                backgroundColor: 'rgba(255,99,132,0.9)',
                                data: femaleCounts
                            }
                        ]
                    };

                    var chartOptions = {
                        maintainAspectRatio: false,
                        responsive: true,
                        legend: {
                            display: true
                        },
                        scales: {
                            xAxes: [{
                                stacked: true,
                                gridLines: {
                                    display: false
                                }
                            }],
                            yAxes: [{
                                stacked: true,
                                stepSize: 1,
                                gridLines: {
                                    display: true
                                }
                            }]
                        }
                    };

                    var chartCanvas = $('#stackedBarChart').get(0).getContext('2d');
                    var chart = new Chart(chartCanvas, {
                        type: 'bar',
                        data: chartData,
                        options: chartOptions
                    });
                },
                error: function (error) {
                    console.log(error);
                }
            });
        },
        error: function (error) {
            console.log(error);
        }
    });
}

function barchart() {
    var barChartCanvas = $('#barChart1').get(0).getContext('2d')
    var temp_gender = {};
    var departmentCounts = {};
    $.ajax({
        url: 'http://localhost:8082/api/Departments',
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + sessionStorage.getItem('token')
        },
        dataType: 'json',
        success: function (departmentsData) {
            // Fetch data from the "Employees" API
            $.ajax({
                url: 'http://localhost:8082/api/Employees',
                type: 'GET',
                //headers: {
                //    'Authorization': 'Bearer ' + sessionStorage.getItem('token')
                //},
                dataType: 'json',
                success: function (employeesData) {

                    var obj = employeesData.data;
                    debugger;
                    // Iterasi melalui data karyawan
                    for (var i = 0; i < obj.length; i++) {
                        console.log(i)
                        var employee = employeesData.data[i];
                        var departmentId = employee.departmentId;
                        var gender = employee.gender;
                        var male = 0;
                        var female = 1;
                        if (!temp_gender.hasOwnProperty(departmentId)) {
                            temp_gender[departmentId] = {};
                        }
                        // Periksa apakah departmentId sudah ada dalam departmentCounts
                        if (!temp_gender[departmentId].hasOwnProperty(gender)) {
                            temp_gender[departmentId][gender] = 1;

                        } else {
                            temp_gender[departmentId][gender]++;
                        }
                    }
                    var objDept = departmentsData.data;
                    var departmentNames = objDept.map(function (department) {
                        return department.name;
                    });
                    debugger;
                    var maleCounts = [];
                    var femaleCounts = [];

                    // Ambil jumlah karyawan laki-laki dan perempuan berdasarkan departemen
                    for (var j = 0; j < departmentNames.length; j++) {
                        var departmentId = objDept[j].id;
                        if (temp_gender.hasOwnProperty(departmentId)) {
                            var counts = temp_gender[departmentId];
                            console.log(counts)
                            maleCounts.push(counts[0]);
                            femaleCounts.push(counts[1]);
                        } else {
                            maleCounts.push(0);
                            femaleCounts.push(0);
                        }
                    }
                    debugger;


                    var chartData = {
                        labels: departmentNames,
                        datasets: [
                            {
                                label: 'Male',
                                backgroundColor: 'rgba(60,141,188,0.9)',
                                borderColor: 'rgba(60,141,188,0.8)',
                                pointRadius: false,
                                pointColor: '#3b8bba',
                                pointStrokeColor: 'rgba(60,141,188,1)',
                                pointHighlightFill: '#fff',
                                pointHighlightStroke: 'rgba(60,141,188,1)',
                                data: maleCounts
                            },
                            {
                                label: 'Female',
                                backgroundColor: 'rgba(255,99,132,0.9)',
                                data: femaleCounts
                            }
                        ]
                    };

                    var barChartData = $.extend(true, {}, chartData)
                    var temp0 = chartData.datasets[0]
                    var temp1 = chartData.datasets[1]
                    barChartData.datasets[0] = temp1
                    barChartData.datasets[1] = temp0

                    var barChartOptions = {
                        responsive: true,
                        stepSize: 1,
                        maintainAspectRatio: false,
                        datasetFill: false
                    }


                    new Chart(barChartCanvas, {
                        type: 'bar',
                        data: barChartData,
                        options: barChartOptions
                    })
                },
                error: function (error) {
                    console.log(error);
                }
            });
        },
        error: function (error) {
            console.log(error);
        }
    });
}
function GetCountDept(callback) {
    var countDept = {};
    $.ajax({
        url: "http://localhost:8082/api/Employees",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        //headers: {
        //    'Authorization': 'Bearer ' + sessionStorage.getItem('token')
        //},
        Succes: function (result) {
            debugger;
            var employees = result.data;

            employees.forEach(function (employee) {
                var deptId = employee.deptId;
                if (!countDept.hasOwnProperty(deptId)) {
                    countDept[deptId] = 1;
                } else {
                    countDept[deptId]++;
                }
            });

            for (var deptId in countDept) {
                if (countDept.hasOwnProperty(deptId)) {
                    console.log('Department ID:', deptId, 'Total Employee:', countDept[deptId]);
                }
            }
            debugger;
            callback(countDept);
        },
        error: function (error) {
            console.log('Error', error);
        }

    });
}

