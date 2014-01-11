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

    hideAddChild: ->
        @roleType.filter('[value=0x1]').prop 'checked', false
        @roleType.filter('[value=0x2]').prop 'checked', true
        @areaAddChild.hide()

    searchVK: ->
        q = @textSearchVK.val()
        return if q is ''

        VK.init
            apiId: @settings.VKApiID

        $.get "https://oauth.vk.com/access_token?client_id=#{@settings.VKApiID}&client_secret=#{@settings.VKApiKey}&v=5.5&grant_type=client_credentials", (r) =>
#            access_token = r.access_token
            if $.isNumeric q
                VK.Api.call 'users.get',
                    user_ids: q
                    fields: 'photo'
                    , (result) =>
                        @tableVK.empty()
                        @tableVK.append @vkTemplate.tmpl(row) for row in result.response
            else
                VK.Api.call 'users.search',
                    q: q
                    sort: 0
                    count: 10
                    fields: 'photo'
                    , (result) =>
                        @tableVK.empty()
                        @tableVK.append @vkTemplate.tmpl(row) for row in result.items

exports.FamilyController = Spine.Controller.create(ops);
