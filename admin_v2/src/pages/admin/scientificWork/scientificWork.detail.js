import React, { Component } from "react";
import { Row, Col, Button, Table, Label } from "reactstrap";
import ReactHtmlParser from "react-html-parser";

export default class ScientificWorkDetail extends Component {
  constructor(props) {
    super(props);
    this.state = { item: {} };
  }

  backToAdminPage = () => {
    this.props.backToAdminPage();
  };

  componentDidMount() {
    this.setState({ item: this.props.ScientificWork });
  }

  render() {
    const { item } = this.state;
    const hasResults = item !== null;
    return (
      <div>
        {hasResults && (
          <div>
            <div>
              {" "}
              <Row>
                <Col md="10">
                  {" "}
                  <h5
                    style={{
                      marginTop: "50px",
                      textTransform: "uppercase",
                      marginLeft: "50px",
                      marginBottom: "50px"
                    }}
                  >
                    {item.name}
                  </h5>
                </Col>
                <Col md="2">
                  <Button
                    className="fa fa-window-close"
                    style={{ float: "right", backgroundColor: "white" }}
                    onClick={this.backToAdminPage}
                  />
                </Col>
              </Row>
            </div>

            <p style={{ marginLeft: "50px", marginRight: "50px" }}>
              {ReactHtmlParser(item.content)}
            </p>
          </div>
        )}
      </div>
    );
  }
}
