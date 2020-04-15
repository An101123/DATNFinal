import { call, put, takeLatest } from "redux-saga/effects";
import {
  GET_BOOKCATEGORY_LIST,
  getBookCategoryListSuccess,
  getBookCategoryListFailed
} from "../actions/bookCategory.list.action";
import ApiBookCategory from "../api/api.bookCategory";

function* getBookCategoryList(action) {
  try {
    const payload = yield call(ApiBookCategory.getBookCategoryList, action.payload.params);
    console.log(payload);
    yield put(getBookCategoryListSuccess(payload));
  } catch (error) {
    yield put(getBookCategoryListFailed());
  }
}

export function* watchBookCategoryListSagasAsync() {
  yield takeLatest(GET_BOOKCATEGORY_LIST, getBookCategoryList);
}
