import React, { Component } from "react";
// import { Link } from 'react-router-dom'
import {
  Badge,
  ButtonGroup,
  Button,
  DropdownItem,
  DropdownMenu,
  DropdownToggle,
  Nav,
  NavItem,
  NavLink,
  UncontrolledDropdown
} from "reactstrap";
import PropTypes from "prop-types";
import {
  AppAsideToggler,
  AppNavbarBrand,
  AppSidebarToggler
} from "@coreui/react";
import logo from "../../../assets/img/brand/logo.svg";
import sygnet from "../../../assets/img/brand/sygnet.svg";
import Nav_Header from "./navs";
import NavbarHeader from "./navs";
const propTypes = {
  children: PropTypes.node
};

const defaultProps = {};

class DefaultHeader extends Component {
  render() {
    // eslint-disable-next-line
    const { children, ...attributes } = this.props;

    return (
      <React.Fragment>
        {/* <AppSidebarToggler className="d-lg-none" display="md" mobile /> */}
        {/* <AppSidebarToggler className="d-md-down-none" display="lg" /> */}
        {/* <AppNavbarBrand
        // full={{
        //   src:
        //     "https://cdn.shopify.com/s/files/1/0097/1643/2943/files/logo2_800x.png?v=1564483220",
        //   width: 200,
        //   height: 50,

        //   alt: "CoreUI Logo"
        // }}
        // minimized={{
        //   src: sygnet,
        //   width: 30,
        //   height: 30,
        //   alt: "CoreUI Logo"
        // }}
        /> */}
        <AppNavbarBrand>
          <h1 style={{ color: "white" }}>DUE</h1>
        </AppNavbarBrand>
        {/* <div className="buttonHeader">
          <Button outline color="secondary">
            Giới thiệu
          </Button>{" "}
          <Button outline color="secondary">
            secondary
          </Button>{" "}
          <Button outline color="secondary">
            secondary
          </Button>{" "}
          <Button style={{ marginRight: "10px" }} outline color="secondary">
            secondary
          </Button>{" "}
        </div> */}
        <Nav_Header />
        {/* <Nav className="d-md-down-none" navbar>
          <NavItem className="px-3">
            <NavLink href="/">Dashboard</NavLink>
          </NavItem>
        </Nav> */}
        {/* <Nav className="ml-auto" navbar>
          <NavItem className="d-md-down-none">
            <NavLink href="#">
              <i className="icon-bell" />
              <Badge pill color="danger">
                5
              </Badge>
            </NavLink>
          </NavItem>
          <NavItem className="d-md-down-none">
            <NavLink href="#">
              <i className="icon-list" />
            </NavLink>
          </NavItem>
          <NavItem className="d-md-down-none">
            <NavLink href="#">
              <i className="icon-location-pin" />
            </NavLink>
          </NavItem>
          <UncontrolledDropdown direction="down">
            <DropdownToggle nav>
              <img
                src={"../../assets/img/avatars/6.jpg"}
                className="img-avatar"
                alt="admin@bootstrapmaster.com"
              />
            </DropdownToggle>
            <DropdownMenu right style={{ right: "auto" }}>
              <DropdownItem header tag="div" className="text-center">
                <strong>Account</strong>
              </DropdownItem>
              <DropdownItem onClick={e => this.props.onLogout(e)}>
                <i className="fa fa-lock" /> Logout
              </DropdownItem>
            </DropdownMenu>
          </UncontrolledDropdown>
        </Nav> */}
        {/* <AppAsideToggler className="d-md-down-none" /> */}
        {/* <AppAsideToggler className="d-lg-none" mobile /> */}
      </React.Fragment>
    );
  }
}

DefaultHeader.propTypes = propTypes;
DefaultHeader.defaultProps = defaultProps;

export default DefaultHeader;
