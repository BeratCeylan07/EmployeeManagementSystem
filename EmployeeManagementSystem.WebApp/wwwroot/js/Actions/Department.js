import {makeAjaxRequest} from "../ajax/GenericAjax.js";
import {createDepartmentPieChar} from './DepartmentPieChart.js';
import {getDepartmentList} from "./DepartmentListService.js";

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
        onConfirm: async (value) => {
            try {
                const response = await departmentDelete(departmentId);
                showCustomDialog({
                    title: 'Success',
                    text: response,
                    icon: 'success',
                    showCancelButton: false,
                    confirmButtonText: 'Ok'
                });
                await makeDepartmentList();
            } catch (error) {
                showCustomDialog({
                    title: 'Error',
                    text: error.responseText,
                    icon: 'error',
                    showCancelButton: false,
                    confirmButtonText: 'Ok'
                });
            }
        },
        onCancel: () => {

        }
    });
});

export async function makeDepartmentList() {
    try {
        const response = await getDepartmentList();
        $("#departmentCount").html(response.length);
        dataTable.clear();
        dataTable.rows.add(response).draw();
        createDepartmentPieChar(response);

    } catch (error) {
        console.error('DataTable verisi yüklenirken hata oluştu:', error);
    }
   
}
async function createNewDepartment(userid,departmentName) {
    const endpoint = "department/CreateDepartment";
    const method = "POST";
    const data ={
        "name": departmentName,
        "userID": userid
    };
    return await makeAjaxRequest(
        endpoint,
        token,
        method,
        data,
        function (response) {
            return response;
        },
        function (xhr, status, error) {
            throw xhr;
        }
    );
}
async function updateDepartment(id,userid,departmentName) {
    const endpoint = `department/UpdateDepartment/${id}`;
    const method = "PUT";
    const data = {
        "id":id,
        "userId": 1,
        "name": departmentName,
    };
    const response = await makeAjaxRequest(
        endpoint,
        token,
        method,
        data,
        function(response) {
            return response;
        },
        function(xhr, status, error) {
            throw xhr;
        }
    );
    return response;
}
async function departmentDelete(departmentId) {
    const endpoint = `department/DeleteDepartment/${departmentId}`;
    const method = "DELETE";
    return await makeAjaxRequest(
        endpoint,
        token,
        method,
        null,
        function (response) {
            return response;
        },
        function (xhr, status, error) {
            throw xhr;
        }
    );
}
export async function departmentDetailInfo(departmentId,modalOpened) {
    const endpoint = `department/GetDepartmentWithEmployees?id=${departmentId}`;
    const method = "GET";
    const response = await makeAjaxRequest(
        endpoint,
        token,
        method,
        null,
        function (response) {
            return response;
        },
        function (xhr, status, error) {
            console.error('Hata:', error);
            throw  xhr.responseText;
        }
    );
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

}
export async function depInEmployeeAdd(depID) {
    $('#newEmployeeModalDepartmentSeted').modal('show');
    $('#employeeFormInDepartment').on('submit', async function (event) {
        event.preventDefault();
        let name = $("#nameInput").val();
        let surname = $("#surnameInput").val();
        let phone = $("#phoneInput").val();
        let email = $("#emailInput").val();
        try {
            const response = await createNewEmployeeInDepartment(depID, 1, name, surname, phone, email);
            showCustomDialog({
                title: 'Success',
                text: response,
                icon: 'success',
                showCancelButton: false,
                confirmButtonText: 'Ok'
            });
            await departmentDetailInfo(depID,true);
            await makeDepartmentList();
            
        }catch (error){
            showCustomDialog({
                title: 'error',
                text: error.responseText,
                icon: 'error',
                showCancelButton: false,
                confirmButtonText: 'Ok'
            });
        }
        
      

    });
    
}
window.depInEmployeeAdd = depInEmployeeAdd;
export async function createNewEmployeeInDepartment(departmentid,userid,name,surname,phone,email) {
    const endpoint = "employee/NewEmployee";
    const method = "POST";
    const data = {
        "departmentId": departmentid,
        "userID": 1,
        "name": name,
        "surname": surname,
        "phone": phone,
        "email": email
    };
    return await makeAjaxRequest(
        endpoint,
        token,
        method,
        data,
        function (response) {
            return response;
        },
        function (xhr, status, error) {
            throw xhr;
        }
    );

   
}

$('#departmentForm').on('submit', async function (event) {
    event.preventDefault();
    
    let department = $("#departmentName").val();
    try {
        const response = await createNewDepartment(userID,department);
        showCustomDialog({
            title: 'Success',
            text: response,
            icon: 'success',
            showCancelButton: false,
            confirmButtonText: 'Ok'
        });
        await makeDepartmentList();
        $('#newDepartmentModal').modal('hide');
        $('#departmentForm')[0].reset();
        
    } catch (error) {
        console.error('Create Error', error);
        showCustomDialog({
            title: 'error',
            text: error.responseText,
            icon: 'error',
            showCancelButton: false,
            confirmButtonText: 'Ok'
        });
    }


});
$('#frmDepartmentDetail').on('submit', async function (event) {
    event.preventDefault();

    let depid = $("#dtDepartmentId").val();
    let department = $("#dtDepartmenName").val();

    try {
        const response = await updateDepartment(depid,userID,department);
        await makeDepartmentList();
        showCustomDialog({
            title: 'Success',
            text: response,
            icon: 'success',
            showCancelButton: false,
            confirmButtonText: 'Ok'
        });

    } catch (error) {
        console.error('Update Error', error.responseText);
        showCustomDialog({
            title: 'error',
            text: error.responseText,
            icon: 'error',
            showCancelButton: false,
            confirmButtonText: 'Ok'
        });
    }
});

$(document).ready(async function () {
    await PullSession();
    await makeDepartmentList();
});
