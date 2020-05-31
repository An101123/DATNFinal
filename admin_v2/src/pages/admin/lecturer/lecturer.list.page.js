import React, { Component } from "react";
import { connect } from "react-redux";
import { Row, Col, Button, FormGroup, Label, Table } from "reactstrap";
import Form from "react-validation/build/form";
import Datetime from "react-datetime";
import { Link } from "react-router-dom";
import moment from "moment";
import ModalConfirm from "../../../components/modal/modal-confirm";
import Pagination from "../../../components/pagination/Pagination";
import ModalInfo from "../../../components/modal/modal-info";
import ValidationInput from "../../../components/common/validation-input";
import { toastSuccess, toastError } from "../../../helpers/toast.helper";
import lodash from "lodash";
import { getLecturerList } from "../../../actions/lecturer.list.action";
import ApiLecturer from "../../../api/api.lecturer";
import { pagination } from "../../../constant/app.constant";
import faculty from "../../../constant/faculty";
import "../../../pages/admin/select-custom.css";
import gender from "../../../constant/gender";
import academicDegree from "../../../constant/academicDegree";
import academicRank from "../../../constant/academicRank";

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

  toggleDeleteModal = () => {
    this.setState((prevState) => ({
      isShowDeleteModal: !prevState.isShowDeleteModal,
    }));
  };

  backToAdminPage = () => {
    this.setState((prevState) => ({
      isShowDetail: !prevState.isShowDetail,
    }));
  };
  toggleModalInfo = (item, title) => {
    this.setState((prevState) => ({
      isShowInfoModal: !prevState.isShowInfoModal,
      item: item || {},
      formTitle: title,
    }));
  };

  toggleDetailPage = (item) => {
    this.setState((prevState) => ({
      isShowDetail: !prevState.isShowDetail,
      item: item,
    }));
  };

  showConfirmDelete = (itemId) => {
    this.setState(
      {
        itemId: itemId,
      },
      () => this.toggleDeleteModal()
    );
  };

  showAddNew = () => {
    let title = "Tạo mới giảng viên";
    let lecturer = {
      name: "",
      faculty: "",
      dateOfBirth: null,
      gender: "",
      academicDegree: "",
      academicRank: "",
    };
    this.toggleModalInfo(lecturer, title);
  };

  showUpdateModal = (item) => {
    let title = "Chỉnh sửa giảng viên";
    this.toggleModalInfo(item, title);
  };

  onModelChange = (el) => {
    let inputName = el.target.name;
    let inputValue = el.target.value;
    let item = Object.assign({}, this.state.item);
    item[inputName] = inputValue;
    this.setState({ item });
    console.log("xxx: ", item);
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
    const params = Object.assign({}, this.state.params, {
      query: this.state.query,
    });
    this.props.getLecturerList(params);
  };

  addLecturer = async () => {
    const {
      name,
      faculty,
      dateOfBirth,
      gender,
      academicDegree,
      academicRank,
    } = this.state.item;
    const lecturer = {
      name,
      faculty,
      dateOfBirth,
      gender,
      academicDegree,
      academicRank,
    };
    try {
      await ApiLecturer.postLecturer(lecturer);
      this.toggleModalInfo();
      this.getLecturerList();
      toastSuccess("Tạo mới thành công");
    } catch (err) {
      toastError(err);
    }
  };

  updateLecturer = async () => {
    const {
      id,
      name,
      faculty,
      dateOfBirth,
      gender,
      academicDegree,
      academicRank,
    } = this.state.item;
    const lecturer = {
      id,
      name,
      faculty,
      dateOfBirth,
      gender,
      academicDegree,
      academicRank,
    };
    try {
      await ApiLecturer.updateLecturer(lecturer);
      this.toggleModalInfo();
      this.getLecturerList();
      toastSuccess("Chỉnh sửa thành công");
    } catch (err) {
      toastError(err);
    }
  };

  deleteLecturer = async () => {
    try {
      await ApiLecturer.deleteLecturer(this.state.itemId);
      this.toggleDeleteModal();
      this.getLecturerList();
      toastSuccess("Xóa thành công");
    } catch (err) {
      toastError(err);
    }
  };

  saveLecturer = () => {
    let { id } = this.state.item;
    if (id) {
      this.updateLecturer();
    } else {
      this.addLecturer();
    }
  };

  onSubmit(e) {
    e.preventDefault();
    this.form.validateAll();
    this.saveLecturer();
  }

  onDateOfBirthChange = (el) => {
    let inputValue = el._d;
    let item = Object.assign({}, this.state.item);
    item["dateOfBirth"] = inputValue;
    this.setState({ item });
  };

  componentDidMount() {
    this.getLecturerList();
  }

  render() {
    const { isShowDeleteModal, isShowInfoModal } = this.state;
    const { item, params = {} } = this.state;
    const { lecturerPagedList } = this.props.lecturerPagedListReducer;
    const { sources, pageIndex, totalPages } = lecturerPagedList;
    const hasResults =
      lecturerPagedList.sources && lecturerPagedList.sources.length > 0;
    return (
      <div className="animated fadeIn">
        <ModalConfirm
          clickOk={this.deleteLecturer}
          isShowModal={isShowDeleteModal}
          toggleModal={this.toggleDeleteModal}
        />

        <ModalInfo
          title={this.state.formTitle}
          isShowModal={isShowInfoModal}
          hiddenFooter
        >
          <div className="modal-wrapper">
            <div className="form-wrapper">
              <Form
                onSubmit={(e) => this.onSubmit(e)}
                ref={(c) => {
                  this.form = c;
                }}
              >
                <Row>
                  <Col>
                    <FormGroup>
                      <ValidationInput
                        name="name"
                        title="Tên"
                        type="text"
                        required={true}
                        value={item.name}
                        onChange={this.onModelChange}
                      />
                    </FormGroup>
                  </Col>
                </Row>
                <Row>
                  <Col>
                    <Label>Giới tính</Label>
                    <FormGroup>
                      <div>
                        <select
                          className="select-custom"
                          name="gender"
                          required={false}
                          onChange={this.onModelChange}
                        >
                          {gender.GENDER.map((item, i) => {
                            return (
                              <option
                                selected={this.state.item.gender === item.name}
                                value={item.id}
                                key={i}
                              >
                                {item.name}
                              </option>
                            );
                          })}
                        </select>
                      </div>
                    </FormGroup>
                  </Col>
                </Row>

                <Row>
                  <Col>
                    <FormGroup>
                      <Label for="examplePassword"> Ngày sinh </Label>
                      <Datetime
                        defaultValue={
                          item.dateOfBirth
                            ? moment(item.dateOfBirth)
                                .add(7, "h")
                                .format("DD-MM-YYYY")
                            : ""
                        }
                        dateFormat="DD-MM-YYYY"
                        timeFormat=""
                        onChange={this.onDateOfBirthChange}
                      />
                    </FormGroup>
                  </Col>
                </Row>

                <Row>
                  <Col>
                    <Label>Học vị</Label>{" "}
                    <FormGroup>
                      <div>
                        <select
                          className="select-custom"
                          name="academicDegree"
                          required={true}
                          onChange={this.onModelChange}
                          value={this.state.academicDegree}
                        >
                          {academicDegree.ACADEMICDEGREE.map((item) => {
                            return (
                              <option value={item.name}>{item.name}</option>
                            );
                          })}
                        </select>
                      </div>
                    </FormGroup>
                  </Col>
                </Row>
                <Row>
                  <Col>
                    <Label>Học hàm</Label>{" "}
                    <FormGroup>
                      <div>
                        <select
                          className="select-custom"
                          name="academicRank"
                          required={true}
                          onChange={this.onModelChange}
                          value={this.state.academicRank}
                        >
                          {academicRank.ACADEMICRANK.map((item) => {
                            return (
                              <option value={item.name}>{item.name}</option>
                            );
                          })}
                        </select>
                      </div>
                    </FormGroup>
                  </Col>
                </Row>
                <Row>
                  <Col>
                    <Label>Khoa</Label>{" "}
                    <FormGroup>
                      <div>
                        <select
                          className="select-custom"
                          name="faculty"
                          required={true}
                          onChange={this.onModelChange}
                          value={this.state.faculty}
                        >
                          {faculty.FACULTY.map((item) => {
                            return (
                              <option value={item.name}>{item.name}</option>
                            );
                          })}
                        </select>
                      </div>
                    </FormGroup>
                  </Col>
                </Row>

                <div className="text-center">
                  <Button color="danger" type="submit">
                    Xác nhận
                  </Button>{" "}
                  <Button color="secondary" onClick={this.toggleModalInfo}>
                    Hủy bỏ
                  </Button>
                </div>
              </Form>
            </div>
          </div>
        </ModalInfo>

        <Row>
          <Col xs="12">
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
                    onClick={() =>
                      this.props.getLecturerList({
                        startTime: this.state.params.startTime,
                        endTime: this.state.params.endTime,
                      })
                    }
                  >
                    Xác nhận
                  </Button>
                </Col>
              </Row>
            </div>
            <br />
            <div className="flex-container header-table">
              <Button
                onClick={this.showAddNew}
                className="btn btn-pill btn-success btn-sm"
              >
                Tạo mới
              </Button>

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
                  <th>Giới tính</th>
                  <th>Khoa</th>
                  <th>Học vị</th>
                  <th>Học hàm</th>
                  <th>Tổng điểm</th>
                  <th>Số giờ quy đổi</th>
                  <th>Thao tác</th>
                </tr>
              </thead>
              <tbody>
                {hasResults &&
                  sources.map((item, index) => {
                    return (
                      <tr key={item.id}>
                        <td>{index + 1}</td>
                        <td onClick={() => {}}>
                          <Link
                            to={{
                              pathname: `/lecturers/${item.id}`,
                              state: {
                                startTime: params.startTime,
                                endTime: params.endTime,
                              },
                            }}
                          >
                            {item.name}
                          </Link>
                        </td>
                        <td>
                          {moment(item.dateOfBirth)
                            .add(7, "h")
                            .format("DD-MM-YYYY")}
                        </td>
                        <td>{item.gender}</td>
                        <td>{item.faculty}</td>
                        <td>{item.academicDegree}</td>
                        <td>{item.academicRank}</td>
                        <td>{item.total}</td>
                        <td>{item.totalHour}</td>
                        <td>
                          <Button
                            className="btn-sm"
                            color="secondary"
                            onClick={() => this.showUpdateModal(item)}
                          >
                            Sửa
                          </Button>
                          <Button
                            className="btn-sm"
                            color="danger"
                            onClick={() => this.showConfirmDelete(item.id)}
                          >
                            Xóa
                          </Button>
                        </td>
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
