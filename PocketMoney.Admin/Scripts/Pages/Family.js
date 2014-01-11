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
      'input:radio[name=RoleType]': 'roleType',
      '#VKQueryString': 'textSearchVK'
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

      return $.get(this.settings.LoadUsersUrl, function(result) {
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
    },
    searchVK: function() {
      var q,
        _this = this;

      q = this.textSearchVK.val();
      if (q === '') {
        return;
      }
      VK.init({
        apiId: this.settings.VKApiID
      });
      return $.get("https://oauth.vk.com/access_token?client_id=" + this.settings.VKApiID + "&client_secret=" + this.settings.VKApiKey + "&v=5.5&grant_type=client_credentials", function(r) {
        if ($.isNumeric(q)) {
          return VK.Api.call('users.get', {
            user_ids: q,
            fields: 'photo'
          }, function(result) {
            var row, _i, _len, _ref, _results;

            _this.tableVK.empty();
            _ref = result.response;
            _results = [];
            for (_i = 0, _len = _ref.length; _i < _len; _i++) {
              row = _ref[_i];
              _results.push(_this.tableVK.append(_this.vkTemplate.tmpl(row)));
            }
            return _results;
          });
        } else {
          return VK.Api.call('users.search', {
            q: q,
            sort: 0,
            count: 10,
            fields: 'photo'
          }, function(result) {
            var row, _i, _len, _ref, _results;

            _this.tableVK.empty();
            _ref = result.items;
            _results = [];
            for (_i = 0, _len = _ref.length; _i < _len; _i++) {
              row = _ref[_i];
              _results.push(_this.tableVK.append(_this.vkTemplate.tmpl(row)));
            }
            return _results;
          });
        }
      });
    }
  };

  exports.FamilyController = Spine.Controller.create(ops);

}).call(this);
