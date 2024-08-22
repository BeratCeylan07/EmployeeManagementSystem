import {makeAjaxRequest} from '../ajax/GenericAjax.js'

export async function getDepartmentList(){

    let endPointHead = "department/GetAllDepartmentsWithEmployeeCount";
    try {
        return await makeAjaxRequest(endPointHead, token, 'GET', null);
    } catch (error) {
        console.error('Çalışan listesi alınamadı:', error);
        throw error;
    }
}