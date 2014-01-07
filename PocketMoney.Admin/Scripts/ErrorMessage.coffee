$.showErrorMessage = (message) ->
    box = $ '#error'
    box.text message
    box.show()

$.getResult = (result, success) ->
    if result.success is false && result.message
        $.showErrorMessage result.message
    else
       success result 
