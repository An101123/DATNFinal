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
import { getPublishBookList } from "../../../actions/publishBook.list.action";
import ApiPublishBook from "../../../api/api.publishBook";
import { pagination } from "../../../constant/app.constant";
import ApiBookCategory from "../../../api/api.bookCategory";
import ApiLecturer from "../../../api/api.lecturer";
import "../../../pages/admin/select-custom.css";
import { Select } from "antd";

class PublishBookListPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isShowDeleteModal: false,
      isShowInfoModal: false,
      isShowContentModal: false,
      item: {},
      itemId: null,
      bookCategorys: [],
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
    let publishBook = {
      name: "",
      time: "",
      bookCategory: "",
      lecturer: "",
      placeOfPublication: "",
    };
    this.toggleModalInfo(publishBook, title);
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
        this.getPublishBookList();
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
      () => this.getPublishBookList()
    );
  };

  getPublishBookList = () => {
    let params = Object.assign({}, this.state.params, {
      query: this.state.query,
    });
    this.props.getPublishBookList(params);
  };

  getBookCategoryList = () => {
    ApiBookCategory.getAllBookCategory().then((values) => {
      this.setState({ bookCategorys: values });
    });
  };

  getLecturerList = () => {
    ApiLecturer.getAllLecturer().then((values) => {
      this.setState({ lecturers: values });
    });
  };

  addPublishBook = async () => {
    console.log("state ==================");
    console.log(this.state);
    const {
      name,
      time,
      placeOfPublication,
      bookCategoryId,
      lecturerIds,
    } = this.state.item;
    const publishBook = {
      name,
      time,
      placeOfPublication,
      bookCategoryId,
      lecturerIds,
    };
    try {
      await ApiPublishBook.postPublishBook(publishBook);
      this.toggleModalInfo();
      this.getPublishBookList();
      toastSuccess("Tạo mới thành công");
    } catch (err) {
      toastError(err);
    }
  };

  updatePublishBook = async () => {
    const { id, name, time, placeOfPublication } = this.state.item;
    const bookCategoryId = this.state.item.bookCategory.id;
    const lecturerIds = this.state.item.lecturerIds
      ? this.state.item.lecturerIds
      : this.state.item.lecturers.map((lecturer) => lecturer.id);
    const publishBook = {
      id,
      name,
      time,
      placeOfPublication,
      bookCategoryId,
      lecturerIds,
    };

    try {
      await ApiPublishBook.updatePublishBook(publishBook);
      this.toggleModalInfo();
      this.getPublishBookList();
      toastSuccess("Đã chỉnh sửa");
    } catch (err) {
      toastError(err);
    }
  };

  deletePublishBook = async () => {
    try {
      await ApiPublishBook.deletePublishBook(this.state.itemId);
      this.toggleDeleteModal();
      this.getPublishBookList();
      toastSuccess("Xóa thành công");
    } catch (err) {
      toastError(err);
    }
  };

  savePublishBook = () => {
    let { id } = this.state.item;
    if (id) {
      this.updatePublishBook();
    } else {
      this.addPublishBook();
    }
  };

  onSubmit(e) {
    e.preventDefault();
    this.form.validateAll();
    this.savePublishBook();
  }

  componentDidMount() {
    this.getPublishBookList();
    this.getBookCategoryList();
    this.getLecturerList();
  }

  render() {
    const {
      isShowDeleteModal,
      isShowInfoModal,
      isShowContentModal,
      isShowDetail,
      item,
      bookCategorys,
      lecturers,
      content,
    } = this.state;
    const { publishBookPagedList } = this.props.publishBookPagedListReducer;
    const { sources, pageIndex, totalPages } = publishBookPagedList;
    const hasResults =
      publishBookPagedList.sources && publishBookPagedList.sources.length > 0;
    const { Option } = Select;

    return (
      <div className="animated fadeIn">
        <ModalConfirm
          clickOk={this.deletePublishBook}
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
                        title="Tên sách"
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
                      <Label className="label-input">
                        Thể loại<span className="text-danger"> *</span>
                      </Label>
                      <br />
                      <select
                        className="select-custom"
                        defaultValue={
                          item.bookCategory ? item.bookCategory.id : ""
                        }
                        id="selectBookCategory"
                        name="bookCategoryId"
                        onChange={this.onModelChange}
                      >
                        <option
                          style={{ textAlign: "center", display: "none" }}
                          key="null"
                        >
                          -- Chọn --
                        </option>
                        {bookCategorys.length > 0
                          ? bookCategorys.map((bookCategory, i) => (
                              <option key={i} value={bookCategory.id}>
                                {bookCategory.name}
                              </option>
                            ))
                          : ""}
                      </select>
                      {/* <Label
                        id="bookCategoryWarning"
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
                      <ValidationInput
                        name="placeOfPublication"
                        title="Nơi xuất bản"
                        type="text"
                        required={true}
                        value={item.placeOfPublication}
                        onChange={this.onModelChange}
                      />
                    </FormGroup>
                  </Col>
                </Row>
                <Row>
                  <Col>
                    <FormGroup>
                      <Label> Thời gian </Label>

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
                  <th>Xuất bản sách</th>
                  <th>Thời gian</th>
                  <th>Thể loại</th>
                  <th>Nơi xuất bản</th>
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
                        <td> {item.name}</td>
                        <td>
                          {moment(item.time).add(7, "h").format("DD-MM-YYYY")}
                        </td>

                        <td>{item.bookCategory.name}</td>
                        <td>{item.placeOfPublication}</td>
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
      </div>
    );
  }
}

export default connect(
  (state) => ({
    publishBookPagedListReducer: state.publishBookPagedListReducer,
  }),
  {
    getPublishBookList,
  }
)(PublishBookListPage);
