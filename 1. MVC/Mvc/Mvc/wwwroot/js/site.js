function validateMaterialize(form = 'form') {
    var inputs = document.getElementById(form).querySelectorAll('.input-validation-error');
    inputs.forEach(input => {
        input.classList.add('invalid');
        if (input.dataset.hasOwnProperty('valRequired') && input.value == '') {
            var divSuperior = input.closest('div');
            var span = divSuperior.querySelector('span');
            if (span != undefined) {
                span.dataset.error = input.dataset.valRequired;
            }
        }
    });
}


const by = {
    Id: (x) => {
        return document.getElementById(x);
    }
}

const toast = {
    Correct: (message) => {
        var toastHTML = `<span>${message}</span><button class="btn-flat toast-action green-text"><i class="material-icons">done</i></button>`;
        M.toast({ html: toastHTML });
    },
    Error: (message) => {
        var toastHTML = `<span>${message}</span><button class="btn-flat toast-action red-text">Error</button>`;
        M.toast({ html: toastHTML });
    }
}