import React, { Component } from "react";
import ApiLecturer from "../../../api/api.lecturer";
import { Row, Col, Button, Table, Label } from "reactstrap";
import ScientificWorkDetail from "../scientificWork/scientificWork.detail";
import ScientificReportDetail from "../scientificReport/scientificReport.detail";
class LecturerDetail extends Component {
  constructor(props) {
    super(props);
    this.state = {
      lecturer: {},
      scientificWorks: [],
      scientificReports: []
    };
  }

  backToAdminPage = () => {
    this.props.backToAdminPage();
  };

  toggleDetailWorkPage = item => {
    this.setState(prevState => ({
      isShowDetailWork: !prevState.isShowDetailWork,
      item: item
    }));
  };

  toggleDetailReportPage = item => {
    this.setState(prevState => ({
      isShowDetailReport: !prevState.isShowDetailReport,
      item: item
    }));
  };
  GetScientificWork = async id => {
    let scientificWorks = await ApiLecturer.GetAllScientificWorkByLecturerId(
      id
    );
    this.setState({ scientificWorks: scientificWorks.data });
  };

  GetScientificReport = async id => {
    let scientificReports = await ApiLecturer.GetAllScientificReportByLecturerId(
      id
    );
    this.setState({ scientificReports: scientificReports.data });
  };

  componentDidMount() {
    this.setState({ lecturer: this.props.lecturer });
    this.GetScientificWork(this.props.lecturer.id);
    this.GetScientificReport(this.props.lecturer.id);
  }

  render() {
    const {
      isShowDetailWork,
      isShowDetailReport,
      item,
      lecturer,
      scientificWorks,
      scientificReports
    } = this.state;

    return (
      <div>
        <Row>
          <Col md="10">
            {" "}
            <h4 style={{ textDecoration: "underline", color: "#0473b3" }}>
              {" "}
              Giảng viên: {lecturer.name}
            </h4>
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
                CÔNG TRÌNH KHOA HỌC
              </Label>
            </div>
            <Table className="admin-table" responsive bordered>
              <thead>
                <tr>
                  <th style={{ width: "50px" }}>STT</th>
                  <th>Công trình khoa học</th>
                  <th style={{ width: "100px" }}>Cấp</th>
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
                  <th style={{ width: "100px" }}>Loại</th>
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
