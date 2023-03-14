if ((document.querySelector('#checkbox-all').checked) == true) {
    document.querySelectorAll('input[type=checkbox]').forEach(function (checkElement) {
        checkElement.checked = true;
    });
} else {
    document.querySelectorAll('input[type=checkbox]').forEach(function (checkElement) {
        checkElement.checked = false;
    });
}

//function checkAll() {
//    document.querySelectorAll('#formElement input[type=checkbox]').forEach(function (checkElement) {
//        checkElement.checked = true;
//    });
//}

    