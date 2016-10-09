﻿function GetTourDetail(id) {
    $.ajax({
        url: "/TourDetail/GetDetailTour/" + id,
        type: "GET",
    })
    .done(function (partialViewResult) {
        $("#tour-details").html(partialViewResult);
    });
}

function searchTour() {
    var id = $('#nametourVietNam').val();
    $.ajax({
        url: "/TourDetail/SearchTourVietNam/" + id,
        type: "GET",
    })
    .done(function (partialViewResult) {
        $("#list-tour-vietnam").html(partialViewResult);
    });
}

function searchTourForeign() {
    var id = $('#nametourForeign').val();
    $.ajax({
        url: "/TourDetail/SearchTourForeign/" + id,
        type: "GET",
    })
    .done(function (partialViewResult) {
        $("#list-tour-foreign").html(partialViewResult);
    });
}

function searchWithDateAndTown() {
    var dayValue = $("#daysearch").val();
    var region = $("#region").val();
    $.ajax({
        url: "/TourDetail/SearchByDateAndTown",
        type: "POST",
        data:
            {
                regionSearch: region,
                dateSearch: dayValue,
            },
    })
    .done(function (partialViewResult) {
        $("#list-tour-vietnam").html(partialViewResult);
    });
}

function activatedTour(id) {
    $.ajax({
        url: "/TourDetail/ActiveTour/" + id,
        type: "GET",
    })
    .done(function (partialViewResult) {
        $("#tour-details").html(partialViewResult);
    });
}

function deleteTour(id) {
    swal({
        title: "Xác nhân xóa tour?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Xóa tour",
        closeOnConfirm: false
    },
    function ()
    {
        $.ajax({
            url: "/TourDetail/DeleteTour/" + id,
            type: "GET",
            success: function (result) {
                swal("Đã xóa!", "Tour đã được xóa thành công!", "success");
            },
            error: function (xhr) {
                alert('Xóa không thành công');
            }
        });
        setTimeout(function () {
            location.reload();
        }, 2000);
              
    });
}

$(document).ready(function () {
    $('.datepicker_init').datetimepicker({
        locale: 'es',
        format: 'YYYY-MM-DD',
        useCurrent: false
    });
    $('.datepicker_end').datetimepicker({
        locale: 'vi',
        format: 'YYYY-MM-DD',
        useCurrent: false
    });
    var listRegion = ["Miền Bắc", "Miền Trung", "Miền Nam"];
    $("#region").select2({
        data: listRegion
    });
});
