import { call, put, takeLatest } from "redux-saga/effects";
import {
  GET_PUBLISHBOOK_LIST,
  getPublishBookListSuccess,
  getPublishBookListFailed
} from "../actions/publishBook.list.action";
import ApiPublishBook from "../api/api.publishBook";

function* getPublishBookList(action) {
  try {
    const payload = yield call(ApiPublishBook.getPublishBooks, action.payload.params);
    console.log(payload);
    yield put(getPublishBookListSuccess(payload));
  } catch (error) {
    yield put(getPublishBookListFailed());
  }
}

export function* watchPublishBookListSagasAsync() {
  yield takeLatest(GET_PUBLISHBOOK_LIST, getPublishBookList);
}
