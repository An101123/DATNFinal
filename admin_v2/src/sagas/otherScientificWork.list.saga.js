import { call, put, takeLatest } from "redux-saga/effects";
import {
  GET_OTHERSCIENTIFICWORK_LIST,
  getOtherScientificWorkListSuccess,
  getOtherScientificWorkListFailed,
} from "../actions/otherScientificWork.list.action";
import ApiOtherScientificWork from "../api/api.otherScientificWork";

function* getOtherScientificWorkList(action) {
  try {
    const payload = yield call(
      ApiOtherScientificWork.getOtherScientificWorks,
      action.payload.params
    );
    console.log(payload);
    yield put(getOtherScientificWorkListSuccess(payload));
  } catch (error) {
    yield put(getOtherScientificWorkListFailed());
  }
}

export function* watchOtherScientificWorkListSagasAsync() {
  yield takeLatest(GET_OTHERSCIENTIFICWORK_LIST, getOtherScientificWorkList);
}
