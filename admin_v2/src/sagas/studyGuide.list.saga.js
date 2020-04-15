import { call, put, takeLatest } from "redux-saga/effects";
import {
  GET_STUDYGUIDE_LIST,
  getStudyGuideListSuccess,
  getStudyGuideListFailed,
} from "../actions/studyGuide.list.action";
import ApiStudyGuide from "../api/api.studyGuide";

function* getStudyGuideList(action) {
  try {
    const payload = yield call(
      ApiStudyGuide.getStudyGuides,
      action.payload.params
    );
    console.log(payload);
    yield put(getStudyGuideListSuccess(payload));
  } catch (error) {
    yield put(getStudyGuideListFailed());
  }
}

export function* watchStudyGuideListSagasAsync() {
  yield takeLatest(GET_STUDYGUIDE_LIST, getStudyGuideList);
}
