var table = $('#TB_Department').DataTable({
    "ajax":
    {
        url: "http://localhost:8082/api/Departments",
        type: "GET",
        headers: {
            'Authorization': 'Bearer ' + sessionStorage.getItem('token')
        },
        "datatype": "json",
        "dataSrc": "data",
        //success: function (result) {
        //    console.log(result)
        //}
    },

    "columns": [
        {
            render: function (data, type, row, meta) {
                //return meta.row + meta.settings._iDisplayStart + 1 + "."
                return meta.row + 1 + "."
            }


        },
        {
            "data": "name"
        },
        {
            "render": function (data, type, row) {
                //return '<button class="btn btn-warning " data-placement="left" data-toggle="tooltip" data-animation="false" title="Edit" onclick="return GetById(' + row.id + ')"><i class="fa fa-pen"></i></button >' + '&nbsp;' +
                //    '<button class="btn btn-danger" data-placement="right" data-toggle="tooltip" data-animation="false" title="Delete" onclick="return Delete(' + row.id + ')"><i class="fa fa-trash"></i></button >'

                return '<button class="btn btn-warning fas fa-edit"  data-placement="left" data-toggle="tooltip" data-animation="false" title="Edit" onclick="return GetById(' + row.id + ')"></button> ' +
                    '<button class="btn btn-danger far fa-trash-alt"  data-placement="right" data-toggle="tooltip" data-animation="false" title="Delete" onclick="return Delete(' + row.id + ')"></button>';
            }

        }
    ],
    //"responsive": true,
    "columnDefs": [
        { "orderable": false, "targets": [0, 2] } // disable sorting on columns 1 and 3
    ]
    //,
    ////ordering: false
    //responsive:true
});


$(document).ready(function () {
    table
})

function Save() {
    debugger;
    var Department = new Object();    //Department.Name = $('#Name').val();    //---30-04-2023    var name = $('#Name').val().trim();
    if (name === '') {
        //alert('Nama Department tidak boleh kosong!');
        GagalNullInsert()
        return false;
    }
    Department.Name = name;    //---    $.ajax({        type: 'POST',        url: 'http://localhost:8082/api/Departments',        headers: {
            'Authorization': 'Bearer ' + sessionStorage.getItem('token')
        },        data: JSON.stringify(Department), //mau ngirim datanya di transform dalam bentuk json        contentType: "application/json; charset=utf-8"    }).then((result) => {        //debugger;        if (result.status == result.status == 201 || result.status == 204 || result.status == 200) {            Succes();        } else {
            //alert("Data gagal dimasukkan! Nama Department Sudah Tersedia!");
            Gagal();
        }
    }).catch((error) => {
        //alert("Data gagal dimasukkan! Nama Department Sudah Tersedia!");
        Gagal();
    })
}

function GetById(id) {
    debugger;    $.ajax({        url: "http://localhost:8082/api/Departments/" + id,        headers: {
            'Authorization': 'Bearer ' + sessionStorage.getItem('token')
        },        type: "GET",        contentType: "application/json; charset=utf-8",        dataType: "json",
        success: function (result) {
            var obj = result.data;
            $('#Id').val(obj.id);
            $('#Name').val(obj.name);
            $('#myModal').modal('show');
            $('#Save').hide();
            $('#Update').show();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);// Handle error response
        }
    });
}

function Delete(id) {
    DeleteAlert().then((response) => {
        if (response.isConfirmed) {
            $.ajax({
                url: "http://localhost:8082/api/Departments/" + id,
                headers: {
                    'Authorization': 'Bearer ' + sessionStorage.getItem('token')
                },
                type: "DELETE",
                dataType: "json",
            }).then((result) => {
                if (result.status == 200) {
                    Swal.fire({
                        title: 'Deleted!',
                        text: 'Your file has been deleted.',
                        icon: 'success',
                        didClose: () => {
                            table.ajax.reload();
                        }
                    })
                }
            })
        } else {
            Swal.fire(
                'Cancelled',
                'Your file is safe :)',
                'error'

            )
        }
    })
}

function ClearScreen() {
    $('#Id').val('');
    $('#Name').val('');
    $('#Update').hide();
    $('#Save').show();
}


function Update() {
    var Department = new Object();
    Department.id = $('#Id').val();
    Department.Name = $('#Name').val();

    //--- 30-04-2023
    if (Department.Name.trim() == "") { // cek apakah input kosong atau hanya spasi
        //alert('Nama Department tidak boleh kosong!');
        GagalNullIUpdate()
        return;
    }
    //---

    $.ajax({
        url: "http://localhost:8082/api/Departments",
        headers: {
            'Authorization': 'Bearer ' + sessionStorage.getItem('token')
        },
        type: "PUT",
        data: JSON.stringify(Department),
        contentType: "application/json; charset=utf-8"
    }).done(function (result, textStatus, xhr) {
        debugger;
        if (xhr.status === 200) {
            UpdateAlert()
        } else {
            alert("Data gagal Diperbaharui");
        }

    }).fail(function (xhr, textStatus, errorThrown) {
        //alert("Terjadi kesalahan saat memperbarui data.");
        UpdateGagal()
    });

}

//---------SWEETALERT--------------------------------------
function Succes() {
    Swal.fire({
        icon: 'success',
        tittle: 'Greats...',
        text: 'Data Berhasil Ditambahkan!',
        didClose: () => {
            table.ajax.reload();
        }
    })
}

function Gagal() {
    Swal.fire({
        icon: 'warning',
        title: 'Data gagal Ditambahkan!',
        text: 'Nama Department Sudah Tersedia!',
        didClose: () => {
            table.ajax.reload();
        }
    })
}

function GagalNullInsert() {
    Swal.fire({
        icon: 'warning',
        title: 'Data gagal Ditambahkan!',
        text: 'Nama Department tidak boleh kosong atau hanya mengandung spasi!',
        didClose: () => {
            table.ajax.reload();
        }
    })
}

function GagalNullIUpdate() {
    Swal.fire({
        icon: 'warning',
        title: 'Data Gagal di Update!',
        text: 'Nama Department tidak boleh kosong atau hanya mengandung spasi!',
        didClose: () => {
            table.ajax.reload();
        }
    })
}

function UpdateAlert() {
    Swal.fire({
        icon: 'success',
        tittle: 'Greats...',
        text: 'Data Berhasil Diperbarui!',
        didClose: () => {
            table.ajax.reload();
        }
    })
}

function UpdateGagal() {
    Swal.fire({
        icon: 'warning',
        title: 'Data Gagal di Update!',
        text: 'Nama Department Sudah Tersedia!',
        didClose: () => {
            table.ajax.reload();
        }
    })
}

function DeleteAlert() {
    return Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        //showCancelButton: true,
        showDenyButton: true,
        confirmButtonText: 'Yes, delete it!',
        denyButtonText: `Don't delete it!`,
    })
}


    //$.ajax({
    //    url: "http://localhost:8082/api/Departments",
    //    type: "PUT",
    //    data: JSON.stringify(Department),
    //    contentType: "application/json; charset=utf-8"
    //}).then((result) => {
    //    //debugger;
    //    if (result.status == result.status == 201 || result.status == 204 || result.status == 200 || result.status == 'undefined') {
    //        alert("Data Berhasil Diperbaharui");
    //        //table.ajax.reload();
    //    }
    //    else {
    //        console.log(result.status);
    //        alert("Data gagal Diperbaharui");
    //        //table.ajax.reload();
    //    }
    //}).catch((error) => {
    //    console.log(error);
    //    alert("Terjadi kesalahan pada server");
    //    //table.ajax.reload();
    //});
//}).then((result) => {
//        debugger;
//        if (result.status == result.status == 201 || result.status == 204 || result.status == 200) {
//            alert("Data Berhasil Dirubah");
//            table.ajax.reload();
//        }
//        else {
//            alert("Data Gagal Dirubah...!");
//            table.ajax.reload();
//        }
//    });
//function Update() {
//    var Department = new Object();
//    Department.id = $('#Id').val();
//    Department.Name = $('#Name').val();
//    $.ajax({
//        url: "http://localhost:8082/api/Departments",
//        type: "PUT",
//        //dataType: "json",
//        data: JSON.stringify(Department),
//        contentType: "application/json; charset=utf-8"
//    }).then((result) => {//        debugger;//        if (result.status == 200) {//            alert("Data Berhasil Diperbaharui");//            table.ajax.reload();//        }//        else {//            alert("Data gagal Diperbaharui");//            table.ajax.reload();//        }//    });
//    //}).then((result) => {//    //    debugger;//    //    if (result.status == result.status == 201 || result.status == 204 || result.status == 200) {//    //        alert("Department name updated successfully!");//    //    }//    //    else {//    //        alert("An error occurred while updating department name.");//    //    }//    //})
//}

////function Delete(id) {//    debugger;//    //const dataId = id;//    $.ajax({//        url: "http://localhost:8082/api/Departments/" + id,//        type: "DELETE",//        dataType: "json",//    }).then((result) => {//        debugger;//        if (result.status == 200) {//            DeleteAlert()//            //    //alert(result.message);//        }//        //else {//        //    alert(result.message);//        //}//    });//}
//function Update() {
//    //debugger;
//    var Department = new Object();
//    Department.id = $('#Id').val();
//    Department.name = $('#Name').val();
//    $.ajax({
//        type: "PUT",
//        url: "http://localhost:8082/api/Departments", // Use the ID to construct the update URL
//        data: JSON.stringify(Department),
//        contentType: "application/json; charset=utf-8"
//        }).then((result) => {//    //    debugger;//        if (result.status == result.status == 201 || result.status == 204 || result.status == 200) {//            alert("Department name updated successfully!");//            table.ajax.reload();//        }//        else {//            alert("An error occurred while updating department name.");//            table.ajax.reload();//        }//        })
////}).then((result) => {////    //debugger;////    if (result.status == result.status == 201 || result.status == 204 || result.status == 200) {////        alert("Data Berhasil Dimasukkan");////    }////    else {////        alert("Data gagal dimasukkan");////    }////})
////    }).then((result) => {
////        //debugger;
////        if (result.status == 200) {
////        alert("Data Berhasil Dirubah");
////        table.ajax.reload();
////    }
////        else {
////        alert("Data Gagal Dirubah...!");
////        table.ajax.reload();
////    }
////})

//}


////handle edit
//function edit(id) {
//    debugger;
//    // Get user input
//    //const newName = prompt("Enter the new department name:");
//    // Make AJAX call to update the department
//    $.ajax({
//        url: "http://localhost:8082/api/Departments" + id, // Use the ID to construct the update URL
//        type: "PUT",
//        dataType: "json",
//        //data: { name: newName },
//        success: function (response) {
//            $('#myModal').modal('show');
//            alert("Department name updated successfully!"); // Handle success response
//        },
//        error: function (error) {
//            alert("An error occurred while updating department name."); // Handle error response
//        }
//    });
//}

//function remove(id) {
//    if (confirm("Are you sure you want to delete this department?")) {
//        $.ajax({
//            url: `http://localhost:8082/api/Departments/${id}`, // Use the ID to construct the delete URL
//            type: "DELETE",
//            dataType: "json",
//            success: function (response) {
//                //$(document).on('click', '#btn-delete', function () { var deleteId = $(this).data('target'); var departmentName = 'Marketing'; // replace with dynamically fetched department name   $(deleteId).find('#department-name').text(departmentName);   $(deleteId).load('modal.html', function() {     $(deleteId).modal('show');   }); });
//                alert("Department deleted successfully!"); //handle remove
//            },
//            error: function (error) {
//                alert("An error occurred while deleting department.");
//            }
//        });
//    }
//}





//"data": null,
                //"render": function (data, type, row) {
                //    return '<button class="btn btn-warning fas fa-edit" onclick="edit(' + row.departmentId + ')"></button> ' +
                //        '<button class="btn btn-danger far fa-trash-alt" onclick="remove(' + row.departmentId + ')"></button>';
                //}

                // Menambahkan kolom "Action" berisi tombol "Edit" dan "Delete" dengan Bootstrap

//    $('#TB_Department').DataTable({
//        "ajax":
//        {
//            url: "http://localhost:8082/api/Departments",
//            type: "GET",
//            "datatype": "json",
//            "dataSrc": "data",
//        },
//        "columns": [
//            {
//                "data": "",
//                "defaultContent": ""
//            },
//            { "data": "name" },
//            {
//                "data": null,
//                "render": function (data, type, row) {
//                    var updateId = "modal-edit-" + data.id;
//                    var deleteId = "modal-delete-" + data.id;
//                    return '<button class="btn btn-warning fas fa-edit" data-toggle="modal" data-target="#' + updateId + '"></button> ' +
//                        '<button class="btn btn-danger far fa-trash-alt" id="btn-delete" data-toggle="modal" data-target="#' + deleteId + '"></button>' +
//                        `<div class="modal fade" id="` + updateId + `">
//                            <div class="modal-dialog">
//                                <div class="modal-content">
//                                    <div class="modal-header">
//                                        <h4 class="modal-title">Edit Nama Department</h4>
//                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
//                                        <span aria-hidden="true">&times;</span>
//                                        </button>
//                                    </div>
//                                    <form>
//                                        <div class="card-body">
//                                            <div class="form-group">
//                                                <label for="name">Name</label>
//                                                <input type="text" class="form-control" id="name" value="`+ data.name + `" name="name">
//                                            </div>
//                                        </div>
//                                        <!-- /.card-body -->
//                                       <div class="card-footer">
//                                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
//                                            <button type="submit" class="btn btn-primary">Save</button>
//                                       </div>
//                                    </form>
//                                    <!-- /.modal-content -->
//                                </div>
//                        <!-- /.modal-dialog -->
//                            </div>
//                        </div>`+
//                        `<div class="modal fade" id="` + deleteId + `" tabindex="-1">
//                            <div class="modal-dialog">
//                                <div class="modal-content">
//                                    <div class="modal-header">
//                                        <h4 class="modal-title">Hapus Data</h4>
//                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
//                                            <span aria-hidden="true">&times;</span>
//                                        </button>
//                                    </div>
//                                <div class="modal-body">
//                                    <h4>Are you sure you want to delete this department? `+ data.name + `?</h4>
//                                </div>
//                                <div class="modal-footer justify-content-between">
//                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
//                                    <button type="button" class="btn btn-danger">Hapus</button>
//                                </div>
//                            </div>
//                            <!-- /.modal-content -->
//                        </div>
//                        <!-- /.modal-dialog -->
//                    </div>`;
//                }
//            }
//        ],
//        "rowCallback": function (row, data, index) {
//            $('td:eq(0)', row).html(index + 1); //generate index
//        }
//    })
//})



//$(document).ready(function () {
//    $('#TB_Department').DataTable({
//        "ajax":
//        {
//            url: "http://localhost:8082/api/Departments",
//            type: "GET",
//            "datatype": "json",
//            "dataSrc": "data",
//            /*success: function (result) {
//                console.log(result)
//            }*/
//        },
//        "columns": [
//            { "data": "name" }
//        ]
//    })
//})