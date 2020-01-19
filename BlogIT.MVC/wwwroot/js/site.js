// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//const $dropdown = $(".dropdown");
//const $dropdownToggle = $(".dropdown-toggle");
//const $dropdownMenu = $(".dropdown-menu");

const $dropdown = $("#dropdown-user");
const $dropdownToggle = $("#dropdown-toggle-user");
const $dropdownMenu = $("#dropdown-menu-user");

const $dropdownAdmin = $("#dropdown-admin");
const $dropdownToggleAdmin = $("#dropdown-toggle-admin");
const $dropdownMenuAdmin = $("#dropdown-menu-admin");

const showClass = "show";

const funcDropdown = function (dropdown, dropdownToggle, dropdownMenu) {
    if (this.matchMedia("(min-width: 768px)").matches) {
        dropdown.hover(
            function () {
                const $this = $(this);
                $this.addClass(showClass);
                $this.find(dropdownToggle).attr("aria-expanded", "true");
                $this.find(dropdownMenu).addClass(showClass);
            },
            function () {
                const $this = $(this);
                $this.removeClass(showClass);
                $this.find(dropdownToggle).attr("aria-expanded", "false");
                $this.find(dropdownMenu).removeClass(showClass);
            }
        );
    } else {
        dropdown.off("mouseenter mouseleave");
    }
};





$(window).on("load resize", function () {
    funcDropdown($dropdown, $dropdownToggle, $dropdownMenu);
    funcDropdown($dropdownAdmin, $dropdownToggleAdmin, $dropdownMenuAdmin);
});


    $(function () {
        $("[data-autocomplete-source]").each(function () {
            var target = $(this);
            target.autocomplete({ source: target.attr("data-autocomplete-source") });
        });
   });
