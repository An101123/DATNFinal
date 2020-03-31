import React, { Component } from "react";
import { connect } from "react-redux";
import lodash from "lodash";
import { getNewsList } from "../../../actions/news.list.action";
import { pagination } from "../../../constant/app.constant";
import "../../../pages/admin/select-custom.css";
import { Row, CardBody } from "reactstrap";
import { List } from "antd";
import "antd/dist/antd.css";
import ReactHtmlParser from "react-html-parser";

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
        take: pagination.defaultTake
      },
      query: ""
    };
    this.delayedCallback = lodash.debounce(this.search, 1000);
  }

  search = e => {
    this.setState(
      {
        params: {
          ...this.state.params,
          skip: 1
        },
        query: e.target.value
      },
      () => {
        this.getNewsList();
      }
    );
  };

  onSearchChange = e => {
    e.persist();
    this.delayedCallback(e);
  };

  getNewsList = () => {
    let params = Object.assign({}, this.state.params, {
      query: this.state.query
    });
    this.props.getNewsList(params);
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
    const { newsPagedList } = this.props.newsPagedListReducer;
    const { sources } = newsPagedList;
    const hasResults =
      newsPagedList.sources && newsPagedList.sources.length > 0;

    return (
      <div>
        <Row>
          <CardBody>
            <img
              src="https://due.udn.vn/portals/_default/skins/dhkt/img/front/logo.png"
              alt="logochichido"
            />
          </CardBody>
        </Row>
        <hr />
        {hasResults && (
          <List
            itemLayout="vertical"
            size="large"
            pagination={{
              onChange: page => {
                console.log(page);
              },
              pageSize: 3
            }}
            dataSource={sources}
            renderItem={item => (
              <List.Item
                key={item.title}
                // actions={[
                //   <IconText icon={StarOutlined} text="156" key="list-vertical-star-o" />,
                //   <IconText icon={LikeOutlined} text="156" key="list-vertical-like-o" />,
                //   <IconText icon={MessageOutlined} text="2" key="list-vertical-message" />,
                // ]}
                extra={<img width={272} alt="logo" src={item.image} />}
              >
                <List.Item.Meta
                  // avatar={<Avatar src={item.avatar} />}
                  title={<a href={item.link}> {item.title} </a>}
                />
                {ReactHtmlParser(item.content)}
              </List.Item>
            )}
          />
        )}
      </div>
    );
  }
}

export default connect(
  state => ({
    newsPagedListReducer: state.newsPagedListReducer
  }),
  {
    getNewsList
  }
)(NewsListPage);
