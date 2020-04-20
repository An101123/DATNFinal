import { all, fork } from "redux-saga/effects";
import { watchUserListSagaAsync } from "./user.list.saga";
import { watchProfileSagasAsync } from "./profile.saga";
import { watchLevelListSagasAsync } from "./level.list.saga";
import { watchScientificWorkListSagasAsync } from "./scientificWork.list.saga";
import { watchScientificReportTypeListSagasAsync } from "./scientificReportType.list.saga";
import { watchScientificReportListSagasAsync } from "./scientificReport.list.saga";
import { watchBookCategoryListSagasAsync } from "./bookCategory.list.saga";
import { watchPublishBookListSagasAsync } from "./publishBook.list.saga";
import { watchLevelStudyGuideListSagasAsync } from "./levelStudyGuide.list.saga";
import { watchStudyGuideListSagasAsync } from "./studyGuide.list.saga";
import { watchOtherScientificWorkListSagasAsync } from "./otherScientificWork.list.saga";
import { watchClassificationOfScientificWorkListSagasAsync } from "./classificationOfScientificWork.list.saga";
import { watchLecturerListSagasAsync } from "./lecturer.list.saga";
import { watchNewsListSagasAsync } from "./news.list.saga";
export default function* sagas() {
  yield all([
    fork(watchUserListSagaAsync),
    fork(watchLevelListSagasAsync),
    fork(watchScientificWorkListSagasAsync),
    fork(watchScientificReportTypeListSagasAsync),
    fork(watchScientificReportListSagasAsync),
    fork(watchBookCategoryListSagasAsync),
    fork(watchPublishBookListSagasAsync),
    fork(watchLevelStudyGuideListSagasAsync),
    fork(watchStudyGuideListSagasAsync),
    fork(watchOtherScientificWorkListSagasAsync),
    fork(watchClassificationOfScientificWorkListSagasAsync),
    fork(watchLecturerListSagasAsync),
    fork(watchNewsListSagasAsync),
    fork(watchProfileSagasAsync),
  ]);
}
