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
import { getStudyGuideList } from "../../../actions/studyGuide.list.action";
import ApiStudyGuide from "../../../api/api.studyGuide";
import { pagination } from "../../../constant/app.constant";
import ApiLevelStudyGuide from "../../../api/api.levelStudyGuide";
import ApiLecturer from "../../../api/api.lecturer";
import "../../../pages/admin/select-custom.css";
import CKEditorInput from "../../../components/common/ckeditor-input";

class StudyGuideListPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isShowDeleteModal: false,
      isShowInfoModal: false,
      isShowContentModal: false,
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
    let title = "Tạo mới";
    let studyGuide = {
      name: "",
      literacy: "",
      instructionTime: "",
      graduationTime: "",
      placeOfTraining: "",
      levelStudyGuide: "",
      lecturer: "",
    };
    this.toggleModalInfo(studyGuide, title);
  };

  showUpdateModal = (item) => {
    let title = "Chỉnh sửa";
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

  onInstructionTimeChange = (el) => {
    let inputValue = el._d;
    let item = Object.assign({}, this.state.item);
    item["instructionTime"] = inputValue;
    this.setState({ item });
  };
  onGraduationTimeChange = (el) => {
    let inputValue = el._d;
    let item = Object.assign({}, this.state.item);
    item["graduationTime"] = inputValue;

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

  addStudyGuide = async () => {
    const {
      name,
      literacy,
      instructionTime,
      graduationTime,
      placeOfTraining,
      levelStudyGuideId,
      lecturerId,
    } = this.state.item;
    const studyGuide = {
      name,
      literacy,
      instructionTime,
      graduationTime,
      placeOfTraining,
      levelStudyGuideId,
      lecturerId,
    };
    console.log(studyGuide);
    try {
      await ApiStudyGuide.postStudyGuide(studyGuide);
      this.toggleModalInfo();
      this.getStudyGuideList();
      toastSuccess("Tạo mới thành công");
    } catch (err) {
      toastError(err);
    }
  };

  updateStudyGuide = async () => {
    const {
      id,
      name,
      literacy,
      instructionTime,
      graduationTime,
      placeOfTraining,
    } = this.state.item;
    const levelStudyGuideId = this.state.item.levelStudyGuide.id;
    const lecturerId = this.state.item.lecturer.id;
    const studyGuide = {
      id,
      name,
      literacy,
      instructionTime,
      graduationTime,
      placeOfTraining,
      levelStudyGuideId,
      lecturerId,
    };

    try {
      await ApiStudyGuide.updateStudyGuide(studyGuide);
      this.toggleModalInfo();
      this.getStudyGuideList();
      toastSuccess("Đã chỉnh sửa");
    } catch (err) {
      toastError(err);
    }
  };

  deleteStudyGuide = async () => {
    try {
      await ApiStudyGuide.deleteStudyGuide(this.state.itemId);
      this.toggleDeleteModal();
      this.getStudyGuideList();
      toastSuccess("Xóa thành công");
    } catch (err) {
      toastError(err);
    }
  };

  saveStudyGuide = () => {
    let { id } = this.state.item;
    if (id) {
      this.updateStudyGuide();
    } else {
      this.addStudyGuide();
    }
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
    const {
      isShowDeleteModal,
      isShowInfoModal,
      isShowContentModal,
      item,
      levelStudyGuides,
      lecturers,
    } = this.state;
    const { studyGuidePagedList } = this.props.studyGuidePagedListReducer;
    const { sources, pageIndex, totalPages } = studyGuidePagedList;
    const hasResults =
      studyGuidePagedList.sources && studyGuidePagedList.sources.length > 0;
    return (
      <div className="animated fadeIn">
        <ModalConfirm
          clickOk={this.deleteStudyGuide}
          isShowModal={isShowDeleteModal}
          toggleModal={this.toggleDeleteModal}
        />
        <Modal isOpen={isShowContentModal}>
          <ModalHeader>{this.state.formTitle}</ModalHeader>

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
                        title="Họ và tên, Tên đề tài"
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
                      <ValidationInput
                        name="literacy"
                        title="Trình độ"
                        type="text"
                        required={true}
                        value={item.literacy}
                        onChange={this.onModelChange}
                      />
                    </FormGroup>
                  </Col>
                </Row>

                <Row>
                  <Col>
                    <FormGroup>
                      <ValidationInput
                        name="placeOfTraining"
                        title="Cơ sở đào tạo"
                        type="text"
                        required={true}
                        value={item.placeOfTraining}
                        onChange={this.onModelChange}
                      />
                    </FormGroup>
                  </Col>
                </Row>

                <Row>
                  <Col>
                    <FormGroup>
                      <Label> Năm hướng dẫn </Label>

                      <Datetime
                        className="select-custom"
                        defaultValue={
                          item.instructionTime
                            ? moment(item.instructionTime)
                                .add(7, "h")
                                .format("DD-MM-YYYY")
                            : ""
                        }
                        dateFormat="DD-MM-YYYY"
                        timeFormat=""
                        onChange={this.onInstructionTimeChange}
                      />
                    </FormGroup>
                  </Col>
                </Row>

                <Row>
                  <Col>
                    <FormGroup>
                      <Label> Năm bảo vệ </Label>

                      <Datetime
                        className="select-custom"
                        defaultValue={
                          item.graduationTime
                            ? moment(item.graduationTime)
                                .add(7, "h")
                                .format("DD-MM-YYYY")
                            : ""
                        }
                        dateFormat="DD-MM-YYYY"
                        timeFormat=""
                        onChange={this.onGraduationTimeChange}
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
                        defaultValue={
                          item.levelStudyGuide ? item.levelStudyGuide.id : ""
                        }
                        id="selectLevelStudyGuide"
                        name="levelStudyGuideId"
                        onChange={this.onModelChange}
                      >
                        <option
                          style={{ textAlign: "center", display: "none" }}
                          key="null"
                        >
                          -- Chọn --
                        </option>
                        {levelStudyGuides.length > 0
                          ? levelStudyGuides.map((levelStudyGuide, i) => (
                              <option key={i} value={levelStudyGuide.id}>
                                {levelStudyGuide.name}
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
                      <select
                        className="select-custom"
                        defaultValue={item.lecturer ? item.lecturer.id : ""}
                        id="selectLecturer"
                        name="lecturerId"
                        onChange={this.onModelChange}
                      >
                        <option style={{ display: "none" }}>-- Chọn --</option>
                        {lecturers.length > 0
                          ? lecturers.map((lecturer, i) => (
                              <option key={i} value={lecturer.id}>
                                {lecturer.name}
                              </option>
                            ))
                          : ""}
                      </select>
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
                  <th>Hướng dẫn sinh viên NCKH</th>
                  <th>Trình độ</th>
                  <th>Cơ sở đào tạo</th>
                  <th>Năm hướng dẫn</th>
                  <th>Năm bảo vệ</th>
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
                              <span style={{ fontWeight: "bolder" }}> ...</span>
                            </span>
                          ) : (
                            item.name
                          )}
                        </td>
                        <td>{item.literacy}</td>
                        <td>{item.placeOfTraining}</td>

                        <td>
                          {moment(item.instructionTime)
                            .add(7, "h")
                            .format("DD-MM-YYYY")}
                        </td>
                        <td>
                          {moment(item.graduationTime)
                            .add(7, "h")
                            .format("DD-MM-YYYY")}
                        </td>

                        <td>{item.levelStudyGuide.name}</td>
                        <td>{item.lecturer.name}</td>

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
    studyGuidePagedListReducer: state.studyGuidePagedListReducer,
  }),
  {
    getStudyGuideList,
  }
)(StudyGuideListPage);
