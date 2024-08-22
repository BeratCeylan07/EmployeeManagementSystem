import {makeAjaxRequest} from "../ajax/GenericAjax.js";
import {getEmployeeList} from "./EmployeeListService.js";
import {deleteEmployeePayment} from "./EmployeePaymentRemoveService.js";

const employeePaymentdataTable = $('#employeePaymentDataTable').DataTable({
    pageLength: 5,
    lengthMenu: [5, 10, 20, 50, 100],
    columns: [
        { title: "ID", data: "id" },
        { title: "Department", data: "employeeDepartment" },
        { title: "Employee", data: "employeeName" },
        { title: "Date", data: "paymentDate" },
        { title: "Amount", data: "amount" },
        {
            title: "Action",
            data: null,
            render: function(data, type, row) {
                return '<button class="btn btn-danger emp-pym-trash-button" data-id="' + row.id + '"><i class="fas fa-trash-alt"></i></button>';
            },
            orderable: false
        }
    ]
});

async function makeDeleteEmployeepPayment(paymentId) {
    showCustomDialog({
        title: 'Warning',   
        text: `Selected Employee's Payment Will Be Deleted`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Delete',
        cancelButtonText: 'Cancel',
        onConfirm: async (value) => {
            try {
                const response = await deleteEmployeePayment(paymentId);
                showCustomDialog({
                    title: 'Success',
                    text: response,
                    icon: 'success',
                    showCancelButton: false,
                    confirmButtonText: 'Ok'
                });
                await makeEmployeePaymentList();

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

}
$('#employeePaymentDataTable').on('click', '.emp-pym-trash-button', async function () {
    const paymentId = $(this).data('id');
    await makeDeleteEmployeepPayment(paymentId);
    
});

export async function EmployeePaymentList(){
    let endPointHead = "emplooyePayment/GetAllEmployeePayments";
    try {
        return await makeAjaxRequest(endPointHead, token, 'GET', null);
    } catch (error) {
        console.error('Çalışan listesi alınamadı:', error);
        throw error;
    }
}
export async function makeEmployeePaymentList() {
    try {
        const response = await EmployeePaymentList();
       
        employeePaymentdataTable.clear();
        employeePaymentdataTable.rows.add(response).draw();

        let totalAmount = 0;
        response.forEach(payment => {
            if (payment.amount) {
                totalAmount += parseFloat(payment.amount);
            }
        });
        
        $('#sumEmployeePayment').html(totalAmount.toFixed());

    } catch (error) {
        console.error('DataTable verisi yüklenirken hata oluştu:', error);
    }

}
export async function employeeSelectList(){
    try {
        const employeeies = await getEmployeeList();
        const selectEmployeeElement = document.getElementById('employeeSelect');
        selectEmployeeElement.innerHTML = '';
        employeeies.forEach(employee => {
            const option = document.createElement('option');

            option.value = employee.id;
            option.textContent = employee.name + " " + employee.surname;

            selectEmployeeElement.append(option);

        });
    }catch(error) {
        showCustomDialog({
            title: 'Error',
            text: error.responseText,
            icon: 'error',
            showCancelButton: false,
            confirmButtonText: 'Ok'
        });
    }
  
}
export async function newEmployeePayment(employeeId,paymentDate,amount) {
    const endpoint = "emplooyePayment/NewEmployeePayment";
    const method = "POST";
    const data = {
        "amount": amount,
        "paymentDate": paymentDate,
        "employeeId": employeeId,
        "userId": userID
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
$('#employeePaymentForm').on('submit', async function (event) {
    event.preventDefault();

    try {
        let employeeId = $('#employeeSelect option:selected').val();
        let paymentDate = $('#paymentDate').val();
        let amount = $('#paymentAmount').val();
        const response = await newEmployeePayment(employeeId, paymentDate, amount);
        showCustomDialog({
            title: 'Success',
            text: response,
            icon: 'success',
            showCancelButton: false,
            confirmButtonText: 'Ok'
        });
        await makeEmployeePaymentList();
        $('#newEmployeePaymentModal').modal('hide');
    }catch(error) {
        showCustomDialog({
            title: 'Error',
            text: error.responseText,
            icon: 'error',
            showCancelButton: false,
            confirmButtonText: 'Ok'
        });
    }
});
$(document).ready(async function () {
    await PullSession();
    await makeEmployeePaymentList();
    await employeeSelectList();

});