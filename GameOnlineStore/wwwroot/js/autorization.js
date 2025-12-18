 $(document).ready(function () {
    $("#phone").mask("+7(999)999-99-99");

    $('#signInForm').submit(function (e) {
        e.preventDefault();
        let loginCredential = {
            login: $('input[name="login"]').val(),
            password: $('input[name="password"]').val(),
            rememberMe: $('input[name="rememberMe"]').is(":checked")
        };
        $.ajax({
            type: 'POST',
            url: 'Account/SignIn',
            contentType: 'application/json',
            data: JSON.stringify(loginCredential),
            success: function (response) {
                if (response.success) {
                    $('#loginModalWindow').modal('hide');
                    window.location.href = response.redirectUrl || '/';
                } else {
                    alert(response.message);
                }
            },
            error: function (xhr) {
                alert(xhr.responseJSON?.message || 'Ошибка сервера');
            }
        });
    });
    $('#registerForm').submit(function (e) {
        e.preventDefault();
        let registerDetails = {
            login: $('input[name="newLogin"]').val(),
            phone: $('input[name="phone"]').val(),
            password: $('input[name="newPassword"]').val(),
            confirmPassword: $('input[name="confirmPassword"]').val()
        };
        $.ajax({
            type: 'POST',
            url: 'Account/Register',
            contentType: 'application/json',
            data: JSON.stringify(registerDetails),
            success: function (response) {
                // Закрыть модальное окно
                $('#loginModalWindow').modal('hide');
                // Отобразить сообщение об успехе
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    });
});