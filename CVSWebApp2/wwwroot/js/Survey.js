let activeIndex = 0;
let scrollTopPadding = -270;
let wrapper;
let fields;
let btnGroup;
let textarea;
var isSubmitted;
var visiblePopover;


function setActiveTab() {
  fields.removeClass('active');
  let activeField = fields.eq(activeIndex);
  activeField.addClass('active');
  activeField.find('input').focus();
}

function deleteAllPopover() {
    for (var i = 0; i < fields.length; i++) {
        if (visiblePopover != i + 1) {
            $(fields[i]).find("[data-toggle='popover']").popover('hide');
        }
    }
}

function deletePopoverByField(field) {
    $(field).find("[data-toggle='popover']").popover('hide');
}


function scrollToActiveField(field) {

  deleteAllPopover();
  let index = fields.index(field);
  if (index !== activeIndex) {

    activeIndex = index;

    var toppadding = 100;
    console.log($(window).width());
    if ($(window).width() <= 380) {
        toppadding = 80;
    }
    console.log(toppadding);
    let offset = $(field).offset().top - toppadding;


    wrapper.animate({ scrollTop: wrapper.scrollTop() + offset  }, 200);


    setTimeout(function () { $(field).find("button").first().focus(); }, 0);
    setActiveTab();


    if (getActiveButtonValue(field) > 0) {
        focusToTextArea(field, getActiveButtonValue(field));
    }
  }
}


function getActiveButtonValue(field) {

    if ($(field).find(".btn.btn-default.active").length > 0) {
        return parseInt($(field).find(".btn.btn-default.active").text());
    }
    return 0;
}

function focusToTextArea(field,visibleRange) {


    //  Focus And show to Text Area
    textarea.css("background-size", "0 2px, 100% 0px");

    $(field).children("textarea").each(function (i, object) {
        $(object).hide();
        if (visibleRange >= parseInt($(object).attr("visible-from")) &&
            visibleRange <= parseInt($(object).attr("visible-to"))) {
            $(object).show();

            setTimeout(function () { $(object).css("background-size", "0 2px, 100% 1px").focus(); }, 0);
            
        }
    });
}

function buttonEnterClick(e,btn) {
    var currentField = $(btn).parent().parent().parent(".field");

    $(btn).parent().fadeOut(300).fadeIn(150).fadeOut(300).fadeIn(150, function () {

        currentField.find(".helpertext").hide();
        let nextFieldIndex = fields.index(currentField) + 1;
        setTimeout(function () { fields.eq(nextFieldIndex).click(); }, 0);


        if (currentField.attr("class").indexOf("last-field") > 0) {
            ShowModalThankYou();
        }
        return false;

    });

}

function SendSurvey() {
    $('#modalSlideUp').modal({ backdrop: 'static', keyboard: false });
}

function scrollToActiveFieldByIndex(index) {
  scrollToActiveField(fields.eq(index));
}


// DOCUMENT CODE START HERE
$(document).ready(() => {
  wrapper = $('.page-container');
  fields = $('.field'); 
  textarea = $('.field textarea');
 
  $(".helpertext").hide();
  $(".lblComment").hide();
  $(".footer").hide();

  //wrapper.scroll(function () {
  //    console.log($(".last-field").offset().top);
  //    if ($(".last-field").offset().top < 180) {
  //        $(".sticky-footer").removeClass("hidden");
  //    }
  //    else {
  //        $(".sticky-footer").addClass("hidden");
  //    }

  //});




 

  setTimeout(function () { $(".first-field").focus(); }, 0);




  $("div[btn-type=select]").children(".fa-check").hide();


  fields.click(function() {
    scrollToActiveField(this);
  });

 $("#question-nav-up").on("click", function () {
      let currentActiveField = $("#formSurvey").find(".field.active");
      if (currentActiveField.length > 0) {
          let nextInputIndex = inputs.index(currentActiveField) - 1;
          if (nextInputIndex > 0) {
              inputs.eq(nextInputIndex).focus();
          }
      }
  });
 
  $("#question-nav-down").on("click", function () {
      let currentActiveField = $("#formSurvey").find(".field.active");
      if (currentActiveField.length > 0) {
          let nextInputIndex = inputs.index(currentActiveField) + 1;
          if (nextInputIndex < inputs.length) {
              inputs.eq(nextInputIndex).focus();
          }
      }
  });


  let inputs = $('.field input');
  inputs.focus(function(){
    scrollToActiveField($(this).parent());
  });


  inputs.keydown(function(event) {
    if (event.keyCode === 13 && this.validity.valid) { // enter
        let nextInputIndex = inputs.index(this) + 1;
        if (nextInputIndex < inputs.length) {
          inputs.eq(nextInputIndex).focus();
        }
      }
  });

    // additional for textarea


  textarea.css("background-size", "0 2px, 100% 0px");

  //textarea.keydown(function (event) {
  //    if (event.keyCode === 13 && this.validity.valid) { // enter

  //        var currentField = $(this).parent();


  //        let nextFieldIndex = fields.index(currentField) + 1;
  //        scrollToActiveField(fields.eq(nextFieldIndex));
  //    }
  //});

  textarea.on("focusout", function (event) {

  });

  textarea.on("focus", function (event) {
      $(".helpertext").remove();
      var helperHtml = '<div class="helpertext">' + 
          '<p class="hint-text"> SHIFT + ENTER to make a line break</p>' + 
          '<div style="height:20px;line-height:24px;">' +
          '<div style="margin-top: 0 !important;" class="btn btn-primary btn-sm m-t-10" onclick="return buttonEnterClick(event,this);">OK<i class="fa fa-check" style="margin-left:5px;"></i></div>' +
          '  press <b>ENTER</b></div></div >';


      $(this).parent(".field").append(helperHtml);
     
  });

 

  textarea.keyup(function (event) {
      $(this).css('height', '20px');
          var h = ( $(this).prop("scrollHeight") + 20) + 'px';
          $(this).css('height', h);
  });

  textarea.keydown(function (event) {
      var textareas = $(this).parent().children("textarea");

      if (event.shiftKey && event.keyCode === 13) {
          //var text = $(this).val();
          //var matches = text.match(/\n/g);
          //var cnt = (matches ? matches.length : 0) + 1;
          //$(this).attr('row', cnt);
          var h =( $(this).prop("scrollHeight") + 20) + 'px';
          $(this).css('height', h);

      }
      else if (event.keyCode === 13 && this.validity.valid) { // enter

          let nextAreaIndex = textareas.index($(this)) + 1;

          if (textareas.eq(nextAreaIndex).length > 0) {
                  textareas.eq(nextAreaIndex).focus();
          }
          else {
              var currentField = $(this).parent();
              if (currentField.attr("class").indexOf("last-field") > 0) {
                  ShowModalThankYou();
              }
              $(this).parent().find(".helpertext").find(".btn").fadeOut(300).fadeIn(150).fadeOut(300).fadeIn(150, function () {
                  
                  let nextFieldIndex = fields.index(currentField) + 1;
                      scrollToActiveField(fields.eq(nextFieldIndex));
              });
              return false;
          }
      }
  });

  setActiveTab();
  
  btnGroup = $('.btn.btn-default'); 

  btnGroup.click(function () {
      $(".tooltip").tooltip('hide');

    $(this).parent(".btn-group-lg").children().removeClass("active");

    if ($(this).attr("btn-type") == "select") {
        $(this).parent().parent().parent().find(".btn-cons").removeClass("active");
    }
    $(this).addClass("active");



    deletePopoverByField($(this).parent(".btn-group-lg").parent(".btn-toolbar").parent(".form-group").parent(".field"));


    var selectedButtonVal = parseInt($(this).text());
    $(this).parent(".btn-group-lg").parent(".btn-toolbar").parent(".form-group").parent(".field").children(".lblComment").hide();
    $(this).parent(".btn-group-lg").parent(".btn-toolbar").parent(".form-group").parent(".field").children("textarea").hide();

    
    if ($(this).attr("btn-type") == "select") {



            $(this).parent().parent().parent().find("i").hide();

            $(this).children(".fa-check").show();

            $(this).fadeOut(300).fadeIn(150).fadeOut(300).fadeIn(150, function () {
                var currentField = $(this).parent().parent().parent().parent();
                let nextFieldIndex = fields.index(currentField) + 1;
                //scrollToActiveField(fields.eq(nextFieldIndex));
                setTimeout(function () { fields.eq(nextFieldIndex).click(); }, 0);
            });
            
        }
        else {

            //  Focus And show to Text Area
            focusToTextArea($(this).parent(".btn-group-lg").parent(".btn-toolbar").parent(".form-group").parent(".field"), selectedButtonVal);


            // Show Element (label) base on visible range
            $(this).parent(".btn-group-lg").parent(".btn-toolbar").parent(".form-group").parent(".field").children(".lblComment").each(function (i, object) {

                if (selectedButtonVal >= parseInt($(object).attr("visible-from")) &&
                    selectedButtonVal <= parseInt($(object).attr("visible-to"))) {
                    $(object).show();
                }
            });


            $(".lblComment.First").hide();
        }

  });


});

function ShowModalThankYou() {
    $("#modalSlideUpSmall").removeClass("fade").modal({
        backdrop: 'static',
        keyboard: false
    });
}


// DOCUMENT CODE END HERE






