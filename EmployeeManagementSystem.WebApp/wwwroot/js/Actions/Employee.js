import {makeAjaxRequest} from "../ajax/GenericAjax.js";
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
$('#employeDataTable').on('click', '.emp-detail-button', function() {
    const employeeId = $(this).data('id');
    employeeDetailInfo(employeeId);
});

const dtbpaymentOfEmployee = $('#paymentOfEmployee').DataTable({
    pageLength: 5,
    lengthMenu: [5, 10, 20, 50, 100],
    columns: [
        { title: "ID", data: "id" },
        { title: "Amount", data: "amount" },
        { title: "Date", data: "isCreatedDate" },
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

window.employeeDelete = employeeDelete;
export async function employeeDelete(employeeId) {
    showCustomDialog({
        title: 'Warning',
        text: 'Selected Employee Will Be Deleted',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Delete',
        cancelButtonText: 'Cancel',
        onConfirm: (value) => {
            console.log("Tıklandı");
            const endpoint = `employee/DeleteEmployee/${employeeId}`;
            const method = "DELETE";
             makeAjaxRequest(
                endpoint,
                token,
                method,
                null,
                function (response) {

                    showCustomDialog({
                        title: 'Success',
                        text: 'Employee Deleted',
                        icon: 'success',
                        showCancelButton: false,
                        confirmButtonText: 'Ok',
                        onConfirm: (value) => {
                            getEmployeeList();
                            getDepartmentListForSelect();
                            $('#employeeDetailModal').modal('hide'); 
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
                            
                        },
                        onCancel: () => {

                        }
                    });
                }
            );
        },
        onCancel: () => {

        }
    });
   
}



export function getEmployeeList(){


    var endPointHead = "employee/GetAllEmployees";

    makeAjaxRequest(
        endPointHead,
        token,
        'GET',
        null,
        function(response) {
            console.log('Başarılı yanıt:', response);
            $("#employeeCount").html(response.length);
            // DataTable'ı temizle
            dataTable.clear();
            // Veriyi DataTable'a ekle
            dataTable.rows.add(response).draw();
            },
        function(xhr, status, error) {
            console.error('Hata:', error);
        }
    );
}

export async function updateEmployee(id,departmentid,userid,name,surname,phone,email,salary) {
    const endpoint = `employee/updateEmployee/${id}`;
    const method = "PUT";
    const data = JSON.stringify({
        "id":id,
        "departmentId": departmentid,
        "userID": userID,
        "name": name,
        "surname": surname,
        "phone": phone,
        "email": email,
        "salary": salary,
    });
    console.log("req: ",data);
    await makeAjaxRequest(
        endpoint,
        token,
        method,
        data,
        function(response) {
            showCustomDialog({
                title: 'Success',
                text: 'Employee info is updated',
                icon: 'success',
                showCancelButton: false,
                confirmButtonText: 'Ok',
                onConfirm: (value) => {
                    getEmployeeList();
                    getDepartmentListForSelect();
                },
                onCancel: () => {

                }
            });
            employeeDetailInfo(id,true);
            getEmployeeList();
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
export async function createNewEmployee(departmentid,userid,name,surname,phone,email,salary) {
    const endpoint = "employee/NewEmployee";
    const method = "POST";
    const data = JSON.stringify({
            "departmentId": departmentid,
            "userID": userID,
            "name": name,
            "surname": surname,
            "phone": phone,
            "email": email,
            "salary": salary,
    });
    await makeAjaxRequest(
        endpoint,
        token,
        method,
        data,
        function(response) {
            console.log('Başarılı yanıt:', response);

            showCustomDialog({
                title: 'Success',
                text: 'Employee Registration Done',
                icon: 'success',
                showCancelButton: false,
                confirmButtonText: 'Ok'
            });
            getEmployeeList();
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

export async function getDepartmentListForSelect(){


    var endPointHead = "department/GetAllDepartments";

    await makeAjaxRequest(
        endPointHead,
        token,
        'GET',
        null,
        function(response) {
            const departments = response;

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



        },
        function(xhr, status, error) {
            console.error('Hata:', error);
        }
    );
}

export async function employeeDetailInfo(employeeId,modalOpened) {
    const endpoint = `employee/GetEmployeeDetails?id=${employeeId}`;
    const method = "GET";
    await makeAjaxRequest(
        endpoint,
        token,
        method,
        null,
        function (response) {
            console.log("employee detail ", response);
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

            let btnemployeeDetailRemoveButton = `<button type="button" onclick="employeeDelete(${response.id})" class="btn btn-danger  w-100" style="margin-top: 7px;"><i class="fas fa-user-times"></i> Employee Delete</button>`;
            $("#btnemployeeDetailRemoveArea").html(btnemployeeDetailRemoveButton);
            
            const employeePaymentList = response.payments;



        },
        function (xhr, status, error) {
            console.error('Hata:', error);
            // Hata durumunda yapılacak işlemler
        }
    );
}


$(document).ready(async function () {
    await PullSession();
    await getEmployeeList();
    await getDepartmentListForSelect();
});
$('#employeeForm').on('submit', async function (event) {
    event.preventDefault(); // Prevent the default form submit behavior

    let department = $("#departmentSelect").val();
    let name = $("#nameInput").val();
    let surname = $("#surnameInput").val();
    let phone = $("#phoneInput").val();
    let email = $("#emailInput").val();
    let salary = $("#salaryInput").val();

    // Call the createNewEmployee function
    await createNewEmployee(department, 1, name, surname, phone, email, salary);

    // Optionally, you can close the modal and reset the form
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

    // Call the createNewEmployee function
    await updateEmployee(employeeid,department, 1, name, surname, phone, email, salary);

    // Optionally, you can close the modal and reset the form
    $('#frmEmployeeInfoEdit')[0].reset();
});