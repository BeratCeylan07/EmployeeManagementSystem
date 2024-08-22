import {makeAjaxRequest} from '../ajax/GenericAjax.js'

window.getEmployeeList = getEmployeeList;
export async function getEmployeeList() {
    const endPointHead = "employee/GetAllEmployees";
    try {
        return await makeAjaxRequest(endPointHead, token, 'GET', null);
    } catch (error) {
        console.error('Çalışan listesi alınamadı:', error);
        throw error;
    }
}