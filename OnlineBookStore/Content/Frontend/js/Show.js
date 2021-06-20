$(".toggle_btn").click(function () {
    $(this).toggleClass("active");
    $(".brands-name ul").toggleClass("active");

    if ($(".toggle_btn").hasClass("active")) {
        $(".toggle_text").text("Rút gọn");
    }
    else {
        $(".toggle_text").text("Xem thêm");
    }
});
