exports = @
$ = jQuery
ops =
    elements: 
        '#areaTable': 'areaTable'
        '#areaAdd': 'areaAdd'
        '#tableUsers > tbody': 'table'
        '#userRowTemplate': 'rowTemplate'
        'li.ChildVisible': 'areaAddChild'
        'input:radio[name=RoleType]': 'roleType'

    events:
        'click #actionAdd': 'showAdd'
        'click #actionCancelAdd': 'hideAdd'
        'click #ChildRole': 'showAddChild'
        'click #ParentRole': 'hideAddChild'

    init: ->
        @loadData()

    loadData: ->
        $.get @urls.loadUsers, (result) =>
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
        

exports.FamilyController = Spine.Controller.create(ops);
