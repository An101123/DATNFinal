import { appConfig } from "../config/app.config";
import RequestHelper from "../helpers/request.helper";

export default class ApiOtherScientificWork {
  static getOtherScientificWorks(params) {
    return RequestHelper.get(appConfig.apiUrl + "otherScientificWorks", params);
  }

  static postOtherScientificWork(otherScientificWork) {
    return RequestHelper.post(
      appConfig.apiUrl + "otherScientificWorks",
      otherScientificWork
    );
  }

  static updateOtherScientificWork(otherScientificWork) {
    return RequestHelper.put(
      appConfig.apiUrl + `otherScientificWorks/${otherScientificWork.id}`,
      otherScientificWork
    );
  }

  static deleteOtherScientificWork(otherScientificWorkId) {
    return RequestHelper.delete(
      appConfig.apiUrl + `otherScientificWorks/${otherScientificWorkId}`
    );
  }
}
