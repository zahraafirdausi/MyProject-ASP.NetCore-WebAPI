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
                Piechart()
            },
            error: function (error) {
                console.log(error);
            }
        });
    })
    
});
    //--------------------
    //-------------
    //- PIE CHART -
    //-------------

    //-------------
    // Get context with jQuery - using jQuery's .get() method.
    //var donutData = {
    //    labels: [
    //        'Chrome',
    //        'IE',
    //        'FireFox',
    //        'Safari',
    //        'Opera',
    //        'Navigator',
    //    ],
    //    datasets: [
    //        {
    //            data: [700, 500, 400, 600, 300, 100],
    //            backgroundColor: ['#f56954', '#00a65a', '#f39c12', '#00c0ef', '#3c8dbc', '#d2d6de'],
    //        }
    //    ]
    //}

    //var pieChartCanvas = $('#pieChart').get(0).getContext('2d')
    //var pieData = donutData;
    //var pieOptions = {
    //    maintainAspectRatio: false,
    //    responsive: true,
    //}
    ////Create pie or douhnut chart
    //// You can switch between pie and douhnut using the method below.
    //new Chart(pieChartCanvas, {
    //    type: 'pie',
    //    data: pieData,
    //    options: pieOptions
    //})
    //Piechart();

//})

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

