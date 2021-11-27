$("#foto-input").on("change", function () {
	if ($(this).prop("files")) {
		const reader = new FileReader();

		reader.onload = (e) => {
			$("#foto").attr("src", e.target.result)
		}

		reader.readAsDataURL($(this).prop("files")[0]);
	}
});
