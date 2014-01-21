(function() {
  $.showErrorMessage = function(message) {
    var box;

    box = $('#error');
    box.text("Ошибка: " + message);
    box.fadeIn(700);
    return setTimeout((function() {
      return box.fadeOut(700);
    }), 10000);
  };

  $.getResult = function(result, success) {
    if (result.Success === false && result.Message) {
      return $.showErrorMessage(result.Message);
    } else {
      return success(result);
    }
  };

}).call(this);
