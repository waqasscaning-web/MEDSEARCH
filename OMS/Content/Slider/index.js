// jQuery text slider by Aziz Natour
// CC BY 4.0 License
// http://creativecommons.org/licenses/by/4.0/
var sucks = null;
var sayTimeout = null;
$("body").on('click', '#nextSlide', function() {
  i++;
  if(i == $('.slide').length) {
    $('.slide.active').removeClass('active out');
    $($('.slide')[0]).addClass('active');
    i = 0;
  }
  else {
      $('.slide.active').addClass('out').next('.slide').addClass('active');
      
  }

    var ptags = $('.slide.active:not(.out) p');
    sucks = ptags;
  var msgs= "";
  $.each(ptags, function (index, value) {
      //console.log(value.innerHTML);
      msgs += value.innerHTML+". ";
    });
    debugger;
  msg.text = msgs;
  //console.log(msg);
  //console.log(speechSynthesis);
  //speechSynthesis.cancel();

  //speechSynthesis.speak(msg);

  say();
  

});
function say() {
    if (speechSynthesis.speaking) {
        // SpeechSyn is currently speaking, cancel the current utterance(s)
        speechSynthesis.cancel();
        console.log('window is speaking');
        // Make sure we don't create more than one timeout...
        if (sayTimeout !== null)
            clearTimeout(sayTimeout);
        debugger;
        sayTimeout = setTimeout(function () { say(); }, 500);
    }
    else {
        // Good to go
        debugger;
        console.log('window is not speaking');
        speechSynthesis.speak(msg);
        console.log('question spoken');
        console.log(msg);
    }
}