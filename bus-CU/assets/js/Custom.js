function calculateDistance() {
    var departure = document.getElementById('departure').value;
    var destination = document.getElementById('destination').value;

    var directionsService = new google.maps.DirectionsService();

    var request = {
        origin: departure,
        destination: destination,
        travelMode: 'DRIVING'
    };

    directionsService.route(request, function(response, status) {
        if (status == 'OK') {
            var distanceInKm = response.routes[0].legs[0].distance.value / 1000; // Distance in kilometers
            var distanceElement = document.getElementById('distance');
            distanceElement.value = 'Distance: ' + distanceInKm + ' km';

            // Calculate price
            var price = distanceInKm * 6; // Assuming price per kilometer is 6 units
            var priceElement = document.getElementById('Price');
            priceElement.value = 'Price: ' + price.toFixed(2); // Displaying price with 2 decimal places

            // Display map
var mapDiv = document.getElementById('map');
mapDiv.style.display = 'block';
            // Display map
            var map = new google.maps.Map(document.getElementById('map'), {
                zoom: 7,
                center: {lat: 41.85, lng: -87.65} // Default centering for the map
            });
            var directionsRenderer = new google.maps.DirectionsRenderer();
            directionsRenderer.setMap(map);
            directionsRenderer.setDirections(response);
        } else {
            alert('Error: ' + status);
        }
    });
    
}

$(function() {
$("#print").click(function() {
window.print();
});
});

document.getElementById("downloadPdfButton").addEventListener("click", function() {
const element = document.getElementById("contentToConvert");
html2pdf().from(element).save();
});


function calculateCharges() {
    var distance = parseInt(document.getElementById('distance1').value);
    var numPassengers = parseInt(document.getElementById('numPassengers').value);
    var totalCharges = 0; // Initialize totalCharges to 0
    var pricePerKm = 6; // Set price of 1 km as 6 PKR

    for (var i = 1; i <= numPassengers; i++) {
        var ageGroupSelect = document.getElementById('passengerAge' + i);
        var ageGroup = ageGroupSelect.options[ageGroupSelect.selectedIndex].value;

        if (ageGroup === '0-5') {
            // No charges
        } else if (ageGroup === '6-12') {
            totalCharges += 0.5 * pricePerKm * distance; // Half charges
        } else if (ageGroup === '12-50') {
            totalCharges += 1 * pricePerKm * distance; // Full charges
        } else {
            totalCharges += 0.7 * pricePerKm * distance; // 30% discount
        }
    }

    // Display the total charges
    var chargesResult = document.getElementById('chargesResult');
    var chargesAmount = document.getElementById('chargesAmount');

    chargesAmount.textContent = 'Total Charges: PKR ' + totalCharges.toFixed(2);
    chargesResult.style.display = 'block';
}

document.getElementById('numPassengers').addEventListener('input', function () {
    var numPassengers = parseInt(this.value);
    var passengerAges = document.getElementById('passengerAges');
    passengerAges.innerHTML = '';

    for (var i = 1; i <= numPassengers; i++) {
        var label = document.createElement('label');
        label.textContent = 'Age of Passenger ' + i + ':';
        var select = document.createElement('select');
        select.classList.add('form-control');
        select.id = 'passengerAge' + i;
        var ageGroupOption = ['0-5', '6-12', '12-50', 'above 50'];

        ageGroupOption.forEach(function (optionValue) {
            var option = document.createElement('option');
            option.value = optionValue;
            option.textContent = optionValue;
            select.appendChild(option);
        });

        var br = document.createElement('br');
        passengerAges.appendChild(label);
        passengerAges.appendChild(select);
        passengerAges.appendChild(br);
    }
});