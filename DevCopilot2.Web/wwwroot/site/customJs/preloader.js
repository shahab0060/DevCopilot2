(function () {
    const heroSlide = document.querySelector('.lcp-image');
    const preloader = document.getElementById('preloader');
    const content = document.getElementById('content');

    const hidePreloader = () => {
        preloader.style.display = 'none';
        content.style.display = 'block';
        document.querySelectorAll('.preloader-load-background-image-url').forEach(element => {
            const imageUrl = element.getAttribute('data-url');
            element.style.backgroundImage = `url(${imageUrl})`;
        });
    };

    if (heroSlide) {
        const backgroundImage = heroSlide.style.backgroundImage;
        if (backgroundImage && backgroundImage.includes('url')) {
            const img = new Image();
            img.src = backgroundImage.replace(/url\((['"])?(.*?)\1\)/gi, '$2');
            img.onload = hidePreloader;
        } else {
            if(heroSlide.complete)
                hidePreloader();
            else
            heroSlide.onload = hidePreloader;
        }
    } else {
        hidePreloader();
    }
})();


window.addEventListener('load', function () {
    document.querySelectorAll('.full-load-background-image-url').forEach(function (element) {
        var imageUrl = element.getAttribute('data-url');
        element.style.backgroundImage = 'url(' + imageUrl + ')';
    });
});
