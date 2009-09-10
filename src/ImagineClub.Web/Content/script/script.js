$(document).ready(function() {
    $("a[rel='external']").click(function(event) {
        window.open($(this).attr("href"));
        event.preventDefault();
    });

    $("#username").focus(function() {
        if ($(this).attr('value') == 'username')
            $(this).attr('value', '');
        //}
    })
    $("#password-plain").focus(function() {
        $(this).hide();
        $("#password").show().focus();
    });
});
