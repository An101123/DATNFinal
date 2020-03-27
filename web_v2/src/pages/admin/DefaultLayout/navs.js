import React from "react";
import { Nav, NavItem, NavLink } from "reactstrap";
const Nav_Header = props => {
  return (
    <div>
      <Nav
        style={{
          float: "right",
          marginTop: "10px",
          marginRight: "50px"
        }}
      >
        <NavItem>
          <NavLink style={{ color: "white" }} href="#">
            Giới thiệu
          </NavLink>
        </NavItem>
        <NavItem>
          <NavLink style={{ color: "white" }} href="#">
            Giảng viên
          </NavLink>
        </NavItem>
        <NavItem>
          <NavLink style={{ color: "white" }} href="#">
            Another Link
          </NavLink>
        </NavItem>
      </Nav>
      <hr />
    </div>
  );
};

export default Nav_Header;
