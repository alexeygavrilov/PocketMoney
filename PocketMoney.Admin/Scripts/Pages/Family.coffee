﻿exports = @
$ = jQuery
ops =
    elements: 
        '#areaTable': 'areaTable'
        '#areaAdd': 'areaAdd'
        '#tableUsers > tbody': 'table'
        '#userRowTemplate': 'rowTemplate'
        '#childNotificationTemplate': 'childNotificationTemplate'
        'li.ChildVisible': 'areaAddChild'
        'li.ParentVisible': 'areaAddParent'
        'input:radio[name=RoleType]': 'roleType'
        '#VKQueryString': 'textSearchVK'
        '#actionSendInvite': 'btnSendInvite'
        '#Email': 'textEmail'
        '#UserName': 'textUserName'
        '#Photo': 'imgPhoto'
        '#NotificationText': 'textNotification'
        '#hiddenUserIdentifier': 'hiddenUserIdentifier'

    events:
        'click #actionAdd': 'showAdd'
        'click #actionCancelAdd': 'hideAdd'
        'click #ChildRole': 'showAddChild'
        'click #ParentRole': 'hideAddChild'
        'click #actionSearchInVK': 'searchVK'

    init: ->
        @loadData()

    loadData: ->
        $.get @settings.GetUsersUrl, (result) =>
            $.getResult result, =>
                @table.empty()
                @table.append @rowTemplate.tmpl(row) for row in result.List

    clearDataSearch: ->
        @textUserName.val ''
        @imgPhoto.attr 'src', '#'
        @textNotification.text ''
        @hiddenUserIdentifier.val ''
        @btnSendInvite.hide()
    
    clearDataAdd: ->
        @textSearchVK.val ''
        @textEmail.val ''
        @clearDataSearch()

    showAdd: ->
        @clearDataAdd()
        @showAddChild()
        @areaTable.hide 'slide', { direction: 'left' }, 400, =>
            @areaAdd.show 'slide', { direction: 'right' }, 400

    hideAdd: ->
        @areaAdd.hide 'slide', { direction: 'right' }, 400, =>
            @areaTable.show 'slide', { direction: 'left' }, 400

    showAddChild: ->
        @roleType.filter('[value=0x1]').prop 'checked', true
        @roleType.filter('[value=0x2]').prop 'checked', false
        @areaAddChild.show()
        @areaAddParent.hide()

    hideAddChild: ->
        @roleType.filter('[value=0x1]').prop 'checked', false
        @roleType.filter('[value=0x2]').prop 'checked', true
        @areaAddChild.hide()
        @areaAddParent.show()
        
    searchVK: ->
        q = @textSearchVK.val()
        return if q is ''

        index = q.indexOf("vk.com/id")
        q = q.substring index + 9 if index > -1

        if $.isNumeric q 
            $.get "#{@settings.GetUserVKUrl}?id=#{q}", (result) =>
                $.getResult result, =>
                    @textUserName.val result.Data.FirstName + ' ' + result.Data.LastName
                    @imgPhoto.attr 'src', result.Data.Photo
                    text = @childNotificationTemplate.tmpl
                        UserName: result.Data.FirstName
                        ParentName: @settings.CurrentUser 
                    @textNotification.text $(text).text()
                    @hiddenUserIdentifier.val result.Data.UserId
                    @btnSendInvite.show()
        else 
            @clearDataSearch()
            $.showErrorMessage "Неправильный формат для идентификации пользователя ВКонтакте. Введите ссылку на пользователя, например: http://vk.com/id236979537"

    selectVKUser: ->
        false
exports.FamilyController = Spine.Controller.create(ops);
