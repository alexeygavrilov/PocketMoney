exports = @
$ = jQuery
ops =
    elements: 
        '#tableUsers > tbody': 'table'
        '#userRowTemplate': 'rowTemplate'
    
    init: ->
        @loadData()

    loadData: ->
        $.get @urls.loadUsers, (result) =>
            $.getResult result, =>
                @table.empty()
                @table.append @rowTemplate.tmpl(row) for row in result.List

exports.FamilyController = Spine.Controller.create(ops);
