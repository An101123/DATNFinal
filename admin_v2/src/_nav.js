export default {
  items: [
    {
      name: "Dashboard",
      url: "/dashboard",
      icon: "fa fa-fw fa-home",
      badge: {
        variant: "info",
        text: "NEW",
      },
    },
    // {
    //   name: "Cấp",
    //   url: "/levels",
    //   icon: "fa fa-list"
    // },

    {
      name: "Đề tài NCKH",
      icon: "fa fa-balance-scale",
      children: [
        {
          name: "Đề tài NCKH",
          url: "/scientificWorks",
          icon: "fa fa-angle-right",
        },
        {
          name: "Cấp",
          url: "/levels",
          icon: "fa fa-angle-right",
        },
      ],
    },
    {
      name: "Bài báo - Báo cáo",
      icon: "fa fa-file-text",
      children: [
        {
          name: "Bài báo - Báo cáo",
          url: "/scientificReports",
          icon: "fa fa-angle-right",
        },
        {
          name: "Thể loại ",
          url: "/scientificReportTypes",
          icon: "fa fa-angle-right",
        },
      ],
    },
    {
      name: "Xuất bản sách",
      icon: "fa fa-pie-chart",
      children: [
        {
          name: "Xuất bản sách",
          url: "/publishBooks",
          icon: "fa fa-angle-right",
        },
        {
          name: "Thể loại sách",
          url: "/bookCategorys",
          icon: "fa fa-angle-right",
        },
      ],
    },
    {
      name: "Hướng dẫn sinh viên ",
      icon: "fa fa-pie-chart",
      children: [
        {
          name: "Hướng dẫn sinh viên ",
          url: "/studyGuides",
          icon: "fa fa-angle-right",
        },
        {
          name: "Phân cấp hướng dẫn",
          url: "/levelStudyGuides",
          icon: "fa fa-angle-right",
        },
      ],
    },
    {
      name: "Giảng viên",
      url: "/lecturers",
      icon: "fa fa-address-book",
    },
    {
      name: "Tin tức",
      url: "/news",
      icon: "fa fa-globe",
    },
    {
      name: "Tài khoản",
      url: "/users",
      icon: "fa fa-users",
    },
  ],
};
