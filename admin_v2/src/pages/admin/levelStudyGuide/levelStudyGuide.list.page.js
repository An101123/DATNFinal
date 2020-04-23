import React, { Component } from "react";
import { connect } from "react-redux";
import { Row, Col, Button, FormGroup, Table } from "reactstrap";
import Form from "react-validation/build/form";
import ModalConfirm from "../../../components/modal/modal-confirm";
import Pagination from "../../../components/pagination/Pagination";
import ModalInfo from "../../../components/modal/modal-info";
import ValidationInput from "../../../components/common/validation-input";
import { toastSuccess, toastError } from "../../../helpers/toast.helper";
import lodash from "lodash";
import { getLevelStudyGuideList } from "../../../actions/levelStudyGuide.list.action";
import ApiLevelStudyGuide from "../../../api/api.levelStudyGuide";
import { pagination } from "../../../constant/app.constant";
import "../../../pages/admin/select-custom.css";

class LevelStudyGuideListPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isShowDeleteModal: false,
      isShowInfoModal: false,
      item: {},
      itemId: null,
      params: {
        skip: pagination.initialPage,
        take: pagination.defaultTake,
      },
      query: "",
    };
    this.delayedCallback = lodash.debounce(this.search, 1000);
  }

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
    let title = "Tạo mới";
    let levelStudyGuide = {
      name: "",
      score: "",
      hoursConverted: "",
    };
    this.toggleModalInfo(levelStudyGuide, title);
  };

  showUpdateModal = (item) => {
    let title = "Chỉnh sửa";
    this.toggleModalInfo(item, title);
  };

  onModelChange = (el) => {
    let inputName = el.target.name;
    let inputValue = el.target.value;
    let item = Object.assign({}, this.state.item);
    item[inputName] = inputValue;
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
        this.getLevelStudyGuideList();
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
      () => this.getLevelStudyGuideList()
    );
  };

  getLevelStudyGuideList = () => {
    let params = Object.assign({}, this.state.params, {
      query: this.state.query,
    });
    this.props.getLevelStudyGuideList(params);
  };

  addLevelStudyGuide = async () => {
    const { name, score, hoursConverted } = this.state.item;
    const levelStudyGuide = { name, score, hoursConverted };
    try {
      await ApiLevelStudyGuide.postLevelStudyGuide(levelStudyGuide);
      this.toggleModalInfo();
      this.getLevelStudyGuideList();
      toastSuccess("Tạo mới thành công");
    } catch (err) {
      toastError(err);
    }
  };

  updateLevelStudyGuide = async () => {
    const { id, name, score, hoursConverted } = this.state.item;
    const levelStudyGuide = { id, name, score, hoursConverted };
    try {
      await ApiLevelStudyGuide.updateLevelStudyGuide(levelStudyGuide);
      this.toggleModalInfo();
      this.getLevelStudyGuideList();
      toastSuccess("Đã chỉnh sửa");
    } catch (err) {
      toastError(err);
    }
  };

  deleteLevelStudyGuide = async () => {
    try {
      await ApiLevelStudyGuide.deleteLevelStudyGuide(this.state.itemId);
      this.toggleDeleteModal();
      this.getLevelStudyGuideList();
      toastSuccess("Xóa thành công");
    } catch (err) {
      toastError(err);
    }
  };

  saveLevelStudyGuide = () => {
    let { id } = this.state.item;
    if (id) {
      this.updateLevelStudyGuide();
    } else {
      this.addLevelStudyGuide();
    }
  };

  onSubmit(e) {
    e.preventDefault();
    this.form.validateAll();
    this.saveLevelStudyGuide();
  }

  componentDidMount() {
    this.getLevelStudyGuideList();
  }

  render() {
    const { isShowDeleteModal, isShowInfoModal, item } = this.state;
    const {
      levelStudyGuidePagedList,
    } = this.props.levelStudyGuidePagedListReducer;
    const { sources, pageIndex, totalPages } = levelStudyGuidePagedList;
    const hasResults =
      levelStudyGuidePagedList.sources &&
      levelStudyGuidePagedList.sources.length > 0;
    return (
      <div className="animated fadeIn">
        <ModalConfirm
          clickOk={this.deleteLevelStudyGuide}
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
                        title="Tên cấp"
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
                        name="score"
                        title="Số điểm quy đổi"
                        type="number"
                        required={true}
                        value={item.score}
                        onChange={this.onModelChange}
                      />
                    </FormGroup>
                  </Col>
                </Row>

                <Row>
                  <Col>
                    <FormGroup>
                      <ValidationInput
                        name="hoursConverted"
                        title="Số giờ quy đổi"
                        type="number"
                        required={true}
                        value={item.hoursConverted}
                        onChange={this.onModelChange}
                      />
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
                  <th>Phân loại</th>
                  <th>Số điểm quy đổi</th>
                  <th>Số giờ chuyển đổi</th>
                  <th>Thao tác</th>
                </tr>
              </thead>
              <tbody>
                {hasResults &&
                  sources.map((item, index) => {
                    return (
                      <tr key={item.id}>
                        <td>{index + 1}</td>
                        <td>{item.name}</td>
                        <td>{item.score}</td>
                        <td>{item.hoursConverted}</td>
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
    levelStudyGuidePagedListReducer: state.levelStudyGuidePagedListReducer,
  }),
  {
    getLevelStudyGuideList,
  }
)(LevelStudyGuideListPage);