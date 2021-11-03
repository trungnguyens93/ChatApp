import { messageActionTypes as actionTypes } from "../actionTypes";

function getMessages(groupId: string) : actionTypes.GetMessagesAction {
    return {
        type: actionTypes.GET_MESSAGES,
        groupId
    }
}

function getMessagesSuccess(
    messages: Array<{
        messageId: string,
        content: string,
        sender: string
    }>) : actionTypes.GetMessagesSuccessAction {
    return {
        type: actionTypes.GET_MESSAGES_SUCCESS,
        messages
    }
}

function getMessagesFail(error: string) : actionTypes.GetMessagesFailAction {
    return {
        type: actionTypes.GET_MESSAGES_FAIL,
        error
    }
}

export {
    getMessages,
    getMessagesSuccess,
    getMessagesFail
}