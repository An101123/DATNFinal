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
import { getScientificWorkList } from "../../../actions/scientificWork.list.action";
import ApiScientificWork from "../../../api/api.scientificWork";
import { pagination } from "../../../constant/app.constant";
import ApiLevel from "../../../api/api.level";
import ApiLecturer from "../../../api/api.lecturer";
import "../../../pages/admin/select-custom.css";
import ScientificWorkDetail from "./scientificWork.detail";
import CKEditorInput from "../../../components/common/ckeditor-input";
import { Select } from "antd";

class ScientificWorkListPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isShowDeleteModal: false,
      isShowInfoModal: false,
      isShowContentModal: false,
      isShowDetail: false,
      item: {},
      itemId: null,
      levels: [],
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
    let title = "Tạo mới công trình khoa học";
    let scientificWork = {
      name: "",
      time: "",
      content: "",
      level: "",
      lecturer: "",
    };
    this.toggleModalInfo(scientificWork, title);
  };

  showUpdateModal = (item) => {
    let title = "Chỉnh sửa công trình khoa học";
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

  onLecturerChange = (value) => {
    let item = Object.assign({}, this.state.item);
    item.lecturerIds = value;
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
        this.getScientificWorkList();
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

  handlePageClick = (e) => {
    this.setState(
      {
        params: {
          ...this.state.params,
          skip: e.selected + 1,
        },
      },
      () => this.getScientificWorkList()
    );
  };

  getScientificWorkList = () => {
    let params = Object.assign({}, this.state.params, {
      query: this.state.query,
    });
    this.props.getScientificWorkList(params);
  };

  getLevelList = () => {
    ApiLevel.getAllLevel().then((values) => {
      this.setState({ levels: values });
    });
  };

  getLecturerList = () => {
    ApiLecturer.getAllLecturer().then((values) => {
      this.setState({ lecturers: values });
    });
  };

  addScientificWork = async () => {
    const { name, time, content, levelId, lecturerIds } = this.state.item;
    const scientificWork = { name, time, content, levelId, lecturerIds };
    try {
      await ApiScientificWork.postScientificWork(scientificWork);
      this.toggleModalInfo();
      this.getScientificWorkList();
      toastSuccess("Tạo mới thành công");
    } catch (err) {
      toastError(err);
    }
  };

  updateScientificWork = async () => {
    const { id, name, time, content } = this.state.item;
    const levelId = this.state.item.level.id;
    const lecturerIds = this.state.item.lecturerIds
      ? this.state.item.lecturerIds
      : this.state.item.lecturers.map((lecturer) => lecturer.id);
    const scientificWork = { id, name, time, content, levelId, lecturerIds };
    try {
      await ApiScientificWork.updateScientificWork(scientificWork);
      this.toggleModalInfo();
      this.getScientificWorkList();
      toastSuccess("Đã chỉnh sửa");
    } catch (err) {
      toastError(err);
    }
  };

  deleteScientificWork = async () => {
    try {
      await ApiScientificWork.deleteScientificWork(this.state.itemId);
      this.toggleDeleteModal();
      this.getScientificWorkList();
      toastSuccess("Xóa thành công");
    } catch (err) {
      toastError(err);
    }
  };

  saveScientificWork = () => {
    let { id } = this.state.item;
    if (id) {
      this.updateScientificWork();
    } else {
      this.addScientificWork();
    }
  };

  onSubmit(e) {
    e.preventDefault();
    this.form.validateAll();
    this.saveScientificWork();
  }

  componentDidMount() {
    this.getScientificWorkList();
    this.getLevelList();
    this.getLecturerList();
  }

  render() {
    const {
      isShowDeleteModal,
      isShowInfoModal,
      isShowContentModal,
      isShowDetail,
      item,
      levels,
      lecturers,
      content,
    } = this.state;
    const {
      scientificWorkPagedList,
    } = this.props.scientificWorkPagedListReducer;
    const { sources, pageIndex, totalPages } = scientificWorkPagedList;
    const hasResults =
      scientificWorkPagedList.sources &&
      scientificWorkPagedList.sources.length > 0;
    const { Option } = Select;

    return (
      <div className="animated fadeIn">
        <ModalConfirm
          clickOk={this.deleteScientificWork}
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
                        Cấp<span className="text-danger"> *</span>
                      </Label>
                      <br />
                      <select
                        className="select-custom"
                        defaultValue={item.level ? item.level.id : ""}
                        id="selectLevel"
                        name="levelId"
                        onChange={this.onModelChange}
                      >
                        <option style={{ display: "none" }} key="null">
                          -- Chọn --
                        </option>
                        {levels.length > 0
                          ? levels.map((level, i) => (
                              <option key={i} value={level.id}>
                                {level.name}
                              </option>
                            ))
                          : ""}
                      </select>
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
                    <th>Công trình khoa học</th>
                    <th>Thời gian</th>
                    <th>Cấp</th>
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

                          <td>{item.level.name}</td>
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
          <ScientificWorkDetail
            ScientificWork={item}
            backToAdminPage={this.backToAdminPage}
          />
        )}
      </div>
    );
  }
}

export default connect(
  (state) => ({
    scientificWorkPagedListReducer: state.scientificWorkPagedListReducer,
  }),
  {
    getScientificWorkList,
  }
)(ScientificWorkListPage);
