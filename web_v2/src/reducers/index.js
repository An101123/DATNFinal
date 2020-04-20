import { combineReducers } from "redux";
import { userListReducer } from "./user.list.reducer";
import { profileReducer } from "./profile.reducer";
import { levelListReducer } from "./level.list.reducer";
import { scientificReportTypeListReducer } from "./scientificReportType.list.reducer";
import { lecturerListReducer } from "./lecturer.list.reducer";
import { scientificWorkListReducer } from "./scientificWork.list.reducer";
import { scientificReportListReducer } from "./scientificReport.list.reducer";
import { publishBookListReducer } from "./publishBook.list.reducer";
import { bookCategoryListReducer } from "./bookCategory.list.reducer";
import { levelStudyGuideListReducer } from "./levelStudyGuide.list.reducer";
import { studyGuideListReducer } from "./studyGuide.list.reducer";
import { classificationOfScientificWorkListReducer } from "./classificationOfScientificWork.list.reducer";
import { otherScientificWorkListReducer } from "./otherScientificWork.list.reducer";
import { newsListReducer } from "./news.list.reducer";

export default combineReducers({
  userPagedListReducer: userListReducer,
  profileReducer: profileReducer,
  levelPagedListReducer: levelListReducer,
  scientificReportTypePagedListReducer: scientificReportTypeListReducer,
  lecturerPagedListReducer: lecturerListReducer,
  scientificWorkPagedListReducer: scientificWorkListReducer,
  scientificReportPagedListReducer: scientificReportListReducer,
  publishBookPagedListReducer: publishBookListReducer,
  bookCategoryPagedListReducer: bookCategoryListReducer,
  levelStudyGuidePagedListReducer: levelStudyGuideListReducer,
  studyGuidePagedListReducer: studyGuideListReducer,
  classificationOfScientificWorkPagedListReducer: classificationOfScientificWorkListReducer,
  otherScientificWorkPagedListReducer: otherScientificWorkListReducer,
  newsPagedListReducer: newsListReducer,
});
