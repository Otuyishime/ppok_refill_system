$(document).ready(function () {
    //alert('change btn');
    $(".paginate_button").addClass("btn btn-default btn-sm");

    $('#submit-button').on('click', function () {
        var formData = new FormData();
        var file = document.getElementById('dataFile');

        formData.append("dataFile", file);
        $.ajax({
            url: '/Home/Recall',
            type: 'GET',
            dataType:'html',
            success: function (data) {
                alert(data);
                $('#imported-recalls').empty();
                $('#imported-recalls').html(data);
            },
            error: function(error){
                alert('Failed to import the recall file');
            }
        });
    });

    $("#due-refills-more-info-btn").on('click', function (event) {
        var target = $('#today-refills-panel');
        event.preventDefault();
        $('html, body').animate({
            scrollTop: target.offset().top - 50
        }, 500);
    });

    $("#pending-refills-more-info-btn").on('click', function (event) {
        var target = $('#pending-refills-panel');
        event.preventDefault();
        $('html, body').animate({
            scrollTop: target.offset().top - 50
        }, 500);
    });
});