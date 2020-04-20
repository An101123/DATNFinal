import React, { Component } from "react";
import { connect } from "react-redux";
import moment from "moment";
import Pagination from "../../../components/pagination/Pagination";
import lodash from "lodash";
import { getOtherScientificWorkList } from "../../../actions/otherScientificWork.list.action";
import { pagination } from "../../../constant/app.constant";
import ApiClassificationOfScientificWork from "../../../api/api.classificationOfScientificWork";
import ApiLecturer from "../../../api/api.lecturer";
import "../../../pages/admin/select-custom.css";
import "../Dashboard/dashboard.css";
import { Row, Col, CardBody, Label, Table } from "reactstrap";

class OtherScientificWorkListPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isShowDetail: false,
      item: {},
      itemId: null,
      classificationOfScientificWorks: [],
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
        this.getOtherScientificWorkList();
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
      () => this.getOtherScientificWorkList()
    );
  };

  getOtherScientificWorkList = () => {
    let params = Object.assign({}, this.state.params, {
      query: this.state.query,
    });
    this.props.getOtherScientificWorkList(params);
  };

  getClassificationOfScientificWorkList = () => {
    ApiClassificationOfScientificWork.getAllClassificationOfScientificWork().then(
      (values) => {
        this.setState({ classificationOfScientificWorks: values });
      }
    );
  };

  getLecturerList = () => {
    ApiLecturer.getAllLecturer().then((values) => {
      this.setState({ lecturers: values });
    });
  };

  onSubmit(e) {
    e.preventDefault();
    this.form.validateAll();
    this.saveOtherScientificWork();
  }

  componentDidMount() {
    this.getOtherScientificWorkList();
    this.getClassificationOfScientificWorkList();
    this.getLecturerList();
  }

  render() {
    const {
      otherScientificWorkPagedList,
    } = this.props.otherScientificWorkPagedListReducer;
    const { sources, pageIndex, totalPages } = otherScientificWorkPagedList;
    const hasResults =
      otherScientificWorkPagedList.sources &&
      otherScientificWorkPagedList.sources.length > 0;
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
          <h3 style={{ color: "#0473b3" }}>BẰNG SÁNG CHẾ</h3>
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
                    <th>Bằng sáng chế</th>
                    <th>Thời gian</th>
                    <th>Giảng viên</th>
                  </tr>
                </thead>
                <tbody>
                  {hasResults &&
                    sources
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
                              {moment(item.time)
                                .add(7, "h")
                                .format("DD-MM-YYYY")}
                            </td>

                            <td>{item.lecturer.name}</td>
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
    otherScientificWorkPagedListReducer:
      state.otherScientificWorkPagedListReducer,
  }),
  {
    getOtherScientificWorkList,
  }
)(OtherScientificWorkListPage);
