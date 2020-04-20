import { call, put, takeLatest } from "redux-saga/effects";
import {
  GET_CLASSIFICATIONOFSCIENTIFICWORK_LIST,
  getClassificationOfScientificWorkListSuccess,
  getClassificationOfScientificWorkListFailed,
} from "../actions/classificationOfScientificWork.list.action";
import ApiClassificationOfScientificWork from "../api/api.classificationOfScientificWork";

function* getClassificationOfScientificWorkList(action) {
  try {
    const payload = yield call(
      ApiClassificationOfScientificWork.getClassificationOfScientificWorkList,
      action.payload.params
    );
    console.log(payload);
    yield put(getClassificationOfScientificWorkListSuccess(payload));
  } catch (error) {
    yield put(getClassificationOfScientificWorkListFailed());
  }
}

export function* watchClassificationOfScientificWorkListSagasAsync() {
  yield takeLatest(
    GET_CLASSIFICATIONOFSCIENTIFICWORK_LIST,
    getClassificationOfScientificWorkList
  );
}
