import React, { Component } from "react";
import Logo from "../Dashboard/components/logo";
import Home from "./components/Home";
import ListNews from "./components/ListNews";
import ListNews2 from "./components/ListNews2";

import { Row, Col } from "reactstrap";
class Dashboard extends Component {
  render() {
    return (
      <div className="app">
        <Logo />
        <Row>
          <Col xs="8">
            <Home />
          </Col>
          <Col xs="4">
            <ListNews2 />
          </Col>
        </Row>
      </div>
    );
  }
}

export default Dashboard;
