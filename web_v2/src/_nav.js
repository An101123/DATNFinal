export default {
  items: [
    {
      name: "Trang chủ",
      url: "/dashboard",
      icon: "fa fa-fw fa-home",
      badge: {
        variant: "info",
        text: "NEW",
      },
    },

    {
      name: "Đề tài NCKH",
      icon: "fa fa-balance-scale",
      url: "/scientificWorks",
      children: [
        {
          name: "NCKH các cấp",
          url: "/scientificWorks",
          icon: "fa fa-angle-right",
        },
        {
          name: "NCKH cấp Nhà Nước",
          url: "/NCKHCapNhaNuoc",
          icon: "fa fa-angle-right",
        },
        {
          name: "NCKH cấp Bộ",
          url: "/NCKHCapBo",
          icon: "fa fa-angle-right",
        },
        {
          name: "NCKH cấp Tỉnh",
          url: "/NCKHCapTinh",
          icon: "fa fa-angle-right",
        },
        {
          name: "NCKH cấp Thành phố",
          url: "/NCKHCapThanhPho",
          icon: "fa fa-angle-right",
        },
        {
          name: "NCKH cấp Cơ sở",
          url: "/NCKHCapCoSo",
          icon: "fa fa-angle-right",
        },
      ],
    },
    {
      name: "Bài báo - Báo cáo",
      icon: "fa fa-file-text",
      children: [
        {
          name: "Bài báo-Báo cáo",
          url: "/scientificReports",
          icon: "fa fa-angle-right",
        },
        {
          name: "Trong nước",
          url: "/BaoCaoKHTrongNuoc",
          icon: "fa fa-angle-right",
        },
        {
          name: "Quốc tế",
          url: "/BaoCaoKHQuocTe",
          icon: "fa fa-angle-right",
        },
      ],
    },
    {
      name: "Xuất bản sách",
      icon: "fa fa-balance-scale",
      url: "/publishBooks",
      children: [
        {
          name: "Chuyên khảo",
          url: "/chuyenkhao",
          icon: "fa fa-angle-right",
        },
        {
          name: "Giáo trình",
          url: "/giaotrinh",
          icon: "fa fa-angle-right",
        },
        {
          name: "Hướng dẫn",
          url: "/huongdan",
          icon: "fa fa-angle-right",
        },
        {
          name: "Tham khảo",
          url: "/thamkhao",
          icon: "fa fa-angle-right",
        },
        {
          name: "Tái bản có chỉnh sửa",
          url: "/taiban",
          icon: "fa fa-angle-right",
        },
      ],
    },
    {
      name: "Hướng dẫn sinh viên NCKH",
      icon: "fa fa-balance-scale",
      url: "/studyGuides",
      children: [
        {
          name: "Cấp Trường",
          url: "/captruong",
          icon: "fa fa-angle-right",
        },
        {
          name: "Cấp Khoa",
          url: "/capkhoa",
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
      name: "Admin",
      url: "http://localhost:4000/login",
      icon: "icon-star",
    },
  ],
};
