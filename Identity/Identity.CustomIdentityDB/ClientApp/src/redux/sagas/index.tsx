import { all, fork } from "redux-saga/effects";
import MemberSaga from "./member";
import MessageSaga from "./message";
import GroupSaga from "./group";

export default function* rootSaga() {
    yield all([
        fork(MemberSaga),
        fork(MessageSaga),
        fork(GroupSaga)
    ]);
}