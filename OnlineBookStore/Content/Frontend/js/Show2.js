$(".toggle_btn2").click(function () {
    $(this).toggleClass("active");
    $(".brands-name2 ul").toggleClass("active");

    if ($(".toggle_btn2").hasClass("active")) {
        $(".toggle_text2").text("Rút gọn");
    }
    else {
        $(".toggle_text2").text("Xem thêm");
    }
});
