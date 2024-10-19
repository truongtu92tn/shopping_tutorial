$(function () {

    if ($("a.confirmDeletion").length) {
        $("a.confirmDeletion").click(function () {
            return confirm("Confirm deletion");
        });
    }

    if ($("div.alert.notification").length) {
        setTimeout(() => {
            $("div.alert.notification").fadeOut("slow");
        }, 2000);
    }

});

function readURL(input) {
	if (input.files && input.files[0]) {
		let reader = new FileReader();

		reader.onload = function (e) {
			$("img#imgpreview").attr("src", e.target.result).width(200).height(200);
		};

		reader.readAsDataURL(input.files[0]);
	}
}

