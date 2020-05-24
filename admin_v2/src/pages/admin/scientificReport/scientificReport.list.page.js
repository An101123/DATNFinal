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
import { getScientificReportList } from "../../../actions/scientificReport.list.action";
import ApiScientificReport from "../../../api/api.scientificReport";
import { pagination } from "../../../constant/app.constant";
import ApiScientificReportType from "../../../api/api.scientificReportType";
import ApiLecturer from "../../../api/api.lecturer";
import "../../../pages/admin/select-custom.css";
import ScientificReportDetail from "./scientificReport.detail";
import CKEditorInput from "../../../components/common/ckeditor-input";
import { Select } from "antd";

class ScientificReportListPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isShowDeleteModal: false,
      isShowInfoModal: false,
      isShowContentModal: false,
      isShowDetail: false,
      item: {},
      itemId: null,
      scientificReportTypes: [],
      lecturers: [],
      content: null,
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

  toggleContentModal = (item, title) => {
    this.setState((prevState) => ({
      isShowContentModal: !prevState.isShowContentModal,
      content: item.content || null,
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
    let title = "Tạo mới bài báo, báo cáo khoa học";
    let scientificReport = {
      name: "",
      time: "",
      content: "",
      scientificReportType: "",
      lecturer: "",
    };
    this.toggleModalInfo(scientificReport, title);
  };

  showUpdateModal = (item) => {
    let title = "Chỉnh sửa bài báo, báo cáo khoa học";
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
  };

  onTimeChange = (el) => {
    let inputValue = el._d;
    let item = Object.assign({}, this.state.item);
    item["time"] = inputValue;
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
        this.getScientificReportList();
      }
    );
  };

  onContentChange = (e) => {
    let item = Object.assign({}, this.state.item);
    item.content = e.editor.getData();
    this.setState({ item });
  };

  onSearchChange = (e) => {
    e.persist();
    this.delayedCallback(e);
  };

  onLecturerChange = (value) => {
    let item = Object.assign({}, this.state.item);
    item.lecturerIds = value;
    this.setState({ item });
  };

  handlePageClick = (e) => {
    this.setState(
      {
        params: {
          ...this.state.params,
          skip: e.selected + 1,
        },
      },
      () => this.getScientificReportList()
    );
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

  addScientificReport = async () => {
    console.log("state ==================");
    console.log(this.state);
    const {
      name,
      time,
      content,
      scientificReportTypeId,
      lecturerIds,
    } = this.state.item;
    const scientificReport = {
      name,
      time,
      content,
      scientificReportTypeId,
      lecturerIds,
    };
    try {
      await ApiScientificReport.postScientificReport(scientificReport);
      this.toggleModalInfo();
      this.getScientificReportList();
      toastSuccess("Tạo mới thành công");
    } catch (err) {
      toastError(err);
    }
  };

  updateScientificReport = async () => {
    const { id, name, time, content } = this.state.item;
    const scientificReportTypeId = this.state.item.scientificReportType.id;
    const lecturerIds = this.state.item.lecturerIds
      ? this.state.item.lecturerIds
      : this.state.item.lecturers.map((lecturer) => lecturer.id);
    const scientificReport = {
      id,
      name,
      time,
      content,
      scientificReportTypeId,
      lecturerIds,
    };
    if (
      !scientificReportTypeId ||
      scientificReportTypeId === "--Select ScientificReportType--"
    ) {
      document.getElementById("scientificReportTypeWarning").style.opacity =
        "1";
    } else {
      document.getElementById("scientificReportTypeWarning").style.opacity =
        "0";
      try {
        await ApiScientificReport.updateScientificReport(scientificReport);
        this.toggleModalInfo();
        this.getScientificReportList();
        toastSuccess("Đã chỉnh sửa");
      } catch (err) {
        toastError(err);
      }
    }
  };

  deleteScientificReport = async () => {
    try {
      await ApiScientificReport.deleteScientificReport(this.state.itemId);
      this.toggleDeleteModal();
      this.getScientificReportList();
      toastSuccess("Xóa thành công");
    } catch (err) {
      toastError(err);
    }
  };

  saveScientificReport = () => {
    let { id } = this.state.item;
    if (id) {
      this.updateScientificReport();
    } else {
      this.addScientificReport();
    }
  };

  onSubmit(e) {
    e.preventDefault();
    this.form.validateAll();
    this.saveScientificReport();
  }

  componentDidMount() {
    this.getScientificReportList();
    this.getScientificReportTypeList();
    this.getLecturerList();
  }

  render() {
    const {
      isShowDeleteModal,
      isShowInfoModal,
      isShowContentModal,
      isShowDetail,
      item,
      scientificReportTypes,
      lecturers,
      content,
    } = this.state;
    const {
      scientificReportPagedList,
    } = this.props.scientificReportPagedListReducer;
    const { sources, pageIndex, totalPages } = scientificReportPagedList;
    const hasResults =
      scientificReportPagedList.sources &&
      scientificReportPagedList.sources.length > 0;
    const { Option } = Select;

    return (
      <div className="animated fadeIn">
        <ModalConfirm
          clickOk={this.deleteScientificReport}
          isShowModal={isShowDeleteModal}
          toggleModal={this.toggleDeleteModal}
        />

        <Modal isOpen={isShowContentModal}>
          <ModalHeader>{this.state.formTitle}</ModalHeader>
          <ModalBody>{content}</ModalBody>

          <ModalFooter className="justify-content-center">
            <Button color="secondary" onClick={this.toggleContentModal}>
              Đóng
            </Button>
          </ModalFooter>
        </Modal>

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
                        title="Tên bài báo, báo cáo khoa học"
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
                      <CKEditorInput
                        title="Nội dung"
                        name="content"
                        data={item.content}
                        required={true}
                        onChange={this.onContentChange}
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
                            ? moment(item.time).add(7, "h").format("DD-MM-YYYY")
                            : ""
                        }
                        dateFormat="DD-MM-YYYY"
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
                        Loại<span className="text-danger"> *</span>
                      </Label>
                      <br />
                      <select
                        className="select-custom"
                        defaultValue={
                          item.scientificReportType
                            ? item.scientificReportType.id
                            : ""
                        }
                        id="selectScientificReportType"
                        name="scientificReportTypeId"
                        onChange={this.onModelChange}
                      >
                        <option style={{ display: "none" }} key="null">
                          -- Chọn --
                        </option>
                        {scientificReportTypes.length > 0
                          ? scientificReportTypes.map(
                              (scientificReportType, i) => (
                                <option key={i} value={scientificReportType.id}>
                                  {scientificReportType.name}
                                </option>
                              )
                            )
                          : ""}
                      </select>
                      <Label
                        id="scientificReportTypeWarning"
                        style={{
                          marginLeft: 20,
                          fontWeight: "bold",
                          opacity: "0",
                        }}
                      >
                        <span className="text-danger">
                          Vui lòng chọn loại bài báo
                        </span>
                      </Label>
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

        {!isShowDetail ? (
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
                    <th>Bài báo - Báo cáo khoa học</th>
                    <th style={{ width: "150px" }}>Thời gian</th>
                    <th>Loại</th>
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
                            {moment(item.time).add(7, "h").format("DD-MM-YYYY")}
                          </td>
                          {/* <td>
                          {item.content.length > 50 ? (
                            <span onClick={() => this.showContentModal(item)}>
                              {item.content.substr(0, 50)}{" "}
                              <span style={{ fontWeight: "bolder" }}> ...</span>
                            </span>
                          ) : (
                            item.content
                          )}
                        </td> */}
                          <td>{item.scientificReportType.name}</td>
                          <td>
                            {item.lecturers.map(
                              (lecturer) => lecturer.name + "; "
                            )}
                          </td>

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
        ) : (
          <ScientificReportDetail
            ScientificReport={item}
            backToAdminPage={this.backToAdminPage}
          />
        )}
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
