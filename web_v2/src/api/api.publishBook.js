import { appConfig } from "../config/app.config";
import RequestHelper from "../helpers/request.helper";

export default class ApiPublishBook {
  static getPublishBooks(params) {
    return RequestHelper.get(appConfig.apiUrl + "publishBooks", params);
  }

  static postPublishBook(publishBook) {
    return RequestHelper.post(appConfig.apiUrl + "publishBooks", publishBook);
  }

  static updatePublishBook(publishBook) {
    return RequestHelper.put(
      appConfig.apiUrl + `publishBooks/${publishBook.id}`,
      publishBook
    );
  }

  static deletePublishBook(publishBookId) {
    return RequestHelper.delete(
      appConfig.apiUrl + `publishBooks/${publishBookId}`
    );
  }
}
