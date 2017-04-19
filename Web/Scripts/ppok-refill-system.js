window.ppok_refill_system_module = (function () {
    // Move view to show the due refills
    moveToShowDueRefills = function (event) {
        var target = $('#today-refills-panel');
        event.preventDefault();
        $('html, body').animate({
            scrollTop: target.offset().top - 50
        }, 500);
    };

    // Move view to show the pending pickups 
    moveToShowPendingPickups = function (event) {
        var target = $('#pending-refills-panel');
        event.preventDefault();
        $('html, body').animate({
            scrollTop: target.offset().top - 50
        }, 500);
    };

    // retrieve all the due refills
    getDueRefills = function () {
        $.ajax({
            url: '/Home/GetDueRefills',
            type: 'POST',
            dataType: 'html',
            success: function (data) {
                $('#due-refills-Container').html(data);
            }
        });
    };

    // retrieve all the pending pickups
    getPendingPickups = function () {
        $.ajax({
            url: '/Home/GetPendingPickups',
            type: 'POST',
            dataType: 'html',
            success: function (data) {
                $('#pending-pickups-Container').empty().html(data);
            }
        });
    };

    // get patient refill confirmation form
    getPatientConfirmationRefillForm = function () {
        var url = '/Patient/getForm';
        var code = $('#code-box').val();
        var patientname = $('#PatientName').val();
        $.ajax({
            url: url,
            type: 'POST',
            dataType: 'html',
            data: { Code: code, PatientName: patientname },
            success: function (data) {
                $('#refill-form').empty();
                $('#refill-form').html(data);
                $('#code-prompt').empty();
            },
            error: function (error) {
                alert('Failed to get the confirmation form');
            }
        });
    };

    // get due pickups data using the signalR
    getDuePickups = function () {
        $(function () {

            var con = $.hubConnection();
            var hub = con.createHubProxy('DuePickUps');
            hub.on('onGetDuePickUp', function (pickups) {
                $('#pending-refills-count').html(pickups);
                getPendingPickups();
            });

            con.start(function () {
                hub.invoke('getDuePickUp');
            });
        });
    };

    return {
        init: function () {
            getDueRefills();
            //getPendingPickups();
        },

        moveToShowDueRefills: function (event) {
            moveToShowDueRefills(event);
        },

        moveToShowPendingPickups: function (event) {
            moveToShowPendingPickups(event);
        },

        getDueRefills: function () {
            getDueRefills();
        },

        getPendingPickups: function () {
            getPendingPickups();
        },

        getPatientConfirmationRefillForm: function () {
            getPatientConfirmationRefillForm();
        },

        getDuePickups: function() {
            getDuePickups();
        }

    };
})(jQuery);