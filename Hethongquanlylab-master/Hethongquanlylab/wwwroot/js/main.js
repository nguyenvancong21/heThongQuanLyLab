/*Portfolio details slider
*/
new Swiper('.portfolio-details-slider', {
 speed: 400,
 loop: true,
 autoplay: {
   delay: 5000,
   disableOnInteraction: false
 },
 pagination: {
   el: '.swiper-pagination',
   type: 'bullets',
   clickable: true
 }
});

/**
* Testimonials slider
*/
new Swiper('.testimonials__slider', {
 speed: 600,
 loop: true,
 autoplay: {
   delay: 5000,
   disableOnInteraction: false
 },
 slidesPerView: 'auto',
 pagination: {
   el: '.testimonial__slider--navigator',
   type: 'bullets',
   clickable: true
 },
 breakpoints: {
   320: {
     slidesPerView: 1,
     spaceBetween: 20
   },

   1200: {
     slidesPerView: 3,
     spaceBetween: 20
   }
 }
});
window.addEventListener('load', () => {
  let portfolioContainer = select('.portfolio__imagecontainer');
  if (portfolioContainer) {
    let portfolioIsotope = new Isotope(portfolioContainer, {
      itemSelector: '.portfolio__item'
    });

    let portfolioFilters = select('#portfolio__flter li', true);

    on('click', '#portfolio__flters li', function(e) {
      e.preventDefault();
      portfolioFilters.forEach(function(el) {
        el.classList.remove('filter__active');
      });
      this.classList.add('filter__active');

      portfolioIsotope.arrange({
        filter: this.getAttribute('data-filter')
      });
      portfolioIsotope.on('arrangeComplete', function() {
        AOS.refresh()
      });
    }, true);
  }

});

/**
 * Initiate portfolio lightbox 
 */
const portfolioLightbox = GLightbox({
  selector: '.portfolio__lightbox'
});