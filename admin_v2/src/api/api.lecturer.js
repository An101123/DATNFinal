import { appConfig } from "../config/app.config";
import RequestHelper from "../helpers/request.helper";

export default class ApiLecturer {
  static getAllLecturer() {
    return RequestHelper.get(appConfig.apiUrl + "lecturers/all");
  }

  static getLecturers(query) {
    console.log("getLecturers", { query });
    return RequestHelper.get(appConfig.apiUrl + "lecturers", query);
  }

  static postLecturer(lecturer) {
    return RequestHelper.post(appConfig.apiUrl + "lecturers", lecturer);
  }

  static updateLecturer(lecturer) {
    return RequestHelper.put(
      appConfig.apiUrl + `lecturers/${lecturer.id}`,
      lecturer
    );
  }

  static deleteLecturer(lecturerId) {
    return RequestHelper.delete(appConfig.apiUrl + `lecturers/${lecturerId}`);
  }
  static GetAllScientificWorkByLecturerId(lecturerId) {
    return RequestHelper.get(
      appConfig.apiUrl + `lecturers/${lecturerId}/scientificWorks`
    );
  }

  static GetAllScientificReportByLecturerId(lecturerId) {
    return RequestHelper.get(
      appConfig.apiUrl + `lecturers/${lecturerId}/scientificReports`
    );
  }
  static GetAllPublishBookByLecturerId(lecturerId) {
    return RequestHelper.get(
      appConfig.apiUrl + `lecturers/${lecturerId}/publishBooks`
    );
  }
  static GetAllStudyGuideByLecturerId(lecturerId) {
    return RequestHelper.get(
      appConfig.apiUrl + `lecturers/${lecturerId}/studyGuides`
    );
  }
  static GetAllOtherScientificWorkByLecturerId(lecturerId) {
    return RequestHelper.get(
      appConfig.apiUrl + `lecturers/${lecturerId}/otherScientificWorks`
    );
  }
}
