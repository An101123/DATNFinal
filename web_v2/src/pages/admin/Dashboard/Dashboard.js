import React, { Component } from "react";
import Logo from "../Dashboard/components/logo";
import Home from "./components/Home";
import ListNews from "./components/ListNews";
import ListNews2 from "./components/ListNews2";

import { Row, Col } from "reactstrap";
import AboutUs from "./components/Aboutus";
import Lecturer from "../lecturer/lecturer.image";
class Dashboard extends Component {
  render() {
    return (
      <div className="app">
        <div className="news">
          <Logo />
          <hr />
          <Row>
            <Col xs="9">
              <Home />
            </Col>
            <Col xs="3">
              <ListNews />
              <ListNews2 />
            </Col>
          </Row>
          <hr />
        </div>

        <div>
          <Row>
            <Lecturer />
          </Row>
        </div>
      </div>
    );
  }
}

export default Dashboard;
