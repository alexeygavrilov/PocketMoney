exports = @
$ = jQuery
ops =
    elements: 
        '#areaList': 'areaList'
        '#areaUser': 'areaUser'
        '#tableUsers > tbody': 'table'
        '#userRowTemplate': 'rowTemplate'
        'input:radio[name=RoleType]': 'roleType'
        '#Email': 'textEmail'
        '#UserName': 'textUserName'
        '#Photo': 'imgPhoto'
        '#UploadPhoto': 'uploadPhoto'
        '#SendNotification': 'checkNotification'
        'div.validation-summary-errors > ul': 'validationSummary'

    events:
        'click #actionAdd': 'showAdd'
        'click #actionCancelAdd': 'hideAdd'
        'click #ChildRole': 'showAddChild'
        'click .selector-by-click': 'selectInputByClick'
        'click #actionCreateUser': 'addUser'

    init: ->
        @loadData()

    loadData: ->
        $.get @settings.GetUsersUrl, (result) =>
            $.getResult result, =>
                @table.empty()
                @table.append @rowTemplate.tmpl(row) for row in result.List

    clearAddForm: ->
        @textUserName.val ''
        @textUserName.removeClass 'input-validation-error'
        @textEmail.val ''
        @textEmail.removeClass 'input-validation-error'
        @imgPhoto.attr 'src', '#'
        @uploadPhoto.empty()
        @validationSummary.empty()
        @roleType.filter('[value=1]').prop 'checked', true
        @checkNotification.prop 'checked', true

    showAdd: ->
        @clearAddForm()
        @textUserName.blur =>
            @validate()

        @textEmail.blur =>
            @validate()

        @areaList.hide 'slide', { direction: 'left' }, 400, =>
            @areaUser.show 'slide', { direction: 'right' }, 400

    hideAdd: ->
        @areaUser.hide 'slide', { direction: 'right' }, 400, =>
            @areaList.show 'slide', { direction: 'left' }, 400

    validate: ->
        @validationSummary.empty()
        @textUserName.removeClass 'input-validation-error'
        @textEmail.removeClass 'input-validation-error'
        if @textUserName.val() is ''
            @validationSummary.append '<li>Имя - это обязательное поле</li>'
            @textUserName.addClass 'input-validation-error'

        if @textEmail.val() isnt '' and not /^[\w-]+(?:\.[\w-]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7}$/.test @textEmail.val()
            @validationSummary.append '<li>Некорректный формат Email</li>'
            @textEmail.addClass 'input-validation-error'


    addUser: ->
        false if !@validate()


    selectInputByClick: (eventObject)->
        control = $('input', $(eventObject.target))
        switch control.attr('type') 
            when 'radio' then control.prop 'checked', true
            when 'checkbox' then control.prop 'checked', !control.prop('checked')

exports.FamilyController = Spine.Controller.create(ops);
