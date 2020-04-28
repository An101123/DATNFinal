import React, { Component } from "react";
import { connect } from "react-redux";
import {
  Row,
  Col,
  Button,
  FormGroup,
  Table,
  Label,
  Modal,
  ModalHeader,
  ModalBody,
  ModalFooter,
} from "reactstrap";
import Form from "react-validation/build/form";
import Datetime from "react-datetime";
import moment from "moment";
import ModalConfirm from "../../../components/modal/modal-confirm";
import Pagination from "../../../components/pagination/Pagination";
import ModalInfo from "../../../components/modal/modal-info";
import ValidationInput from "../../../components/common/validation-input";
import { toastSuccess, toastError } from "../../../helpers/toast.helper";
import lodash from "lodash";
import { getOtherScientificWorkList } from "../../../actions/otherScientificWork.list.action";
import ApiOtherScientificWork from "../../../api/api.otherScientificWork";
import { pagination } from "../../../constant/app.constant";
import ApiClassificationOfScientificWork from "../../../api/api.classificationOfScientificWork";
import ApiLecturer from "../../../api/api.lecturer";
import "../../../pages/admin/select-custom.css";
import { Select } from "antd";

class OtherScientificWorkListPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isShowDeleteModal: false,
      isShowInfoModal: false,
      isShowContentModal: false,
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

  backToAdminPage = () => {
    this.setState((prevState) => ({
      isShowDetail: !prevState.isShowDetail,
    }));
  };

  toggleDeleteModal = () => {
    this.setState((prevState) => ({
      isShowDeleteModal: !prevState.isShowDeleteModal,
    }));
  };

  toggleModalInfo = (item, title) => {
    this.setState((prevState) => ({
      isShowInfoModal: !prevState.isShowInfoModal,
      item: item || {},
      formTitle: title,
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
    let title = "Tạo mới ";
    let otherScientificWork = {
      name: "",
      time: "",
      classificationOfScientificWork: "",
      lecturer: "",
    };
    this.toggleModalInfo(otherScientificWork, title);
  };

  showUpdateModal = (item) => {
    let title = "Chỉnh sửa  ";
    this.toggleModalInfo(item, title);
  };
  showContentModal = (item) => {
    let title = item.name;
    this.toggleContentModal(item, title);
  };

  onModelChange = (el) => {
    let inputName = el.target.name;
    let inputValue = el.target.value;
    let item = Object.assign({}, this.state.item);
    item[inputName] = inputValue;
    this.setState({ item });
    console.log(item);
  };

  onTimeChange = (el) => {
    let inputValue = el._d;
    let item = Object.assign({}, this.state.item);
    item["time"] = inputValue;
    this.setState({ item });
  };
  onLecturerChange = (value) => {
    let item = Object.assign({}, this.state.item);
    item.lecturerIds = value;
    this.setState({ item });
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

  addOtherScientificWork = async () => {
    const {
      name,
      time,
      classificationOfScientificWorkId,
      lecturerIds,
    } = this.state.item;
    const otherScientificWork = {
      name,
      time,
      classificationOfScientificWorkId,
      lecturerIds,
    };
    try {
      await ApiOtherScientificWork.postOtherScientificWork(otherScientificWork);
      this.toggleModalInfo();
      this.getOtherScientificWorkList();
      toastSuccess("Tạo mới thành công");
    } catch (err) {
      toastError(err);
    }
  };

  updateOtherScientificWork = async () => {
    const { id, name, time } = this.state.item;
    const classificationOfScientificWorkId = this.state.item
      .classificationOfScientificWork.id;
    const lecturerIds = this.state.item.lecturerIds
      ? this.state.item.lecturerIds
      : this.state.item.lecturers.map((lecturer) => lecturer.id);
    const otherScientificWork = {
      id,
      name,
      time,
      classificationOfScientificWorkId,
      lecturerIds,
    };

    try {
      await ApiOtherScientificWork.updateOtherScientificWork(
        otherScientificWork
      );
      this.toggleModalInfo();
      this.getOtherScientificWorkList();
      toastSuccess("Đã chỉnh sửa");
    } catch (err) {
      toastError(err);
    }
  };

  deleteOtherScientificWork = async () => {
    try {
      await ApiOtherScientificWork.deleteOtherScientificWork(this.state.itemId);
      this.toggleDeleteModal();
      this.getOtherScientificWorkList();
      toastSuccess("Xóa thành công");
    } catch (err) {
      toastError(err);
    }
  };

  saveOtherScientificWork = () => {
    let { id } = this.state.item;
    if (id) {
      this.updateOtherScientificWork();
    } else {
      this.addOtherScientificWork();
    }
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
      isShowDeleteModal,
      isShowInfoModal,
      item,
      classificationOfScientificWorks,
      lecturers,
    } = this.state;
    const {
      otherScientificWorkPagedList,
    } = this.props.otherScientificWorkPagedListReducer;
    const { sources, pageIndex, totalPages } = otherScientificWorkPagedList;
    const hasResults =
      otherScientificWorkPagedList.sources &&
      otherScientificWorkPagedList.sources.length > 0;
    const { Option } = Select;

    return (
      <div className="animated fadeIn">
        <ModalConfirm
          clickOk={this.deleteOtherScientificWork}
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
                        title="Tên công trình khoa học"
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
                    <FormGroup>
                      <Label for="examplePassword"> Thời gian </Label>

                      <Datetime
                        className="select-custom"
                        defaultValue={
                          item.time
                            ? moment(item.time).add(7, "h").format("YYYY")
                            : ""
                        }
                        dateFormat="YYYY"
                        timeFormat=""
                        onChange={this.onTimeChange}
                      />
                    </FormGroup>
                  </Col>
                </Row>

                <Row>
                  <Col>
                    <FormGroup>
                      <Label className="label-input">
                        Phân loại<span className="text-danger"> *</span>
                      </Label>
                      <br />
                      <select
                        className="select-custom"
                        defaultValue={
                          item.classificationOfScientificWork
                            ? item.classificationOfScientificWork.id
                            : ""
                        }
                        id="selectClassificationOfScientificWork"
                        name="classificationOfScientificWorkId"
                        onChange={this.onModelChange}
                      >
                        <option style={{ display: "none" }} key="null">
                          -- Chọn --
                        </option>
                        {classificationOfScientificWorks.length > 0
                          ? classificationOfScientificWorks.map(
                              (classificationOfScientificWork, i) => (
                                <option
                                  key={i}
                                  value={classificationOfScientificWork.id}
                                >
                                  {classificationOfScientificWork.name}
                                </option>
                              )
                            )
                          : ""}
                      </select>
                      {/* <Label
                        id="classificationOfScientificWorkWarning"
                        style={{
                          marginLeft: 20,
                          fontWeight: "bold",
                          opacity: "0"
                        }}
                      >
                        <span className="text-danger">Vui lòng chọn cấp</span>
                      </Label> */}
                    </FormGroup>
                  </Col>
                </Row>

                <Row>
                  <Col>
                    <FormGroup>
                      <Label className="label-input">
                        Giảng viên<span className="text-danger"> *</span>
                      </Label>
                      <br />
                      <Select
                        mode="multiple"
                        style={{ display: "block" }}
                        placeholder="Please select"
                        onChange={this.onLecturerChange}
                        defaultValue={
                          item.lecturers
                            ? item.lecturers.map((lecturer) => lecturer.id)
                            : undefined
                        }
                      >
                        {lecturers.length > 0
                          ? lecturers.map((lecturer, i) => (
                              <Option key={i} value={lecturer.id}>
                                {lecturer.name}
                              </Option>
                            ))
                          : ""}
                      </Select>
                      <Label
                        id="lecturerWarning"
                        style={{
                          marginLeft: 20,
                          fontWeight: "bold",
                          opacity: "0",
                        }}
                      >
                        <span className="text-danger">
                          Vui lòng chọn giảng viên
                        </span>
                      </Label>
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
                  <th>Công trình khoa học</th>
                  <th>Thời gian</th>
                  <th>Phân loại</th>
                  <th>Giảng viên</th>
                  <th>Thao tác</th>
                </tr>
              </thead>
              <tbody>
                {hasResults &&
                  sources.map((item, index) => {
                    return (
                      <tr key={item.id}>
                        <td>{index + 1}</td>
                        <td onClick={() => this.toggleDetailPage(item)}>
                          {item.name.length > 100 ? (
                            <span>
                              {item.name.substr(0, 100)}{" "}
                              <span style={{ fontWeight: "bolder" }}> ...</span>
                            </span>
                          ) : (
                            item.name
                          )}
                        </td>
                        <td>{moment(item.time).add(7, "h").format("YYYY")}</td>
                        <td>{item.classificationOfScientificWork.name}</td>
                        <td>
                          {item.lecturers.map(
                            (lecturer) => lecturer.name + "; "
                          )}
                        </td>{" "}
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
    otherScientificWorkPagedListReducer:
      state.otherScientificWorkPagedListReducer,
  }),
  {
    getOtherScientificWorkList,
  }
)(OtherScientificWorkListPage);
