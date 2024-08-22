import {makeAjaxRequest} from "../ajax/GenericAjax.js";

export async function deleteEmployeePayment(paymentId) {


    const endpoint = "emplooyePayment/DeleteEmployeePayment/"+paymentId;
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
