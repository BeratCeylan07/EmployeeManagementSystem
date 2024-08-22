let URL = "https://localhost:7161/api/";

export async function makeAjaxRequest(endpoint, token, method, data) {
    try {
        // AJAX çağrısını doğrudan Promise olarak döndür
        return await $.ajax({
            url: URL + endpoint,
            type: method,
            data: JSON.stringify(data), // Veriyi JSON formatına dönüştür
            contentType: "application/json",
            timeout: 0,
            headers: {
                "Authorization": `Bearer ${token}`
            }
        }); // Başarılı yanıtı döndür
    } catch (error) {
        throw error; // Hata oluşursa bu hatayı fırlat
    }
}
