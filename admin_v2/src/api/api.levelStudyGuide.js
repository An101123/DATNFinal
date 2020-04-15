import { appConfig } from "../config/app.config";
import RequestHelper from "../helpers/request.helper";

export default class ApiLevelStudyGuide {
  static getAllLevelStudyGuide() {
    return RequestHelper.get(appConfig.apiUrl + "levelStudyGuides/all");
  }

  static getLevelStudyGuideList(params) {
    return RequestHelper.get(appConfig.apiUrl + "levelStudyGuides", params);
  }

  static postLevelStudyGuide(levelStudyGuide) {
    return RequestHelper.post(
      appConfig.apiUrl + "levelStudyGuides",
      levelStudyGuide
    );
  }

  static updateLevelStudyGuide(levelStudyGuide) {
    return RequestHelper.put(
      appConfig.apiUrl + `levelStudyGuides/${levelStudyGuide.id}`,
      levelStudyGuide
    );
  }

  static deleteLevelStudyGuide(levelStudyGuideId) {
    return RequestHelper.delete(
      appConfig.apiUrl + `levelStudyGuides/${levelStudyGuideId}`
    );
  }
}
