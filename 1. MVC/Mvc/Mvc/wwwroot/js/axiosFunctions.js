async function getDataAsync() {
    switch (arguments.length) {
        case 1:
            return loadData(arguments[0]);
        case 2:
            return loadDataWithPatameters(arguments[0], arguments[1], '');
        case 3:
            return loadDataWithPatameters(arguments[0], arguments[1], arguments[2]);
    }
}

async function loadData(url) {
    try {
        let response = await axios.get(url);
        return response.data;
    } catch (err) {
        handleError_(err);
        return {};
    }
}

async function loadDataWithPatameters(url, form, message) {
    try {
        let response = await axios.post(url, form);
        let data = response.data;

        var toastHTML = `<span>${message != '' ? message : 'Información guardada.'}</span > <button class="btn-flat toast-action green-text"><i class="material-icons">done</i></button>`;
        M.toast({ html: toastHTML });

        return data;
    } catch (err) {
        handleError_(err);
        return undefined;
    }
}

function handleError_(error) {
    console.error(error);
    var toastHTML = `<span>${error.response.data.join('<br/>') }</span><button class="btn-flat toast-action red-text">Error</button>`;
    M.toast({ html: toastHTML });
}