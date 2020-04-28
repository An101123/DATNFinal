import React, { Component } from "react";
import { connect } from "react-redux";
import { Row, Col, Table, Label, CardBody } from "reactstrap";
import moment from "moment";
import Pagination from "../../../components/pagination/Pagination";
import lodash from "lodash";
import { getScientificReportList } from "../../../actions/scientificReport.list.action";
import { pagination } from "../../../constant/app.constant";
import ApiScientificReportType from "../../../api/api.scientificReportType";
import ApiLecturer from "../../../api/api.lecturer";
import "../../../pages/admin/select-custom.css";
import "../Dashboard/dashboard.css";
import ScientificReportDetail from "./scientificReport.detail";

class ScientificReportListPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isShowDetail: false,
      item: {},
      itemId: null,
      scientificReportTypes: [],
      lecturers: [],
      params: {
        skip: pagination.initialPage,
        take: pagination.defaultTake,
      },
      query: "",
    };
    this.delayedCallback = lodash.debounce(this.search, 300);
  }

  backToAdminPage = () => {
    this.setState((prevState) => ({
      isShowDetail: !prevState.isShowDetail,
    }));
  };

  toggleDetailPage = (item) => {
    this.setState((prevState) => ({
      isShowDetail: !prevState.isShowDetail,
      item: item,
    }));
  };

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
        this.getScientificReportList();
      }
    );
  };

  onSearchChange = (e) => {
    e.persist();
    this.delayedCallback(e);
  };

  getScientificReportList = () => {
    let params = Object.assign({}, this.state.params, {
      query: this.state.query,
    });
    this.props.getScientificReportList(params);
  };

  getScientificReportTypeList = () => {
    ApiScientificReportType.getAllScientificReportType().then((values) => {
      this.setState({ scientificReportTypes: values });
    });
  };

  getLecturerList = () => {
    ApiLecturer.getAllLecturer().then((values) => {
      this.setState({ lecturers: values });
    });
  };

  componentDidMount() {
    this.getScientificReportList();
    this.getScientificReportTypeList();
    this.getLecturerList();
  }

  render() {
    const { isShowDetail, item } = this.state;
    const {
      scientificReportPagedList,
    } = this.props.scientificReportPagedListReducer;
    const { sources, pageIndex, totalPages } = scientificReportPagedList;
    const hasResults =
      scientificReportPagedList.sources &&
      scientificReportPagedList.sources.length > 0;
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
            BÀI BÁO - BÁO CÁO KHOA HỌC QUỐC TẾ
          </h3>
          {!isShowDetail ? (
            <Row className="nckh">
              <Col xs="12">
                <div className="flex-container header-table">
                  <Label
                    className="label label-default"
                    // style={{ fontWeight: "500px" }}
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
                      <th>Bài báo - Báo cáo khoa học</th>
                      <th>Thời gian</th>
                      <th>Loại</th>
                      <th>Giảng viên</th>
                    </tr>
                  </thead>
                  <tbody>
                    {hasResults &&
                      sources
                        .filter((value) => {
                          if (
                            value.scientificReportType.name === "Tạp chí ISI" ||
                            value.scientificReportType.name === "Tạp chí SCI" ||
                            value.scientificReportType.name ===
                              "Tạp chí quốc tế/Kỷ yếu hội nghị quốc tế có ISSN"
                          ) {
                            return true;
                          }
                          return false;
                        })
                        .map((item, index) => {
                          return (
                            <tr key={item.id}>
                              <td>{index + 1}</td>
                              <td onClick={() => this.toggleDetailPage(item)}>
                                {item.name.length > 100 ? (
                                  <span>
                                    {item.name.substr(0, 100)}{" "}
                                    <span style={{ fontWeight: "bolder" }}>
                                      {" "}
                                      ...
                                    </span>
                                  </span>
                                ) : (
                                  item.name
                                )}
                              </td>
                              <td>
                                {moment(item.time)
                                  .add(7, "h")
                                  .format("DD-MM-YYYY")}
                              </td>
                              <td>{item.scientificReportType.name}</td>
                              <td>
                                {item.lecturers.map(
                                  (lecturer) => lecturer.name + "; "
                                )}
                              </td>{" "}
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
          ) : (
            <ScientificReportDetail
              ScientificReport={item}
              backToAdminPage={this.backToAdminPage}
            />
          )}
        </div>
      </div>
    );
  }
}

export default connect(
  (state) => ({
    scientificReportPagedListReducer: state.scientificReportPagedListReducer,
  }),
  {
    getScientificReportList,
  }
)(ScientificReportListPage);
