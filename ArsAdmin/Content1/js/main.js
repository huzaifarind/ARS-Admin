(function ($) {

    "use strict";

    var fullHeight = function () {

        $('.js-fullheight').css('height', $(window).height());
        $(window).resize(function () {
            $('.js-fullheight').css('height', $(window).height());
        });

    };
    fullHeight();

    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
    });

})(jQuery);

//customer booking 
$(document).ready(function () {
    var actions = '<a class="add" title="Add"><i class="material-icons">&#xE03B;</i></a>' +
        '<a class="edit" title="Edit"><i class="material-icons">&#xE254;</i></a>' +
        '<a class="delete" title="Delete"><i class="material-icons">&#xE872;</i></a>';

    // Append table with add row form on add new button click
    $(".add-colum").click(function () {
        $(this).attr("disabled", "disabled");
        var index = $("table tbody tr:last-child").index();
        var row = '<tr>' +
            '<td><input type="text" class="form-control" name="fullName" id="fullName"></td>' +
            '<td><input type="text" class="form-control" name="cnic" id="cnic"></td>' +
            '<td><input type="text" class="form-control" name="mobileNo" id="mobileNo"></td>' +
            '<td><input type="text" class="form-control" name="email" id="email"></td>' +
            '<td>' + actions + '</td>' +
            '</tr>';
        $("table").append(row);
        $("table tbody tr").eq(index + 1).find(".add, .edit").toggle();
    });

    // Add row on add button click
    $(document).on("click", ".add", function () {
        var empty = false;
        var input = $(this).parents("tr").find('input[type="text"]');
        input.each(function () {
            if (!$(this).val()) {
                $(this).addClass("error");
                empty = true;
            } else {
                $(this).removeClass("error");
            }
        });
        $(this).parents("tr").find(".error").first().focus();
        if (!empty) {
            input.each(function () {
                $(this).parent("td").html($(this).val());
            });
            $(this).parents("tr").find(".add, .edit").toggle();
            $(".add-colum").removeAttr("disabled");
        }
    });

    // Edit row on edit button click
    $(document).on("click", ".edit", function () {
        $(this).parents("tr").find("td:not(:last-child)").each(function () {
            var currentValue = $(this).text();
            $(this).html('<input type="text" class="form-control" value="' + currentValue + '">');
        });
        $(this).parents("tr").find(".add, .edit").toggle();
        $(".add-colum").attr("disabled", "disabled");
    });

    // Delete row on delete button click
    $(document).on("click", ".delete", function () {
        $(this).parents("tr").remove();
        $(".add-colum").removeAttr("disabled");
    });
});


// booking table
$(document).ready(function () {
    var actions = '<a class="add" title="Add"><i class="material-icons">&#xE03B;</i></a>' +
        '<a class="edit" title="Edit"><i class="material-icons">&#xE254;</i></a>' +
        '<a class="delete" title="Delete"><i class="material-icons">&#xE872;</i></a>';

 // Append table with add row form on add new button click
    $(".add-new").click(function () {
        $(this).attr("disabled", "disabled");
        var index = $("table tbody tr:last-child").index();
        var row = '<tr>' +
            '<td><input type="text" class="form-control" name="bookingId"></td>' +
            '<td><input type="text" class="form-control" name="customerName"></td>' +
            '<td><input type="text" class="form-control" name="flightNumber"></td>' +
            '<td><input type="text" class="form-control" name="departureDate"></td>' +
            '<td>' + actions + '</td>' +
            '</tr>';
        $("table").append(row);
        $("table tbody tr").eq(index + 1).find(".add, .edit").toggle();
    });


    // Add row on add button click
    $(document).on("click", ".add", function () {
        var empty = false;
        var input = $(this).parents("tr").find('input[type="text"]');
        input.each(function () {
            if (!$(this).val()) {
                $(this).addClass("error");
                empty = true;
            } else {
                $(this).removeClass("error");
            }
        });
        $(this).parents("tr").find(".error").first().focus();
        if (!empty) {
            input.each(function () {
                $(this).parent("td").html($(this).val());
            });
            $(this).parents("tr").find(".add, .edit").toggle();
            $(".add-new").removeAttr("disabled");
        }
    });
    // Edit row on edit button click
    $(document).on("click", ".edit", function () {
        $(this).parents("tr").find("td:not(:last-child)").each(function () {
            var currentValue = $(this).text();
            $(this).html('<input type="text" class="form-control" value="' + currentValue + '">');
        });
        $(this).parents("tr").find(".edit").toggle();
    });

    // Delete row on delete button click
    $(document).on("click", ".delete", function () {
        $(this).parents("tr").remove();
        $(".add-new").removeAttr("disabled");
    });
});
