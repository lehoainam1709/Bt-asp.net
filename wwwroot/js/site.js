// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    $('[data-toggle="tooltip"]').tooltip()
});

function AddToCartAlert() {
    Swal.fire({
        icon: 'success',
        title: 'Đã thêm vào giỏ hàng',
        showConfirmButton: false,
        timer: 1500
    });
}