import { appConfig } from "../config/app.config";
import RequestHelper from "../helpers/request.helper";

export default class ApiClassificationOfScientificWork {
  static getAllClassificationOfScientificWork() {
    return RequestHelper.get(
      appConfig.apiUrl + "classificationOfScientificWorks/all"
    );
  }

  static getClassificationOfScientificWorkList(params) {
    return RequestHelper.get(
      appConfig.apiUrl + "classificationOfScientificWorks",
      params
    );
  }

  static postClassificationOfScientificWork(classificationOfScientificWork) {
    return RequestHelper.post(
      appConfig.apiUrl + "classificationOfScientificWorks",
      classificationOfScientificWork
    );
  }

  static updateClassificationOfScientificWork(classificationOfScientificWork) {
    return RequestHelper.put(
      appConfig.apiUrl +
        `classificationOfScientificWorks/${classificationOfScientificWork.id}`,
      classificationOfScientificWork
    );
  }

  static deleteClassificationOfScientificWork(
    classificationOfScientificWorkId
  ) {
    return RequestHelper.delete(
      appConfig.apiUrl +
        `classificationOfScientificWorks/${classificationOfScientificWorkId}`
    );
  }
}
