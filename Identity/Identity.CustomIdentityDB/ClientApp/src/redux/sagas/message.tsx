import { all, fork, call, put, takeLatest } from "redux-saga/effects";
import { messageActions as actionCreators } from "../actionCreators";
import { messageActionTypes as actionTypes } from "../actionTypes";
import { getMessagesService } from "../../services";

function* getMessages({ groupId } : { type : string; groupId : string }) {
    const { data } = yield call(getMessagesService, groupId);
    if (data) {
        yield put(actionCreators.getMessagesSuccess(data?.messages));
    }
}

function* watchMessage() {
    yield takeLatest(actionTypes.GET_MESSAGES, getMessages);
}

export default function* messageSaga() {
    yield all([fork(watchMessage)]);
}