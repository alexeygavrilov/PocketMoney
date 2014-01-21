(function() {
  var $, exports, ops;

  exports = this;

  $ = jQuery;

  ops = {
    elements: {
      '#areaTable': 'areaTable',
      '#areaAdd': 'areaAdd',
      '#tableUsers > tbody': 'table',
      '#tableVK': 'tableVK',
      '#userRowTemplate': 'rowTemplate',
      '#vkRowTemplate': 'vkTemplate',
      'li.ChildVisible': 'areaAddChild',
      'li.ParentVisible': 'areaAddParent',
      'input:radio[name=RoleType]': 'roleType',
      '#VKQueryString': 'textSearchVK',
      '#actionSendInvite': 'btnSendInvite',
      '#Email': 'textEmail',
      '#UserName': 'textUserName'
    },
    events: {
      'click #actionAdd': 'showAdd',
      'click #actionCancelAdd': 'hideAdd',
      'click #ChildRole': 'showAddChild',
      'click #ParentRole': 'hideAddChild',
      'click #actionSearchInVK': 'searchVK'
    },
    init: function() {
      return this.loadData();
    },
    loadData: function() {
      var _this = this;

      return $.get(this.settings.GetUsersUrl, function(result) {
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
    clearDataAdd: function() {
      this.textSearchVK.val('');
      this.textEmail.val('');
      this.textUserName.val('');
      this.tableVK.empty();
      return this.btnSendInvite.prop('disabled', true);
    },
    showAdd: function() {
      var _this = this;

      this.clearDataAdd();
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
      this.areaAddChild.show();
      return this.areaAddParent.hide();
    },
    hideAddChild: function() {
      this.roleType.filter('[value=0x1]').prop('checked', false);
      this.roleType.filter('[value=0x2]').prop('checked', true);
      this.areaAddChild.hide();
      return this.areaAddParent.show();
    },
    searchVK: function() {
      var index, q,
        _this = this;

      q = this.textSearchVK.val();
      if (q === '') {
        return;
      }
      index = q.indexOf("vk.com/id");
      if (index > -1) {
        q = q.substring(index + 9);
      }
      if ($.isNumeric(q)) {
        return $.get("" + this.settings.GetUserVKUrl + "?id=" + q, function(result) {
          return $.getResult(result, function() {
            _this.tableVK.empty();
            return _this.tableVK.append(_this.vkTemplate.tmpl(result.Data));
          });
        });
      } else {
        this.tableVK.empty();
        return $.showErrorMessage("Неправильный формат для идентификации пользователя ВКонтакте. Введите ссылку на пользователя, например: http://vk.com/id236979537");
      }
    },
    selectVKUser: function() {
      return false;
    }
  };

  exports.FamilyController = Spine.Controller.create(ops);

}).call(this);
