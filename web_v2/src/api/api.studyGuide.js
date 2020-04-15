import { appConfig } from "../config/app.config";
import RequestHelper from "../helpers/request.helper";

export default class ApiStudyGuide {
  static getStudyGuides(params) {
    return RequestHelper.get(appConfig.apiUrl + "studyGuides", params);
  }

  static postStudyGuide(studyGuide) {
    return RequestHelper.post(appConfig.apiUrl + "studyGuides", studyGuide);
  }

  static updateStudyGuide(studyGuide) {
    return RequestHelper.put(
      appConfig.apiUrl + `studyGuides/${studyGuide.id}`,
      studyGuide
    );
  }

  static deleteStudyGuide(studyGuideId) {
    return RequestHelper.delete(
      appConfig.apiUrl + `studyGuides/${studyGuideId}`
    );
  }
}
