(function() {
  var $, exports, ops;

  exports = this;

  $ = jQuery;

  ops = {
    elements: {
      '#areaTable': 'areaTable',
      '#areaAdd': 'areaAdd',
      '#tableUsers > tbody': 'table',
      '#userRowTemplate': 'rowTemplate',
      'li.ChildVisible': 'areaAddChild',
      'input:radio[name=RoleType]': 'roleType'
    },
    events: {
      'click #actionAdd': 'showAdd',
      'click #actionCancelAdd': 'hideAdd',
      'click #ChildRole': 'showAddChild',
      'click #ParentRole': 'hideAddChild'
    },
    init: function() {
      return this.loadData();
    },
    loadData: function() {
      var _this = this;

      return $.get(this.urls.loadUsers, function(result) {
        return $.getResult(result, function() {
          var row, _i, _len, _ref, _results;

          _this.table.empty();
          _ref = result.List;
          _results = [];
          for (_i = 0, _len = _ref.length; _i < _len; _i++) {
            row = _ref[_i];
            _results.push(_this.table.append(_this.rowTemplate.tmpl(row)));
          }
          return _results;
        });
      });
    },
    showAdd: function() {
      var _this = this;

      return this.areaTable.hide('slide', {
        direction: 'left'
      }, 400, function() {
        return _this.areaAdd.show('slide', {
          direction: 'right'
        }, 400);
      });
    },
    hideAdd: function() {
      var _this = this;

      return this.areaAdd.hide('slide', {
        direction: 'right'
      }, 400, function() {
        return _this.areaTable.show('slide', {
          direction: 'left'
        }, 400);
      });
    },
    showAddChild: function() {
      this.roleType.filter('[value=0x1]').prop('checked', true);
      this.roleType.filter('[value=0x2]').prop('checked', false);
      return this.areaAddChild.show();
    },
    hideAddChild: function() {
      this.roleType.filter('[value=0x1]').prop('checked', false);
      this.roleType.filter('[value=0x2]').prop('checked', true);
      return this.areaAddChild.hide();
    }
  };

  exports.FamilyController = Spine.Controller.create(ops);

}).call(this);
