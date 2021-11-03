export const GET_MEMBER = "member/GET_MEMBER";
export interface GetMemberAction {
  type: typeof GET_MEMBER,
  memberId: string
}

export const GET_MEMBER_SUCCESS = "member/GET_MEMBER_SUCCESS";
export interface GetMemberSuccessAction {
  type: typeof GET_MEMBER_SUCCESS,
  username: string
}

export const GET_MEMBER_FAIL = "member/GET_MEMBER_FAIL";
export interface GetMemberFailAction {
  type: typeof GET_MEMBER_FAIL;
  error: any;
}

export type MemberAction = 
  | GetMemberAction
  | GetMemberSuccessAction
  | GetMemberFailAction;