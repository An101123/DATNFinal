import React from "react";
import {
  Card,
  CardImg,
  CardText,
  CardBody,
  CardTitle,
  CardSubtitle,
  Button
} from "reactstrap";

const ListNews = props => {
  return (
    <div className="slide-container">
      <Card>
        <CardImg
          top
          width="100%"
          src="https://due.udn.vn/Portals/0/Editor/TruyenThong_DUE/Nam%202019%202/Quy%204/Giai%203%20NCKH%20TP/Giai3NCKHcapTP2019.jpg"
          alt="Card image cap"
        />
        <CardBody>
          <CardTitle>
            <a href="https://due.udn.vn/vi-vn/nghiencuukhoahoc/nghiencuukhoahocchitiet/id/10838/cid/186">
              Đề tài của sinh viên trường Đại học Kinh tế giành giải cao tại
              cuộc thi nghiên cứu khoa học thành phố Đà Nẵng 2019
            </a>
          </CardTitle>
        </CardBody>
      </Card>
    </div>
  );
};

export default ListNews;
