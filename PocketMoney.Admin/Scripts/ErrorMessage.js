(function() {
  $.showErrorMessage = function(message) {
    var box;

    box = $('#error');
    box.text(message);
    return box.show();
  };

  $.getResult = function(result, success) {
    if (result.success === false && result.message) {
      return $.showErrorMessage(result.message);
    } else {
      return success(result);
    }
  };

}).call(this);
