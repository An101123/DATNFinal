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
import { getNewsList } from "../../../actions/news.list.action";
import ApiNews from "../../../api/api.news";
import ImagePicker from "../../../components/common/image-picker";
import { pagination } from "../../../constant/app.constant";
import "../../../pages/admin/select-custom.css";
import { uploadFile } from "../../../helpers/upload_file.helper";
import CKEditorInput from "../../../components/common/ckeditor-input";
import NewsDetail from "./news.detail";

class NewsListPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isShowDeleteModal: false,
      isShowInfoModal: false,
      isShowDetail: false,

      item: {},
      image: null,
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

  backToAdminPage = () => {
    this.setState((prevState) => ({
      isShowDetail: !prevState.isShowDetail,
    }));
  };

  showAddNew = () => {
    let title = "Tạo mới tin tức";
    let news = {
      title: "",
      content: "",
      link: "",
      image: null,
    };
    this.toggleModalInfo(news, title);
  };

  onModelChange = (el) => {
    let inputName = el.target.name;
    let inputValue = el.target.value;
    let item = Object.assign({}, this.state.item);
    item[inputName] = inputValue;
    this.setState({ item });
  };

  onImageChange = (file) => {
    this.setState({ image: file });
    let reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => {
      document.getElementById("itemImage").src = reader.result;
    };
    reader.onerror = function (error) {
      console.log("Error: ", error);
    };
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
        this.getNewsList();
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
      () => this.getNewsList()
    );
  };

  getNewsList = () => {
    let params = Object.assign({}, this.state.params, {
      query: this.state.query,
    });
    this.props.getNewsList(params);
  };

  addNews = async () => {
    console.log("state ==================");
    console.log(this.state);
    const { title, content } = this.state.item;
    try {
      var image = await uploadFile("Image", this.state.image);
      const news = { title, content, image };
      await ApiNews.postNews(news);
      this.toggleModalInfo();
      this.getNewsList();
      toastSuccess("Tạo mới thành công");
    } catch (err) {
      toastError(err);
    }
  };

  deleteNews = async () => {
    try {
      await ApiNews.deleteNews(this.state.itemId);
      this.toggleDeleteModal();
      this.getNewsList();
      toastSuccess("Xóa thành công");
    } catch (err) {
      toastError(err);
    }
  };

  saveNews = () => {
    let { id } = this.state.item;
    if (id) {
      this.updateNews();
    } else {
      this.addNews();
    }
  };
  onContentChange = (e) => {
    let item = Object.assign({}, this.state.item);
    item.content = e.editor.getData();
    this.setState({ item });
  };

  onSubmit(e) {
    e.preventDefault();
    this.form.validateAll();
    this.saveNews();
  }

  componentDidMount() {
    this.getNewsList();
  }

  render() {
    const {
      isShowDeleteModal,
      isShowInfoModal,
      item,
      image,
      isShowDetail,
    } = this.state;
    const { newsPagedList } = this.props.newsPagedListReducer;
    const { sources, pageIndex, totalPages } = newsPagedList;
    const hasResults =
      newsPagedList.sources && newsPagedList.sources.length > 0;
    return (
      <div className="animated fadeIn">
        <ModalConfirm
          clickOk={this.deleteNews}
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
                        name="title"
                        title="Chủ đề"
                        type="text"
                        required={true}
                        value={item.title}
                        onChange={this.onModelChange}
                      />
                    </FormGroup>
                  </Col>
                </Row>

                {/* <Row>
                  <Col>
                    <FormGroup>
                      <ValidationInput
                        name="link"
                        title="Liên kết"
                        type="text"
                        required={true}
                        value={item.link}
                        onChange={this.onModelChange}
                      />
                    </FormGroup>
                  </Col>
                </Row> */}

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
                  <Col style={{ position: "relative" }}>
                    <ImagePicker
                      title="Image"
                      onImageChange={this.onImageChange}
                    />
                    {(item.image || image) && (
                      <span>
                        <img
                          id="itemImage"
                          alt=""
                          src={item.image && item.image}
                          width="100"
                          height="80"
                        />
                        <span
                          style={{ position: "absolute", left: 100 }}
                          className="fa fa-times"
                          onClick={() => this.setState({ image: null })}
                        />
                      </span>
                    )}
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
                    <th>Chủ đề</th>
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
                            {item.title}
                          </td>
                          <td>
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
          <NewsDetail News={item} backToAdminPage={this.backToAdminPage} />
        )}
      </div>
    );
  }
}

export default connect(
  (state) => ({
    newsPagedListReducer: state.newsPagedListReducer,
  }),
  {
    getNewsList,
  }
)(NewsListPage);
