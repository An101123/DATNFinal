import React, { Component } from "react";
import Logo from "../Dashboard/components/logo";
import Home from "./components/Home";
import ListNews from "./components/ListNews";
import ListNews2 from "./components/ListNews2";

import { Row, Col } from "reactstrap";
import AboutUs from "./components/Aboutus";
class Dashboard extends Component {
  render() {
    return (
      <div className="app">
        <div className="news">
          <Logo />
          <Row>
            <Col xs="8">
              <Home />
            </Col>
            <Col xs="4">
              <ListNews />
              <ListNews2 />
            </Col>
          </Row>
        </div>
        <div className="aboutus">
          <Row>
            <AboutUs />
          </Row>
        </div>
      </div>
    );
  }
}

export default Dashboard;
