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
  static getLecturerById(lecturerId, startTime, endTime) {
    return RequestHelper.get(
      appConfig.apiUrl +
        `lecturers/${lecturerId}?startTime=${startTime}&endTime=${endTime}`
    );
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
  static GetAllScientificWorkByLecturerId(lecturerId, startTime, endTime) {
    return RequestHelper.get(
      appConfig.apiUrl +
        `lecturers/${lecturerId}/scientificWorks?startTime=${startTime}&endTime=${endTime}`
    );
  }

  static GetAllScientificReportByLecturerId(lecturerId, startTime, endTime) {
    return RequestHelper.get(
      appConfig.apiUrl +
        `lecturers/${lecturerId}/scientificReports?startTime=${startTime}&endTime=${endTime}`
    );
  }
  static GetAllPublishBookByLecturerId(lecturerId, startTime, endTime) {
    return RequestHelper.get(
      appConfig.apiUrl +
        `lecturers/${lecturerId}/publishBooks?startTime=${startTime}&endTime=${endTime}`
    );
  }
  static GetAllStudyGuideByLecturerId(lecturerId) {
    return RequestHelper.get(
      appConfig.apiUrl + `lecturers/${lecturerId}/studyGuides`
    );
  }
  static GetAllOtherScientificWorkByLecturerId(lecturerId, startTime, endTime) {
    return RequestHelper.get(
      appConfig.apiUrl +
        `lecturers/${lecturerId}/otherScientificWorks?startTime=${startTime}&endTime=${endTime}`
    );
  }
}
