import { memberActionTypes as actions } from "../actionTypes";

const initialState = {
  data: [],
  isLoading: true,
  hasErrored: false,
  errorMessage: "",
};

export function member(state = initialState, action: actions.MemberAction) {
  switch (action.type) {
    case actions.GET_MEMBER: {
      return {
        ...state,
        isLoading: true
      }
    }
    case actions.GET_MEMBER_SUCCESS: {
      return {
        ...state,
        isLoading: false,
      }
    }
    case actions.GET_MEMBER_FAIL: {
      return {
        ...state,
        isLoading: false,
        error: action.error
      }
    }
    default:
      return state;
  }
}
