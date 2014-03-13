(function() {
  var $, exports, ops;

  exports = this;

  $ = jQuery;

  ops = {
    elements: {
      '#areaList': 'areaList',
      '#areaUser': 'areaUser',
      '#tableUsers > tbody': 'table',
      '#userRowTemplate': 'rowTemplate',
      'input:radio[name=RoleType]': 'roleType',
      '#Email': 'textEmail',
      '#UserName': 'textUserName',
      '#Photo': 'imgPhoto',
      '#UploadPhoto': 'uploadPhoto',
      '#SendNotification': 'checkNotification',
      'div.validation-summary-errors > ul': 'validationSummary'
    },
    events: {
      'click #actionAdd': 'showAdd',
      'click #actionCancelAdd': 'hideAdd',
      'click #ChildRole': 'showAddChild',
      'click .selector-by-click': 'selectInputByClick',
      'click #actionCreateUser': 'addUser'
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
    clearAddForm: function() {
      this.textUserName.val('');
      this.textUserName.removeClass('input-validation-error');
      this.textEmail.val('');
      this.textEmail.removeClass('input-validation-error');
      this.imgPhoto.attr('src', '#');
      this.uploadPhoto.empty();
      this.validationSummary.empty();
      this.roleType.filter('[value=1]').prop('checked', true);
      return this.checkNotification.prop('checked', true);
    },
    showAdd: function() {
      var _this = this;

      this.clearAddForm();
      this.textUserName.blur(function() {
        return _this.validate();
      });
      this.textEmail.blur(function() {
        return _this.validate();
      });
      return this.areaList.hide('slide', {
        direction: 'left'
      }, 400, function() {
        return _this.areaUser.show('slide', {
          direction: 'right'
        }, 400);
      });
    },
    hideAdd: function() {
      var _this = this;

      return this.areaUser.hide('slide', {
        direction: 'right'
      }, 400, function() {
        return _this.areaList.show('slide', {
          direction: 'left'
        }, 400);
      });
    },
    validate: function() {
      this.validationSummary.empty();
      this.textUserName.removeClass('input-validation-error');
      this.textEmail.removeClass('input-validation-error');
      if (this.textUserName.val() === '') {
        this.validationSummary.append('<li>Имя - это обязательное поле</li>');
        this.textUserName.addClass('input-validation-error');
      }
      if (this.textEmail.val() !== '' && !/^[\w-]+(?:\.[\w-]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7}$/.test(this.textEmail.val())) {
        this.validationSummary.append('<li>Некорректный формат Email</li>');
        return this.textEmail.addClass('input-validation-error');
      }
    },
    addUser: function() {
      if (!this.validate()) {
        return false;
      }
    },
    selectInputByClick: function(eventObject) {
      var control;

      control = $('input', $(eventObject.target));
      switch (control.attr('type')) {
        case 'radio':
          return control.prop('checked', true);
        case 'checkbox':
          return control.prop('checked', !control.prop('checked'));
      }
    }
  };

  exports.FamilyController = Spine.Controller.create(ops);

}).call(this);
