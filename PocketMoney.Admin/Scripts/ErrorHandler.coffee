polljQueryExistence = ->
    delay 1000, ->
        if typeof(jQuery) isnt 'undefined'
            (jQuery document).ajaxError (e, jqXHR, settings) ->
                errorMessage = null
                jsonParseError = false
                
                return if jqXHR.statusText is 'abort'
                try
                    errorMessage = (JSON.parse jqXHR.responseText).errorMessage
                catch error
                    jsonParseError = true
                    errorMessage = error.message

                $.showErrorMessage "The server has returned <strong>\"#{errorMessage}\"</strong>,  which was unexpected."
        else
            polljQueryExistence()

polljQueryExistence()