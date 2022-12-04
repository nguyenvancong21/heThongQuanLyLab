var count = total/10;
var first =$(`
    <li class="prev disabled"><a href="#">&lt;</a></li>
    <li class="active"><a href="#">1</a></li>
`)
$('.pagination ul').append(first);
for( var i=2; i<count+1; i++){
    var trHTML = $(`
                        <li><a href="#">${i}</a></li>    
                    `);
                
                $('.pagination ul').append(trHTML);

}
var last =$(`
  <li class="next"><a href="#">&gt;</a></li>
`)
$('.pagination ul').append(last);



$('.pagination li').on('click', function(event) {
  event.preventDefault();
  var $this = $(this),
      $pagination = $this.parent(),
      $pages = $pagination.children(),
      $active = $pagination.find('.active');
  
  if($this.hasClass('prev')) {
    if ($pages.index($active) > 1) {
      $active.removeClass('active').prev().addClass('active');
    }
  } else if($this.hasClass('next')) {
    if ($pages.index($active) < $pages.length - 2) {
      $active.removeClass('active').next().addClass('active');
    }
  } else {
    $this.addClass('active').siblings().removeClass('active');
  }
  
  $active = $pagination.find('.active');
  
  $('.prev')[$pages.index($active) == 1 ? 'addClass' : 'removeClass']('disabled');
  $('.next')[$pages.index($active) == $pages.length - 2 ? 'addClass' : 'removeClass']('disabled');
  
});

$('.pagination li:eq(1)').trigger('click');
