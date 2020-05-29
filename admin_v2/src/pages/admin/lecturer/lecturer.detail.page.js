import React, { Component } from "react";
import ApiLecturer from "../../../api/api.lecturer";
import { Row, Col, Button, Table, Label, FormGroup } from "reactstrap";
import Datetime from "react-datetime";
import moment from "moment";
import { connect } from "react-redux";

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
      params: {
        startTime: props.location.state.startTime,
        endTime: props.location.state.endTime,
      },
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
  GetScientificWork = async (id, startTime, endTime) => {
    let scientificWorks = await ApiLecturer.GetAllScientificWorkByLecturerId(
      id,
      startTime,
      endTime
    );
    this.setState({ scientificWorks: scientificWorks.data });
  };

  GetLecturerById = async (id, startTime, endTime) => {
    const res = await ApiLecturer.getLecturerById(id, startTime, endTime);
    this.setState({ lecturer: res });
  };

  GetScientificReport = async (id, startTime, endTime) => {
    let scientificReports = await ApiLecturer.GetAllScientificReportByLecturerId(
      id,
      startTime,
      endTime
    );
    this.setState({ scientificReports: scientificReports.data });
  };

  GetPublishBook = async (id, startTime, endTime) => {
    let publishBooks = await ApiLecturer.GetAllPublishBookByLecturerId(
      id,
      startTime,
      endTime
    );
    this.setState({ publishBooks: publishBooks.data });
  };

  GetStudyGuide = async (id) => {
    let studyGuides = await ApiLecturer.GetAllStudyGuideByLecturerId(id);
    this.setState({ studyGuides: studyGuides.data });
  };
  GetOtherScientificWork = async (id, startTime, endTime) => {
    let otherScientificWorks = await ApiLecturer.GetAllOtherScientificWorkByLecturerId(
      id,
      startTime,
      endTime
    );
    this.setState({ otherScientificWorks: otherScientificWorks.data });
  };

  handleFilter = () => {
    const { match = {} } = this.props;
    const { params = {} } = match;
    const { id } = params;
    const { startTime, endTime } = this.state.params;
    this.GetScientificWork(id, startTime, endTime);
    this.GetScientificReport(id, startTime, endTime);
    this.GetPublishBook(id, startTime, endTime);
    // this.GetStudyGuide(id);
    this.GetOtherScientificWork(id, startTime, endTime);
    this.GetLecturerById(id, startTime, endTime);
  };

  componentDidMount() {
    const { match = {}, location = {} } = this.props;
    const { params = {} } = match;
    const { id } = params;
    const { startTime, endTime } = location.state;
    console.log(this.props);

    this.setState({ lecturer: this.props.lecturer });
    this.GetScientificWork(id, startTime, endTime);
    this.GetScientificReport(id, startTime, endTime);
    this.GetPublishBook(id, startTime, endTime);
    // this.GetStudyGuide(id);
    this.GetOtherScientificWork(id, startTime, endTime);
    this.GetLecturerById(id, startTime, endTime);
  }

  render() {
    const {
      lecturer = {},
      scientificWorks,
      scientificReports,
      publishBooks,
      studyGuides,
      otherScientificWorks,
      params = {},
    } = this.state;
    const { startTime, endTime } = params;
    return (
      <div>
        <Row>
          <Col md="8">
            {" "}
            <h4 style={{ color: "#0473b3" }}> Giảng viên: {lecturer.name}</h4>
          </Col>
          <Col md="2">
            <h4 style={{ color: "#0473b3" }}> Tổng điểm: {lecturer.total}</h4>
          </Col>
          <Col md="2">
            <h4 style={{ color: "#0473b3" }}>
              {" "}
              Tổng giờ: {lecturer.totalHour}
            </h4>
          </Col>
        </Row>
        <hr />
        <div className="border border-dark">
          <Row style={{ marginTop: "10px", marginLeft: "10px" }}>
            <Col xs="5">
              <Row>
                <Col xs="3" sm="3" md="3" lg="3">
                  <FormGroup>
                    <Label for="examplePassword">
                      {" "}
                      <strong>Từ: </strong>
                    </Label>
                  </FormGroup>
                </Col>
                <Col xs="9" sm="9" md="9" lg="9">
                  <FormGroup>
                    <Datetime
                      dateFormat="YYYY-MM-DD"
                      timeFormat={false}
                      isValidDate={(curr) => curr.isBefore(moment())}
                      value={startTime}
                      onChange={(date) =>
                        this.setState({
                          params: {
                            ...this.state.params,
                            startTime: date.format("YYYY-MM-DD"),
                          },
                        })
                      }
                    />
                  </FormGroup>
                </Col>
              </Row>
            </Col>

            {/* End Day */}
            <Col xs="5">
              <Row>
                <Col xs="3" sm="3" md="3" lg="3">
                  <FormGroup>
                    <Label for="examplePassword">
                      <strong>Đến: </strong>{" "}
                    </Label>
                  </FormGroup>
                </Col>
                <Col xs="9" sm="9" md="9" lg="9">
                  <FormGroup>
                    <Datetime
                      dateFormat="YYYY-MM-DD"
                      timeFormat={false}
                      isValidDate={(current) =>
                        current.isAfter(this.state.params.startTime)
                      }
                      value={endTime}
                      onChange={(date) =>
                        this.setState({
                          params: {
                            ...this.state.params,
                            endTime: date.format("YYYY-MM-DD"),
                          },
                        })
                      }
                    />
                  </FormGroup>
                </Col>
              </Row>
            </Col>
            <Col xs="2">
              <Button
                className="btn btn-pill btn-success btn-sm"
                onClick={this.handleFilter}
              >
                Xác nhận
              </Button>
            </Col>
          </Row>
        </div>
        <br />

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
                  <th style={{ width: "100px" }}>Thời gian</th>
                  <th style={{ width: "100px" }}>Cấp</th>
                  <th style={{ width: "100px" }}>Số điểm quy đổi</th>
                  <th style={{ width: "100px" }}>Số giờ quy đổi</th>
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

                      <td>
                        {moment(item.time).add(7, "h").format("DD-MM-YYYY")}
                      </td>
                      <td>{item.level.name}</td>
                      <td>{item.level.score}</td>
                      <td>{item.level.hoursConverted}</td>
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
                  <th style={{ width: "100px" }}>Thời gian</th>
                  <th style={{ width: "100px" }}>Loại</th>

                  <th style={{ width: "100px" }}>Số điểm quy đổi</th>
                  <th style={{ width: "100px" }}>Số giờ quy đổi</th>
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

                      <td>
                        {moment(item.time).add(7, "h").format("DD-MM-YYYY")}
                      </td>
                      <td>{item.scientificReportType.name}</td>
                      <td>{item.scientificReportType.score}</td>
                      <td>{item.scientificReportType.hoursConverted}</td>
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
                  <th style={{ width: "100px" }}>Thời gian</th>
                  <th style={{ width: "100px" }}>Loại</th>
                  <th style={{ width: "100px" }}>Số điểm quy đổi</th>
                  <th style={{ width: "100px" }}>Số giờ quy đổi</th>
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

                      <td>
                        {moment(item.time).add(7, "h").format("DD-MM-YYYY")}
                      </td>
                      <td>{item.bookCategory.name}</td>
                      <td>{item.bookCategory.score}</td>
                      <td>{item.bookCategory.hoursConverted}</td>
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
                  <th style={{ width: "100px" }}>Thời gian</th>
                  <th style={{ width: "100px" }}>Cấp</th>
                  <th style={{ width: "100px" }}>Số điểm quy đổi</th>
                  <th style={{ width: "100px" }}>Số giờ quy đổi</th>
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
                      <td>
                        {moment(item.instructionTime)
                          .add(7, "h")
                          .format("DD-MM-YYYY")}
                      </td>
                      <td>{item.levelStudyGuide.name}</td>
                      <td>{item.levelStudyGuide.score}</td>
                      <td>{item.levelStudyGuide.hoursConverted}</td>{" "}
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
                  <th style={{ width: "100px" }}>Số điểm quy đổi</th>
                  <th style={{ width: "100px" }}>Số giờ quy đổi</th>
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
                        <td>{item.classificationOfScientificWork.score}</td>
                        <td>
                          {item.classificationOfScientificWork.hoursConverted}
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
                  <th style={{ width: "100px" }}>Thời gian</th>
                  <th style={{ width: "100px" }}>Loại</th>
                  <th style={{ width: "100px" }}>Số điểm quy đổi</th>
                  <th style={{ width: "100px" }}>Số giờ quy đổi</th>
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
                        <td>
                          {moment(item.time).add(7, "h").format("DD-MM-YYYY")}
                        </td>
                        <td>{item.classificationOfScientificWork.name}</td>
                        <td>{item.classificationOfScientificWork.score}</td>
                        <td>
                          {item.classificationOfScientificWork.hoursConverted}
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

export default connect((state) => ({
  lecturerPagedListReducer: state.lecturerPagedListReducer,
}))(LecturerDetail);
