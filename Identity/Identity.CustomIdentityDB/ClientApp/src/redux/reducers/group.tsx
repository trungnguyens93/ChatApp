import { groupActionTypes as actions } from "../actionTypes"

const initialState = {
    data: [],
    isLoading: true,
    hasErrored: false,
    errorMessage: "",
};

export function groups(state = initialState, action: actions.GroupAction) {
    switch (action.type) {
        case actions.GET_GROUPS: {
            return {
                ...state,
                isLoading: true
            }
        }

        case actions.GET_GROUPS_SUCCESS: {
            return {
                ...state,
                isLoading: false
            }
        }

        case actions.GET_GROUPS_FAIL: {
            return {
                ...state,
                isLoading: false,
                error: action.error
            }
        }

        default:
            return state
    }
}