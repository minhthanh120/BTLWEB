
    function setBgWithDominantColor(curIndex) {
            var img = $('.mtf-pic-list').children().eq(curIndex).children('img')[0];
            if (img.src.indexOf('file://') === -1) {
        RGBaster.colors(img, {
            paletteSize: 1,
            success: function (payload) {
                $('.mtf-pic-viewer').css('backgroundColor', payload.palette[0]);
            }
        });
            }
        }
        $('.view-product').mtfpicviewer({
            selector: 'img',
            attrSelector: 'src',
            parentSelector: 'div',
            className: 'pic-viewer',
            debug: false,
            onChange: function (curIndex, preIndex) {
        setBgWithDominantColor(curIndex);
            },
            onOpen: function (curIndex) {
        setBgWithDominantColor(curIndex);
            },
            onClose: function (curIndex) {
                var $img = $('.mtf-pic-list').children().eq(curIndex).children('img');
                $('html,body').animate({'scrollTop': $('img[src="' + $img.attr('src') + '"]').offset().top });
            }
        });