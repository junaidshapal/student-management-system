
//ShowInPopup
var showInPopup = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            debugger
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html(title);
            $("#form-modal").modal("show");
        }
    });
}

//AddOrUpdate Student
var AddOrUpdate = (Id) => {

    $.ajax({
        type: "GET",
        url: "/Students/AddOrUpdate",
        data: {
            id: Id
        },
        success: function (res) {
            debugger
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html('Update Student');
            $("#form-modal").modal('show');

        }
    });
}

//SaveStudent
function SaveStudent() {
    debugger
    var obj = {
        studentId: $("#StudentId").val(),
        name: $("#Name").val(),
        age: $("#Age").val(),
        fatherName: $("#FatherName").val(),
        regNo: $("#RegNo").val(),
        department: $("#Department").val(),
        semester: $("#Semester").val(),
        contactNumber: $("#ContactNumber").val(),
        cnic: $("#CNIC").val(),
        gender: $("#Gender").val(),
        address: $("#Address").val(),
    };

    var formData = new FormData();

    formData.append("studentId", obj.studentId);
    formData.append("name", obj.name);
    formData.append("age", obj.age);
    formData.append("fatherName", obj.fatherName);
    formData.append("regNo", obj.regNo);
    formData.append("department", obj.department);
    formData.append("semester", obj.semester);
    formData.append("contactNumber", obj.contactNumber);
    formData.append("cnic", obj.cnic);
    formData.append("gender", obj.gender);
    formData.append("address", obj.address);

    if (obj.name == "" || obj.age == "" || obj.cnic == "" || obj.address == "" || obj.contactNumber == "" || obj.department == "" ||
        obj.fatherName == "" || obj.regNo == "" || obj.semester == "") {
        $.notify("Please enter the required fields", "error");
        //$.ajax({
        //    success: function (res) {
        //        $("#view-all-students").html(res.html);
        //        $.notify("Please enter the required fields!", "warning");
        //    },
        //});
        return false;
    }


    //check if regsiteration number exists
    $.ajax({
        type: "GET",
        url: "/Students/CheckRegisterationNumber",
        data: {
            Id: obj.studentId
        },
        success: function (response) {
            if (response) {
                debugger
                $.notify("Registration number or CNIC already exists!", "warning");
            }
            else {
                var image = $('#fileImage').get(0).files[0];

                formData.append("Image", image);

                $.ajax({
                    type: "POST",
                    url: "/Students/AddOrUpdate",
                    contentType: false,
                    processData: false,
                    data: formData,
                    success: function (res) {
                        $("#view-all-students").html(res);
                        $("#form-modal .modal-body").html('');
                        $("#form-modal .modal-title").html('');
                        $("#form-modal").modal('hide');
                        //$.notify('Submitted successfully', { globalposition: 'top center', classname: 'success' });
                        $.notify("Submitted successfully", "success");

                    },
                    error: function (err) {
                        console.log(err);
                    }
                });
            }
        }
    });
}


///Search Students///
function Filter() {
    var searchString = $('#txtSearchString').val();
    $.ajax({
        type: "GET",
        url: "/Students/IndexPartial",
        data: {
            searchString: searchString
        },
        success: function (response) {
            $('#view-all-students').html(response);
        }
    })
}




//DeleteStudent
DeleteStudent = form => {

    if (confirm('Are you sure you want to delete the record of this Student?')) {
        try {
            $.ajax({
                type: "POST",
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,

                success: function (res) {
                    debugger
                    $("#view-all-students").html(res.html);
                    $.notify("Deleted successfully", "error");
                },
                error: function (err) {
                    console.log(err);
                }
            });
        } catch (e) {
            console.log(e);
        }

    }
    return false;
}



//////LECTURERS

//AddOREdit Lecturer
var AddOrEdit = (Id) => {
    $.ajax({
        type: "GET",
        url: "/Lecturers/AddOrEdit",
        data: {
            id: Id,
        },
        success: function (res) {
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html('Update Lecturer');
            $("#form-modal").modal('show');
        }
    });
}

///Save Teacher

function SaveLecturer() {
    var obj = {
        lecturerId: $("#LecturerId").val(),
        firstName: $("#FirstName").val(),
        lastName: $("#LastName").val(),
        age: $("#Age").val(),
        department: $("#Department").val(),
        cNIC: $("#CNIC").val(),
        roleId: $('#RoleId').val()
    };

    $.ajax({
        type: "POST",
        url: "/Lecturers/AddOrEdit",
        data: {
            lecturer: obj,
        },
        success: function (res) {
            $("#view-all-lecturers").html(res);
            $("#form-modal .modal-body").html('');
            $("#form-modal .modal-title").html('');
            $("#form-modal").modal('hide');
            $.notify("Submitted successfully", "success");
        },
        error: function (err) {
            console.log(err);
        }
    });
}



//DeleteLecturer
DeleteLecturer = form => {

    if (confirm('Are you sure you want to delete the record of this Lecturer?')) {
        try {
            $.ajax({
                type: "POST",
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,

                success: function (res) {
                    debugger
                    $("#view-all-lecturers").html(res.html);
                    $.notify("Deleted successfully", "error");
                },
                error: function (err) {
                    console.log(err);
                }
            });
        } catch (e) {
            console.log(e);
        }

    }
    return false;
}
