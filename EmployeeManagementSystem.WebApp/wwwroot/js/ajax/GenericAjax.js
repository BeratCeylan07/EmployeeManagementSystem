let URL = "https://localhost:7161/api/";

export async function makeAjaxRequest(endpoint, token, method, data, successCallback, errorCallback) {
  
   await $.ajax({
        url: URL+endpoint,
        type: method, // GET, POST, PUT, DELETE, vb.
        data: data, // İstek verileri (JSON, FormData, vb.)
       contentType: "application/json",
       headers: {
           "Content-Type": "application/json",
           "Authorization": `Bearer ${token}`
       },
        success: function(response) {
            if (typeof successCallback === 'function') {
                successCallback(response);
            }
        },
        error: function(xhr, status, error) {
            if (typeof errorCallback === 'function') {
                errorCallback(xhr, status, error);
            }
        }
    });
}
