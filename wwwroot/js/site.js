$('img').on('error', function () {
    $(this).attr('src', '/img/placeholder.jpg');
});
