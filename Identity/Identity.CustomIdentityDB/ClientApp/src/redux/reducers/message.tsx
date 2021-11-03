import { messageActionTypes as actions } from "../actionTypes";

const initialState = {
  data: [],
  isLoading: true,
  hasErrored: false,
  errorMessage: "",
};

export function messages( state = initialState, action: actions.MessageAction) {
  switch (action.type) {
    case actions.GET_MESSAGES: {
      return {
        ...state,
        isLoading: true
      }
    }
    case actions.GET_MESSAGES_SUCCESS: {
      return {
        ...state,
        isLoading: false
      }
    }
    case actions.GET_MESSAGES_FAIL: {
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
