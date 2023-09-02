var imgPreview = function (event) {
    var output = document.getElementById('imgProducto');
    output.src = URL.createObjectURL(event.target.files[0]);
    output.onload = function () {
        URL.revokeObjectURL(output.src) // free memory
    }
};