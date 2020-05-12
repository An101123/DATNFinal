import React, { Component } from "react";
import ApiLecturer from "../../../api/api.lecturer";
import { Row, Col, Button, Table, Label } from "reactstrap";
import moment from "moment";

class LecturerDetail extends Component {
  constructor(props) {
    super(props);
    this.state = {
      lecturer: {},
      scientificWorks: [],
      scientificReports: [],
      publishBooks: [],
      studyGuides: [],
      otherScientificWorks: [],
    };
  }

  backToAdminPage = () => {
    this.props.backToAdminPage();
  };

  toggleDetailWorkPage = (item) => {
    this.setState((prevState) => ({
      isShowDetailWork: !prevState.isShowDetailWork,
      item: item,
    }));
  };

  toggleDetailReportPage = (item) => {
    this.setState((prevState) => ({
      isShowDetailReport: !prevState.isShowDetailReport,
      item: item,
    }));
  };
  GetScientificWork = async (id) => {
    let scientificWorks = await ApiLecturer.GetAllScientificWorkByLecturerId(
      id
    );
    this.setState({ scientificWorks: scientificWorks.data });
  };

  GetScientificReport = async (id) => {
    let scientificReports = await ApiLecturer.GetAllScientificReportByLecturerId(
      id
    );
    this.setState({ scientificReports: scientificReports.data });
  };

  GetPublishBook = async (id) => {
    let publishBooks = await ApiLecturer.GetAllPublishBookByLecturerId(id);
    this.setState({ publishBooks: publishBooks.data });
  };

  GetStudyGuide = async (id) => {
    let studyGuides = await ApiLecturer.GetAllStudyGuideByLecturerId(id);
    this.setState({ studyGuides: studyGuides.data });
  };
  GetOtherScientificWork = async (id) => {
    let otherScientificWorks = await ApiLecturer.GetAllOtherScientificWorkByLecturerId(
      id
    );
    this.setState({ otherScientificWorks: otherScientificWorks.data });
  };

  componentDidMount() {
    this.setState({ lecturer: this.props.lecturer });
    this.GetScientificWork(this.props.lecturer.id);
    this.GetScientificReport(this.props.lecturer.id);
    this.GetPublishBook(this.props.lecturer.id);
    this.GetStudyGuide(this.props.lecturer.id);
    this.GetOtherScientificWork(this.props.lecturer.id);
  }

  render() {
    const {
      lecturer,
      scientificWorks,
      scientificReports,
      publishBooks,
      studyGuides,
      otherScientificWorks,
    } = this.state;

    return (
      <div>
        <Row>
          <Col md="10">
            {" "}
            <h4 style={{ color: "#0473b3" }}> Giảng viên: {lecturer.name}</h4>
          </Col>

          <Col md="2">
            <Button
              className="fa fa-window-close"
              style={{ float: "right", backgroundColor: "white" }}
              onClick={this.backToAdminPage}
            />
          </Col>
        </Row>

        <Row className="nckh">
          <Col xs="12">
            <div className="flex-container header-table">
              <Label className="label label-default">
                {" "}
                ĐỀ TÀI NGHIÊN CỨU KHOA HỌC
              </Label>
            </div>
            <Table className="admin-table" responsive bordered>
              <thead>
                <tr>
                  <th style={{ width: "50px" }}>STT</th>
                  <th>Đề tài NCKH</th>
                  <th style={{ width: "200px" }}>Cấp</th>
                  <th style={{ width: "200px" }}>Thời gian</th>
                </tr>
              </thead>
              <tbody>
                {scientificWorks.map((item, index) => {
                  return (
                    <tr key={item.id}>
                      <td>{index + 1}</td>
                      <td onClick={() => this.toggleDetailWorkPage(item)}>
                        {item.name}
                      </td>
                      <td>{item.level.name}</td>
                      <td>
                        {moment(item.time).add(7, "h").format("DD-MM-YYYY")}
                      </td>
                    </tr>
                  );
                })}
              </tbody>
            </Table>
          </Col>
        </Row>

        <Row className="bckh">
          <Col xs="12">
            <div className="flex-container header-table">
              <Label className="label label-default">
                {" "}
                BÀI BÁO - BÁO CÁO KHOA HỌC
              </Label>
            </div>
            <Table className="admin-table" responsive bordered>
              <thead>
                <tr>
                  <th style={{ width: "50px" }}>STT</th>
                  <th>Bài báo - Báo cáo khoa học</th>
                  <th style={{ width: "200px" }}>Loại</th>
                  <th style={{ width: "200px" }}>Thời gian</th>
                </tr>
              </thead>
              <tbody>
                {scientificReports.map((item, index) => {
                  return (
                    <tr key={item.id}>
                      <td>{index + 1}</td>
                      <td onClick={() => this.toggleDetailReportPage(item)}>
                        {item.name}
                      </td>
                      <td>{item.scientificReportType.name}</td>
                      <td>
                        {moment(item.time).add(7, "h").format("DD-MM-YYYY")}
                      </td>
                    </tr>
                  );
                })}
              </tbody>
            </Table>
          </Col>
        </Row>
        <Row className="xbs">
          <Col xs="12">
            <div className="flex-container header-table">
              <Label className="label label-default"> XUẤT BẢN SÁCH</Label>
            </div>
            <Table className="admin-table" responsive bordered>
              <thead>
                <tr>
                  <th style={{ width: "50px" }}>STT</th>
                  <th>Xuất bản sách</th>
                  <th style={{ width: "200px" }}>Loại</th>
                  <th style={{ width: "200px" }}>Thời gian</th>
                </tr>
              </thead>
              <tbody>
                {publishBooks.map((item, index) => {
                  return (
                    <tr key={item.id}>
                      <td>{index + 1}</td>
                      <td onClick={() => this.toggleDetailReportPage(item)}>
                        {item.name}
                      </td>
                      <td>{item.bookCategory.name}</td>
                      <td>
                        {moment(item.time).add(7, "h").format("DD-MM-YYYY")}
                      </td>
                    </tr>
                  );
                })}
              </tbody>
            </Table>
          </Col>
        </Row>

        <Row className="hdsv">
          <Col xs="12">
            <div className="flex-container header-table">
              <Label className="label label-default">
                {" "}
                HƯỚNG DẪN SINH VIÊN NCKH
              </Label>
            </div>
            <Table className="admin-table" responsive bordered>
              <thead>
                <tr>
                  <th style={{ width: "50px" }}>STT</th>
                  <th>Họ và tên, tên đề tài</th>
                  <th style={{ width: "200px" }}>Cấp</th>
                  <th style={{ width: "200px" }}>Thời gian</th>
                </tr>
              </thead>
              <tbody>
                {studyGuides.map((item, index) => {
                  return (
                    <tr key={item.id}>
                      <td>{index + 1}</td>
                      <td onClick={() => this.toggleDetailReportPage(item)}>
                        {item.name}
                      </td>
                      <td>{item.levelStudyGuide.name}</td>
                      <td>
                        {moment(item.time).add(7, "h").format("DD-MM-YYYY")}
                      </td>
                    </tr>
                  );
                })}
              </tbody>
            </Table>
          </Col>
        </Row>
        <Row className="hdsv">
          <Col xs="12">
            <div className="flex-container header-table">
              <Label className="label label-default"> BẰNG SÁNG CHẾ</Label>
            </div>
            <Table className="admin-table" responsive bordered>
              <thead>
                <tr>
                  <th style={{ width: "50px" }}>STT</th>
                  <th>Bằng sáng chế</th>
                  <th style={{ width: "200px" }}>Thời gian</th>
                </tr>
              </thead>
              <tbody>
                {otherScientificWorks
                  .filter((value) => {
                    if (
                      value.classificationOfScientificWork.name ===
                      "Bằng sáng chế"
                    ) {
                      return true;
                    }
                    return false;
                  })
                  .map((item, index) => {
                    return (
                      <tr key={item.id}>
                        <td>{index + 1}</td>
                        <td>{item.name}</td>
                        <td>
                          {moment(item.time).add(7, "h").format("DD-MM-YYYY")}
                        </td>
                      </tr>
                    );
                  })}
              </tbody>
            </Table>
          </Col>
        </Row>
        <Row className="hdsv">
          <Col xs="12">
            <div className="flex-container header-table">
              <Label className="label label-default">
                {" "}
                CÔNG TRÌNH KHOA HỌC KHÁC
              </Label>
            </div>
            <Table className="admin-table" responsive bordered>
              <thead>
                <tr>
                  <th style={{ width: "50px" }}>STT</th>
                  <th>Công trình khoa học khác</th>
                  <th>Loại</th>
                  <th style={{ width: "200px" }}>Thời gian</th>
                </tr>
              </thead>
              <tbody>
                {otherScientificWorks
                  .filter((value) => {
                    if (
                      value.classificationOfScientificWork.name !==
                      "Bằng sáng chế"
                    ) {
                      return true;
                    }
                    return false;
                  })
                  .map((item, index) => {
                    return (
                      <tr key={item.id}>
                        <td>{index + 1}</td>
                        <td> {item.name}</td>
                        <td>{item.classificationOfScientificWork.name}</td>
                        <td>
                          {moment(item.time).add(7, "h").format("DD-MM-YYYY")}
                        </td>
                      </tr>
                    );
                  })}
              </tbody>
            </Table>
          </Col>
        </Row>
      </div>
    );
  }
}

export default LecturerDetail;
