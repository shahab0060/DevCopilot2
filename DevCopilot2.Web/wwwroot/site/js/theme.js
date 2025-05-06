"use strict";

$(function () {

    $(".btn-items-decrease").on("click", function () {
        var input = $(this).siblings(".input-items");
        if (parseInt(input.val(), 10) >= 1) {
            input.val(parseInt(input.val(), 10) - 1);
        }
    });

    $(".btn-items-increase").on("click", function () {
        var input = $(this).siblings(".input-items");
        input.val(parseInt(input.val(), 10) + 1);
    });

    function setVhVar() {
        var vh = window.innerHeight * 0.01;
        // Then we set the value in the --vh custom property to the root of the document
        document.documentElement.style.setProperty("--vh", vh + "px");
    }

    setVhVar();

    window.addEventListener("resize", setVhVar);

    var navbar = $(".navbar"),
        navbarCollapse = $(".navbar-collapse");

    $(".navbar.bg-transparent .navbar-collapse").on(
        "show.bs.collapse",
        function () {
            makeNavbarWhite();
        }
    );

    $(".navbar.bg-transparent .navbar-collapse").on(
        "hidden.bs.collapse",
        function () {
            makeNavbarTransparent();
        }
    );

    function makeNavbarWhite() {
        navbar.addClass("was-transparent");
        if (navbar.hasClass("navbar-dark")) {
            navbar.addClass("was-navbar-dark");
            navbar.removeClass("navbar-dark");
        } else {
            navbar.addClass("was-navbar-light");
        }

        navbar.removeClass("bg-transparent");

        navbar.addClass("bg-white");
        navbar.addClass("navbar-light");
    }

    function makeNavbarTransparent() {
        navbar.removeClass("bg-white");
        navbar.removeClass("navbar-light");
        navbar.removeClass("was-transparent");

        navbar.addClass("bg-transparent");
        if (navbar.hasClass("was-navbar-dark")) {
            navbar.addClass("navbar-dark");
        } else {
            navbar.addClass("navbar-light");
        }
    }

    // ------------------------------------------------------- //
    //   Bootstrap tooltips
    // ------------------------------------------------------- //

    $('[data-bs-toggle="tooltip"]').tooltip();

    $(".detail-option-btn-label").on("click", function () {
        var button = $(this);

        button
            .parents(".detail-option")
            .find(".detail-option-btn-label")
            .removeClass("active");

        button.toggleClass("active");
    });
});

