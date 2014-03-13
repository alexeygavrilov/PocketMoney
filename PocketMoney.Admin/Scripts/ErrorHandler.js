(function() {
  var polljQueryExistence;

  polljQueryExistence = function() {
    return delay(1000, function() {
      if (typeof jQuery !== 'undefined') {
        return (jQuery(document)).ajaxError(function(e, jqXHR, settings) {
          var error, errorMessage, jsonParseError;

          errorMessage = null;
          jsonParseError = false;
          if (jqXHR.statusText === 'abort') {
            return;
          }
          try {
            errorMessage = (JSON.parse(jqXHR.responseText)).errorMessage;
          } catch (_error) {
            error = _error;
            jsonParseError = true;
            errorMessage = error.message;
          }
          return $.showErrorMessage("The server has returned <strong>\"" + errorMessage + "\"</strong>,  which was unexpected.");
        });
      } else {
        return polljQueryExistence();
      }
    });
  };

  polljQueryExistence();

}).call(this);
