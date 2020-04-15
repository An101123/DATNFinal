
import { appConfig } from "../config/app.config";
import RequestHelper from "../helpers/request.helper";

export default class ApiBookCategory {
  static getAllBookCategory() {
    return RequestHelper.get(appConfig.apiUrl + "bookCategorys/all");
  }

  static getBookCategoryList(params) {
    return RequestHelper.get(appConfig.apiUrl + "bookCategorys", params);
  }

  static postBookCategory(bookCategory) {
    return RequestHelper.post(appConfig.apiUrl + "bookCategorys", bookCategory);
  }

  static updateBookCategory(bookCategory) {
    return RequestHelper.put(appConfig.apiUrl + `bookCategorys/${bookCategory.id}`, bookCategory);
  }

  static deleteBookCategory(bookCategoryId) {
    return RequestHelper.delete(appConfig.apiUrl + `bookCategorys/${bookCategoryId}`);
  }
}
