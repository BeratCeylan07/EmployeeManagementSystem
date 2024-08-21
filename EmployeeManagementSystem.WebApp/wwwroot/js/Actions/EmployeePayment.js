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

await PullSession();

const dataTable = $('#employeePaymentDataTable').DataTable({
    pageLength: 5,
    lengthMenu: [5, 10, 20, 50, 100],
    columns: [
        { title: "ID", data: "id" },
        { title: "Department", data: "employeeDepartment" },
        { title: "Employee", data: "employeeName" },
        { title: "Date", data: "paymentDate" },
        { title: "Amunt", data: "amount" },
        {
            title: "Action",
            data: null,
            render: function(data, type, row) {
                return '<button class="btn btn-danger emp-trash-button" data-id="' + row.id + '"><i class="fas fa-trash-alt"></i></button>';
            },
            orderable: false
        }
    ]
});

$('#employeePaymentDataTable').on('click', '.emp-trash-button', function() {
    const paymentId = $(this).data('id');
    
});

export async function getEmployeeList(){


    var endPointHead = "emplooyePayment/GetAllEmployeePayments";

    await makeAjaxRequest(
        endPointHead,
        token,
        'GET',
        null,
        function(response) {
            console.log('Başarılı yanıt:', response);
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

await getEmployeeList();