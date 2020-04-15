import React from "react";

const Dashboard = React.lazy(() => import("./pages/admin/Dashboard/Dashboard"));

const LevelListPage = React.lazy(() =>
  import("./pages/admin/level/level.list.page")
);

const LecturerListPage = React.lazy(() =>
  import("./pages/admin/lecturer/lecturer.list.page")
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

const NewsListPage = React.lazy(() =>
  import("./pages/admin/news/news.list.page")
);

const LevelNhaNuocListPage = React.lazy(() =>
  import("./pages/admin/scientificWork/levelNhaNuoc.list.page")
);

const LevelBoListPage = React.lazy(() =>
  import("./pages/admin/scientificWork/levelBo.list.page")
);
const LevelTinhListPage = React.lazy(() =>
  import("./pages/admin/scientificWork/levelTinh.list.page")
);

const LevelThanhPhoListPage = React.lazy(() =>
  import("./pages/admin/scientificWork/levelThanhPho.list.page")
);

const LevelCoSoListPage = React.lazy(() =>
  import("./pages/admin/scientificWork/levelCoSo.list.page")
);

const TrongNuocListPage = React.lazy(() =>
  import("./pages/admin/scientificReport/trongNuoc.list.page")
);

const QuocTeListPage = React.lazy(() =>
  import("./pages/admin/scientificReport/quocTe.list.page")
);

const PublishBookListPage = React.lazy(() =>
  import("./pages/admin/publishBook/publishBook.list.page")
);
const ChuyenKhaoListPage = React.lazy(() =>
  import("./pages/admin/publishBook/chuyenkhao.list.page")
);
const GiaoTrinhListPage = React.lazy(() =>
  import("./pages/admin/publishBook/giaotrinh.list.page")
);
const HuongDanListPage = React.lazy(() =>
  import("./pages/admin/publishBook/huongdan.list.page")
);
const ThamKhaoListPage = React.lazy(() =>
  import("./pages/admin/publishBook/thamkhao.list.page")
);
const TaiBanListPage = React.lazy(() =>
  import("./pages/admin/publishBook/taiban.list.page")
);

const CapTruongListPage = React.lazy(() =>
  import("./pages/admin/studyGuide/capTruong.list.page")
);
const CapKhoaListPage = React.lazy(() =>
  import("./pages/admin/studyGuide/capKhoa.list.page")
);

const routes = [
  // {
  //   path: "/",
  //   exact: true,
  //   name: "Admin",
  //   component: DefaultLayout
  // },
  { path: "/dashboard", name: "", component: Dashboard },
  { path: "/levels", name: "Cấp", component: LevelListPage },
  { path: "/lecturers", name: "Giảng viên", component: LecturerListPage },
  {
    path: "/scientificWorks",
    name: "Công trình khoa học",
    component: ScientificWorkListPage,
  },
  {
    path: "/NCKHCapNhaNuoc",
    name: "Nghiên cứu khoa học cấp Nhà Nước",
    component: LevelNhaNuocListPage,
  },
  {
    path: "/NCKHCapBo",
    name: "Nghiên cứu khoa học cấp Bộ",
    component: LevelBoListPage,
  },
  {
    path: "/NCKHCapTinh",
    name: "Nghiên cứu khoa học cấp Tỉnh",
    component: LevelTinhListPage,
  },
  {
    path: "/NCKHCapThanhPho",
    name: "Nghiên cứu khoa học cấp Thành phố",
    component: LevelThanhPhoListPage,
  },
  {
    path: "/NCKHCapCoSo",
    name: "Nghiên cứu khoa học cấp Cơ sở",
    component: LevelCoSoListPage,
  },
  {
    path: "/scientificReportTypes",
    name: "Loại Bài báo - Báo cáo",
    component: ScientificReportTypeListPage,
  },
  {
    path: "/scientificReports",
    name: "Bài báo - Báo cáo",
    component: ScientificReportListPage,
  },
  {
    path: "/BaoCaoKHTrongNuoc",
    name: "Bài báo, báo cáo khoa học trong nước",
    component: TrongNuocListPage,
  },
  {
    path: "/BaoCaoKHQuocTe",
    name: "Bài báo, báo cáo khoa học quốc tế",
    component: QuocTeListPage,
  },
  {
    path: "/publishBooks",
    name: "Xuất bản sách",
    component: PublishBookListPage,
  },
  {
    path: "/chuyenkhao",
    name: "Chuyên khảo",
    component: ChuyenKhaoListPage,
  },
  {
    path: "/giaotrinh",
    name: "Giáo trình",
    component: GiaoTrinhListPage,
  },
  {
    path: "/huongdan",
    name: "Hướng dẫn",
    component: HuongDanListPage,
  },
  {
    path: "/thamkhao",
    name: "Tham khảo",
    component: ThamKhaoListPage,
  },
  {
    path: "/taiban",
    name: "Tái bản có chỉnh sửa",
    component: TaiBanListPage,
  },
  {
    path: "/captruong",
    name: "Cấp Trường",
    component: CapTruongListPage,
  },
  {
    path: "/capkhoa",
    name: "Cấp Khoa",
    component: CapKhoaListPage,
  },
  {
    path: "/news",
    name: "Tin tức",
    component: NewsListPage,
  },
];

export default routes;
