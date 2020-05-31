import React, { Component } from "react";
import { connect } from "react-redux";
import moment from "moment";
import Pagination from "../../../components/pagination/Pagination";
import lodash from "lodash";
import { getLecturerList } from "../../../actions/lecturer.list.action";
import { pagination } from "../../../constant/app.constant";
import "../../../pages/admin/select-custom.css";
import "../Dashboard/dashboard.css";
import { Row, Col, CardBody, Label, Table } from "reactstrap";
import LecturerDetail from "./lecturer.detail";

class LecturerListPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isShowDeleteModal: false,
      isShowInfoModal: false,
      isShowDetail: false,
      item: {},
      itemId: null,
      params: {
        skip: pagination.initialPage,
        take: pagination.defaultTake,
      },
      query: "",
    };
    this.delayedCallback = lodash.debounce(this.search, 1);
  }

  toggleDetailPage = (item) => {
    this.setState((prevState) => ({
      isShowDetail: !prevState.isShowDetail,
      item: item,
    }));
  };

  backToAdminPage = () => {
    this.setState((prevState) => ({
      isShowDetail: !prevState.isShowDetail,
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
        this.getLecturerList();
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
      () => this.getLecturerList()
    );
  };

  getLecturerList = () => {
    let params = Object.assign({}, this.state.params, {
      query: this.state.query,
    });
    console.log(params);
    this.props.getLecturerList(params);
  };

  componentDidMount() {
    this.getLecturerList();
  }

  render() {
    const { lecturerPagedList } = this.props.lecturerPagedListReducer;
    const { isShowDetail, item } = this.state;
    const { sources, pageIndex, totalPages } = lecturerPagedList;
    const hasResults =
      lecturerPagedList.sources && lecturerPagedList.sources.length > 0;
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
        {!isShowDetail ? (
          <div>
            <h3 style={{ color: "#0473b3" }}>GIẢNG VIÊN</h3>
            <Row className="nckh">
              <Col xs="12">
                <div className="flex-container header-table">
                  <Label className="label label-default"></Label>
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
                      <th>Tên</th>
                      <th>Ngày sinh</th>
                      <th>Học vị</th>
                      <th>Học hàm</th>
                      <th>Khoa</th>
                    </tr>
                  </thead>
                  <tbody>
                    {hasResults &&
                      sources.map((item, index) => {
                        return (
                          <tr key={item.id}>
                            <td>{index + 1}</td>
                            <td onClick={() => this.toggleDetailPage(item)}>
                              {item.name}
                            </td>
                            <td>
                              {moment(item.dateOfBirth)
                                .add(7, "h")
                                .format("DD-MM-YYYY")}
                            </td>
                            <td>{item.academicDegree}</td>
                            <td>{item.academicRank}</td>
                            <td>{item.faculty}</td>
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
        ) : (
          <LecturerDetail
            lecturer={item}
            backToAdminPage={this.backToAdminPage}
          />
        )}
      </div>
    );
  }
}

export default connect(
  (state) => ({
    lecturerPagedListReducer: state.lecturerPagedListReducer,
  }),
  {
    getLecturerList,
  }
)(LecturerListPage);
