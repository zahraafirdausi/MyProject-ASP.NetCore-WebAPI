//$(document).ready(function () {

//    $('#loginForm').on("submit", function (event) {
//        //console.log("clicked submit");
//        event.preventDefault();
//        debugger;
//        var Account = new Object();

//        Account.email = $('#email').val();
//        Account.password = $('#password').val();
//        $.ajax({
//            url: "http://localhost:8082/LoginVM",
//            type: 'POST',
//            data: JSON.stringify(Account), //mau ngirim datanya di transform dalam bentuk json
//            contentType: "application/json; charset=utf-8",
//            success: function (result) {
//                console.log(result);

//                $.post("/Account/Login", { email: Account.email })
//                    .done(function () {
//                        var getToken = result.token
//                        sessionStorage.setItem("token", getToken) //simpan token pada session storage
//                        console.log(getToken)
//                        debugger;
//                        swal({
//                            title: result.message,
//                            icon: "success",
//                        })
//                            .then((successAlert) => {
//                                if (successAlert) {
//                                    //location.replace("/departments/index");
//                                    location.replace("/chart/index");
//                                } else {
//                                    //location.replace("/departments/index");
//                                    location.replace("/chart/index");
//                                }
//                            });
//                    })
//                    .fail(function () {
//                        alert("Login Gagal! fail");
//                    })
//                    .always(function () {

//                    });
//            }, error: function (errormessage) {
//                //alert("Login Gagal!"); // Handle error response
//                swal({
//                    title: "Login Gagal!",
//                    text: "Harap Periksa Kembali Email dan Password Anda!",
//                    icon: "error"
//                })
//            }

//        });
//    })

//});

function login() {
    var Account = new Object();

    Account.email = $('#email').val();
    Account.password = $('#password').val();
    $.ajax({
        url: "http://localhost:8082/LoginVM",
        type: 'POST',
        data: JSON.stringify(Account), //mau ngirim datanya di transform dalam bentuk json
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            console.log(result);

            $.post("/Account/Login", { email: Account.email })
                .done(function () {
                    var getToken = result.token
                    sessionStorage.setItem("token", getToken) //simpan token pada session storage
                    console.log(getToken)
                    debugger;
                    swal({
                        title: result.message,
                        icon: "success",
                    })
                        .then((successAlert) => {
                            if (successAlert) {
                                //location.replace("/departments/index");
                                location.replace("/chart/index");
                            } else {
                                //location.replace("/departments/index");
                                location.replace("/chart/index");
                            }
                        });
                })
                .fail(function () {
                    alert("Login Gagal! fail");
                })
                .always(function () {

                });
        }, error: function (errormessage) {
            //alert("Login Gagal!"); // Handle error response
            swal({
                title: "Login Gagal!",
                text: "Harap Periksa Kembali Email dan Password Anda!",
                icon: "error"
            })
        }

    });
}

function Logout() {
    //$('#logoutForm'){
        sessionStorage.removeItem('token');
        window.location.href = 'https://localhost:7189';
    };
    //$('#logoutForm').click(function (e) {
        //e.preventDefault();
        //$.ajax({
            //url: '/api/logout',
            //type: 'POST',
            //success: function () {
            //    //sessionStorage.removeItem('email');
            //    sessionStorage.removeItem('token');
            //    window.location.href = 'https://localhost:7189';
            //},
            //error: function () {
            //    alert('An error occurred while processing your request. Please try again later.');
            //}
            //sessionStorage.removeItem('token');
            //window.location.href = 'https://localhost:7189';
        //});
    //});
    //sessionStorage.removeItem('email');
    //sessionStorage.removeItem('token');
    // redirect ke halaman login
    //window.location.href = 'https://localhost:7189';

//}