//Gallery

let modal;
let modal2;
let imgModal;
let imgSelector;

//Image Capture and Preview //////////////////////////////////////////////////////////////////
$(document).on("click", "#imgSelect", function () {
    let img = $(".img-check:checked");
    if (img.length > 0) {
        img.each(function () {
            console.log($(this).val());
            let value = $(this).val();
            src = $(this).closest("label").find("img").attr("src");
            $(".imagePreview").attr("src", src);
            $(".GID").val(value);
            imgModal.hide();
        });
    } else {
        $("#result").html("<div class='alert alert-danger'>No Image Selected</div>");
    }
});
function openModal(m) {
    if (m == modal2) {
        $("#formLoad").load("/Course/Create");
    } else if (m == imgModal) {
        imgSelector.load("/Gallery/SelectImage");
    }
    m.show();
    $("#update").hide();
    $("#save").show();
}
function closeModal() {
    deleteModal.hide();
}