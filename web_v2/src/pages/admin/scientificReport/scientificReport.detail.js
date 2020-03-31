import React, { Component } from "react";
import { Row, Col, Button, Table, Label } from "reactstrap";
import ReactHtmlParser from "react-html-parser";

export default class ScientificReportDetail extends Component {
  constructor(props) {
    super(props);
    this.state = { item: {} };
  }

  backToAdminPage = () => {
    this.props.backToAdminPage();
  };

  componentDidMount() {
    this.setState({ item: this.props.ScientificReport });
  }

  render() {
    const { item } = this.state;
    const hasResult = item !== null;
    return (
      <div>
        {hasResult && (
          <div>
            <Row className="nckh">
              <Col xs="12">
                <div className="flex-container header-table">
                  <Label
                    className="label label-default"
                    style={{ fontWeight: "bold" }}
                  >
                    {item.name}{" "}
                  </Label>
                  <Button
                    className="fa fa-window-close"
                    style={{ backgroundColor: "white" }}
                    onClick={this.backToAdminPage}
                  />
                </div>
                <Table className="admin-table" responsive bordered>
                  <tbody>
                    <td>{ReactHtmlParser(item.content)}</td>
                  </tbody>
                </Table>
              </Col>
            </Row>
          </div>
        )}
      </div>
    );
  }
}
