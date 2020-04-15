import { all, fork } from "redux-saga/effects";
import { watchUserListSagaAsync } from "./user.list.saga";
import { watchProfileSagasAsync } from "./profile.saga";
import { watchLevelListSagasAsync } from "./level.list.saga";
import { watchScientificReportTypeListSagasAsync } from "./scientificReportType.list.saga";
import { watchLecturerListSagasAsync } from "./lecturer.list.saga";
import { watchScientificWorkListSagasAsync } from "./scientificWork.list.saga";
import { watchScientificReportListSagasAsync } from "./scientificReport.list.saga";
import { watchPublishBookListSagasAsync } from "./publishBook.list.saga";
import { watchBookCategoryListSagasAsync } from "./bookCategory.list.saga";
import { watchLevelStudyGuideListSagasAsync } from "./levelStudyGuide.list.saga";
import { watchStudyGuideListSagasAsync } from "./studyGuide.list.saga";
import { watchNewsListSagasAsync } from "./news.list.saga";

export default function* sagas() {
  yield all([
    fork(watchUserListSagaAsync),
    fork(watchLevelListSagasAsync),
    fork(watchScientificReportTypeListSagasAsync),
    fork(watchLecturerListSagasAsync),
    fork(watchScientificWorkListSagasAsync),
    fork(watchScientificReportListSagasAsync),
    fork(watchPublishBookListSagasAsync),
    fork(watchBookCategoryListSagasAsync),
    fork(watchLevelStudyGuideListSagasAsync),
    fork(watchStudyGuideListSagasAsync),
    fork(watchNewsListSagasAsync),
    fork(watchProfileSagasAsync),
  ]);
}
