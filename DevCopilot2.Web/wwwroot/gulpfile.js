const gulp = require('gulp');
const concat = require('gulp-concat');
const cleanCSS = require('gulp-clean-css');

gulp.task('styles', function() {
    return gulp.src(['site/use.fontawesome.com/releases/v5.10.0/css/all.css',
                     'site/vendor/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.css',
                     'site/vendor/nouislider/nouislider.css',
                     'Shared/notifications.min.css',
                     'Shared/persianDatepicker-default.css',
                     'Shared/waitMe.min.css',
                     'Shared/CustomShared.css'])
        .pipe(concat('allDeferCssPack.css'))
        .pipe(cleanCSS())
        .pipe(gulp.dest('site/packs'));
});
gulp.task('scripts', function() {
    return gulp.src([
        'site/vendor/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.concat.min.js',
        'site/js/custom-scrollbar-init.min.js',
        'site/vendor/smooth-scroll/smooth-scroll.polyfills.min.js',
        'Shared/persianDatepicker.min.js',
        'Shared/waitMe.min.js',
        'Shared/notifications.min.js',
        'Shared/CustomSharedJs.js',
        'Shared/LocationSelect.js',
        'site/customJs/SiteCustomSharedJs.js',
        'site/js/theme.js'
        ]) 
        .pipe(concat('allJsDeferPack.js'))
        .pipe(gulp.dest('site/packs'));
});
gulp.task('add-zoom', function() {
    return gulp.src([
        'site/vendor/photoswipe/photoswipe.min.js', 
        'site/vendor/photoswipe/photoswipe-ui-default.min.js', 
        'site/js/photoswipe-init.js',
        'site/vendor/jquery-zoom/jquery.zoom.min.js',
        'site/vendor/jquery-zoom/jquery-zoom-init.js'
        ]) 
        .pipe(concat('ZoomPack.js'))
        .pipe(gulp.dest('site/packs'));
});
gulp.task('add-swiper-pack', function() {
    return gulp.src([
        'site/vendor/swiper/swiper-bundle.min.js', 
        'site/vendor/swiper/swiper-init.js',
        'site/js/sliders-init.js'   
        ]) 
        .pipe(concat('swiper-pack.js'))
        .pipe(gulp.dest('site/vendor/swiper'));
});
gulp.task('add-aos-pack', function() {
    return gulp.src([
        'site/vendor/aos/aos.js', 
        'site/vendor/aos/aos-init.js'
        ]) 
        .pipe(concat('aos-pack.js'))
        .pipe(gulp.dest('site/vendor/aos'));
});
gulp.task('add-ofi-pack', function() {
    return gulp.src([
        'site/vendor/object-fit-images/ofi.min.js', 
        'site/vendor/object-fit-images/ofi-init.js'
        ]) 
        .pipe(concat('ofi-pack.js'))
        .pipe(gulp.dest('site/vendor/object-fit-images'));
});

gulp.task('default', gulp.series('styles', 'scripts','add-zoom',
    'add-swiper-pack','add-aos-pack','add-ofi-pack'));