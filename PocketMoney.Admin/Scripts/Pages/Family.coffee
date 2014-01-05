for row in $ '#tableUsers tbody tr' 
    do(row) ->
        userId = $ 'UserId', row
        $('#actionView', row).click =>
            alert userId