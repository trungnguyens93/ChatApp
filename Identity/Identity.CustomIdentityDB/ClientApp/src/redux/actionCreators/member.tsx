import { memberActionTypes as actionTypes } from "../actionTypes";

function getMemberDetail(memberId: string) : actionTypes.GetMemberAction {
    return {
        type: actionTypes.GET_MEMBER,
        memberId
    }
}

function getMemberSuccess(username: string): actionTypes.GetMemberSuccessAction {
    return {
        type: actionTypes.GET_MEMBER_SUCCESS,
        username
    }
}

function getMemberFail(error: string) : actionTypes.GetMemberFailAction {
    return {
        type: actionTypes.GET_MEMBER_FAIL,
        error
    }
}

export {
    getMemberDetail,
    getMemberSuccess,
    getMemberFail
}