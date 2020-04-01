import React, { Component } from "react";
import { connect } from "react-redux";
import { Row, Col, Button, Table } from "reactstrap";
import moment from "moment";
import ModalConfirm from "../../../components/modal/modal-confirm";
import Pagination from "../../../components/pagination/Pagination";
import lodash from "lodash";
import { getLecturerList } from "../../../actions/lecturer.list.action";
import { pagination } from "../../../constant/app.constant";
import "../../../pages/admin/select-custom.css";


export default connect(
  state => ({
    lecturerPagedListReducer: state.lecturerPagedListReducer
  }),
  {
    getLecturerList
  }
)(LecturerListPage);
