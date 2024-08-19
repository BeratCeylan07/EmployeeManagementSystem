import {makeAjaxRequest} from "../ajax/GenericAjax.js";
import {createDepartmentPieChar} from './DepartmentPieChart.js';
let userID = "";
let token = "";
async function PullSession() {
    var endPointHead = "/Session/PullSession";


    await $.ajax({
        url:endPointHead,
        type: 'POST',
        contentType: "application/json",
        success: function(response) {
            userID = response.userID;
            token = response.token;

        },
        error: function(xhr, status, error) {

        }
    });

}
const dataTable = $('#departmenDataTable').DataTable({
    pageLength: 5,
    lengthMenu: [5, 10, 20, 50, 100],
    columns: [
        { title: "ID", data: "id" },
        { title: "Name", data: "name" },
        { title: "Employee Count", data: "employeeCount" },
        {   
            title: "Action",
            data: null,
            render: function(data, type, row) {
                return '<button class="btn btn-info dep-detail-button" data-id="' + row.id + '"><i class="fas fa-eye"></i></button> ' +
                    '<button class="btn btn-danger dep-delete-button" data-id="' + row.id + '"><i class="fas fa-times"></i></button>';
            },
            orderable: false
        }
    ]
});

const dataTableDtInfo = $('#employeeListOfDepartment').DataTable({
    pageLength: 5,
    lengthMenu: [5, 10, 20, 50, 100],
    columns: [
        { title: "ID", data: "id" },
        { title: "Name", data: "name" },
        { title: "Surname", data: "surname" },
        { title: "Phone", data: "phone" }
    ]
});
$('#departmenDataTable').on('click', '.dep-detail-button', function() {
    const departmentId = $(this).data('id');
    departmentDetailInfo(departmentId);
});
$('#departmenDataTable').on('click', '.dep-delete-button', function() {
    const departmentId = $(this).data('id');
    showCustomDialog({
        title: 'Warning',
        text: 'Selected Department Will Be Deleted',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Delete',
        cancelButtonText: 'Cancel',
        onConfirm: (value) => {
            console.log(departmentId);
            departmentDelete(departmentId);
        },
        onCancel: () => {

        }
    });
});
export async function getDepartmentList(){


    var endPointHead = "department/GetAllDepartmentsWithEmployeeCount";

    await makeAjaxRequest(
        endPointHead,
        token,
        'GET',
        null,
        function(response) {
            console.log('Başarılı yanıt:', response);
            $("#departmentCount").html(response.length);
            // DataTable'ı temizle
            dataTable.clear();
            // Veriyi DataTable'a ekle
            dataTable.rows.add(response).draw();
            
            createDepartmentPieChar(response);
        },
        function(xhr, status, error) {
            console.error('Hata:', error);
        }
    );
}
window.getDepartmentList = getDepartmentList;
export async function createNewDepartment(userid,departmentName) {
    const endpoint = "department/CreateDepartment";
    const method = "POST";
    const data = JSON.stringify({
        "name": departmentName,
        "userID": userid
    });
    await  makeAjaxRequest(
        endpoint,
        token,
        method,
        data,
        function(response) {
            console.log('Başarılı yanıt:', response);
            $('#newDepartmentModal').modal('hide');
            getDepartmentList();
        },
        function(xhr, status, error) {
            console.error('Hata:', error);
            // Hata durumunda yapılacak işlemler
        }
    );
}

export async function departmentDelete(departmentId) {
    const endpoint = `department/DeleteDepartment/${departmentId}`;
    const method = "DELETE";
    await makeAjaxRequest(
        endpoint,
        token,
        method,
        null,
        function (response) {

            showCustomDialog({
                title: 'Success',
                text: 'Department Deleted',
                icon: 'success',
                showCancelButton: false,
                confirmButtonText: 'Ok',
                onConfirm: (value) => {
                    getDepartmentList();
                    
                },
                onCancel: () => {

                }
            });
        },
        function (xhr, status, error) {
            console.error('Hata:', xhr.responseText + ' ' + xhr.status);
            showCustomDialog({
                title: 'error',
                html: '<b>' + xhr.responseText + ' <hr>Error Code: ' + xhr.status + '</b>',
                icon: 'error',
                showCancelButton: false,
                confirmButtonText: 'Ok',
                onConfirm: (value) => {
                    getDepartmentList();

                },
                onCancel: () => {

                }
            });
        }
    );
}

export async function departmentDetailInfo(departmentId,modalOpened) {
    const endpoint = `department/GetDepartmentWithEmployees?id=${departmentId}`;
    const method = "GET";
    await makeAjaxRequest(
        endpoint,
        token,
        method,
        null,
        function (response) {
            const dtInEmployeeList = response.employees;
            $('#dtDepartmentId').val(response.id);
            $('#titleDepName').html(response.name);
            $('#dtDepartmenName').val(response.name);
            if(!modalOpened)
            {
                $('#departmentDetailModal').modal('show');
            }
            let employeeAddButton = `<button type="button" onclick="depInEmployeeAdd(${response.id})" class="btn btn-success btn-sm" style="margin-top: 7px;"><i class="fas fa-user-plus"></i> Employee Add</button>`;
            $("#departmentDetailEmployeeAddBtnArea").html(employeeAddButton);

            // DataTable'ı temizle
            dataTableDtInfo.clear();
            // Veriyi DataTable'a ekle
            dataTableDtInfo.rows.add(dtInEmployeeList).draw();
            
            

        },
        function (xhr, status, error) {
            console.error('Hata:', error);
            // Hata durumunda yapılacak işlemler
        }
    );
}
export async function depInEmployeeAdd(depID) {
    console.log("dep id kontrol:", depID);
    $('#newEmployeeModalDepartmentSeted').modal('show');
    $('#employeeFormInDepartment').on('submit', async function (event) {
        event.preventDefault();

        let name = $("#nameInput").val();
        let surname = $("#surnameInput").val();
        let phone = $("#phoneInput").val();
        let email = $("#emailInput").val();

        await createNewEmployeeInDepartment(depID, 1, name, surname, phone, email);
    });
    
}
window.depInEmployeeAdd = depInEmployeeAdd;
export async function createNewEmployeeInDepartment(departmentid,userid,name,surname,phone,email) {
    const endpoint = "employee/NewEmployee";
    const method = "POST";
    const data = JSON.stringify({
        "departmentId": departmentid,
        "userID": 1,
        "name": name,
        "surname": surname,
        "phone": phone,
        "email": email
    });
    await makeAjaxRequest(
        endpoint,
        method,
        data,
        function(response) {
            getDepartmentList();
            departmentDetailInfo(departmentid,true);

            $('#newEmployeeModalDepartmentSeted').modal('hide');

            showCustomDialog({
                title: 'Success',
                text: 'Employee Registration Done',
                icon: 'success',
                showCancelButton: false,
                confirmButtonText: 'Ok'
            });
        },
        function(xhr, status, error) {
            console.error('Hata:', error);
            // Hata durumunda yapılacak işlemler
        }
    );
}
export async function updateDepartment(id,userid,departmentName) {
    const endpoint = `department/UpdateDepartment/${id}`;
    const method = "PUT";
    const data = JSON.stringify({
        "id":id,
        "userId": 1,
        "name": departmentName,
    });
    console.log("req: ",data);
    await makeAjaxRequest(
        endpoint,
        token,
        method,
        data,
        function(response) {
            departmentDetailInfo(id,true);
            showCustomDialog({
                title: 'Success',
                text: 'Department info is updated',
                icon: 'success',
                showCancelButton: false,
                confirmButtonText: 'Ok',
                onConfirm: (value) => {
                    getDepartmentList();
                    departmentDetailInfo(id,true);
                },
                onCancel: () => {

                }
            });
   
        },
        function(xhr, status, error) {
            showCustomDialog({
                title: 'error',
                html: '<b>' + xhr.responseText + ' <hr>Error Code: ' + xhr.status + '</b>',
                icon: 'error',
                showCancelButton: false,
                confirmButtonText: 'Ok',
                onConfirm: (value) => {

                },
                onCancel: () => {

                }
            });
        }
    );
}

$(document).ready(async function () {
    await PullSession();
    await getDepartmentList();
});


$('#departmentForm').on('submit', async function (event) {
    event.preventDefault();

    let department = $("#departmentName").val();

    await createNewDepartment(userID, department);

});
$('#frmDepartmentDetail').on('submit', function(event) {
    event.preventDefault();
    
    let depid = $("#dtDepartmentId").val();
    let department = $("#dtDepartmenName").val();

    updateDepartment(depid,1, department);

});


