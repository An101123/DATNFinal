import { call, put, takeLatest } from "redux-saga/effects";
import {
  GET_LEVELSTUDYGUIDE_LIST,
  getLevelStudyGuideListSuccess,
  getLevelStudyGuideListFailed,
} from "../actions/levelStudyGuide.list.action";
import ApiLevelStudyGuide from "../api/api.levelStudyGuide";

function* getLevelStudyGuideList(action) {
  try {
    const payload = yield call(
      ApiLevelStudyGuide.getLevelStudyGuideList,
      action.payload.params
    );
    console.log(payload);
    yield put(getLevelStudyGuideListSuccess(payload));
  } catch (error) {
    yield put(getLevelStudyGuideListFailed());
  }
}

export function* watchLevelStudyGuideListSagasAsync() {
  yield takeLatest(GET_LEVELSTUDYGUIDE_LIST, getLevelStudyGuideList);
}
