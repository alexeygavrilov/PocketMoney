$.showErrorMessage = (message) ->
    box = $ '#error'
    box.text "Ошибка: #{message}"
    box.fadeIn 700
    setTimeout ( -> 
        box.fadeOut 700
        ), 10000

$.getResult = (result, success) ->
    if result.Success is false && result.Message
        $.showErrorMessage result.Message
    else
       success result 
