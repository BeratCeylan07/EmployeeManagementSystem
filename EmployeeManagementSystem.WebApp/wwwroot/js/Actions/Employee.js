import {makeAjaxRequest} from "../ajax/GenericAjax.js";
import {getEmployeeList} from "./EmployeeListService.js";
import {getDepartmentList} from "./DepartmentListService.js";


const dataTable = $('#employeDataTable').DataTable({
    pageLength: 5,
    lengthMenu: [5, 10, 20, 50, 100],
    columns: [
        { title: "ID", data: "id" },
        { title: "Name", data: "name" },
        { title: "Surname", data: "surname" },
        { title: "Department Name", data: "departmentName" },
        {
            title: "Action",
            data: null,
            render: function(data, type, row) {
                return '<button class="btn btn-info emp-detail-button" data-id="' + row.id + '"><i class="fas fa-eye"></i></button>';
            },
            orderable: false
        }
    ]
});
$('#employeDataTable').on('click', '.emp-detail-button', async function () {
    const employeeId = $(this).data('id');
    console.log(employeeId);
    await employeeDetailInfo(employeeId);
});

const dtbpaymentOfEmployee = $('#paymentOfEmployee').DataTable({
    pageLength: 5,
    lengthMenu: [5, 10, 20, 50, 100],
    columns: [
        { title: "ID", data: "id" },
        { title: "Amount", data: "amount" },
        { title: "Date", data: "paymentDate" },
        {
            title: "Action",
            data: null,
            render: function(data, type, row) {
                return '<button class="btn btn-danger emp-detail-button" data-id="' + row.id + '"><i class="fas fa-trash"></i></button>';
            },
            orderable: false
        }
    ]
});

export async function makeEmployeeDelete(employeeId) {
    console.log(employeeId);
    showCustomDialog({
        title: 'Warning',
        text: 'Selected Employee Will Be Deleted',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Delete',
        cancelButtonText: 'Cancel',
        onConfirm: async (value) => {
            try {
                const response = await deleteEmployee(employeeId);
                console.log(response);
                showCustomDialog({
                    title: 'Success',
                    text: response,
                    icon: 'success',
                    showCancelButton: false,
                    confirmButtonText: 'Ok'
                });
                await makeEmployeListDataTable();
                $('#employeeDetailModal').modal('hide');

            } catch (error) {
                console.log(error);
                showCustomDialog({
                    title: 'Error',
                    text: error.responseText,
                    icon: 'error',
                    showCancelButton: false,
                    confirmButtonText: 'Ok'
                });
            }        },
        onCancel: () => {

        }
    });
   
}



async function makeEmployeListDataTable() {
    try {
        const response = await getEmployeeList();
        console.log('Çalışan Listesi:', response);
        $("#employeeCount").html(response.length);
        dataTable.clear();
        dataTable.rows.add(response).draw();
    } catch (error) {
        console.error('DataTable verisi yüklenirken hata oluştu:', error);
    }
}


export async function updateEmployee(id,departmentid,userid,name,surname,phone,email,salary) {
    const endpoint = `employee/updateEmployee/${id}`;
    const method = "PUT";
    const data = {
        "id":id,
        "departmentId": departmentid,
        "userID": userID,
        "name": name,
        "surname": surname,
        "phone": phone,
        "email": email,
        "salary": salary,
    };
    console.log("req: ",data);
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
export async function createNewEmployee(departmentid,userid,name,surname,phone,email,salary) {
    
    
    const endpoint = "employee/NewEmployee";
    const method = "POST";
    const data = {
            "departmentId": departmentid,
            "userID": userID,
            "name": name,
            "surname": surname,
            "phone": phone,
            "email": email,
            "salary": salary,
    };
    console.log("req: ",data);
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
export async function getDepartmentListForSelect(){


 
    const departments = await getDepartmentList();
    const selectElementNewEmployee = document.getElementById('departmentSelect');
    selectElementNewEmployee.innerHTML = '';
    const selectElementDetailEmployee = document.getElementById('dtEmployeedepartmentSelect');
    selectElementDetailEmployee.innerHTML = '';
    departments.forEach(department => {
        const option = document.createElement('option');
        const option2 = document.createElement('option');

        option.value = department.id;
        option.textContent = department.name;

        option2.value = department.id;
        option2.textContent = department.name;

        selectElementNewEmployee.append(option);

        selectElementDetailEmployee.append(option2);

    });
}

export async function employeeDetailInfo(employeeId,modalOpened) {
    
    const endpoint = `employee/GetEmployeeDetails?id=${employeeId}`;
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
    
    $('#dtEmployeeName').val(response.name);
    $('#titleeEmployeeFullName').html(response.name + " " + response.surname);
    $('#dtEmployeeSurname').val(response.surname);
    $('#dtEmployeePhone').val(response.phone);
    $('#dtEmployeeEmail').val(response.email);
    $('#dtEmployeeSalary').val(response.salary);
    $('#dtEmployeedepartmentSelect').val(response.departmenId);
    $('#dtEmployeeId').val(employeeId);
    $('#employeeDetailModal').modal('show');

    let employeePaymentAddButton = `<button type="button" onclick="employeePaymentAdd(${response.id})" class="btn btn-success btn-sm" style="margin-top: 7px;" disabled><i class="fas fa-plus"></i> Payment Add(cooming soon)</button>`;
    $("#btnemployeeDetailPaymentAdding").html(employeePaymentAddButton);

    let btnemployeeDetailRemoveButton = `<button type="button" onclick="makeEmployeeDelete(${response.id})" class="btn btn-danger  w-100" style="margin-top: 7px;"><i class="fas fa-user-times"></i> Employee Delete</button>`;
    $("#btnemployeeDetailRemoveArea").html(btnemployeeDetailRemoveButton);

    const employeePaymentList = response.payments;
    console.log(employeePaymentList);
    dtbpaymentOfEmployee.clear();
    dtbpaymentOfEmployee.rows.add(employeePaymentList).draw();
    
}

export async function deleteEmployee(employeeId) {


    const endpoint = `employee/DeleteEmployee/${employeeId}`;
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

 

$('#employeeForm').on('submit', async function (event) {
    event.preventDefault(); // Prevent the default form submit behavior

    let department = $("#departmentSelect").val();
    let name = $("#nameInput").val();
    let surname = $("#surnameInput").val();
    let phone = $("#phoneInput").val();
    let email = $("#emailInput").val();
    let salary = $("#salaryInput").val();

    try {
        const response = await createNewEmployee(department, userID, name, surname, phone, email, salary);
        console.log('Create Reponse: ', response);
        showCustomDialog({
            title: 'Success',
            text: response,
            icon: 'success',
            showCancelButton: false,
            confirmButtonText: 'Ok'
        });
        await makeEmployeListDataTable();
  
    } catch (error) {
        console.error('Create Error', error.responseText);
        showCustomDialog({
            title: 'error',
            text: error.responseText,
            icon: 'error',
            showCancelButton: false,
            confirmButtonText: 'Ok'
        });
    }
    $('#newEmployeeModal').modal('hide');
    $('#employeeForm')[0].reset();
});



$('#frmEmployeeInfoEdit').on('submit', async function (event) {
    event.preventDefault(); // Prevent the default form submit behavior
    let employeeid = $('#dtEmployeeId').val();
    let department = $("#dtEmployeedepartmentSelect").val();
    let name = $("#dtEmployeeName").val();
    let surname = $("#dtEmployeeSurname").val();
    let phone = $("#dtEmployeePhone").val();
    let email = $("#dtEmployeeEmail").val();
    let salary = $("#dtEmployeeSalary").val();
    try {
        const response = await updateEmployee(employeeid,department, userID, name, surname, phone, email, salary);
        console.log('Update Reponse: ', response);
        showCustomDialog({
            title: 'Success',
            text: response,
            icon: 'success',
            showCancelButton: false,
            confirmButtonText: 'Ok'
        });
        employeeDetailInfo(employeeid,true);
        await makeEmployeListDataTable();

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
    await makeEmployeListDataTable();
    await getDepartmentListForSelect();
})