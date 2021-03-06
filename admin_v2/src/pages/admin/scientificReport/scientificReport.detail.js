import React, { Component } from "react";
import { Row, Col, Button } from "reactstrap";
import ReactHtmlParser from "react-html-parser";
import ApiLecturer from "../../../api/api.lecturer";

export default class ScientificReportDetail extends Component {
  constructor(props) {
    super(props);
    this.state = { item: {}, lecturers: [] };
  }

  backToAdminPage = () => {
    this.props.backToAdminPage();
  };

  getLecturerList = () => {
    ApiLecturer.getAllLecturer().then((values) => {
      this.setState({ lecturers: values });
    });
  };
  componentDidMount() {
    this.setState({ item: this.props.ScientificReport });
    this.getLecturerList();
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
                      textTransform: "uppercase",
                      marginLeft: "50px",
                      marginBottom: "50px",
                      marginTop: "50px",
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
