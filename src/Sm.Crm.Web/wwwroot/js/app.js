$(function () {
    $("#modal-action")
        .on('show.bs.modal', function (e) {
            var button = $(e.relatedTarget);
            var url = button.attr("href");
            var modal = $(this);
            modal.find('.modal-content').html('Loading...');
            modal.find('.modal-content').load(url);
        })
        .on('hidden.bs.modal', function (e) {
            var modal = $(this);
            modal.removeData('bs.modal');
            modal.find('.modal-content').empty();
        });

    $(document).on("submit", '.modal form', function (event) {
        // form'un otomatik gönderilmesini engelle
        event.preventDefault();

        // form'u kendimiz ajax post ile göndereceğiz
        let form = $(this);
        let url = form.attr("action"); // /app/customers/addcustomer
        let modal = $(".modal");

        // form içerisindeki tüm verileri al
        let formData = form.serialize(); // companyName=xxx&city=xxx

        modal.find('.modal-content').html('Processing...');

        // AJAX post işlemi yap, yani formu gönderme işlemini
        $.post(url, formData)
            .done(function (result) {
                debugger;
                if (!result.isSuccess) {
                    toastr.error("An error occured!", "Post Error");
                    modal.find('.modal-content').html(result);
                } else {
                    location.href = result.redirectUrl;
                }
            })
            .fail(function (e) {
                toastr.error("An error occured", "Fail");
            });
    });

});
