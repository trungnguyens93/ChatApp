export const GET_GROUPS = "groups/GET_GROUPS";
export interface GetGroupsAction {
    type: typeof GET_GROUPS,
    memberId: string
}

export const GET_GROUPS_SUCCESS = "groups/GET_GROUPS_SUCCESS";
export interface GetGroupsSuccessAction {
    type: typeof GET_GROUPS_SUCCESS,
    groups: Array<{
        groupId: string,
        name: string
    }>
}

export const GET_GROUPS_FAIL = "groups/GET_GROUPS_FAIL";
export interface GetGroupsFailAction {
    type: typeof GET_GROUPS_FAIL,
    error: string
}

export type GroupAction =
    | GetGroupsAction
    | GetGroupsSuccessAction
    | GetGroupsFailAction;