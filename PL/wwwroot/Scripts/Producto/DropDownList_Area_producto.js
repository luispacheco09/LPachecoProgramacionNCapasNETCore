
$(document).ready(function () {

    $("#ddlArea").change(function () {

        $("#ddlDepartamento").empty();
        var url = '/Producto/GetDepartamentosList';

        $.ajax({
            type: 'POST',
            url: url,
            dataType: 'json',
            data: { IdArea: $("#ddlArea").val() },
            success: function (departamentos) {
                $("#ddlDepartamento").append('<option value="0">' + 'Seleccione un departamento0' + '</option>');
                $.each(departamentos, function (i, departamentos) {
                    $("#ddlDepartamento").append('<option value="'
                        + departamentos.idDepartamento + '">'
                        + departamentos.nombre + '</option>');
                });
            },
            error: function (ex) {
                alert('Failed.' + ex);
            }
        });
    });
});
