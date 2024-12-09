$(document).ready(function() {
    let distance = localStorage.getItem('distance');

    if (distance) {
        $('#distance1').text(distance);
    } else {
        $('#distance1').text('Distance not found');
    }
    

    let economy_price = 100; // Example price, replace with your actual price
    let firstSeatLabel = 1;
    var details = [];

    var $cart = $('#selected-seats'),
        $counter = $('#counter'),
        $total = $('#total'),
        sc = $('#seat-map').seatCharts({
            map: [
                '_____',
                '_e_ee',
                '_e_ee',
                '_e_ee',
                '_e_ee',
                '_e_ee',
                '_e_ee',
                '_e_ee',
                '_e_ee',
                '_e_ee',
                '_e_ee',
            ],
            seats: {
                e: {
                    price: economy_price,
                    classes: 'economy-class',
                    category: 'Economy Class'
                }

            },
            naming: {
                top: false,
                getLabel: function(character, row, column) {
                    return firstSeatLabel++;
                },
            },
            click: function() {
                if (this.status() == 'available') {
                    $(event.target).toggleClass('animated rubberBand');

                    $('<li class="p-b-4">' + this.data().category + ' Seat # ' +
                        this.settings.label + ': <b>Ksh ' + this.data().price +
                        '</b> <a href="javascript:void(0);" class="cancel-cart-item btn btn-danger btn-sm"><i class="fa fa-trash"></i> cancel</a></li>')
                    .attr('id', 'cart-item-' + this.settings.id)
                    .data('seatId', this.settings.id)
                    .appendTo($cart);

                    $counter.text(sc.find('selected').length + 1);

                    let totalPrice = parseFloat(localStorage.getItem('distance')) * parseFloat($('#price').text().trim()) * (sc.find('selected').length + 1);
                    $total.text(totalPrice.toFixed(2));

                    var selectedSeatsCount = sc.find('selected').length + 1;
                    $('#age-group-container').empty();
                    for (var i = 0; i < selectedSeatsCount; i++) {
                        var selectAgeGroup = $('<select class="form-control age-group-select"></select>');
                        selectAgeGroup.append('<option value="0-5">0-5</option>');
                        selectAgeGroup.append('<option value="6-12">6-12</option>');
                        selectAgeGroup.append('<option value="12-50">12-50</option>');
                        selectAgeGroup.append('<option value="above 50">Above 50</option>');
                        $('#age-group-container').append(selectAgeGroup);
                    }

                    details.push({
                        ['seatNo']: this.settings.label,
                        ['price']: this.data().price
                    });

                    return 'selected';
                } else if (this.status() == 'selected') {
                    $(event.target).toggleClass('animated rubberBand');

                    $counter.text(sc.find('selected').length - 1);

                    $('#cart-item-' + this.settings.id).remove();

                    let totalPrice = parseFloat(localStorage.getItem('distance')) * parseFloat($('#price').text().trim()) * (sc.find('selected').length - 1);
                    $total.text(totalPrice.toFixed(2));

                    no = this.settings.label;
                    var filtered = details.filter(function(item) {
                        return item.seatNo != no;
                    });
                    details = filtered;

                    return 'available';
                } else if (this.status() == 'unavailable') {
                    return 'unavailable';
                } else {
                    return this.style();
                }
            }
        });

    $('#selected-seats').on('click', '.cancel-cart-item', function() {
        $('#' + sc.get($(this).parents('li:first').data('seatId')).settings.id)
            .toggleClass('animated rubberBand');
        sc.get($(this).parents('li:first').data('seatId')).click();
    });

    let booked_seats = function(bus_id) {
        $.ajax({
            method: 'GET',
            url: 'api/book.php?bus_id=' + $.trim(bus_id) + '&booked_seats',
            success: function(data) {
                sc.find('unavailable').status('available');
                data.forEach((element => sc.get([sc.seatIds[element - 1]]).status('unavailable')))
            },
            error: function(data) {
                console.log(data)
            }
        });
    };

    $('#bookBtn').on('click', function() {
        var selectedSeats = sc.find('selected');
        var totalPrice = 0;

        selectedSeats.each(function(index, seat) {
            var seatPrice = $(seat).data().price;
            var ageGroup = $('.age-group-select').eq(index).val();

            if (ageGroup === '0-5') {
                totalPrice += 0;
            } else if (ageGroup === '6-12') {
                totalPrice += seatPrice / 2;
            } else if (ageGroup === 'above 50') {
                totalPrice += seatPrice * 0.7; // 30% discount for age above 50
            } else {
                totalPrice += seatPrice;
            }
        });

        $total.text(totalPrice.toFixed(2));
    });
});
     

let recalculateTotal = sc => {
    var total = 0;

    //basically find every selected seat and sum its price
    sc.find('selected').each(function() {
        total += this.data().price;
    });

    return total;
}
//this will handle "[cancel]" link clicks
$('#selected-seats').on('click', '.cancel-cart-item', function() {
    $('#'+sc.get($(this).parents('li:first').data('seatId')).settings.id)
        .toggleClass('animated rubberBand');
    //let's just trigger Click event on the appropriate seat, so we don't have to repeat the logic here
    sc.get($(this).parents('li:first').data('seatId')).click();
});


let booked_seats = function(bus_id) {
    $.ajax({
        method: 'GET', //https://examinationcomplaint.theschemaqhigh.co.ke/HCI/api/book/
        url: 'api/book.php?bus_id='+$.trim(bus_id)+'&booked_seats',
        success: function (data) {
            sc.find('unavailable').status('available');
            data.forEach((element => sc.get([sc.seatIds[element-1]]).status('unavailable')))
        },
        error: function (data) {
            console.log(data)
        }
    });
};


$('select').niceSelect();

$('.g-link').on('click', function(event) {
    $('.gallery1 a').trigger('click')
});

var $gallery = $('.gallery1 a').simpleLightbox();

//jQuery time
var current_fs, next_fs, previous_fs, busId=0,seatsArray = []; //fieldsets
var left, opacity, scale; //fieldset properties which we will animate
var animating; //flag to prevent quick multi-click glitches


$(".next_button").on('click', function(event) {

    if (animating) return false;
    animating = true;

    current_fs = $(this).parent().parent();
    next_fs = current_fs.next();

    if ($("fieldset").index(next_fs) == 1) {
        $('span.from').text($('select.from').val());
        $('span.to').text($('input.to').val());
        $.ajax({
            method: 'GET', //https://examinationcomplaint.theschemaqhigh.co.ke/HCI/api/book/
            url: 'https://examinationcomplaint.theschemaqhigh.co.ke/HCI/api/book/?bus_id=1&booked_seats',
            success: function (data) {
                $('span.seats-left').text(51 - data.length);
            },
            error: function (data) {
                console.log(data)
            }
        });

    }
 else if ($("fieldset").index(current_fs) == 3) {
        animating = false;
        if (!verifyInfoForm()) {

        } else {
            goNext(next_fs, current_fs)
            $('span.show-email').text($('input[name="email"]').val());
        }
    } else {
        goNext(next_fs, current_fs)
        if ($("fieldset").index(next_fs) == 5) {
            $data = {
                'busId' : busId,
                'seats' : seatsArray.toString(),
                'personalInfo' : $('form').serializeArray()
            }
            $data = JSON.stringify($data)
            console.log($data);
            $.ajax({
                method: 'POST',//https://examinationcomplaint.theschemaqhigh.co.ke/HCI/api/book/
                url: 'api/book.php',
                data: $data,
                dataType: 'json',
                success: function (data) {
                    console.log(data)
                },
                error: function (data) {
                    console.log(data)
                }
            });
        }
    }



    //     let date = $('#date').val();
    //     let regexp = /^(\d{4})[\/\-](0?[1-9]|1[012])[\/\-](0?[1-9]|[12][0-9]|3[01])$/;
    //     if (!regexp.test(date)) return 0;
    // }

    //go to next fs
    // $(".next_button").unbind('click',goNext(next_fs, current_fs));

});


$(".book_btn").click(function() {
    busId = Number($(this).data('bus')); //determine which bus was choosen
    let busName = $(this).parents('.bus-details').children('.bus-name').text();
    $('span.number_plate').text($(this).parents('.bus-details').children('.bus-name').text())
    if (animating) return false;
    animating = true;

    current_fs = $(this).parents('fieldset');
    next_fs = current_fs.next();

    if ($("fieldset").index(next_fs) == 2) {
        booked_seats(busId);
        setInterval( booked_seats(busId),3000);
    }

    //de-activate current step on progressbar
    goNext(next_fs, current_fs)

});

$(".previous_button").click(function() {

    current_fs = $(this).parent().parent();

    previous_fs = current_fs.prev();

    if ($("fieldset").index(previous_fs) == 2) {
        booked_seats(busId);
        setInterval( booked_seats(busId),3000);
    }

    //de-activate current step on progressbar
    if (previous_fs.length > 0) {
        if (animating) return false;
        animating = true;
        $("#progressbar li ").eq($("fieldset").index(current_fs)).removeClass("active");

        //show the previous fieldset
        previous_fs.show();
        //hide the current fieldset with style
        current_fs.animate({
            opacity: 0
        }, {

            duration: 100,
            complete: function() {
                current_fs.hide();
                previous_fs.css('opacity', 1);
                animating = false;
            },
            //this comes from the custom easing plugin
            easing: 'easeInOutBack'
        });
    }
});

function goNext(next_fs, current_fs) {
    $("#progressbar li ").eq($("fieldset").index(next_fs)).addClass("active");
    //show the next fieldset
    next_fs.show();

    current_fs.animate({
        opacity: 0
    }, {
        duration: 10,
        complete: function() {
            current_fs.hide();
            animating = false;
        },
        //this comes from the custom easing plugin
        easing: 'easeInOutBack'
    });
}

function verifyInfoForm() {
    let name = $('input[name="name"]'),
        id = $('input[name="id"]'),
        phone = $('input[name="phone"]'),
        email = $('input[name="email"]'),
        nameRegexp = /^[a-zA-Z]+\s+[a-zA-Z\s]+$/,
        idRegexp = /^\d{8}$/,
        emailRegexp = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i,
        phoneRegexp = /^(0|\+92)3\d{9}$/,
        isValid = []
    if ($.trim(name.val()) == '') {
        name.addClass('is-invalid')
        name.parent().find('small.text-danger').remove()
        name.parent().append('<small class="text-danger">Name field cannot be empty</small>')
        isValid['name'] = false;
    } else if (!nameRegexp.test($.trim(name.val()))) {
        name.addClass('is-invalid')
        name.parent().find('small.text-danger').remove()
        name.parent().append('<small class="text-danger">Full name can only consist of alphabetical and atlest two names.</small>')
        isValid['name'] = false;
    } else {
        name.removeClass('is-invalid')
        name.addClass('is-valid')
        name.parent().find('small.text-danger').remove();
        isValid['name'] = true;
    }

    // phone no

    if ($.trim(phone.val()) == '') {
        phone.addClass('is-invalid')
        phone.parent().find('small.text-danger').remove()
        phone.parent().append('<small class="text-danger">Phone No field cannot be empty</small>')
        isValid['phone'] = false;
    } else if (!phoneRegexp.test($.trim(phone.val()))) {
        phone.addClass('is-invalid')
        phone.parent().find('small.text-danger').remove()
        phone.parent().append('<small class="text-danger">Please provide a valid phone number.</small>')
        isValid['phone'] = false;
    } else {
        phone.removeClass('is-invalid')
        phone.addClass('is-valid')
        phone.parent().find('small.text-danger').remove();
        isValid['phone'] = true;
    }
    // id
    if ($.trim(id.val()) == '') {
        id.addClass('is-invalid')
        id.parent().find('small.text-danger').remove()
        id.parent().append('<small class="text-danger">Id field cannot be empty</small>')
        isValid['id'] = false;
    } else if (!idRegexp.test($.trim(id.val()))) {
        id.addClass('is-invalid')
        id.parent().find('small.text-danger').remove()
        id.parent().append('<small class="text-danger">Please provide a valid ID number.</small>')
        isValid['id'] = false;
    } else {
        id.removeClass('is-invalid')
        id.addClass('is-valid')
        id.parent().find('small.text-danger').remove();
        isValid['id'] = true;
    }

    // email
    if ($.trim(email.val()) == '') {
        email.addClass('is-invalid')
        email.parent().find('small.text-danger').remove()
        email.parent().append('<small class="text-danger">Email field cannot be empty</small>')
        isValid['email'] = false;
    } else if (!emailRegexp.test($.trim(email.val()))) {
        email.addClass('is-invalid')
        email.parent().find('small.text-danger').remove()
        email.parent().append('<small class="text-danger">Please provide a valid email address.</small>')
        isValid['email'] = false;
    } else {
        email.removeClass('is-invalid')
        email.addClass('is-valid')
        email.parent().find('small.text-danger').remove();
        isValid['email'] = true;
    }

    return Object.values(isValid).every(function (value) {
        return value == true;
    });
}


$('#date_form').on('submit', (event) => {
    event.preventDefault();
})

$('ul#progressbar').on('click', 'li', function(event) {
    let i = $('#progressbar li').index(this)
    let j = $('#progressbar').find('li.active').length - 1

    if (i > j) {
        if (j == 1) {
            Lobibox.notify('error', {
                showClass: 'fadeInDown',
                hideClass: 'fadeUpDown',
                iconSource: 'fontAwesome',
                // img: 'assets/images/logo-96.png',
                title: "No Bus Selected!",
                continueDelayOnInactiveTab: true,
                size: 'mini',
                msg: 'Please select a bus from which you want to book.'
            });
        }else{
            $("fieldset").eq(j).find('.next_button').trigger('click')
        }
    }else if (i < j) $("fieldset").eq(j).find('.previous_button').trigger('click')

});
if ('serviceWorker' in navigator) {
    window.addEventListener('load', function() {
        navigator.serviceWorker.register('service-worker.js')
            .then(function() {
                console.log("Service Worker Registered,");
            });
    });
}