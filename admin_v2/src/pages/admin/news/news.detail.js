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
            <Row className="nckh">
              <Col xs="12">
                <div className="flex-container header-table">
                  <Label
                    className="label label-default"
                    style={{ fontWeight: "bold" }}
                  >
                    {item.title}{" "}
                  </Label>
                  <Button
                    className="fa fa-window-close"
                    style={{ backgroundColor: "white" }}
                    onClick={this.backToAdminPage}
                  />
                </div>
                <Table className="admin-table" responsive bordered>
                  <tbody>
                    <td>
                      {" "}
                      <div>
                        <td style={{ textAlign: "center" }}>
                          <img
                            style={{ width: "100%" }}
                            src={item.image}
                            alt=""
                          />
                        </td>
                      </div>
                      {ReactHtmlParser(item.content)}
                    </td>
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
