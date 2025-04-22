function confirmDelete() {
    return confirm("Do you want to delete this costumer from the register?");
}

//landskod för telefonnummer 
document.addEventListener("DOMContentLoaded", function () {
    const visibleInput = document.querySelector("#visiblePhone");
    const hiddenInput = document.querySelector("#fullPhone");

    if (visibleInput && hiddenInput) {
        const iti = window.intlTelInput(visibleInput, {
            loadUtils: () => import("https://cdn.jsdelivr.net/npm/intl-tel-input@25.3.1/build/js/utils.js"),
            initialCountry: "se",
            preferredCountries: ["se", "no", "fi", "dk"],
            separateDialCode: true
        });

        // När pluginet laddats sätt värdet från databasen 
        iti.promise.then(() => {
            iti.setNumber(hiddenInput.value); 
        });

        // Uppdatera det dolda fältet med rätt nummer
        const form = visibleInput.closest("form");
        if (form) {
            form.addEventListener("submit", function () {
                hiddenInput.value = iti.getNumber(); 
            });
        }
    }
});




//Ändra kundinfo
document.querySelectorAll('.edit-btn').forEach(button => {
    button.addEventListener('click', function () {
        document.getElementById('customerId').value = button.getAttribute('data-id');
        document.getElementById('firstName').value = button.getAttribute('data-firstname');
        document.getElementById('lastName').value = button.getAttribute('data-lastname');
        document.getElementById('streetAddress').value = button.getAttribute('data-streetaddress');
        document.getElementById('city').value = button.getAttribute('data-city');
        document.getElementById('postalCode').value = button.getAttribute('data-postalcode');
        document.getElementById('country').value = button.getAttribute('data-country');
        document.getElementById('email').value = button.getAttribute('data-email');
        document.getElementById('phone').value = button.getAttribute('data-phone');
       

    });
});
