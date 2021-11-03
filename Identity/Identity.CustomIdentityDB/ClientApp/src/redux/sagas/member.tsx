import { all, fork, call, put, takeLatest } from "redux-saga/effects";
import { memberActions as actionCreators } from "../actionCreators";
import { memberActionTypes as actionTypes } from "../actionTypes";
import { getMemberDetailService } from "../../services";


function* getMemberDetail({ memberId } : { type: string; memberId: string }) {
    const { data } = yield call(getMemberDetailService, memberId);
    if (data) {
        yield put(actionCreators.getMemberSuccess(data?.username));
    }
}

function* watchMember() {
    yield takeLatest(actionTypes.GET_MEMBER, getMemberDetail);
}

export default function* memberSaga() {
    yield all([fork(watchMember)]);
}