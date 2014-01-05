(function() {
  var row, _fn, _i, _len, _ref;

  _ref = $('#tableUsers tbody tr');
  _fn = function(row) {
    var userId,
      _this = this;

    userId = $('UserId', row);
    return $('#actionView', row).click(function() {
      return alert(userId);
    });
  };
  for (_i = 0, _len = _ref.length; _i < _len; _i++) {
    row = _ref[_i];
    _fn(row);
  }

}).call(this);
