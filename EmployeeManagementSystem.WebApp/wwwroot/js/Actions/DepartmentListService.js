import {makeAjaxRequest} from '../ajax/GenericAjax.js'

export async function getDepartmentList(){

    let endPointHead = "department/GetAllDepartmentsWithEmployeeCount";
    try {
        return await makeAjaxRequest(endPointHead, token, 'GET', null);
    } catch (error) {
        console.error('Department listesi alınamadı:', error);
        throw error;
    }
}

export async function getDepartmentSalaryTotalList(){
    let endPointHead = "department/GetAllDepartmentsSalaryTotal";
    try {
        return await makeAjaxRequest(endPointHead, token, 'GET', null);
    } catch (error) {
        console.error('Department listesi alınamadı:', error);
        throw error;
    }
}