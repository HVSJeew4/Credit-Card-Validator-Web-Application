$(document).ready(function () {
    const apiUrl = "https://localhost:7172/api/CreditCard/validate"; // Update with your backend URL

    $('#cardNumber').on('input', function () {
        const cardNumber = $(this).val();
        const sanitizedInput = cardNumber.replace(/\D/g, '');
        $(this).val(sanitizedInput);

        if (sanitizedInput.length > 16) {
            $(this).addClass('is-invalid');
            $('#result').hide();
        } else {
            $(this).removeClass('is-invalid');
        }
    });

    $('#validateBtn').on('click', function () {
        const cardNumber = $('#cardNumber').val().trim();

        if (!cardNumber) {
            displayError("Card number cannot be empty.");
            return;
        }

        if (!/^\d+$/.test(cardNumber)) {
            displayError("Card number must contain only digits.");
            return;
        }

        if (![15, 16].includes(cardNumber.length)) {
            displayError("Card number must be 15 or 16 digits long.");
            return;
        }

        $.ajax({
            url: apiUrl,
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ cardNumber: cardNumber }),
            success: function (response) {
                $('#result').show();
                if (response.isValid) {
                    $('#resultMessage')
                        .removeClass('alert-danger')
                        .addClass('alert-success')
                        .text(`Valid card! Type: ${response.cardType}`);
                } else {
                    $('#resultMessage')
                        .removeClass('alert-success')
                        .addClass('alert-danger')
                        .text(response.message || 'Invalid card number.');
                }
            },
            error: function () {
                displayError('Error occurred while validating the card. Please try again.');
            }
        });
        
    });

    function displayError(message) {
        $('#result').show();
        $('#resultMessage')
            .removeClass('alert-success')
            .addClass('alert-danger')
            .text(message);
    }
});
