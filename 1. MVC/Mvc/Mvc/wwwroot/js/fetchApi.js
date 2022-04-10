function getDataText(url, form, funcion, preFuncion, postFuncion) {
    typeof preFuncion == 'function' && preFuncion();
    fetchResponse(url, form, false)
        .then(data => {
            funcion(data);
        })
        .catch(error => {
            handleError(error, 'No se pudo leer la información.');
        })
        .then(response => {
            typeof postFuncion == 'function' && postFuncion();
        });
}

function saveData(url, form, funcion, preFuncion, postFuncion) {
    typeof preFuncion == 'function' && preFuncion();
    fetchResponse(url, form)
        .then(data => {
            var toastHTML = '<span>Información guardada.</span><button class="btn-flat toast-action green-text"><i class="material-icons">done</i></button>';
            M.toast({ html: toastHTML });
            funcion(data);
        })
        .catch(error => {
            handleError(error, 'Problema al guardar información.');
        })
        .then(response => {
            typeof postFuncion == 'function' && postFuncion();
        });
}

function deleteData(url, form, funcion, preFuncion, postFuncion) {
    typeof preFuncion == 'function' && preFuncion();
    fetchResponse(url, form)
        .then(data => {
            toast.Correct('Registro eliminado.');
            funcion(data);
        })
        .catch(error => {
            handleError(error, 'No se elimino el registro.');
        })
        .then(response => {
            typeof postFuncion == 'function' && postFuncion();
        });
}

async function fetchResponse(url, form, isJson = true) {
    let response = await fetch(url, {
        method: "POST",
        body: form
    });
    if (response.ok) {
        return isJson ? response.json() : response.text();
    } else {
        return Promise.reject({
            status: response.status,
            statusText: response.statusText,
            response: response.json()
        });
    }
}



let handleError = (error, errorMessage) => {
    console.error(error);
    var toastHTML = `<span>${errorMessage}</span><button class="btn-flat toast-action red-text">Error</button>`;
    M.toast({ html: toastHTML });
}