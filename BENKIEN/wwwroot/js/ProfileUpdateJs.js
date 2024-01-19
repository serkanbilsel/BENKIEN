
    document.addEventListener("DOMContentLoaded", function () {
            const viewMode = document.getElementById("view-mode");
    const editMode = document.getElementById("edit-mode");
    const editButton = document.getElementById("edit-button");
    const saveButton = document.getElementById("save-button");
    const cancelButton = document.getElementById("cancel-button");
    const confirmNewPassword = document.getElementById("ConfirmNewPassword");
    const errorMessage = document.getElementById("error-message");
    const successMessage = document.getElementById("success-message");

    editButton.addEventListener("click", function () {
        viewMode.style.display = "none";
    editMode.style.display = "block";
    errorMessage.style.display = "none";
    successMessage.style.display = "none";
            });

    cancelButton.addEventListener("click", function () {
        viewMode.style.display = "block";
    editMode.style.display = "none";
    errorMessage.style.display = "none";
    successMessage.style.display = "none";
            });

    saveButton.addEventListener("click", function (event) {
                // Burada şifreleri kontrol et
                const newPassword = document.getElementById("NewPassword").value;
    if (newPassword !== confirmNewPassword.value) {
        // Eğer şifreler eşleşmiyorsa formun submitini engelle
        event.preventDefault();
    // Hata olduğu durumda düzenleme bölümünü kapatma ve hata mesajını gösterme
    errorMessage.style.display = "block";
    successMessage.style.display = "none";
                } else {
        // Eğer şifreler eşleşiyorsa düzenleme bölümünü kapat
        errorMessage.style.display = "none";
    successMessage.style.display = "block";
                }
            });
        });
