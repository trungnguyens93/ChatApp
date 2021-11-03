import { all, fork, call, put, takeLatest } from "redux-saga/effects";
import { groupActions as actionCreators  } from "../actionCreators";
import { groupActionTypes as actionTypes } from "../actionTypes";
import { getGroupsService } from "../../services";

function* getGroups({ memberId } : { type : string; memberId : string }) {
    const { data } = yield call(getGroupsService, memberId);
    if (data) {
        yield put(actionCreators.getGroupsSuccess(data?.groups));
    }
}

function* watchGroups() {
    yield takeLatest(actionTypes.GET_GROUPS, getGroups);
}

export default function* groupSaga() {
    yield all([fork(watchGroups)]);
}