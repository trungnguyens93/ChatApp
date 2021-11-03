import { groupActionTypes as actionTypes } from "../actionTypes";

function getGroups(memberId: string) : actionTypes.GetGroupsAction {
    return {
        type: actionTypes.GET_GROUPS,
        memberId
    }
}

function getGroupsSuccess(
    groups: Array<{
        groupId: string,
        name: string
    }>) : actionTypes.GetGroupsSuccessAction {
    return {
        type: actionTypes.GET_GROUPS_SUCCESS,
        groups
    }
}

function getGroupsFail(error: string) : actionTypes.GetGroupsFailAction {
    return {
        type: actionTypes.GET_GROUPS_FAIL,
        error
    }
}

export {
    getGroups,
    getGroupsSuccess,
    getGroupsFail
}