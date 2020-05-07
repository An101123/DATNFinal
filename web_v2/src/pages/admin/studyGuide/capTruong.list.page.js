import React, { Component } from "react";
import { connect } from "react-redux";
import moment from "moment";
import Pagination from "../../../components/pagination/Pagination";
import lodash from "lodash";
import { getStudyGuideList } from "../../../actions/studyGuide.list.action";
import { pagination } from "../../../constant/app.constant";
import ApiLevelStudyGuide from "../../../api/api.levelStudyGuide";
import ApiLecturer from "../../../api/api.lecturer";
import "../../../pages/admin/select-custom.css";
import "../Dashboard/dashboard.css";
import { Row, Col, CardBody, Label, Table } from "reactstrap";

class StudyGuideListPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isShowDetail: false,
      item: {},
      itemId: null,
      levelStudyGuides: [],
      lecturers: [],
      params: {
        skip: pagination.initialPage,
        take: pagination.defaultTake,
      },
      query: "",
    };
    this.delayedCallback = lodash.debounce(this.search, 300);
  }

  search = (e) => {
    this.setState(
      {
        params: {
          ...this.state.params,
          skip: 1,
        },
        query: e.target.value,
      },
      () => {
        this.getStudyGuideList();
      }
    );
  };

  onSearchChange = (e) => {
    e.persist();
    this.delayedCallback(e);
  };

  handlePageClick = (e) => {
    this.setState(
      {
        params: {
          ...this.state.params,
          skip: e.selected + 1,
        },
      },
      () => this.getStudyGuideList()
    );
  };

  getStudyGuideList = () => {
    let params = Object.assign({}, this.state.params, {
      query: this.state.query,
    });
    this.props.getStudyGuideList(params);
  };

  getLevelStudyGuideList = () => {
    ApiLevelStudyGuide.getAllLevelStudyGuide().then((values) => {
      this.setState({ levelStudyGuides: values });
    });
  };

  getLecturerList = () => {
    ApiLecturer.getAllLecturer().then((values) => {
      this.setState({ lecturers: values });
    });
  };

  onSubmit(e) {
    e.preventDefault();
    this.form.validateAll();
    this.saveStudyGuide();
  }

  componentDidMount() {
    this.getStudyGuideList();
    this.getLevelStudyGuideList();
    this.getLecturerList();
  }

  render() {
    const { studyGuidePagedList } = this.props.studyGuidePagedListReducer;
    const { sources, pageIndex, totalPages } = studyGuidePagedList;
    const hasResults =
      studyGuidePagedList.sources && studyGuidePagedList.sources.length > 0;
    return (
      <div className="animated fadeIn">
        <Row>
          <CardBody>
            <img
              src="https://due.udn.vn/portals/_default/skins/dhkt/img/front/logo.png"
              alt="logochichido"
            />
          </CardBody>
        </Row>
        <hr />

        <div>
          <h3 style={{ color: "#0473b3" }}>
            HƯỚNG DẪN SINH VIÊN NCKH CẤP TRƯỜNG
          </h3>
          <hr />

          <Row className="nckh">
            <Col xs="12">
              <div className="flex-container header-table">
                <Label
                  className="label label-default"
                  // style={{ fontWeight: "bolder" }}
                ></Label>
                <input
                  onChange={this.onSearchChange}
                  className="form-control form-control-sm"
                  placeholder="Tìm kiếm..."
                />
              </div>
              <Table className="admin-table" responsive bordered>
                <thead>
                  <tr>
                    <th>STT</th>
                    <th>Họ và tên, Tên đề tài</th>
                    <th>Trình độ</th>
                    <th>Cơ sở đào tạo</th>
                    <th>Năm hướng dẫn</th>
                    <th>Năm bảo vệ</th>
                    <th>Phân loại</th>
                    <th>Giảng viên</th>
                  </tr>
                </thead>
                <tbody>
                  {hasResults &&
                    sources
                      .filter((value) => {
                        if (value.levelStudyGuide.name === "Trường") {
                          return true;
                        }
                        return false;
                      })
                      .map((item, index) => {
                        return (
                          <tr key={item.id}>
                            <td>{index + 1}</td>
                            <td> {item.name}</td>
                            <td>{item.literacy}</td>
                            <td>{item.placeOfTraining}</td>
                            <td>
                              {moment(item.instructionTime)
                                .add(7, "h")
                                .format("YYYY")}
                            </td>
                            <td>
                              {moment(item.graduationTime)
                                .add(7, "h")
                                .format("YYYY")}
                            </td>
                            <td>{item.levelStudyGuide.name}</td>
                            <td>{item.lecturer.name}</td>{" "}
                          </tr>
                        );
                      })}
                </tbody>
              </Table>
              {hasResults && totalPages > 1 && (
                <Pagination
                  initialPage={0}
                  totalPages={totalPages}
                  forcePage={pageIndex - 1}
                  pageRangeDisplayed={2}
                  onPageChange={this.handlePageClick}
                />
              )}
            </Col>
          </Row>
        </div>
      </div>
    );
  }
}

export default connect(
  (state) => ({
    studyGuidePagedListReducer: state.studyGuidePagedListReducer,
  }),
  {
    getStudyGuideList,
  }
)(StudyGuideListPage);
