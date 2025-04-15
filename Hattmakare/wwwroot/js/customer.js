function confirmDelete() {
    return confirm("Do you want to delete this costumer from the register?");
}



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
