import React, { Component } from "react";
import { Row, Col, Button, FormGroup, Table, Label } from "reactstrap";
import ReactHtmlParser from "react-html-parser";

export default class NewsDetail extends Component {
  constructor(props) {
    super(props);
    this.state = { item: {} };
  }

  backToAdminPage = () => {
    this.props.backToAdminPage();
  };

  componentDidMount() {
    this.setState({ item: this.props.News });
  }

  render() {
    const { item } = this.state;
    const hasResults = item !== null;
    return (
      <div>
        {hasResults && (
          <div>
            <div>
              <Button
                className="fa fa-window-close"
                style={{
                  backgroundColor: "white",
                  float: "right",
                  marginLeft: "100px"
                }}
                onClick={this.backToAdminPage}
              />
              <h2
                style={{
                  color: "#0473b3",
                  textTransform: "uppercase",
                  marginLeft: "50px",
                  marginTop: "30px",
                  fontFamily: "Arial"
                }}
              >
                {item.title}{" "}
              </h2>
            </div>

            <img
              style={{
                marginLeft: "100px",
                marginRight: "100px",
                marginTop: "30px",
                marginBottom: "50px",
                width: "80%"
              }}
              src={item.image}
              alt=""
            />
            <p style={{ marginLeft: "50px", marginRight: "50px" }}>
              {ReactHtmlParser(item.content)}
            </p>
            <hr />
          </div>
        )}
      </div>
    );
  }
}
