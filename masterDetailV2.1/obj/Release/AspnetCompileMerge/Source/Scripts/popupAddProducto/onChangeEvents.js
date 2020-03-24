
// Funcion para agregar el evento ONCHANGE a el campo CANTIDAD y VALIDARLO
document.getElementById("prodCantidad").addEventListener("change", () => {    
    console.log("FUNCIONA MI ARCHIVO JAVASCRIPT");
    var cantidad = document.getElementById("prodCantidad").value;

    console.log(cantidad, 'cantidad desde JS');
    if (cantidad < 1) {
        $('#errorCantidad').remove();
        $('#prodCantidad').after('<span id="errorCantidad" class="error field-validation-erro">El campo cantidad es obligatorio.</span>');
    } else {
        $('#errorCantidad').remove();
    }
});


// Funcion para agregar el evento ONCHANGE a el campo IDPRODUCTO y VALIDARLO
document.getElementById("prodIdSelect").addEventListener("change", () => {
    console.log("FUNCIONA MI ARCHIVO JAVASCRIPT producto");
    var idProducto = document.getElementById("prodIdSelect").value;

    console.log(idProducto, 'cantidad desde JS');
    if (idProducto == 0 || idProducto == undefined || idProducto == null) {
        $('#errorIdprod').remove();
        $('#prodIdSelect    ').after('<span id="errorIdprod" class="error field-validation-erro">El campo producto es obligatorio.</span>');
    } else {
        $('#errorIdprod').remove();
    }
});