$('img').on('error', function () {
    $(this).attr('src', '/img/placeholder.jpg');
});

$('.js-toggle-favorite').on('click', async function () {
    const id = $(this).data('id');

    const res = await fetch(
        `/Productos/ToggleFavorite/${id}`,
        {
            method: 'POST',
            // mode: 'same-origin',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }
        }
    );

    if (res.ok) {
        $(`#favorite-${id}`).toggleClass('far fas');
    }
});
