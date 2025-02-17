$(document).ready(function () {
    alert("1");
    GetSpecialization();
    //$('#state').attr('disabled', true);
    //$('#city').attr('disabled', true);
});

function GetSpecialization() {
    debugger;
    alert("2");
    $.ajax({
        url: '/Specialization/GetSpecialization',
        success: function (result) {
            $.each(result, function (i,data) {
                $('#specialization').append('<option value=' + data.s_id + '>' + data.s_name + '</option>');
            });
        },
        error: function (ex) {
            alert('Failed to retrieve specialization!!'+ex);
        }
    });
}
