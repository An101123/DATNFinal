import React from "react";
import { Card, CardImg, CardBody, CardTitle } from "reactstrap";

const ListNews = () => {
  return (
    <div className="news">
      <Card width="50%">
        <CardImg
          top
          width="70%"
          src="https://due.udn.vn/Portals/0/Editor/TruyenThong_DUE/Nam%202019%202/Quy%204/Giai%203%20NCKH%20TP/Giai3NCKHcapTP2019.jpg"
          alt="Card image cap"
        />
        <CardBody>
          <CardTitle width="70%">
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
