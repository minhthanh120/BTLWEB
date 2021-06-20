$(".toggle_btn1").click(function () {
    $(this).toggleClass("active");
    $(".brands-name1 ul").toggleClass("active");

    if ($(".toggle_btn1").hasClass("active")) {
        $(".toggle_text1").text("Rút gọn");
    }
    else {
        $(".toggle_text1").text("Xem thêm");
    }
});
