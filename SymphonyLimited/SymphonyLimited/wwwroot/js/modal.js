// Declare variables for data posting
const deleteModal = new bootstrap.Modal(document.getElementById('deleteModal'));
function openModal(modalId) {
    let target = document.getElementById(modalId);
    let targetModal = new bootstrap.Modal(target);
    if (modalId === "otherModal") {
        targetModal = modal2
        $("#formLoad").load("/Course/Create");
        console.log("Model2");
    }
    console.log("Model");
    targetModal.show();
    $("#update").hide();
    $("#save").show();
}
function closeModal() {
    deleteModal.hide();
}