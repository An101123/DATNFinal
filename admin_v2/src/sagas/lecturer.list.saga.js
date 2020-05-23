import { call, put, takeLatest } from "redux-saga/effects";
import {
  GET_LECTURER_LIST,
  getLecturerListSuccess,
  getLecturerListFailed,
} from "../actions/lecturer.list.action";
import ApiLecturer from "../api/api.lecturer";

function* getLecturerList(action) {
  try {
    const { payload = {} } = action;
    const res = yield call(ApiLecturer.getLecturers, payload);
    yield put(getLecturerListSuccess(res));
  } catch (error) {
    yield put(getLecturerListFailed());
  }
}

export function* watchLecturerListSagasAsync() {
  yield takeLatest(GET_LECTURER_LIST, getLecturerList);
}
