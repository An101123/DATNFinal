import React, { Component } from "react";
import "../../Dashboard/dashboard.css";
import { Row, CardBody } from "reactstrap";

class Logo extends Component {
  constructor(props) {
    super(props);

    this.toggle = this.toggle.bind(this);
  }

  toggle() {
    this.setState({
      //   dropdownOpen: !this.state.dropdownOpen
    });
  }

  render() {
    return (
      <div className="animated fadeIn">
        <Row>
          <CardBody>
            <img
              // className="logokinhte"
              src="https://due.udn.vn/portals/_default/skins/dhkt/img/front/logo.png"
              alt="logochichido"
            />
          </CardBody>
        </Row>
      </div>
    );
  }
}

export default Logo;
