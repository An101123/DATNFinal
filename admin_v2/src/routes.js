import React from "react";
import DefaultLayout from "./pages/admin/Admin";

const Dashboard = React.lazy(() => import("./pages/admin/Dashboard/Dashboard"));

const LevelListPage = React.lazy(() =>
  import("./pages/admin/level/level.list.page")
);
const ScientificWorkListPage = React.lazy(() =>
  import("./pages/admin/scientificWork/scientificWork.list.page")
);

const ScientificReportListPage = React.lazy(() =>
  import("./pages/admin/scientificReport/scientificReport.list.page")
);
const ScientificReportTypeListPage = React.lazy(() =>
  import("./pages/admin/scientificReportType/scientificReportType.list.page")
);

const BookCategoryListPage = React.lazy(() =>
  import("./pages/admin/bookCategory/bookCategory.list.page")
);
const PublishBookListPage = React.lazy(() =>
  import("./pages/admin/publishBook/publishBook.list.page")
);

const LevelStudyGuideListPage = React.lazy(() =>
  import("./pages/admin/levelStudyGuide/levelStudyGuide.list.page")
);
const StudyGuideListPage = React.lazy(() =>
  import("./pages/admin/studyGuide/studyGuide.list.page")
);

const LecturerListPage = React.lazy(() =>
  import("./pages/admin/lecturer/lecturer.list.page")
);
const NewsListPage = React.lazy(() =>
  import("./pages/admin/news/news.list.page")
);

const UserListPage = React.lazy(() =>
  import("./pages/admin/user/user.list.page")
);
const routes = [
  {
    path: "/",
    exact: true,
    name: "Admin",
    component: DefaultLayout,
  },
  { path: "/dashboard", name: "Dashboard", component: Dashboard },
  { path: "/levels", name: "Cấp", component: LevelListPage },
  {
    path: "/scientificWorks",
    name: "Đề tài NCKH",
    component: ScientificWorkListPage,
  },
  {
    path: "/scientificReports",
    name: "Bài báo - Báo cáo",
    component: ScientificReportListPage,
  },
  {
    path: "/scientificReportTypes",
    name: "Loại Bài báo - Báo cáo",
    component: ScientificReportTypeListPage,
  },

  {
    path: "/publishBooks",
    name: "Xuất bản sách",
    component: PublishBookListPage,
  },
  {
    path: "/bookCategorys",
    name: "Thể loại sách",
    component: BookCategoryListPage,
  },
  {
    path: "/levelStudyGuides",
    name: "Phân cấp hướng dẫn",
    component: LevelStudyGuideListPage,
  },
  {
    path: "/studyGuides",
    name: "Hướng dẫn sinh viên ",
    component: StudyGuideListPage,
  },
  { path: "/lecturers", name: "Giảng viên", component: LecturerListPage },
  {
    path: "/news",
    name: "Tin tức",
    component: NewsListPage,
  },
  {
    path: "/users",
    name: "Tài khoản",
    component: UserListPage,
  },
];

export default routes;
