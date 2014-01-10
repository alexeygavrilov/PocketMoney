exports = @
$ = jQuery
ops =
    elements: 
        '#areaTable': 'areaTable'
        '#areaAdd': 'areaAdd'
        '#tableUsers > tbody': 'table'
        '#tableVK': 'tableVK'
        '#userRowTemplate': 'rowTemplate'
        '#vkRowTemplate': 'vkTemplate'
        'li.ChildVisible': 'areaAddChild'
        'input:radio[name=RoleType]': 'roleType'
        '#VKQueryString': 'textSearchVK'

    events:
        'click #actionAdd': 'showAdd'
        'click #actionCancelAdd': 'hideAdd'
        'click #ChildRole': 'showAddChild'
        'click #ParentRole': 'hideAddChild'
        'click #actionSearchInVK': 'searchVK'

    init: ->
        @loadData()

    loadData: ->
        $.get @settings.LoadUsersUrl, (result) =>
            $.getResult result, =>
                @table.empty()
                @table.append @rowTemplate.tmpl(row) for row in result.List

    showAdd: ->
        @areaTable.hide 'slide', { direction: 'left' }, 400, =>
            @areaAdd.show 'slide', { direction: 'right' }, 400

    hideAdd: ->
        @areaAdd.hide 'slide', { direction: 'right' }, 400, =>
            @areaTable.show 'slide', { direction: 'left' }, 400

    showAddChild: ->
        @roleType.filter('[value=0x1]').prop 'checked', true
        @roleType.filter('[value=0x2]').prop 'checked', false
        @areaAddChild.show()
        VK.init
            apiId: @settings.VKApiKey

    hideAddChild: ->
        @roleType.filter('[value=0x1]').prop 'checked', false
        @roleType.filter('[value=0x2]').prop 'checked', true
        @areaAddChild.hide()

    searchVK: ->
        q = @textSearchVK.val()
        return if q is ''

        if $.isNumeric q
            VK.Api.call 'users.get',
                user_ids: @searchVK.val()
                fields: 'photo'
                , (result) =>
                    @tableVK.empty()
                    @tableVK.append @vkTemplate.tmpl(row) for row in result
        else
            VK.Api.call 'users.search',
                q: @searchVK.val()
                sort: 0
                count: 10
                fields: 'photo,bdate'
                , (result) =>
                    @tableVK.empty()
                    @tableVK.append @vkTemplate.tmpl(row) for row in result.items

exports.FamilyController = Spine.Controller.create(ops);
