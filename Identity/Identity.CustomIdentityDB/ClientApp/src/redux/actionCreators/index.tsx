import { 
    getMemberDetail,
    getMemberSuccess,
    getMemberFail
} from "./member";

import {
    getMessages,
    getMessagesSuccess,
    getMessagesFail
} from "./message";

import {
    getGroups,
    getGroupsSuccess,
    getGroupsFail
} from "./group";

export const memberActions = {
    getMemberDetail,
    getMemberSuccess,
    getMemberFail
}

export const messageActions = {
    getMessages,
    getMessagesSuccess,
    getMessagesFail
}

export const groupActions = {
    getGroups,
    getGroupsSuccess,
    getGroupsFail
}